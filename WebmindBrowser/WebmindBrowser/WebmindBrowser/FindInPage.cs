using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using mshtml;//这个得引入mshtml.dll文件，而这个文件5.66Mb,是不可以接受的

namespace WebmindBrowser
{
    public partial class FindInPage : Form
    {
        public WebBrowser webBrowser = null;
        public FindInPage(WebBrowser wb)
        {
            webBrowser = wb;
            InitializeComponent();
        }
        /*
         * 这次我们希望实现一个和ie一模一样的查找功能，以对web页面内的文字进行查找。
文本查找要借助于textrange对象的findtext方法。但是，.net里并没有这个对象。这是因为，
         * .net 2.0提供的htmldocument，htmlwindow，htmlelement等类，
         * 只不过是对原有mshtml这个com组件的不完整封装，只提供了mshtml的部分功能。
         * 所以许多时候，我们仍旧要借助mshtml来实现我们需要的功能。
         * 好在这些.net类都提供了domdocument这个属性，使得我们很容易把.net对象转换为com对象使用。下面的代码演示了如何查找web页面的文本。 
（需要添加mshtml的引用，并加上using mshtml;） 

        */
        // 建立一个查找用的textrange（ihtmltxtrange接口）   
        //private ihtmltxtrange searchrange = null;  
        private void nextOne_Click(object sender, EventArgs e)
        {
            string txtkeyword = search.Text.ToString();
            // document的domdocument属性，就是该对象内部的com对象。  
            /*
            ihtmldocument2 document = (ihtmldocument2)webBrowser.document.domdocument;
            string keyword = txtkeyword.text.trim();
            if (keyword == "")
                return;
            // ie的查找逻辑就是，如果有选区，就从当前选区开头+1字符处开始查找；没有的话就从页面最初开始查找。  
            // 这个逻辑其实是有点不大恰当的，我们这里不用管，和ie一致即可。    
            if (document.selection.type.tolower() != "none")
            {
                searchrange = (ihtmltxtrange)document.selection.createrange();
                searchrange.collapse(true);
                searchrange.movestart("character", 1);
            }
            else
            {
                ihtmlbodyelement body = (ihtmlbodyelement)document.body;
                searchrange = (ihtmltxtrange)body.createtextrange();
            }
            // 如果找到了，就选取（高亮显示）该关键字；否则弹出消息。  
            if (searchrange.findtext(keyword, 1, 0))
            {
                searchrange.select();
            }
            else
            {
                MessageBox.Show("已搜索到文档结尾");
            }
             * */
        }
    }
}
