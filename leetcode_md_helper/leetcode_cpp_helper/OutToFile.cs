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

namespace leetcode_cpp_helper
{
    public partial class frmMain : Form
    {
        string strComment = "//";
        string strEnter = "\r\n";

        ///////////////////////////////////////////////////////////////////////////////////////
        /// Common
        private void Modify_File_Test_cpp(bool useDefault, string strId, string strDir = "")
        {
            if (txt_path_test_cpp.Text == "") return;

            string strFile = txt_path_test_cpp.Text;

            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Test.cpp] file not exist!");
                return;
            }

            string strUseDefault = "#define USE_DEFAULT_INCLUDE";
            string strOld = "#define SOLUTION_ID						SOLUTION_CPP_FOLDER_NAME_ID_";
            if (!useDefault)
            {
                strOld = "#define SOLUTION_CPP_FULL_PATH			\"../../" + strDir + "/";
            }
            string strInsert = strOld + strId;
            if (!useDefault)
            {
                strInsert = strOld + strId + "/SOLUTION.cpp\"";
            }
            string strText = "";

            FileStream fs = new FileStream(strFile, FileMode.Open);
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    if (str.IndexOf(strUseDefault) != -1)
                    {
                        strText += (useDefault) ? strUseDefault : strComment + strUseDefault;
                        strText += strEnter;
                        continue;
                    }
                    if (str.IndexOf(strOld) != -1)
                    {
                        strText += strInsert + strEnter;    // insert content here
                        continue;
                    }

