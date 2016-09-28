1.这里一切的相对路径起点都是bin/debug/...
2.系统对地址支持不怎么好，不如说地址sina.com，明显的可以自动转成sina.com.cn，但这个系统目前做不到，还有cache记忆功能也没实现
3.
  by kosko 2011-4-1
4.我开始修复浏览器的没实现好的功能了，工具栏里一切，我都开始修复了。
5.当 文件->新建窗口 时，由于内存里Dictionary已经加载了key，现在又加载次，肯定出exception，我加了一个判断是否containkey语句
6.文件->打开 功能做得很不好，我也不知道该往哪个方向改，暂且放下
7.文件->关闭标签页，如果开始没有标签，就有可能犯了NullPointer的exception，我在MainWindow.cs里的MenuItem_File_CloseTab_Click函数里，加了对mdiTabStrip1.Activate句柄是否ok的测验
8.查看->源代码,从CodeProject上看到，用WebBroswer.DocumentText,然后StreamWriter进一个文件，最后用一个进程(NotePad.exe)打开那个文件便可
9.终于决定选用WBrowser里的history和favorite方案了，这样我就要对WebMind系统大作手续了,以前做的是直接调用IE里的Environment.SpecialFolder.Favorites和..
10.我用一个tabControl放在leftFormPanel里，代替了原来的那个什么收缩控件，然后在tabControl里放进了3个tabPage，分别代表收藏夹、历史记录、源。现在已经实现了收藏夹的添加及显示功能，用的不是IE的那个，而是自己写、读的xml。实现的是这样的，先写进xml，再分析如果TreeView可视，那么直接更新TreeView的节点
11.在写进历史记录那个那个addHistory(),我费了好大的劲，因为浏览一个网页，下载、解析网页过程在WebView类里进行，而addHistory()必须写在MainWindow类里，因为有可能要更新TreeView，而又不能用static关键字(很明显，static函数不能调用非static属性和方法),而在WebView类里做一个MainWindow的对象太耗费内存了，最后我的解决方法是:在MainWindow里设置一个static Uri url，然后在WebView.cs里的WebBrowserContent_Navigated里复制，然后再在MainWindow.cs里的addressBar_Url_KeyDown以及addressBar相关函数里调用addHistory()
12.历史->删除历史记录,我已经做好了，具体是MainWindow.cs里的MenuItem_History_Delete_Click()函数
13.查看->转到里的 前进 后退 主页 等我都写完了相应的处理函数
14.查看->全屏也好使了
15.查看里还有 放缩 文字大小 编码 语言 等没有写好功能
16.编辑->全选后才能使得复制、剪切Enabled，我只能在Clicked事件里设置相关的Enabled属性，关于复杂的实现，我不能做到
17.工具下面的很多我都删去了，增加了计算器、Google Mail等服务.
18.用execCommand实现了编辑->字体里的几个量度
19.把工具栏上的历史一栏删去，并把历史->删除历史记录移至工具一栏里
20.帮助里三个栏目我是直接弹出简单的窗口来应付的
21.下面我核心要做的是，重新打开上次浏览会话，这个定义我理解：上次关闭浏览器时，那些尚未关闭的浏览页面(当然about:blank除外),把这些页面的urls放在一个xml里，下次启动浏览器时估计加载这个xml放进一个url列表里，如果点击“重新打开上次会话”，那么就把这些url载入webbrowser里
22.在getCurrentBrowser()函数里，可以知道，this.mdiTabStrip这个是个控件,tab是标签页,this.mdiTabStrip.Tabs.Count就是数当前的标签页的个数
23.点击MdiTabStrip出现一个新tab的响应函数是MainWindow.cs里的onMdiTabStript_Click
24.有mdiTabStrip->Tabs->Tab->Tab.Form.WebBrowserContent,并保存在lastSession.xml里,这样便完成了恢复上次会话内容。保存进xml是在MainWindow_FormClosed函数里进行的，而恢复上次会话内容，是在那个响应事件里进行的
25.收藏夹->整理收藏夹，弹出一个整理收藏夹的对话框，然后让你删除、改名link等等功能。实现是添加了一个OrganizeFavorites.cs(窗体程序)
26.给左侧面板处的收藏夹、历史记录添加了一个右键点击的contextmenustrip,可以让用户实现右键点击出一个快捷菜单，上面有 在新窗口打开 在新窗体打开 重命名 删除等等功能。实现方法是，favTreeView(historyTreeView)里响应一个NodeMouseClick事件,在这个事件里捕捉点击的链接的些信息，最重要的是不要忘记了在建立TreeView时的node的要设置node.ContextMenuStrip = favContextMenu;之类的语句!!!
27.favContextMenu对应着收藏夹面板里的右键点击菜单,LinkContextMenu对应着收藏夹栏的右键点击菜单，而histContextMenu对应着浏览历史面板里右键点击菜单
28.调整了心理调节建议的类别,这一切都在EmotionSuggestion.cs这个类里，我把本来是三类推荐算法(0、1、2编程了0、1)变成了两类，对应在UserEvaluation文件里保存了相应的Record。
29.对于心理推荐模块，核心处理在EmotionSuggestion.cs里，改过之后，还剩两种推荐算法，完全随机和根据心理值推荐算法，而建议库来源于bin/DeBug下的SuggestionsBehavior.xml和SuggestionRandom.xml。而时间定制使用的是定时器控件Timer，其中在MainWindow.cs里的timerAutoSuggestion变量就是负责推荐窗口的弹出的。
30.对于定时器，还有一个用的地方，便是timerEmotionAnalysis，即定时启动心理分析工作：
     1)URL 分析:统计当天的网络浏览时间 更新当天网页的平均停留时间 分析每天的上网时段(每天的起始上网在哪个时段)
      2)Content 分析:分析网页的情感 网页的内容分析，其实是将该段时间内浏览的网页(已经通过documentCompleted那个事件里调用了内容、情感分析，并储存分类了结果到字典里，现在就是直接运用那个字典里的内容)的分析，首先来测算该用户浏览每个类别文本的概率，然后根据不同的概率值，在BehaviorFeatureAnalysis里记录用户“经常访问、很少访问”等。
     3)更新心理趋势预测值:更新心理预测记录。其实，这个就是根据BehaviorFeatureAnalysis计算出七个心理维度值。
     4)更新曲线图
31.关于浏览器模块，我想，这个还是比较复杂的。关于菜单栏及其各个选项等，我心里都有数，因为全是我做的。现在需要了解的就是mdi那一块，以及url的navigate工作。其实核心就是WebView那个类。而WebView里那个类，其实也是使用了(ExtendedWebBrowser)WebBrowserContent这个变量，而ExtendedWebBrowser : System.Windows.Forms.WebBrowser，因此一些基本的请求连接函数就在这个(ExtendedWebBrowser)里了。