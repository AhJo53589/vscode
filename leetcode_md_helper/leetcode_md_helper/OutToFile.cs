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
        private void Create_CommitFile()
        {
            string strText;
            strText = "git pull \r\n";
            strText += "git add -A \r\n";
            strText += "git commit -m";
            strText += "\"" + txtIn_IdTitleE.Text + "\" \r\n";
            strText += "git push \r\n";

            string strFile = txtOut_CommitFilePath.Text;
            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);
            lblOut_Commit.Visible = true;

            //Process.Start(strFile);
        }

        private void Create_AnswerFile()
        {
            // example: 
            //# `（简单）`  [48.Rotate 旋转图像](https://leetcode-cn.com/problems/rotate-image/)
            string strText;
            strText = GenerateString_Difficult_Link();

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
            strText += GenerateString_SolutionLink();

            // ### 答题
            strText += GenerateString_Answer();

            // ### 其它
            strText += GenerateString_Answer_2();

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
                        //    if (str == "# leetcode-cn") iMark = 1;  // find title
                        //}
                        //else if (iMark == 1)
                        //{
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
            return iProblemsCount + 1;
        }

        private void Modify_SolutionsFile()
        {
            if (txtIn_SolutionLink.Text == "") return;

            string strFile = txtOut_SolutionsFilePath.Text;

            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Solutions.md] file not exist!");
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
                        if (str == "# Solutions") iMark = 1;  // find title
                    }
                    else if (iMark == 1)
                    {
                        if (str == "## All Solutions") iMark = 10;  // find title
                    }
                    else if (iMark == 10)
                    {
                        if (str == "") continue;
                        int iReadNo = GetDirectoryNo_FromDirectoryString(str);
                        if (iReadNo > iInsertNo)
                        {
                            strText += strInsert_SelectedSolution + "\n";    // insert content here
                            iMark = 20;  // iMakr == 20, insert completed
                        }
                    }
                    // copy this line
                    strText += str + "\n";
                }
                if (iMark == 10)
                {
                    strText += strInsert_SelectedSolution + "\n";    // insert content here
                    iMark = 20;  //  iMakr == 20, insert completed
                }

                if (iMark != 20)
                {
                    MessageBox.Show(@"[Solutions.md] insert failed!");
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);
            }
            lblOut_Solutions.Visible = true;

            Process.Start(strFile);
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
    }
}