                    // copy this line
                    strText += str + strEnter;
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);
            }
            btn_open_test_cpp.BackColor = System.Drawing.Color.Aqua;
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        /// Launcher
        private void Find_In_File_Define_IdName_h()
        {
            string strFile = txt_path_define_h.Text;
            if (strFile == "") return;

            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Define_IdName.h] file not exist!");
                return;
            }

            txt_launcher_main_name.Text = "";

            string strSearch = "#define SOLUTION_CPP_FOLDER_NAME_ID_" + txt_launcher_main_id.Text + " ";

            FileStream fs = new FileStream(strFile, FileMode.Open);
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    if (str.IndexOf(strSearch) != -1)
                    {
                        string[] s = str.Split('\t');
                        txt_launcher_main_name.Text = s[s.Length - 1];
                        break;
                    }
                }
                sr.Close();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        /// New Cpp
        private void Create_File_Solution_cpp(string newPath, string fileName)
        {
            if (txt_new_cpp_in_func.Text == ""
                || txt_new_cpp_out_return_type.Text == "" || txt_new_cpp_out_func_name.Text == "" || txt_new_cpp_out_param.Text == ""
                || txt_new_cpp_out_param_value.Text == "") return;

            string strFile = newPath + fileName;
            string strText = strEnter;

            // 答题代码
            strText += "//////////////////////////////////////////////////////////////////////////" + strEnter;
            strText += txt_new_cpp_in_func_2.Text + strEnter + strEnter;

            // 转接函数
            strText += "//////////////////////////////////////////////////////////////////////////" + strEnter;
            strText += txt_new_cpp_out_return_type.Text + " " + "_solution_run" + txt_new_cpp_out_param.Text + strEnter;

            strText += "{" + strEnter;
            {   // 筛选测试用例              
                strText += "\t" + strComment + "int caseNo = -1;" + strEnter;        
                strText += "\t" + strComment + "static int caseCnt = 0;" + strEnter;
                strText += "\t" + strComment + "if (caseNo != -1 && caseCnt++ != caseNo) return {};" + strEnter + strEnter;
            }
            bool useSolution = true;
            if (useSolution)
            {
                // Sample:
                // Solution sln;
                // return sln.twoSum(nums, target);
                strText += "\tSolution sln;" + strEnter;
                strText += "\treturn sln." + txt_new_cpp_out_func_name.Text + txt_new_cpp_out_param_value.Text + ";" + strEnter;
            }
            else
            {
                // Sample:
                // return twoSum(nums, target);
                strText += "\treturn " + txt_new_cpp_out_func_name.Text + txt_new_cpp_out_param_value.Text + ";" + strEnter;
            }
            strText += "}" + strEnter + strEnter;

            strText += strComment + "#define USE_SOLUTION_CUSTOM" + strEnter;
            strText += strComment + txt_new_cpp_out_return_type.Text + " " + "_solution_custom(TestCases &tc)" + strEnter;
            strText += strComment + "{" + strEnter;
            strText += strComment + "\treturn {};" + strEnter;
            strText += strComment + "}" + strEnter + strEnter;

            // 测试用例
            strText += "//////////////////////////////////////////////////////////////////////////" + strEnter;
            strText += strComment + "#define USE_GET_TEST_CASES_IN_CPP" + strEnter;
            strText += strComment + "vector<string> _get_test_cases_string()" + strEnter;
            strText += strComment + "{" + strEnter;
            strText += strComment + "\treturn {};" + strEnter;
            strText += strComment + "}" + strEnter;

            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);
        }

        private void Create_File_TestCases_txt(string newPath, string fileName)
        {
            if (txt_new_cpp_in_func_testcase.Text == "") return;

            string strFile = newPath + fileName;

            string strText = txt_new_cpp_in_func_testcase.Text;

            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        /// Generate MD
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
                            strText += strInsert + strEnter;    // insert content here
                            iMark = 29;  // iMakr == 29, insert completed
                        }
                        else
                        {
                            iProblemsCount += GetFinishStatus_From_InfoForm_Problem(str);
                            int iReadNo = GetId_From_InfoForm_Problem(str);
                            if (iReadNo > iInsertNo)
                            {
                                strText += strInsert + strEnter;    // insert content here
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
                    strText += strInsert + strEnter;    // insert content here
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
            btn_open_problemset_all.BackColor = System.Drawing.Color.Aqua;

            return iProblemsCount + 1;
        }

        private void Modify_File_Readme_md(int iProblemsCount)
        {
            string strFile = txt_path_readme_md.Text;
            if (strFile == "") return;
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
                                    strText += strInsert_SelectedSolution + strEnter;    // insert content here
                                    iMark = 19;  // iMakr == 19, insert completed
                                }
                            }
                            else
                            {
                                strText += strInsert_SelectedSolution + strEnter;    // insert content here
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

                        iMark = 100;
                    }

                    // copy this line
                    strText += str + strEnter;
                }
                if (iMark != 100)
                {
                    MessageBox.Show(@"[README.md] insert failed!");
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);
            }
            btn_open_readme_md.BackColor = System.Drawing.Color.Aqua;
        }

        private void Modify_File_Contest_md(string strFile)
        {
            if (strFile == "") return;
            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Contest.md] file not exist!");
                return;
            }

            if (txt_in_contest.Text != "" &&   
                !Directory.Exists(txt_path_main.Text + txt_in_contest.Text))
            {
                string strInsert_Contest = GenerateString_InfoForm_Contest() + strEnter;

                StreamWriter sw = File.AppendText(strFile);
                sw.Write(strInsert_Contest);
            }

            btn_open_contest_md.BackColor = System.Drawing.Color.Aqua;
        }

        private void Modify_File_Update_md(string strFile)
        {
            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Update.md] file not exist!");
                return;
            }

            // example: 
            // * 133.CloneGraph 克隆图
            string strInsert = "* " + txt_in_id.Text + "." + txt_in_titleE.Text + " " + txt_in_titleC.Text + strEnter;
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
                        strText += strInsert + strEnter;
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
                            strText += strDate + strEnter + strInsert + strEnter;
                            strText += strEnter;
                            strText += "---" + strEnter;
                            iMark = 3;  // iMakr == 3, insert completed
                        }

                    }
                    if (iMark == 0 && str == "---") iMark = 1;  // iMark == 1, find first of "---'

                    // copy this line
                    strText += str + strEnter;
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);

                btn_open_update_md.BackColor = System.Drawing.Color.Aqua;
            }
        }

        private void Modify_File_Solutions_md(string strFile)
        {
            if (txt_in_solution_link.Text == "") return;
            if (strFile == "") return;
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
                            strText += strInsert_SelectedSolution + strEnter;    // insert content here
                            iMark = 20;  // iMakr == 20, insert completed
                        }
                    }
                    // copy this line
                    strText += str + strEnter;
                }
                if (iMark == 12)
                {
                    strText += strInsert_SelectedSolution + strEnter;    // insert content here
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
            btn_open_solutions_md.BackColor = System.Drawing.Color.Aqua;
        }

        private void Create_File_Contest_Problems_Readme_md()
        {
            string strFile = txt_path_contest_problems.Text;
            if (strFile == "") return;

            string strText = "";

            strText += "# " + GenerateString_InfoForm_Contest_Title() + strEnter + strEnter;
            strText += "[返回](../../README.md)" + strEnter + strEnter;

            strText += "## Problems & Solutions" + strEnter + strEnter;

            strText += "|     | #   | 名称                 | 题目                  | 答题          | 题解 | 难度 |" + strEnter;
            strText += "| --- | --- | -------------------- | --------------------- | ------------- | ---- | ---- |" + strEnter;

            strText += GenerateString_InfoForm_Problem("../../problems/") + strEnter;

            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);

            btn_open_contest_problems.BackColor = System.Drawing.Color.Aqua;
        }

        private void Modify_File_Contest_Problems_Readme_md()
        {
            string strFile = txt_path_contest_problems.Text;
            if (strFile == "") return;
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
                    strText += str + strEnter;
                }
                if (iMark == 22)
                {
                    strText += strInsert + strEnter;    // insert content here
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
            btn_open_contest_problems.BackColor = System.Drawing.Color.Aqua;
        }

        private void Create_File_Answer_Readme_md(string strFile)
        {
            if (strFile == "") return;

            // example: 
            //# `（简单）`  [48.Rotate 旋转图像](https://leetcode-cn.com/problems/rotate-image/)
            string strText;
            strText = "# " + GenerateString_DiffIdTitleECLink() + strEnter + strEnter;

            // ### 题目描述
            strText += "### 题目描述" + strEnter;
            strText += txt_in_description.Text;
            strText += strEnter + strEnter;

            // ---
            strText += "---" + strEnter;

            // ### 思路
            strText += "### 思路" + strEnter;
            strText += "```" + strEnter + "```" + strEnter;
            strText += strEnter;

            // 题解链接
            strText += GenerateString_SolutionLink() + strEnter + strEnter;

            // ### 答题
            strText += GenerateString_Answer() + strEnter + strEnter;

            // ### 其它
            strText += GenerateString_Answer_Other() + strEnter + strEnter;

            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);
            btn_open_answer_readme_md.BackColor = System.Drawing.Color.Aqua;
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
                            strText += strInsert_SelectedSolution + strEnter;    // insert content here
                            iMark = 20;  // iMakr == 20, insert completed
                        }
                    }
                    // copy this line
                    strText += str + strEnter;
                }
                if (iMark == 1)
                {
                    strText += strInsert_SelectedSolution + strEnter;    // insert content here
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

            btn_open_define_h.BackColor = System.Drawing.Color.Aqua;
        }

        private void Create_CommitFile()
        {
            if (txt_path_commit_bat.Text == "") return;

            string strText;
            strText = "git pull" + strEnter;
            strText += "git add -A" + strEnter;
            strText += "git commit -m";
            strText += "\"" + txt_in_id.Text + "." + txt_in_titleE.Text + "\"" + strEnter;
            strText += "git push" + strEnter;

            string strFile = txt_path_commit_bat.Text;
            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);
            btn_open_commit_bat.Visible = true;
        }


    }
}