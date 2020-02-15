using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitBrowser();

        }

        public ChromiumWebBrowser browser;
        public void InitBrowser()
        {
            Cef.Initialize(new CefSettings());
            browser = new ChromiumWebBrowser("https://leetcode-cn.com/problems/two-sum/");
            browser.FrameLoadEnd += WebBrowser_FrameLoadEnd;    //加载完成
            this.panel1.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
        }

        public void WebBrowser_FrameLoadEnd(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                String html = browser.GetSourceAsync().Result;
                GetCode_From_Page(html);
            }));
        }


        private string GetCode_From_Page(in string input)
        {
            string output = "";
            // get code
            // Sample: 
            // <input name="code" type="hidden" value="
            // class Solution ... "
            // ><input name="question" type="hidden" value="1">
            string pattern = "<input name=\"code\"[\\s\\S]+?><input name=\"question\"";
            textBox1.Text = pattern + "\r\n";
            foreach (Match match in Regex.Matches(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline))
            {
                output = match.Value;
            }
            textBox1.Text += output;
            return output;
        }
    }
}
