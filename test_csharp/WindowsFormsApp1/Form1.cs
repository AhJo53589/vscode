using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
//using CefSharp.WinForms;
using CefSharp.OffScreen;

namespace WindowsFormsApp1
{
    public class Browser
    {

        /// <summary>
        /// The browser page
        /// </summary>
        public ChromiumWebBrowser Page { get; private set; }
        /// <summary>
        /// The request context
        /// </summary>
        public RequestContext RequestContext { get; private set; }

        // chromium does not manage timeouts, so we'll implement one
        private ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        public Browser()
        {
            var settings = new CefSettings()
            {
                //By default CefSharp will use an in-memory cache, you need to     specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache"),
            };

            //Autoshutdown when closing
            CefSharpSettings.ShutdownOnExit = true;

            //Perform dependency check to make sure all relevant resources are in our     output directory.
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

            RequestContext = new RequestContext();
            Page = new ChromiumWebBrowser("", null, RequestContext);
            PageInitialize();
        }

        /// <summary>
        /// Open the given url
        /// </summary>
        /// <param name="url">the url</param>
        /// <returns></returns>
        public void OpenUrl(string url)
        {
            try
            {
                Page.LoadingStateChanged += PageLoadingStateChanged;
                if (Page.IsBrowserInitialized)
                {
                    Page.Load(url);

                    //create a 60 sec timeout 
                    bool isSignalled = manualResetEvent.WaitOne(TimeSpan.FromSeconds(60));
                    manualResetEvent.Reset();

                    //As the request may actually get an answer, we'll force stop when the timeout is passed
                    if (!isSignalled)
                    {
                        Page.Stop();
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                //happens on the manualResetEvent.Reset(); when a cancelation token has disposed the context
            }
            Page.LoadingStateChanged -= PageLoadingStateChanged;
        }

        /// <summary>
        /// Manage the IsLoading parameter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            // Check to see if loading is complete - this event is called twice, one when loading starts
            // second time when it's finished
            if (!e.IsLoading)
            {
                manualResetEvent.Set();
            }
        }

        /// <summary>
        /// Wait until page initialization
        /// </summary>
        private void PageInitialize()
        {
            SpinWait.SpinUntil(() => Page.IsBrowserInitialized);
        }
    }
    

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
            browser = new ChromiumWebBrowser("https://leetcode-cn.com/problems/kth-largest-element-in-a-stream/");
            browser.FrameLoadEnd += WebBrowser_FrameLoadEnd;    //加载完成


            //Cef.Initialize(new CefSettings());
            //browser = new ChromiumWebBrowser("https://leetcode-cn.com/problems/kth-largest-element-in-a-stream/");
            ////browser = new ChromiumWebBrowser("https://leetcode-cn.com/contest/weekly-contest-175/problems/check-if-n-and-its-double-exist/");
            //browser.FrameLoadEnd += WebBrowser_FrameLoadEnd;    //加载完成
            //this.panel1.Controls.Add(browser);
            //browser.Dock = DockStyle.Fill;
        }


        public void WebBrowser_FrameLoadEnd(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                String html = browser.GetSourceAsync().Result;
                //textBox1.Text += html;

                GetCode_From_Page(html);
            }));
        }

        private void DeEscape(ref string output)
        {
            output = output.Replace("&lt;", "<");
            output = output.Replace("&gt;", ">");
            output = output.Replace("&amp;", "&");
            output = output.Replace("&apos;", "\'");
            output = output.Replace("&quot;", "\"");
        }

        private string GetCode_From_Page(in string input)
        {
            string output = "";
            // get code
            // Sample: 
            // <input name="code" type="hidden" value="
            // class Solution ... "
            // ><input name="question"
            string pattern = "<input name=\"code\"[\\s\\S]+?><input name=\"question\"";
            textBox1.Text = pattern + "\r\n";
            foreach (Match match in Regex.Matches(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline))
            {
                output = match.Value;
            }
            pattern = "class\\s[\\s\\S]+?><input";
            foreach (Match match in Regex.Matches(output, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline))
            {
                output = match.Value;
            }
            output = output.Replace("><input", "");
            DeEscape(ref output);

            textBox1.Text += output;
            Create_File_Solution_cpp(@"C:\AhJo53589\leetcode-cn\", @"1.cpp", output);

            ////////////////////////////////////////////
            pattern = "<input name=\"testcase\"[\\s\\S]+?><input name=\"lang\"";
            foreach (Match match in Regex.Matches(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline))
            {
                output = match.Value;
            }
            output = output.Replace("><input name=\"lang\"", "");
            output = output.Replace("<input name=\"testcase\" type=\"hidden\" value=\"", "");
            DeEscape(ref output);

            textBox1.Text += output;
            Create_File_Solution_cpp(@"C:\AhJo53589\leetcode-cn\", @"2.txt", output);

            return output;
        }

        private void Create_File_Solution_cpp(string newPath, string fileName, string strCode, bool isCustom = false)
        {

            if (strCode == null || strCode == "") return;

            string strFile = newPath + fileName;
            string strText = strCode;

            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);
        }

    }
}
