using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace leetcode_md_helper
{
    public partial class frmMain : Form
    {
        private string m_strId;
        private string m_strTitleE;
        private string m_strTitleC;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Test path
            //txt_FilePath.Text = @"C:\Ikaruga Files\Work\AhJo53589\leetcode-cn";
            //txt_FilePath.Text = @"C:\Fenix_Files\Workspace\AhJo53589\leetcode-cn";
            txt_FilePath.Text = System.Windows.Forms.Application.StartupPath;

            txtOut_ReadmeFilePath.Text = txt_FilePath.Text + @"/README.md";
            txtOut_ProblemsFilePath.Text = txt_FilePath.Text + @"/Problems.md";
            txtOut_UpdateFilePath.Text = txt_FilePath.Text + @"/Update.md";
        }

        private void CalcMainString()
        {
            string[] s = txtIn_IdTitleC.Text.Split('.');
            m_strId = s[0];
            m_strTitleC = s[1];
            if (m_strTitleC[0] == ' ') m_strTitleC = m_strTitleC.Substring(1);
            s = txtIn_Link.Text.Split('/');
            m_strTitleE = s[4];
        }

        private string GetDifficult_FromDirectoryString(string str)
        {
            // example: 
            // * `（简单）`  [1.TwoSum 两数之和](./problems/1.TwoSum/README.md)
            return str.Substring(2, 6);
        }

        private string GetIdTitleEC_FromDirectoryString(string str)
        {
            // example: 
            // * `（简单）`  [1.TwoSum 两数之和](./problems/1.TwoSum/README.md)
            string[] s = str.Split('[');
            s = s[1].Split(']');
            return s[0];
        }

        private string GetAnswerFilePath_FromDirectoryString(string str)
        {
            // example: 
            // * `（简单）`  [1.TwoSum 两数之和](./problems/1.TwoSum/README.md)
            string[] s = str.Split('(');
            s = s[1].Split(')');
            return s[0];
        }

        private int GetDirectoryNo_FromDirectoryString(string str)
        {
            // example: 
            // * `（简单）`  [1.TwoSum 两数之和](./problems/1.TwoSum/README.md)
            string[] s = str.Split('[');
            s = s[1].Split('.');
            int i;
            int.TryParse(s[0], out i);
            return i;
        }

        private string GenerateDirectoryString()
        {
            // example: 
            //* `（简单）` [198.Rob 打家劫舍] (./problems/198.Rob/README.md)
            string strOutput = "* `（";
            strOutput += cmbIn_Difficult.Text;
            strOutput += "）` [";
            strOutput += txtIn_IdTitleE.Text;
            strOutput += " ";
            strOutput += m_strTitleC;
            strOutput += "](./problems/";
            strOutput += txtIn_IdTitleE.Text;
            strOutput += "/README.md)";
            return strOutput;
        }

        private string GenerateDirectoryString_WithSelectedSolution()
        {
            // example: 
            //* `（简单）` [198.Rob 打家劫舍] (./problems/198.Rob/README.md) | [发布的题解] (https://leetcode-cn.com/problems/house-robber/solution/da-jia-jie-she-by-ikaruga) | 
            string strOutput = GenerateDirectoryString();
            strOutput += " | [发布的题解](";
            strOutput += txtIn_SolutionLink.Text;
            strOutput += ") |";
            return strOutput;
        }

        private void Create_AnswerFile()
        {
            // example: 
            //# `（简单）`  [48.Rotate 旋转图像](https://leetcode-cn.com/problems/rotate-image/)
            string strText;
            strText = "# `（";
            strText += cmbIn_Difficult.Text;
            strText += "）`  [";
            strText += txtIn_IdTitleE.Text;
            strText += " ";
            strText += m_strTitleC;
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

            // 题解链接
            if (txtIn_SolutionLink.Text != "")
            {
                strText += "[题解] (";
                strText += txtIn_SolutionLink.Text;
                strText += ")\n\n";
            }

            // ### 答题
            strText += "### 答题\n";
            strText += "``` C++\n";
            strText += txtIn_Answer.Text + "\n";
            strText += "```\n";
            strText += "\n";

            // ### 其它
            strText += "### 其它\n";
            strText += "``` C++\n";
            strText += "```\n";
            strText += "\n";


            string strFile = txtOut_AnswerFilePath.Text;
            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);
            lblOut_Answer.Visible = true;

            Process.Start(strFile);
        }

        private void Modify_ReadmeFile(int iProblemsCount)
        {
            string strFile = txtOut_ReadmeFilePath.Text;

            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[README.md] file not exist!");
                return;
            }

            string strInsert_SelectedSolution = GenerateDirectoryString_WithSelectedSolution();
            int iInsertNo = 0;
            int.TryParse(m_strId, out iInsertNo);
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
                        if (str == "## Selected Solutions") iMark = 10;  // find title
                    }
                    else if (iMark == 10)
                    {
                        if (str == "") continue;
                        if (txtIn_SolutionLink.Text == "") iMark = 19;
                        int iReadNo = GetDirectoryNo_FromDirectoryString(str);
                        if (iReadNo > iInsertNo)
                        {
                            strText += strInsert_SelectedSolution + "\n";    // insert content here
                            iMark = 19;  // iMakr == 29, insert completed
                        }

                        if (str == "## Problems & Solutions")
                        {
                            strText += strInsert_SelectedSolution + "\n";    // insert content here
                            iMark = 20;  // find title
                        }
                    }
                    else if (iMark == 19)
                    {
                        if (str == "## Problems & Solutions") iMark = 20;  // find title
                    }
                    else if (iMark == 20)
                    {
                        string[] s1 = str.Split('（');
                        string[] s2 = str.Split('/');
                        str = s1[0] + "（";
                        str += iProblemsCount.ToString();
                        str += " /";
                        str += s2[1];

                        iMark = 30;
                    }

                    // copy this line
                    strText += str + "\n";
                }
                if (iMark != 30)
                {
                    MessageBox.Show(@"[README.md] insert failed!");
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);
            }
            lblOut_Readme.Visible = true;

            Process.Start(strFile);
        }

        private int Modify_ProblemsFile()
        {
            int iProblemsCount = 0;
            string strFile = txtOut_ProblemsFilePath.Text;

            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Problems.md] file not exist!");
                return iProblemsCount;
            }

            string strInsert = GenerateDirectoryString();
            int iInsertNo = 0;
            int.TryParse(m_strId, out iInsertNo);
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
                        if (str == "## Problems & Solutions") iMark = 20;  // find title
                    }
                    else if (iMark == 20)
                    {
                        if (str == "") continue;
                        iProblemsCount++;
                        int iReadNo = GetDirectoryNo_FromDirectoryString(str);
                        if (iReadNo > iInsertNo)
                        {
                            strText += strInsert + "\n";    // insert content here
                            iMark = 29;  // iMakr == 29, insert completed
                        }
                    }
                    else if (iMark == 29)
                    {
                        if (str == "") continue;
                        iProblemsCount++;
                    }

                    // copy this line
                    strText += str + "\n";
                }
                if (iMark == 20)
                {
                    strText += strInsert + "\n";    // insert content here
                    iMark = 29;  // iMakr == 29, insert completed
                }
                if (iMark != 29)
                {
                    MessageBox.Show(@"[Problems.md] insert failed!");
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);
            }
            lblOut_Problems.Visible = true;

            Process.Start(strFile);
            return iProblemsCount;
        }

        private void Modify_UpdateFile()
        {
            string strFile = txtOut_UpdateFilePath.Text;

            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Update.md] file not exist!");
                return;
            }

            // example: 
            // * 133.CloneGraph 克隆图
            string strInsert = "* " + m_strId + "." + m_strTitleE + " " + m_strTitleC + "\n";
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

                lblOut_Update.Visible = true;
            }

            Process.Start(strFile);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            CalcMainString();

            txtTitle_TextChanged(sender, e);
            int iProblemsCount = Modify_ProblemsFile();
            Modify_UpdateFile();
            Create_AnswerFile();
            Modify_ReadmeFile(iProblemsCount);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtIn_IdTitleC.Text = "";
            txtIn_IdTitleE.Text = "";
            txtIn_Link.Text = "";
            txtIn_Description.Text = "";
            txtIn_SolutionLink.Text = "";
            txtIn_Answer.Text = "";
            txtOut_AnswerFilePath.Text = "";
            lblOut_IdTitleE.Visible = false;
            lblOut_Readme.Visible = false;
            lblOut_Problems.Visible = false;
            lblOut_Update.Visible = false;
            lblOut_Answer.Visible = false;
            btnGenerate.Enabled = false;
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            if (txtIn_IdTitleC.Text != "" && txtIn_Link.Text != "")
            {
                CalcMainString();
                string strIdTitleE = m_strId + "." + m_strTitleE;

                string str = txt_FilePath.Text;
                str += @"/problems/" + strIdTitleE + @"/README.md";

                txtIn_IdTitleE.Text = strIdTitleE;
                Clipboard.SetText(strIdTitleE);
                txtOut_AnswerFilePath.Text = str;

                lblOut_IdTitleE.Visible = true;
                btnGenerate.Enabled = true;
            }
            else
            {
                txtIn_IdTitleE.Text = "";
                lblOut_IdTitleE.Visible = false;
                btnGenerate.Enabled = false;
            }
        }

        private void btnFixFile_Click(object sender, EventArgs e)
        {
            //string strFile = txtOut_ProblemsFilePath.Text;

            //if (!File.Exists(strFile))
            //{
            //    MessageBox.Show(@"[README.md] file not exist!");
            //    return;
            //}

            //string strText = "";
            //int iMark = 0;

            //FileStream fs = new FileStream(strFile, FileMode.Open);
            //using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            //{
            //    while (!sr.EndOfStream)
            //    {
            //        string str = sr.ReadLine();
            //        if (iMark == 0)
            //        {
            //            if (str == "# leetcode-cn") iMark = 1;  // find title
            //        }
            //        else if (iMark == 1)
            //        {
            //            if (str == "## Problems & Solutions") iMark = 20;  // find title
            //        }
            //        else if (iMark == 20)
            //        {
            //            if (str == "") continue;
            //            string strDifficult = GetDifficult_FromDirectoryString(str);
            //            string strIdTitle = GetIdTitleEC_FromDirectoryString(str);
            //            string strAnswer = GetAnswerFilePath_FromDirectoryString(str);

            //            str = "* ";
            //            str += strDifficult;
            //            str += " ";
            //            str += strIdTitle;
            //            str += " | [详情](";
            //            str += strAnswer;
            //            str += ")";
            //        }

            //        // copy this line
            //        strText += str + "\n";
            //    }
            //    sr.Close();

            //    UTF8Encoding utf8 = new UTF8Encoding(false);
            //    File.WriteAllText(strFile, strText, utf8);
            //}
            //lblOut_Readme.Visible = true;
        }
    }
}
