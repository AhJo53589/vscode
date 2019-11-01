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
            if (txt_path_commit_bat.Text == "") return;

            string strText;
            strText = "git pull \r\n";
            strText += "git add -A \r\n";
            strText += "git commit -m";
            strText += "\"" + txt_in_id.Text + "." + txt_in_titleE.Text + "\" \r\n";
            strText += "git push \r\n";

            string strFile = txt_path_commit_bat.Text;
            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);
            btn_open_commit_bat.Visible = true;
        }

        private void Create_File_Answer_Readme_md()
        {
            if (txt_path_answer_readme_md.Text == "") return;

            // example: 
            //# `（简单）`  [48.Rotate 旋转图像](https://leetcode-cn.com/problems/rotate-image/)
            string strText;
            strText = "# " + GenerateString_DiffIdTitleECLink() + "\r\n\r\n";

            // ### 题目描述
            strText += "### 题目描述\r\n";
            strText += txt_in_description.Text;
            strText += "\r\n\r\n";

            // ---
            strText += "---\r\n";

            // ### 思路
            strText += "### 思路\r\n";
            strText += "```\r\n```\r\n";
            strText += "\r\n";

            // 题解链接
            strText += GenerateString_SolutionLink() + "\r\n\r\n";

            // ### 答题
            strText += GenerateString_Answer() + "\r\n\r\n";

            // ### 其它
            strText += GenerateString_Answer_Other() + "\r\n\r\n";

            string strFile = txt_path_answer_readme_md.Text;
            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);
            btn_open_answer_readme_md.Visible = true;
        }

        private void Modify_File_Readme_md(int iProblemsCount)
        {
            if (txt_path_readme_md.Text == "") return;
            string strFile = txt_path_readme_md.Text;
            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[README.md] file not exist!");
                return;
            }

            string strInsert_SelectedSolution = GenerateString_InfoForm_Problem("./problems/");
            int.TryParse(txt_in_id.Text, out int iInsertNo);
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
                    else if (iMark == 10 || iMark == 11)
                    {
                        if (str == "") continue;
                        if (str.IndexOf("|") != -1) iMark++;     
                    }
                    else if (iMark == 12)
                    {
                        if (txt_in_solution_link.Text == "")
                        {
                            iMark = 19;
                        }
                        else
                        {
                            if (str.IndexOf("|") != -1)
                            {
                                int iReadNo = GetId_From_InfoForm_Problem(str);
                                if (iReadNo > iInsertNo)
                                {
                                    strText += strInsert_SelectedSolution + "\r\n";    // insert content here
                                    iMark = 19;  // iMakr == 19, insert completed
                                }
                            }
                            else
                            {
                                strText += strInsert_SelectedSolution + "\r\n";    // insert content here
                                iMark = 19;  // find title
                            }
                        }
                    }
                    else if (iMark == 19)
                    {
                        if (str == "## Problemset / All") iMark = 20;  // find title
                    }
                    else if (iMark == 20)
                    {
                        string[] s1 = str.Split('（');
                        string[] s2 = str.Split('/');
                        str = s1[0] + "（";
                        str += iProblemsCount.ToString();
                        str += " /";
                        str += s2[1];

                        iMark = 29;
                    }
                    else if (iMark == 29)
                    {
                        if (str == "## Contest") iMark = 30;  // find title
                    }
                    else if (iMark == 30 || iMark == 31)
                    {
                        if (str == "") continue;
                        if (str.IndexOf("|") != -1) iMark++;
                    }
                    else if (iMark == 32)
                    {
                        if (txt_path_contest.Text != "" &&
                            !Directory.Exists(txt_path_main.Text + txt_path_contest.Text))
                        {
                            string strInsert_Contest = GenerateString_InfoForm_Contest();
                            strText += strInsert_Contest + "\r\n";    // insert content here
                        }
                        iMark = 100;  // iMakr == 19, insert completed
                    }

                    // copy this line
                    strText += str + "\r\n";
                }
                if (iMark != 100)
                {
                    MessageBox.Show(@"[README.md] insert failed!");
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);
            }
            btn_open_readme_md.Visible = true;
        }

        private int Modify_File_ProblemsetAll_Readme_md()
        {
            if (txt_path_problemset_all.Text == "") return 0;

            int iProblemsCount = 0;
            string strFile = txt_path_problemset_all.Text;

            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[problemset/all/README.md] file not exist!");
                return iProblemsCount;
            }

            string strInsert = GenerateString_InfoForm_Problem("../../problems/");
            int.TryParse(txt_in_id.Text, out int iInsertNo);
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
                        if (str == "## All") iMark = 20;  // find title
                    }
                    else if (iMark == 20 || iMark == 21)
                    {
                        if (str == "") continue;
                        if (str.IndexOf("|") != -1) iMark++;
                    }
                    else if (iMark == 22)
                    {
                        if (str == "") continue;
                        if (str == "## Season/2019-fall")
                        {
                            strText += strInsert + "\r\n";    // insert content here
                            iMark = 29;  // iMakr == 29, insert completed
                        }
                        else
                        {
                            iProblemsCount += GetFinishStatus_From_InfoForm_Problem(str);
                            int iReadNo = GetId_From_InfoForm_Problem(str);
                            if (iReadNo > iInsertNo)
                            {
                                strText += strInsert + "\r\n";    // insert content here
                                iMark = 29;  // iMakr == 29, insert completed
                            }
                        }
                    }
                    else if (iMark == 29)
                    {
                        // TODO: iProblemsCount
                        if (str == "") continue;
                        iProblemsCount++;
                    }

                    // copy this line
                    strText += str + "\r\n";
                }
                if (iMark == 22)
                {
                    strText += strInsert + "\r\n";    // insert content here
                    iMark = 29;  // iMakr == 29, insert completed
                }
                if (iMark != 29)
                {
                    MessageBox.Show(@"[problemset/all/README.md] insert failed!");
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);
            }
            btn_open_problemset_all.Visible = true;

            return iProblemsCount + 1;
        }

        private void Create_File_Contest_Problems_Readme_md()
        {
            if (txt_path_contest_problems.Text == "") return;

            string strText = "";

            strText += "# " + GenerateString_InfoForm_Contest_Title() + "\r\n\r\n";
            strText += "[返回](../../README.md)" + "\r\n\r\n";

            strText += "## Problems & Solutions" + "\r\n\r\n";

            strText += "|     | #   | 名称                 | 题目                  | 答题          | 题解 | 难度 |" + "\r\n";
            strText += "| --- | --- | -------------------- | --------------------- | ------------- | ---- | ---- |" + "\r\n";

            strText += GenerateString_InfoForm_Problem("../../problems/") + "\r\n";

            string strFile = txt_path_contest_problems.Text;
            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);

            btn_open_contest_problems.Visible = true;
        }

        private void Modify_File_Contest_Problems_Readme_md()
        {
            if (txt_path_contest_problems.Text == "") return;
            string strFile = txt_path_contest_problems.Text;
            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[contest/../README.md] file not exist!");
                return;
            }

            string strInsert = GenerateString_InfoForm_Problem("../../problems/");
            int.TryParse(txt_in_id.Text, out int iInsertNo);
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
                        if (str == "## Problems & Solutions") iMark = 20;  // find title
                    }
                    else if (iMark == 20 || iMark == 21)
                    {
                        if (str == "") continue;
                        if (str.IndexOf("|") != -1) iMark++;
                    }
                    else if (iMark == 22)
                    {
                        if (str == "") continue;
                        int iReadNo = GetId_From_InfoForm_Problem(str);
                        if (iReadNo > iInsertNo)
                        {
                            strText += strInsert + "\r\n";    // insert content here
                            iMark = 29;  // iMakr == 29, insert completed
                        }
                    }
                    else if (iMark == 29)
                    {
                        if (str == "") continue;
                    }

                    // copy this line
                    strText += str + "\r\n";
                }
                if (iMark == 22)
                {
                    strText += strInsert + "\r\n";    // insert content here
                    iMark = 29;  // iMakr == 29, insert completed
                }
                if (iMark != 29)
                {
                    MessageBox.Show(@"[contest/../README.md] insert failed!");
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);
            }
            btn_open_contest_problems.Visible = true;
        }

        private void Modify_File_Solutions_md()
        {
            if (txt_in_solution_link.Text == "") return;
            string strFile = txt_path_solutions_md.Text;
            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Solutions.md] file not exist!");
                return;
            }

            string strInsert_SelectedSolution = GenerateString_InfoForm_Problem("./problems/");
            int.TryParse(txt_in_id.Text, out int iInsertNo);
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
                    else if (iMark == 10 || iMark == 11)
                    {
                        if (str == "") continue;
                        if (str.IndexOf("|") != -1) iMark++;
                    }
                    else if (iMark == 12)
                    {
                        if (str == "") continue;
                        int iReadNo = GetId_From_InfoForm_Problem(str);
                        if (iReadNo > iInsertNo)
                        {
                            strText += strInsert_SelectedSolution + "\r\n";    // insert content here
                            iMark = 20;  // iMakr == 20, insert completed
                        }
                    }
                    // copy this line
                    strText += str + "\r\n";
                }
                if (iMark == 12)
                {
                    strText += strInsert_SelectedSolution + "\r\n";    // insert content here
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
            btn_open_solutions_md.Visible = true;
        }

        private void Modify_File_Update_md()
        {
            string strFile = txt_path_update_md.Text;

            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Update.md] file not exist!");
                return;
            }

            // example: 
            // * 133.CloneGraph 克隆图
            string strInsert = "* " + txt_in_id.Text + "." + txt_in_titleE.Text + " " + txt_in_titleC.Text + "\n";
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

                btn_open_update_md.Visible = true;
            }
        }

        private void Copy_File_Solution_cpp()
        {
            // path
            string sourcePath = txt_path_main.Text + @"\problems\_test_" + m_strCodeSelect + @"\";
            sourcePath += "SOLUTION.cpp";
            string targetPath = txt_path_solution_cpp.Text;

            // read source
            if (!File.Exists(sourcePath))
            {
                MessageBox.Show(@"[SOLUTION.cpp] file not exist!");
                return;
            }

            string strText = "";
            FileStream fs = new FileStream(sourcePath, FileMode.Open);
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                int iMark = 0;
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();

                    if (iMark == 0)
                    {
                        if (str.IndexOf("_get_test_cases_filestream()") != -1)
                        {
                            iMark = 1;
                        }
                    }
                    else if (iMark == 1)
                    {
                        string strComment = "";
                        if (str.IndexOf("//") != -1)
                        {
                            strComment = "//";
                        }
                        if (str.IndexOf("return") != -1)
                        {
                            strText += strComment + "\treturn " + "\"../../problems/" + txt_in_titleE.Text + "/tests.txt\";" + "\r\n";
                            continue;
                        }
                    }

                    // copy this line
                    strText += str + "\r\n";
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(targetPath, strText, utf8);
            }

            btn_open_solution_cpp.Visible = true;
        }

        private void Copy_File_Tests_txt()
        {
            string sourcePath = txt_path_main.Text + @"\problems\_test_" + m_strCodeSelect + @"\";
            sourcePath += "tests.txt";
            string targetPath = txt_path_tests_txt.Text;

            System.IO.File.Copy(sourcePath, targetPath, true);
            btn_open_tests_txt.Visible = true;
        }

        private void Modify_File_Define_IdName_h()
        {
            if (txt_path_define_h.Text == "") return;
            string strFile = txt_path_define_h.Text;
            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Define_IdName.h] file not exist!");
                return;
            }

            string strInsert_SelectedSolution = "#define SOLUTION_CPP_FOLDER_NAME_ID_" + txt_in_id.Text + " \t" + txt_in_titleE.Text;
            int.TryParse(txt_in_id.Text, out int iInsertNo);
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
                        if (str == "#pragma once") iMark = 1;  // find title
                    }
                    else if (iMark == 1)
                    {
                        if (str == "") continue;

                        string[] s = str.Split(' ');
                        s = s[1].Split('_');
                        int.TryParse(s[5], out int iReadNo);

                        if (iReadNo > iInsertNo)
                        {
                            strText += strInsert_SelectedSolution + "\r\n";    // insert content here
                            iMark = 20;  // iMakr == 20, insert completed
                        }
                    }
                    // copy this line
                    strText += str + "\r\n";
                }
                if (iMark == 1)
                {
                    strText += strInsert_SelectedSolution + "\r\n";    // insert content here
                    iMark = 20;  //  iMakr == 20, insert completed
                }

                if (iMark != 20)
                {
                    MessageBox.Show(@"[Define_IdName.h] insert failed!");
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);
            }

            btn_open_define_h.Visible = true;
        }

        private void Modify_File_Test_cpp()
        {
            if (txt_path_test_cpp.Text == "") return;

            string strFile = txt_path_test_cpp.Text;

            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Test.cpp] file not exist!");
                return;
            }

            string strOld = "#include SOLUTION_CPP_PATH_NAME(SOLUTION_CPP_FOLDER_NAME_ID_";
            string strInsert = strOld + txt_in_id.Text + ")";
            int.TryParse(txt_in_id.Text, out int iInsertNo);
            string strText = "";

            FileStream fs = new FileStream(strFile, FileMode.Open);
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    if (str.IndexOf(strOld) != -1)
                    {
                        strText += strInsert + "\r\n";    // insert content here
                        continue;
                    }

                    // copy this line
                    strText += str + "\r\n";
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);
            }
            btn_open_test_cpp.Visible = true;
        }

    }
}