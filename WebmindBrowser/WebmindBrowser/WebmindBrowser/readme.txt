1.����һ�е����·����㶼��bin/debug/...
2.ϵͳ�Ե�ַ֧�ֲ���ô�ã�����˵��ַsina.com�����ԵĿ����Զ�ת��sina.com.cn�������ϵͳĿǰ������������cache���书��Ҳûʵ��
3.
  by kosko 2011-4-1
4.�ҿ�ʼ�޸��������ûʵ�ֺõĹ����ˣ���������һ�У��Ҷ���ʼ�޸��ˡ�
5.�� �ļ�->�½����� ʱ�������ڴ���Dictionary�Ѿ�������key�������ּ��شΣ��϶���exception���Ҽ���һ���ж��Ƿ�containkey���
6.�ļ�->�� �������úܲ��ã���Ҳ��֪�������ĸ�����ģ����ҷ���
7.�ļ�->�رձ�ǩҳ�������ʼû�б�ǩ�����п��ܷ���NullPointer��exception������MainWindow.cs���MenuItem_File_CloseTab_Click��������˶�mdiTabStrip1.Activate����Ƿ�ok�Ĳ���
8.�鿴->Դ����,��CodeProject�Ͽ�������WebBroswer.DocumentText,Ȼ��StreamWriter��һ���ļ��������һ������(NotePad.exe)���Ǹ��ļ����
9.���ھ���ѡ��WBrowser���history��favorite�����ˣ������Ҿ�Ҫ��WebMindϵͳ����������,��ǰ������ֱ�ӵ���IE���Environment.SpecialFolder.Favorites��..
10.����һ��tabControl����leftFormPanel�������ԭ�����Ǹ�ʲô�����ؼ���Ȼ����tabControl��Ž���3��tabPage���ֱ�����ղؼС���ʷ��¼��Դ�������Ѿ�ʵ�����ղؼе���Ӽ���ʾ���ܣ��õĲ���IE���Ǹ��������Լ�д������xml��ʵ�ֵ��������ģ���д��xml���ٷ������TreeView���ӣ���ôֱ�Ӹ���TreeView�Ľڵ�
11.��д����ʷ��¼�Ǹ��Ǹ�addHistory(),�ҷ��˺ô�ľ�����Ϊ���һ����ҳ�����ء�������ҳ������WebView������У���addHistory()����д��MainWindow�����Ϊ�п���Ҫ����TreeView�����ֲ�����static�ؼ���(�����ԣ�static�������ܵ��÷�static���Ժͷ���),����WebView������һ��MainWindow�Ķ���̫�ķ��ڴ��ˣ�����ҵĽ��������:��MainWindow������һ��static Uri url��Ȼ����WebView.cs���WebBrowserContent_Navigated�︴�ƣ�Ȼ������MainWindow.cs���addressBar_Url_KeyDown�Լ�addressBar��غ��������addHistory()
12.��ʷ->ɾ����ʷ��¼,���Ѿ������ˣ�������MainWindow.cs���MenuItem_History_Delete_Click()����
13.�鿴->ת����� ǰ�� ���� ��ҳ ���Ҷ�д������Ӧ�Ĵ�����
14.�鿴->ȫ��Ҳ��ʹ��
15.�鿴�ﻹ�� ���� ���ִ�С ���� ���� ��û��д�ù���
16.�༭->ȫѡ�����ʹ�ø��ơ�����Enabled����ֻ����Clicked�¼���������ص�Enabled���ԣ����ڸ��ӵ�ʵ�֣��Ҳ�������
17.��������ĺܶ��Ҷ�ɾȥ�ˣ������˼�������Google Mail�ȷ���.
18.��execCommandʵ���˱༭->������ļ�������
19.�ѹ������ϵ���ʷһ��ɾȥ��������ʷ->ɾ����ʷ��¼��������һ����
20.������������Ŀ����ֱ�ӵ����򵥵Ĵ�����Ӧ����
21.�����Һ���Ҫ�����ǣ����´��ϴ�����Ự�������������⣺�ϴιر������ʱ����Щ��δ�رյ����ҳ��(��Ȼabout:blank����),����Щҳ���urls����һ��xml��´����������ʱ���Ƽ������xml�Ž�һ��url�б�������������´��ϴλỰ������ô�Ͱ���Щurl����webbrowser��
22.��getCurrentBrowser()���������֪����this.mdiTabStrip����Ǹ��ؼ�,tab�Ǳ�ǩҳ,this.mdiTabStrip.Tabs.Count��������ǰ�ı�ǩҳ�ĸ���
23.���MdiTabStrip����һ����tab����Ӧ������MainWindow.cs���onMdiTabStript_Click
24.��mdiTabStrip->Tabs->Tab->Tab.Form.WebBrowserContent,��������lastSession.xml��,����������˻ָ��ϴλỰ���ݡ������xml����MainWindow_FormClosed��������еģ����ָ��ϴλỰ���ݣ������Ǹ���Ӧ�¼�����е�
25.�ղؼ�->�����ղؼУ�����һ�������ղؼеĶԻ���Ȼ������ɾ��������link�ȵȹ��ܡ�ʵ���������һ��OrganizeFavorites.cs(�������)
26.�������崦���ղؼС���ʷ��¼�����һ���Ҽ������contextmenustrip,�������û�ʵ���Ҽ������һ����ݲ˵��������� ���´��ڴ� ���´���� ������ ɾ���ȵȹ��ܡ�ʵ�ַ����ǣ�favTreeView(historyTreeView)����Ӧһ��NodeMouseClick�¼�,������¼��ﲶ׽��������ӵ�Щ��Ϣ������Ҫ���ǲ�Ҫ�������ڽ���TreeViewʱ��node��Ҫ����node.ContextMenuStrip = favContextMenu;֮������!!!
27.favContextMenu��Ӧ���ղؼ��������Ҽ�����˵�,LinkContextMenu��Ӧ���ղؼ������Ҽ�����˵�����histContextMenu��Ӧ�������ʷ������Ҽ�����˵�
28.������������ڽ�������,��һ�ж���EmotionSuggestion.cs�������Ұѱ����������Ƽ��㷨(0��1��2�����0��1)��������࣬��Ӧ��UserEvaluation�ļ��ﱣ������Ӧ��Record��
29.���������Ƽ�ģ�飬���Ĵ�����EmotionSuggestion.cs��Ĺ�֮�󣬻�ʣ�����Ƽ��㷨����ȫ����͸�������ֵ�Ƽ��㷨�����������Դ��bin/DeBug�µ�SuggestionsBehavior.xml��SuggestionRandom.xml����ʱ�䶨��ʹ�õ��Ƕ�ʱ���ؼ�Timer��������MainWindow.cs���timerAutoSuggestion�������Ǹ����Ƽ����ڵĵ����ġ�
30.���ڶ�ʱ��������һ���õĵط�������timerEmotionAnalysis������ʱ�����������������
     1)URL ����:ͳ�Ƶ�����������ʱ�� ���µ�����ҳ��ƽ��ͣ��ʱ�� ����ÿ�������ʱ��(ÿ�����ʼ�������ĸ�ʱ��)
      2)Content ����:������ҳ����� ��ҳ�����ݷ�������ʵ�ǽ��ö�ʱ�����������ҳ(�Ѿ�ͨ��documentCompleted�Ǹ��¼�����������ݡ���з���������������˽�����ֵ�����ھ���ֱ�������Ǹ��ֵ��������)�ķ�����������������û����ÿ������ı��ĸ��ʣ�Ȼ����ݲ�ͬ�ĸ���ֵ����BehaviorFeatureAnalysis���¼�û����������ʡ����ٷ��ʡ��ȡ�
     3)������������Ԥ��ֵ:��������Ԥ���¼����ʵ��������Ǹ���BehaviorFeatureAnalysis������߸�����ά��ֵ��
     4)��������ͼ
31.���������ģ�飬���룬������ǱȽϸ��ӵġ����ڲ˵����������ѡ��ȣ������ﶼ��������Ϊȫ�������ġ�������Ҫ�˽�ľ���mdi��һ�飬�Լ�url��navigate��������ʵ���ľ���WebView�Ǹ��ࡣ��WebView���Ǹ��࣬��ʵҲ��ʹ����(ExtendedWebBrowser)WebBrowserContent�����������ExtendedWebBrowser : System.Windows.Forms.WebBrowser�����һЩ�������������Ӻ����������(ExtendedWebBrowser)���ˡ