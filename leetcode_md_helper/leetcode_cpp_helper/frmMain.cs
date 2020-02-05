using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace leetcode_cpp_helper
{
    public partial class frmMain : Form
    {
        private string m_strDifficult;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txt_path_main.Text = System.Windows.Forms.Application.StartupPath;
            debug_path_main();
            Reset();
            Clear();
        }

        [Conditional("DEBUG")]
        private void debug_path_main()
        {
            // Test path
            txt_path_main.Text = @"C:\AhJo53589\leetcode-cn";
        }

        private void Reset()
        {
            // Directory tab tabpage
            txt_path_problems.Text = txt_path_main.Text + @"\problems";
            txt_path_problems_test.Text = txt_path_main.Text + @"\problems_test";

            txt_path_readme_md.Text = txt_path_main.Text + @"\README.md";
            txt_path_update_md.Text = txt_path_main.Text + @"\Update.md";
            txt_path_problemset_all.Text = txt_path_main.Text + @"\problemset\all\README.md";
            txt_path_solutions_md.Text = txt_path_main.Text + @"\Solutions.md";
            txt_path_commit_bat.Text = txt_path_main.Text + @"\git_commit.bat";

            txt_path_test_cpp.Text = txt_path_main.Text + @"\test\Test\Test.cpp";
            txt_path_define_h.Text = txt_path_main.Text + @"\test\Common\Define_IdName.h";
        }

        private void Clear()
        {
            Clear_Launcher();
            Clear_New_Cpp();
            Clear_Generate_MD();
            Clear_Directory();
        }

        private void Clear_Launcher()
        {
            // Launcher tabpage
            txt_launcher_id.Text = "";
            txt_launcher_titleE.Text = "";
            txt_launcher_dir_temp.Text = "problems_test";
            txt_launcher_id_temp.Text = "0";
        }

        private void Clear_New_Cpp()
        {
            // New Cpp tabpage
            txt_new_cpp_dir_temp.Text = "problems_test";
            txt_new_cpp_id_temp.Text = "0";
            txt_new_cpp_in_func_2.Text = "";
            txt_new_cpp_in_func.Text = "";
            txt_new_cpp_out_return_type.Text = "";
            txt_new_cpp_out_func_name.Text = "";
            txt_new_cpp_out_param.Text = "";
            txt_new_cpp_out_param_value.Text = "";
            txt_new_cpp_in_func_testcase.Text = "";
        }

        private void Clear_Generate_MD()
        {
            // Generate MD tabpage
            rb_in_difficult_1.Checked = true;
            cb_in_finish.Checked = true;
            txt_in_link.Text = "";
            txt_path_contest.Text = "";
            txt_in_id_titleC.Text = "";
            txt_in_id.Text = "";
            txt_in_titleE.Text = "";
            txt_in_titleC.Text = "";
            txt_in_solution_link.Text = "";
            txt_in_description.Text = "";
            txt_in_answer.Text = "";
            txt_in_answer_other.Text = "";
            txt_generate_md_dir.Text = "problems_test";
            cb_in_skip_md.Checked = false;
            txt_generate_md_id.Text = "0";

            m_strDifficult = rb_in_difficult_1.Text;
            btnGenerate.Enabled = false;
        }

        private void Clear_Directory()
        {
            // Directory tab tabpage
            txt_path_contest_problems.Text = "";
            txt_path_answer_readme_md.Text = "";
            txt_path_solution_cpp.Text = "";
            txt_path_tests_txt.Text = "";

            btn_open_readme_md.Visible = false;
            btn_open_update_md.Visible = false;
            btn_open_problemset_all.Visible = false;
            btn_open_contest_problems.Visible = false;
            btn_open_solutions_md.Visible = false;
            btn_open_answer_readme_md.Visible = false;
            btn_open_solution_cpp.Visible = false;
            btn_open_tests_txt.Visible = false;
            btn_open_test_cpp.Visible = false;
            btn_open_define_h.Visible = false;
        }


        //private void Modify_File_Define_IdName_h()
        //{
        //    if (txt_path_define_h.Text == "") return;

        //    string strFile = txt_path_define_h.Text;

        //    if (!File.Exists(strFile))
        //    {
        //        MessageBox.Show(@"[Define_IdName.h] file not exist!");
        //        return;
        //    }

        //    string strInsert_SelectedSolution = "#define SOLUTION_CPP_FOLDER_NAME_ID_" + txt_in_id.Text + " \t" + txt_in_titleE.Text;
        //    int.TryParse(txt_in_id.Text, out int iInsertNo);
        //    string strText = "";
        //    int iMark = 0;

        //    FileStream fs = new FileStream(strFile, FileMode.Open);
        //    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
        //    {
        //        while (!sr.EndOfStream)
        //        {
        //            string str = sr.ReadLine();
        //            if (iMark == 0)
        //            {
        //                if (str == "#pragma once") iMark = 1;  // find title
        //            }
        //            else if (iMark == 1)
        //            {
        //                if (str == "") continue;

        //                int iReadNo = 0;
        //                string[] s = str.Split(' ');
        //                s = s[1].Split('_');
        //                int.TryParse(s[5], out iReadNo);

        //                if (iReadNo > iInsertNo)
        //                {
        //                    strText += strInsert_SelectedSolution + "\r\n";    // insert content here
        //                    iMark = 20;  // iMakr == 20, insert completed
        //                }
        //            }
        //            // copy this line
        //            strText += str + "\r\n";
        //        }
        //        if (iMark == 1)
        //        {
        //            strText += strInsert_SelectedSolution + "\r\n";    // insert content here
        //            iMark = 20;  //  iMakr == 20, insert completed
        //        }

        //        if (iMark != 20)
        //        {
        //            MessageBox.Show(@"[Define_IdName.h] insert failed!");
        //        }
        //        sr.Close();

        //        UTF8Encoding utf8 = new UTF8Encoding(false);
        //        File.WriteAllText(strFile, strText, utf8);
        //    }
        //}


        //private int Modify_File_ProblemsetAll_Readme_md()
        //{
        //    if (txt_path_problemset_all.Text == "") return 0;

        //    int iProblemsCount = 0;
        //    string strFile = txt_path_problemset_all.Text;

        //    if (!File.Exists(strFile))
        //    {
        //        MessageBox.Show(@"[problemset/all/README.md] file not exist!");
        //        return iProblemsCount;
        //    }

        //    string strInsert = GenerateString_InfoForm_Problem("../../problems/");
        //    int.TryParse(txt_in_id.Text, out int iInsertNo);
        //    string strText = "";
        //    int iMark = 0;

        //    FileStream fs = new FileStream(strFile, FileMode.Open);
        //    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
        //    {
        //        while (!sr.EndOfStream)
        //        {
        //            string str = sr.ReadLine();
        //            if (iMark == 0)
        //            {
        //                if (str == "## All") iMark = 20;  // find title
        //            }
        //            else if (iMark == 20 || iMark == 21)
        //            {
        //                if (str == "") continue;
        //                iMark++;
        //            }
        //            else if(iMark == 22)
        //            {
        //                if (str == "") continue;
        //                iProblemsCount++;
        //                int iReadNo = GetId_From_InfoForm_Problem(str);
        //                if (iReadNo > iInsertNo)
        //                {
        //                    strText += strInsert + "\r\n";    // insert content here
        //                    iMark = 29;  // iMakr == 29, insert completed
        //                }
        //            }
        //            else if (iMark == 29)
        //            {
        //                if (str == "") continue;
        //                iProblemsCount++;
        //            }

        //            // copy this line
        //            strText += str + "\r\n";
        //        }
        //        if (iMark == 22)
        //        {
        //            strText += strInsert + "\r\n";    // insert content here
        //            iMark = 29;  // iMakr == 29, insert completed
        //        }
        //        if (iMark != 29)
        //        {
        //            MessageBox.Show(@"[problemset/all/README.md] insert failed!");
        //        }
        //        sr.Close();

        //        UTF8Encoding utf8 = new UTF8Encoding(false);
        //        File.WriteAllText(strFile, strText, utf8);
        //    }
        //    return iProblemsCount + 1;
        //}

        //private void Modify_File_Solutions_md()
        //{
        //    if (txt_in_solution_link.Text == "") return;
        //    if (txt_path_solutions_md.Text == "") return;
        //    string strFile = txt_path_solutions_md.Text;
        //    if (!File.Exists(strFile))
        //    {
        //        MessageBox.Show(@"[Solutions.md] file not exist!");
        //        return;
        //    }

        //    string strInsert_SelectedSolution = GenerateString_InfoForm_Problem("./problems/");
        //    int.TryParse(txt_in_id.Text, out int iInsertNo);
        //    string strText = "";
        //    int iMark = 0;

        //    FileStream fs = new FileStream(strFile, FileMode.Open);
        //    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
        //    {
        //        while (!sr.EndOfStream)
        //        {
        //            string str = sr.ReadLine();
        //            if (iMark == 0)
        //            {
        //                if (str == "# Solutions") iMark = 1;  // find title
        //            }
        //            else if (iMark == 1)
        //            {
        //                if (str == "## All Solutions") iMark = 10;  // find title
        //            }
        //            else if (iMark == 10 || iMark == 11)
        //            {
        //                if (str == "") continue;
        //                iMark++;
        //            }
        //            else if (iMark == 12)
        //            {
        //                if (str == "") continue;
        //                int iReadNo = GetId_From_InfoForm_Problem(str);
        //                if (iReadNo > iInsertNo)
        //                {
        //                    strText += strInsert_SelectedSolution + "\n";    // insert content here
        //                    iMark = 20;  // iMakr == 20, insert completed
        //                }
        //            }
        //            // copy this line
        //            strText += str + "\n";
        //        }
        //        if (iMark == 12)
        //        {
        //            strText += strInsert_SelectedSolution + "\n";    // insert content here
        //            iMark = 20;  //  iMakr == 20, insert completed
        //        }

        //        if (iMark != 20)
        //        {
        //            MessageBox.Show(@"[Solutions.md] insert failed!");
        //        }
        //        sr.Close();

        //        UTF8Encoding utf8 = new UTF8Encoding(false);
        //        File.WriteAllText(strFile, strText, utf8);
        //    }
        //}


        //private void Modify_File_Readme_md(int iProblemsCount)
        //{
        //    if (txt_path_readme_md.Text == "") return;
        //    string strFile = txt_path_readme_md.Text;
        //    if (!File.Exists(strFile))
        //    {
        //        MessageBox.Show(@"[README.md] file not exist!");
        //        return;
        //    }

        //    string strInsert_SelectedSolution = GenerateString_InfoForm_Problem("./problems/");
        //    int.TryParse(txt_in_id.Text, out int iInsertNo);
        //    string strText = "";
        //    int iMark = 0;

        //    FileStream fs = new FileStream(strFile, FileMode.Open);
        //    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
        //    {
        //        while (!sr.EndOfStream)
        //        {
        //            string str = sr.ReadLine();
        //            if (iMark == 0)
        //            {
        //                if (str == "# leetcode-cn") iMark = 1;  // find title
        //            }
        //            else if (iMark == 1)
        //            {
        //                if (str == "## Selected Solutions") iMark = 10;  // find title
        //            }
        //            else if (iMark == 10 || iMark == 11)
        //            {
        //                if (str == "") continue;
        //                if (str.IndexOf("|") != -1) iMark++;
        //            }
        //            else if (iMark == 12)
        //            {
        //                if (txt_in_solution_link.Text == "")
        //                {
        //                    iMark = 19;
        //                }
        //                else
        //                {
        //                    if (str.IndexOf("|") != -1)
        //                    {
        //                        int iReadNo = GetId_From_InfoForm_Problem(str);
        //                        if (iReadNo > iInsertNo)
        //                        {
        //                            strText += strInsert_SelectedSolution + "\r\n";    // insert content here
        //                            iMark = 19;  // iMakr == 19, insert completed
        //                        }
        //                    }
        //                    else
        //                    {
        //                        strText += strInsert_SelectedSolution + "\r\n";    // insert content here
        //                        iMark = 19;  // find title
        //                    }
        //                }
        //            }
        //            else if (iMark == 19)
        //            {
        //                if (str == "## Problemset / All") iMark = 20;  // find title
        //            }
        //            else if (iMark == 20)
        //            {
        //                string[] s1 = str.Split('（');
        //                string[] s2 = str.Split('/');
        //                str = s1[0] + "（";
        //                str += iProblemsCount.ToString();
        //                str += " /";
        //                str += s2[1];

        //                iMark = 29;
        //            }
        //            else if (iMark == 29)
        //            {
        //                if (str == "## Contest") iMark = 30;  // find title
        //            }
        //            else if (iMark == 30 || iMark == 31)
        //            {
        //                if (str == "") continue;
        //                if (str.IndexOf("|") != -1) iMark++;
        //            }
        //            else if (iMark == 32)
        //            {
        //                iMark = 100;  // iMakr == 19, insert completed
        //            }

        //            // copy this line
        //            strText += str + "\r\n";
        //        }
        //        if (iMark != 100)
        //        {
        //            MessageBox.Show(@"[README.md] insert failed!");
        //        }
        //        sr.Close();

        //        UTF8Encoding utf8 = new UTF8Encoding(false);
        //        File.WriteAllText(strFile, strText, utf8);
        //    }
        //    btn_open_readme_md.Visible = true;
        //}

        ////////////////////////////////////////////////////////////////////////////////////
        //private string GetLink_From_DiffTitleLink(string str)
        //{
        //    // example: 
        //    // `（简单）`  [1.two-sum 两数之和](https://leetcode-cn.com/problems/two-sum/)
        //    string[] s = str.Split('(');
        //    s = s[1].Split(')');
        //    return s[0];
        //}

        //private string GetId_From_DiffTitleLink(string str)
        //{
        //    // example: 
        //    // `（简单）`  [1.two-sum 两数之和](https://leetcode-cn.com/problems/two-sum/)
        //    string[] s = str.Split('[');
        //    s = s[1].Split(']');
        //    s = s[0].Split('.');
        //    return s[0];
        //}

        //private string GetTitleE_From_DiffTitleLink(string str)
        //{
        //    // example: 
        //    // `（简单）`  [1.two-sum 两数之和](https://leetcode-cn.com/problems/two-sum/)
        //    string[] s = str.Split('/');

        //    // https://leetcode-cn.com/contest/weekly-contest-159/problems/check-if-it-is-a-straight-line/
        //    // https://leetcode-cn.com/contest/season/2019-fall/problems/guess-numbers/
        //    if (s[3] == "problems") return s[4];
        //    if (s[5] == "problems") return s[6];
        //    if (s[6] == "problems") return s[7];
        //    return s[4];
        //}

        //private string GetTitleC_From_DiffTitleLink(string str)
        //{
        //    // example: 
        //    // `（简单）`  [1.two-sum 两数之和](https://leetcode-cn.com/problems/two-sum/)
        //    string[] s = str.Split('[');
        //    s = s[1].Split(']');
        //    s = s[0].Split(' ');
        //    if (s.Length == 3) return s[1] + " " + s[2];
        //    return s[1];
        //}

        //private string GetDifficult_From_DiffTitleLink(string str)
        //{
        //    // example: 
        //    // `（简单）`  [1.two-sum 两数之和](https://leetcode-cn.com/problems/two-sum/)
        //    string[] s = str.Split('（');
        //    s = s[1].Split('）');
        //    return s[0];
        //}

        //private int GetId_From_InfoForm_Problem(string str)
        //{
        //    string[] s = str.Split('|');
        //    int.TryParse(s[2], out int id);
        //    return id;
        //}

        //private string GenerateString_InfoForm_Problem(string path)
        //{
        //    // 有题解
        //    // | √ | 1 | [two-sum](../../problems/two-sum) | [两数之和](../../problems/two-sum/README.md) | [cpp](../../problems/two-sum/SOLUTION.cpp) | [查看](https://leetcode-cn.com/problems/two-sum/solution/liang-shu-zhi-he-by-leetcode-2/) | 简单 |
        //    // 无题解
        //    // | √ | 1 | [two-sum](../../problems/two-sum) | [两数之和](../../problems/two-sum/README.md) | [cpp](../../problems/two-sum/SOLUTION.cpp) |  | 简单 |
        //    string strText = "";
        //    strText += "| √";
        //    strText += " | " + txt_in_id.Text;
        //    strText += " | " + GenerateString_TitleAndFolderPath(path);
        //    strText += " | " + GenerateString_TitleAndFileLink(path);
        //    strText += " | " + GenerateString_CppFilePath(path);
        //    if (txt_in_solution_link.Text == "")
        //    {
        //        strText += " | " + " ";
        //    }
        //    else
        //    {
        //        strText += " | " + "[查看](" + txt_in_solution_link.Text + ")";
        //    }
        //    //strText += " | " + txt_in_difficult.Text;
        //    strText += " | ";
        //    return strText;
        //}

        //private string GenerateString_TitleAndFolderPath(string path)
        //{
        //    // example: 
        //    // [two-sum](../../problems/two-sum)
        //    string strText = "";
        //    strText += "[" + txt_in_titleE.Text + "]";
        //    strText += "(" + path + txt_in_titleE.Text + ")";
        //    return strText;
        //}

        //private string GenerateString_TitleAndFileLink(string path)
        //{
        //    // example: 
        //    // [两数之和](../../problems/two-sum/README.md)
        //    string strText = "";
        //    strText += "[" + txt_in_titleC.Text + "]";
        //    strText += "(" + path + txt_in_titleE.Text + "/README.md)";
        //    return strText;
        //}

        //private string GenerateString_CppFilePath(string path)
        //{
        //    // example: 
        //    // [cpp](../../problems/two-sum/SOLUTION.cpp)
        //    string strText = "";
        //    strText += "[cpp]";
        //    strText += "(" + path + txt_in_titleE.Text + "/SOLUTION.cpp)";
        //    return strText;
        //}

        //private string GenerateString_DiffIdTitleECLink()
        //{
        //    // example: 
        //    // `（简单）` [1.two-sum 两数之和](https://leetcode-cn.com/problems/two-sum/)
        //    string strText = "";
        //    //strText += "`（" + txt_in_difficult.Text + "）` ";
        //    //strText += "[" + txt_in_id.Text + "." + txt_in_titleE.Text + " " + txt_in_titleC.Text + "]";
        //    //strText += "(" + txt_in_link.Text + ")";
        //    return strText;
        //}




        ///////////////////////////////////////////////////////////////////////////////////////
        /// event
        ///////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////
        /// Launcher
        private void btn_launcher_active_Click(object sender, EventArgs e)
        {
            try
            {
                Modify_File_Test_cpp(true, txt_launcher_id.Text);
                Process.Start("explorer.exe", txt_path_problems.Text + "\\" + txt_launcher_titleE.Text);
            }
            catch
            {

            }
        }
  
        private void btn_launcher_active_temp_Click(object sender, EventArgs e)
        {
            try
            {
                Modify_File_Test_cpp(false, txt_launcher_id_temp.Text, txt_launcher_dir_temp.Text);
                Process.Start("explorer.exe", txt_launcher_dir_temp.Text + "\\" + txt_launcher_id_temp.Text);
            }
            catch
            {

            }
        }

        private void txt_launcher_id_TextChanged(object sender, EventArgs e)
        {
            Find_In_File_Define_IdName_h();
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        /// New Cpp
        private void btn_new_cpp_clear_Click(object sender, EventArgs e)
        {
            Clear_New_Cpp();
        }

        private void txt_new_cpp_in_func_TextChanged(object sender, EventArgs e)
        {
            SplitFuncParam();
        }

        private void btn_new_cpp_create_Click(object sender, EventArgs e)
        {
            string newPath = txt_path_main.Text + "\\" + txt_new_cpp_dir_temp.Text + "\\" + txt_new_cpp_id_temp.Text;
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            Create_File_Solution_cpp(newPath, @"\SOLUTION.cpp");
            Create_File_TestCases_txt(newPath, @"\tests.txt");

            Modify_File_Test_cpp(false, txt_new_cpp_id_temp.Text, txt_new_cpp_dir_temp.Text);

            Process.Start("explorer.exe", newPath);
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        /// Generate MD     
        private void btn_generate_md_clear_Click(object sender, EventArgs e)
        {
            Clear_Generate_MD();
        }

        private void rb_in_difficult_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            m_strDifficult = rb.Text;
        }

        private void txt_in_Title_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_in_id_titleC.Text != "" && txt_in_link.Text != "")
                {
                    SplitPathAndTitleE_From_Link();

                    if (txt_path_contest.Text != "")
                    {
                        txt_path_contest_problems.Text = txt_path_main.Text + txt_path_contest.Text + @"\README.md";
                    }
                    else
                    {
                        txt_path_contest_problems.Text = "";
                    }
                    txt_path_answer_readme_md.Text = txt_path_main.Text + @"\problems\" + txt_in_titleE.Text + @"\README.md";
                    txt_path_solution_cpp.Text = txt_path_main.Text + @"\problems\" + txt_in_titleE.Text + @"\SOLUTION.cpp";
                    txt_path_tests_txt.Text = txt_path_main.Text + @"\problems\" + txt_in_titleE.Text + @"\tests.txt";

                    btnGenerate.Enabled = true;
                }
                else
                {
                    btnGenerate.Enabled = false;
                }
            }
            catch
            {

            }
        }

        private void btnCopy_SolutionLink_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GenerateString_SolutionLink());
        }

        private void btnCopy_Description_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txt_in_description.Text);
        }

        private void btnCopy_Answer_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GenerateString_Answer());
        }

        private void btnCopy_Answer_2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GenerateString_Answer_Other());
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            // 集合文件
            int iProblemsCount = Modify_File_ProblemsetAll_Readme_md();
            Modify_File_Readme_md(iProblemsCount);
            Modify_File_Update_md();
            Modify_File_Solutions_md();

            // 比赛目录
            if (txt_path_contest.Text != "")
            {
                if (!Directory.Exists(txt_path_main.Text + txt_path_contest.Text))
                {
                    Directory.CreateDirectory(txt_path_main.Text + txt_path_contest.Text);
                }
                if (!File.Exists(txt_path_contest_problems.Text))
                {
                    Create_File_Contest_Problems_Readme_md();
                }
                else
                {
                    Modify_File_Contest_Problems_Readme_md();
                }
            }

            // 问题目录
            string oldPath = txt_path_main.Text + "\\" + txt_generate_md_dir.Text + "\\" + txt_generate_md_id.Text;
            string newPath = txt_path_problems.Text + "\\" + txt_in_titleE.Text;
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            if (!cb_in_skip_md.Checked)
            {
                Create_File_Answer_Readme_md(txt_path_answer_readme_md.Text);
            }

            CopyDirectory(oldPath, newPath);
            if (Directory.Exists(oldPath))
            {
                Directory.Delete(oldPath, true);
            }
            btn_open_solution_cpp.Visible = true;
            btn_open_tests_txt.Visible = true;

            // 程序目录
            Modify_File_Test_cpp(true, txt_in_id.Text);
            Modify_File_Define_IdName_h();

            // 提交文件
            Create_CommitFile();
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        /// Directory
        private void btn_open_main_dir_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", txt_path_main.Text + "\\");
        }

        private void btn_open_problems_dir_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", txt_path_problems.Text + "\\");
        }

        private void btn_open_problems_test_dir_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", txt_path_problems_test.Text + "\\");
        }

        private void btn_open_readme_md_Click(object sender, EventArgs e)
        {
            Process.Start(txt_path_readme_md.Text);
        }

        private void btn_open_update_md_Click(object sender, EventArgs e)
        {
            Process.Start(txt_path_update_md.Text);
        }

        private void btn_open_problemset_file_Click(object sender, EventArgs e)
        {
            Process.Start(txt_path_problemset_all.Text);
        }

        private void btn_open_contest_problems_Click(object sender, EventArgs e)
        {
            Process.Start(txt_path_contest_problems.Text);
        }

        private void btn_open_solutions_file_Click(object sender, EventArgs e)
        {
            Process.Start(txt_path_solutions_md.Text);
        }

        private void btn_open_commit_bat_Click(object sender, EventArgs e)
        {
            Process.Start(txt_path_readme_md.Text);
        }

        private void btn_open_answer_readme_md_Click(object sender, EventArgs e)
        {
            Process.Start(txt_path_answer_readme_md.Text);
        }

        private void btn_open_solution_cpp_Click(object sender, EventArgs e)
        {
            Process.Start(txt_path_solution_cpp.Text);
        }

        private void btn_open_tests_txt_Click(object sender, EventArgs e)
        {
            Process.Start(txt_path_tests_txt.Text);
        }

        private void btn_open_test_cpp_Click(object sender, EventArgs e)
        {
            Process.Start(txt_path_test_cpp.Text);
        }

        private void btn_open_define_file_Click(object sender, EventArgs e)
        {
            Process.Start(txt_path_define_h.Text);
        }

    }
}
