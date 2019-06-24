﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace leetcode_md_helper
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Test path
            //txtFolder.Text = @"C:\Ikaruga Files\Work\AhJo53589\leetcode-cn\";
            txtPath.Text = System.Windows.Forms.Application.StartupPath;
            txtOut_Directory.Text = txtPath.Text + @"/README.md";
            txtOut_Log.Text = txtPath.Text + @"/problems/README.md";
        }

        private int GetDirectoryNo(string str)
        {
            // example: 
            // * `（简单）`  [1.TwoSum 两数之和](./problems/1.TwoSum/README.md)
            // 题解[C++](./ problems / 1.TwoSum / 1.TwoSum.cpp)
            string[] s = str.Split('[');
            s = s[1].Split('.');
            int i;
            int.TryParse(s[0], out i);
            return i;
        }

        private string GenerateDirectoryString(string strId, string strTitleE, string strTitleC)
        {
            string strIdTitleE = strId + "." + strTitleE;

            // example: 
            // * `（简单）`  [1.TwoSum 两数之和](./problems/1.TwoSum/README.md)
            // 题解[C++](./ problems / 1.TwoSum / 1.TwoSum.cpp)
            string strOutput = "* `（";
            strOutput += cmbIn_Difficult.Text;
            strOutput += "）`  [";
            strOutput += strIdTitleE;
            strOutput += " ";
            strOutput += strTitleC;
            strOutput += "](./problems/";
            strOutput += strIdTitleE;
            strOutput += "/README.md) 题解 [C++](./problems/";
            strOutput += strIdTitleE;
            strOutput += "/";
            strOutput += strIdTitleE;
            strOutput += ".cpp)";

            return strOutput;
        }

        private void CreateDescriptionMDFile(string strId, string strTitleE, string strTitleC)
        {
            string strIdTitleE = strId + "." + strTitleE;

            // example: 
            //# `（简单）`  [48.Rotate 旋转图像](https://leetcode-cn.com/problems/rotate-image/)
            string strText;
            strText = "# `（";
            strText += cmbIn_Difficult.Text;
            strText += "）`  [";
            strText += strIdTitleE;
            strText += " ";
            strText += strTitleC;
            strText += "](";
            strText += txtIn_Link.Text;
            strText += ")";
            strText += "\n\n";

            // ### 题目描述
            strText += "### 题目描述\n";
            strText += txtIn_Description.Text;
            strText += "\n\n";

            // ---
            strText += "---\n";

            // ### 思路
            strText += "### 思路\n";
            strText += "```\n```\n";
            strText += "\n";

            // ### 答题
            strText += "### 答题\n";
            strText += "``` C++\n";
            strText += txtIn_Answer.Text + "\n";
            strText += "```\n";
            strText += "\n";


            string strFile = txtOut_Answer.Text;
            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);
            lblOut_Answer.Visible = true;
        }

        private void UpdateDirectoryMDFile(string strId, string strTitleE, string strTitleC)
        {
            string strFile = txtOut_Directory.Text;

            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[README.md] file not exist!");
                return;
            }

            string strInsert = GenerateDirectoryString(strId, strTitleE, strTitleC);
            int iInsertNo = GetDirectoryNo(strInsert);
            string strText = "";
            int iMark = 0;

            FileStream fs = new FileStream(strFile, FileMode.Open);
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    if (iMark == 0)
                    {
                        if (str == "# leetcode-cn") iMark = 1;  // find title
                    }
                    else if (iMark == 1)
                    {
                        if (str == "## Problems & Solutions") iMark = 2;  // find title
                    }
                    else if (iMark == 2)
                    {
                        if (str == "") continue;
                        int iReadNo = GetDirectoryNo(str);
                        if (iReadNo > iInsertNo)
                        {
                            strText += strInsert + "\n";    // insert content here
                            iMark = 3;  // iMakr == 3, insert completed
                        }
                    }

                    // copy this line
                    strText += str + "\n";
                }
                if (iMark == 2)
                {
                    strText += strInsert + "\n";    // insert content here
                    iMark = 3;  // iMakr == 3, insert completed
                }
                if (iMark != 3)
                {
                    MessageBox.Show(@"[README.md] insert failed!");
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);
            }
            lblOut_Directory.Visible = true;
        }

        private void UpdateLogMDFile(string strId, string strTitleE, string strTitleC)
        {
            string strFile = txtOut_Log.Text;

            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[problems/README.md] file not exist!");
                return;
            }

            // example: 
            // * 133.CloneGraph 克隆图
            string strInsert = "* " + strId + "." + strTitleE + " " + strTitleC + "\n";
            string strText = "";
            string strDate = "## " + DateTime.Now.ToString("yyyyMMdd");
            int iMark = 0;

            FileStream fs = new FileStream(strFile, FileMode.Open);
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    if (iMark == 2)
                    {
                        strText += strInsert + "\n";
                        iMark = 3;  // iMakr == 3, insert completed
                    }
                    if (iMark == 1)
                    {
                        if (str == strDate)
                        {
                            iMark = 2;  // find today, insert content next line
                        }
                        else
                        {
                            // insert new date and content
                            strText += strDate + "\n" + strInsert + "\n";
                            strText += "\n";
                            strText += "---" + "\n";
                            iMark = 3;  // iMakr == 3, insert completed
                        }

                    }
                    if (iMark == 0 && str == "---") iMark = 1;  // iMark == 1, find first of "---'

                    // copy this line
                    strText += str + "\n";
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);

                lblOut_Log.Visible = true;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string[] s = txtIn_Title.Text.Split('.');
            string strId = s[0];
            string strTitleC = s[1];

            s = txtIn_Link.Text.Split('/');
            string strTitleE = s[4];

            string strIdTitleE = strId + "." + strTitleE;

            UpdateDirectoryMDFile(strId, strTitleE, strTitleC);
            UpdateLogMDFile(strId, strTitleE, strTitleC);
            CreateDescriptionMDFile(strId, strTitleE, strTitleC);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtIn_Title.Text = "";
            txtIn_Link.Text = "";
            txtIn_Description.Text = "";
            txtOut_Answer.Text = "";
            lblOut_Directory.Visible = false;
            lblOut_Log.Visible = false;
            lblOut_Answer.Visible = false;
            btnGenerate.Enabled = false;
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            if (txtIn_Title.Text != "" && txtIn_Link.Text != "")
            {
                string[] s = txtIn_Title.Text.Split('.');
                string strId = s[0];
                string strTitleC = s[1];

                s = txtIn_Link.Text.Split('/');
                string strTitleE = s[4];

                string strIdTitleE = strId + "." + strTitleE;

                string str = txtPath.Text;
                str += @"/problems/" + strIdTitleE + @"/README.md";

                txtOut_Answer.Text = str;

                btnGenerate.Enabled = true;
            }
            else
            {
                btnGenerate.Enabled = false;
            }
        }
    }
}
