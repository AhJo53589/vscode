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
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            reset();
        }

        private void reset()
        {
            txt_path_main.Text = @"C:\AhJo53589\leetcode-cn\leetcode-cn_2";
            txt_path_old_problems.Text = txt_path_main.Text + @"\_problems";
            txt_path_curr_folder.Text = "";
            txt_path_new_problems.Text = txt_path_main.Text + @"\problems";
            txt_path_prev_folder.Text = txt_in_titleE.Text;
            txt_path_hold_problems.Text = txt_path_main.Text + @"\_problems_hold";

            txt_path_define_h.Text = txt_path_main.Text + @"\test\Common\Define_IdName.h";
            txt_path_test_cpp.Text = txt_path_main.Text + @"\test\Test\Test.cpp";

            txt_path_problemset_all.Text = txt_path_main.Text + @"\problemset\all\README.md";
            txt_path_solutions_md.Text = txt_path_main.Text + @"\Solutions.md";

            txt_in_difficult.Text = "";
            txt_in_id.Text = "";
            txt_in_titleE.Text = "";
            txt_in_titleC.Text = "";
            txt_in_link.Text = "";
            txt_in_solution_link.Text = "";

            txt_in_func.Text = "";
            txt_out_return_type.Text = "";
            txt_out_func_name.Text = "";
            txt_out_param.Text = "";
            txt_out_param_value.Text = "";
            txt_in_func_2.Text = "";
            txt_in_func_testcase.Text = "";

            btn_get_folder.Enabled = true;
            btn_transfer_folder.Enabled = false;
        }

        public static void CopyDirectory(string srcPath, string destPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (!Directory.Exists(destPath + "\\" + i.Name))
                        {
                            Directory.CreateDirectory(destPath + "\\" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                        }
                        CopyDirectory(i.FullName, destPath + "\\" + i.Name);    //递归调用复制子文件夹
                    }
                    else
                    {
                        File.Copy(i.FullName, destPath + "\\" + i.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private void Open_AnswerFile(string path)
        {
            string strFile = path + @"\README.md";

            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[README.md] file not exist!");
                return;
            }

            FileStream fs = new FileStream(strFile, FileMode.Open);
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                bool bFirstLine = true;
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    if (bFirstLine)
                    {
                        bFirstLine = false;
                        txt_in_difficult.Text = GetDifficult_From_DiffTitleLink(str);
                        txt_in_id.Text = GetId_From_DiffTitleLink(str);
                        txt_in_titleE.Text = GetTitleE_From_DiffTitleLink(str);
                        txt_in_titleC.Text = GetTitleC_From_DiffTitleLink(str);
                        txt_in_link.Text = GetLink_From_DiffTitleLink(str);
                    }

                    // [发布的题解] (https://leetcode-cn.com/problems/search-in-rotated-sorted-array/solution/sou-suo-xuan-zhuan-pai-xu-shu-zu-by-ikaruga/)
                    if (str.IndexOf("[发布的题解]") != -1)
                    {
                        string[] s = str.Split('(');
                        s = s[1].Split(')');
                        txt_in_solution_link.Text = s[0];
                    }
                }
                sr.Close();
            }
        }

        private void Copy_File_AnswerReadme_md(string oldPath, string newPath)
        {
            string strFile = oldPath + @"\README.md";

            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[README.md] file not exist!");
                return;
            }

            string strText = "";

            FileStream fs = new FileStream(strFile, FileMode.Open);
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                bool bFirstLine = true;
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    if (bFirstLine)
                    {
                        bFirstLine = false;
                        str = "# " + GenerateString_DiffIdTitleECLink();
                    }

                    // copy this line
                    strText += str + "\r\n";
                }
                sr.Close();

                strFile = newPath + @"\README.md";
                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);
            }

            //Process.Start(strFile);
        }

        private void Create_File_Solution_cpp(string newPath)
        {
            if (txt_in_func.Text == ""
                || txt_out_return_type.Text == "" || txt_out_func_name.Text == "" || txt_out_param.Text == ""
                || txt_out_param_value.Text == "") return;

            string strFile = newPath + @"\SOLUTION.cpp";

            // 测试函数
            string strText = "\r\n";

            strText += txt_in_func_2.Text + "\r\n\r\n";

            strText += "//////////////////////////////////////////////////////////////////////////" + "\r\n";
            strText += txt_in_func.Text + "\r\n\r\n";

            // 转接函数
            strText += "//////////////////////////////////////////////////////////////////////////" + "\r\n";
            strText += txt_out_return_type.Text + " " + "_solution_run" + txt_out_param.Text + "\r\n";
            strText += "{\r\n";
            strText += "\treturn " + txt_out_func_name.Text + txt_out_param_value.Text + ";" + "\r\n";
            strText += "}\r\n\r\n";

            strText += "//" + "#define USE_SOLUTION_CUSTOM" + "\r\n";
            strText += "//" + txt_out_return_type.Text + " " + "_solution_custom(TestCases &tc)" + "\r\n";
            strText += "//" + "{\r\n";
            strText += "//" + "}\r\n\r\n";

            // 测试用例
            strText += "//////////////////////////////////////////////////////////////////////////" + "\r\n";
            strText += "vector<string> _get_test_cases_string()" + "\r\n";
            strText += "{\r\n";
            strText += "\treturn {};\r\n";
            strText += "}\r\n\r\n";

            string strComment = "";
            if (txt_in_func_testcase.Text == "")
            {
                strComment = "//";
            }
            strText += strComment + "#define USE_GET_TEST_CASES_FILESTREAM" + "\r\n";
            strText += strComment + "string _get_test_cases_filestream()" + "\r\n";
            strText += strComment + "{\r\n";
            strText += strComment + "\treturn " + "\"../../problems/" + txt_in_titleE.Text + "/tests.txt\";" + "\r\n";
            strText += strComment + "}\r\n\r\n";

            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);

            Process.Start(strFile);
        }

        private void Create_File_TestCases_txt(string newPath)
        {
            if (txt_in_func_testcase.Text == "") return;

            string strFile = newPath + @"\tests.txt";

            string strText = txt_in_func_testcase.Text;

            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);

            //Process.Start(strFile);
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
            int iInsertNo = 0;
            int.TryParse(txt_in_id.Text, out iInsertNo);
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

                        int iReadNo = 0;
                        string[] s = str.Split(' ');
                        s = s[1].Split('_');
                        int.TryParse(s[5], out iReadNo);

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

            //Process.Start(strFile);
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

            string strInsert = GenerateString_InfoForm_Problem();
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
                        iMark++;
                    }
                    else if(iMark == 22)
                    {
                        if (str == "") continue;
                        iProblemsCount++;
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

            //Process.Start(strFile);
            return iProblemsCount + 1;
        }

        private void Modify_File_Solutions_md()
        {
            if (txt_path_solutions_md.Text == "") return;
            string strFile = txt_path_solutions_md.Text;
            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Solutions.md] file not exist!");
                return;
            }

            string strInsert_SelectedSolution = GenerateString_InfoForm_Problem();
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
                        iMark++;
                    }
                    else if (iMark == 12)
                    {
                        if (str == "") continue;
                        int iReadNo = GetId_From_InfoForm_Problem(str);
                        if (iReadNo > iInsertNo)
                        {
                            strText += strInsert_SelectedSolution + "\n";    // insert content here
                            iMark = 20;  // iMakr == 20, insert completed
                        }
                    }
                    // copy this line
                    strText += str + "\n";
                }
                if (iMark == 12)
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

            //Process.Start(strFile);
        }

        ////////////////////////////////////////////////////////////////////////////////////
        private string GetLink_From_DiffTitleLink(string str)
        {
            // example: 
            // `（简单）`  [1.two-sum 两数之和](https://leetcode-cn.com/problems/two-sum/)
            string[] s = str.Split('(');
            s = s[1].Split(')');
            return s[0];
        }

        private string GetId_From_DiffTitleLink(string str)
        {
            // example: 
            // `（简单）`  [1.two-sum 两数之和](https://leetcode-cn.com/problems/two-sum/)
            string[] s = str.Split('[');
            s = s[1].Split(']');
            s = s[0].Split('.');
            return s[0];
        }

        private string GetTitleE_From_DiffTitleLink(string str)
        {
            // example: 
            // `（简单）`  [1.two-sum 两数之和](https://leetcode-cn.com/problems/two-sum/)
            string[] s = str.Split('/');

            // https://leetcode-cn.com/contest/weekly-contest-159/problems/check-if-it-is-a-straight-line/
            // https://leetcode-cn.com/contest/season/2019-fall/problems/guess-numbers/
            if (s[3] == "problems") return s[4];
            if (s[5] == "problems") return s[6];
            if (s[6] == "problems") return s[7];
            return s[4];
        }

        private string GetTitleC_From_DiffTitleLink(string str)
        {
            // example: 
            // `（简单）`  [1.two-sum 两数之和](https://leetcode-cn.com/problems/two-sum/)
            string[] s = str.Split('[');
            s = s[1].Split(']');
            s = s[0].Split(' ');
            return s[1];
        }

        private string GetDifficult_From_DiffTitleLink(string str)
        {
            // example: 
            // `（简单）`  [1.two-sum 两数之和](https://leetcode-cn.com/problems/two-sum/)
            string[] s = str.Split('（');
            s = s[1].Split('）');
            return s[0];
        }

        private int GetId_From_InfoForm_Problem(string str)
        {
            string[] s = str.Split('|');
            int.TryParse(s[2], out int id);
            return id;
        }

        private string GenerateString_InfoForm_Problem()
        {
            // 有题解
            // | √ | 1 | [two-sum](../../problems/two-sum) | [两数之和](../../problems/two-sum/README.md) | [cpp](../../problems/two-sum/SOLUTION.cpp) | [查看](https://leetcode-cn.com/problems/two-sum/solution/liang-shu-zhi-he-by-leetcode-2/) | 简单 |
            // 无题解
            // | √ | 1 | [two-sum](../../problems/two-sum) | [两数之和](../../problems/two-sum/README.md) | [cpp](../../problems/two-sum/SOLUTION.cpp) |  | 简单 |
            string strText = "";
            strText += "| √";
            strText += " | " + txt_in_id.Text;
            strText += " | " + GenerateString_TitleAndFolderPath();
            strText += " | " + GenerateString_TitleAndFileLink();
            strText += " | " + GenerateString_CppFilePath();
            if (txt_in_solution_link.Text == "")
            {
                strText += " | " + " ";
            }
            else
            {
                strText += " | " + "[查看](" + txt_in_solution_link.Text + ")";
            }
            strText += " | " + txt_in_difficult.Text;
            strText += " | ";
            return strText;
        }

        private string GenerateString_TitleAndFolderPath()
        {
            // example: 
            // [two-sum](../../problems/two-sum)
            string strText = "";
            strText += "[" + txt_in_titleE.Text + "]";
            strText += "(../../problems/" + txt_in_titleE.Text + ")";
            return strText;
        }

        private string GenerateString_TitleAndFileLink()
        {
            // example: 
            // [两数之和](../../problems/two-sum/README.md)
            string strText = "";
            strText += "[" + txt_in_titleC.Text + "]";
            strText += "(../../problems/" + txt_in_titleE.Text + "/README.md)";
            return strText;
        }

        private string GenerateString_CppFilePath()
        {
            // example: 
            // [cpp](../../problems/two-sum/SOLUTION.cpp)
            string strText = "";
            strText += "[cpp]";
            strText += "(../../problems/" + txt_in_titleE.Text + "/SOLUTION.cpp)";
            return strText;
        }

        private string GenerateString_DiffIdTitleECLink()
        {
            // example: 
            // `（简单）` [1.two-sum 两数之和](https://leetcode-cn.com/problems/two-sum/)
            string strText = "";
            strText += "`（" + txt_in_difficult.Text + "）` ";
            strText += "[" + txt_in_id.Text + "." + txt_in_titleE.Text + " " + txt_in_titleC.Text + "]";
            strText += "(" + txt_in_link.Text + ")";
            return strText;
        }

        private void SplitFuncParam()
        {
            if (txt_in_func.Text == "") return;

            // vector<int> twoSum(vector<int> &nums, int target)
            string str = txt_in_func.Text;
            string[] s = str.Split(' ');
            // vector<int>
            txt_out_return_type.Text = s[0];
            s = s[1].Split('(');
            // twoSum
            txt_out_func_name.Text = s[0];

            // (vector<int> &nums, int target)
            s = str.Split('(');
            s = s[1].Split(')');
            txt_out_param.Text = "(" + s[0] + ")";

            // (nums, target)
            string temp = "(";
            string[] sValue = s[0].Split(',');
            for (int i = 0; i < sValue.Length; i++)
            {
                if (sValue[i][0] == ' ') sValue[i] = sValue[i].Substring(1);
                s = sValue[i].Split(' ');
                if (s[s.Length - 1][0] == '*' || s[s.Length - 1][0] == '&')
                {
                    temp += s[s.Length - 1].Substring(1);
                }
                else
                {
                    temp += sValue[i] = s[s.Length - 1];
                }
                if (i != sValue.Length - 1) temp += ",";
            }
            temp += ")";
            txt_out_param_value.Text = temp;
        }

        private void btn_get_folder_Click(object sender, EventArgs e)
        {
            DirectoryInfo folder = new DirectoryInfo(txt_path_old_problems.Text);
            FileSystemInfo[] fileinfo = folder.GetFileSystemInfos();
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo)
                {
                    txt_path_curr_folder.Text = i.Name;
                    string first_path = txt_path_old_problems.Text + "\\" + i.Name;
                    Open_AnswerFile(first_path);
                    break;
                }
            }

            string oldPath = txt_path_old_problems.Text + "\\" + txt_path_curr_folder.Text;
            DirectoryInfo folder2 = new DirectoryInfo(oldPath);
            FileInfo[] fileList = folder2.GetFiles();
            foreach (FileInfo file in fileList)
            {
                if (file.Extension == ".cpp")
                {
                    string temp = file.FullName;
                    Process.Start(temp);
                }
            }

            btn_open_link_Click(sender, e);

            btn_get_folder.Enabled = false;
            btn_transfer_folder.Enabled = true;
        }

        private void btn_transfer_folder_Click(object sender, EventArgs e)
        {
            string oldPath = txt_path_old_problems.Text + "\\" + txt_path_curr_folder.Text;
            string newPath = txt_path_new_problems.Text + "\\" + txt_in_titleE.Text;
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            Copy_File_AnswerReadme_md(oldPath, newPath);
            Create_File_Solution_cpp(newPath);
            Create_File_TestCases_txt(newPath);
            Modify_File_Define_IdName_h();
            Modify_File_Test_cpp();
            Modify_File_ProblemsetAll_Readme_md();
            Modify_File_Solutions_md();

            // 删除已经重新生成的
            System.IO.File.Delete(oldPath + "\\README.md");

            DirectoryInfo folder = new DirectoryInfo(oldPath);
            FileInfo[] fileList = folder.GetFiles();
            foreach (FileInfo file in fileList)
            {
                if (file.Extension == ".cpp")
                {
                    file.Delete();
                }
            }

            // 拷贝剩下的文件
            CopyDirectory(oldPath, newPath);

            // 删除原来文件夹
            if (Directory.Exists(oldPath))
            {
                Directory.Delete(oldPath, true);
            }

            btn_transfer_folder.Enabled = false;
            reset();
        }

        private void btn_open_link_Click(object sender, EventArgs e)
        {
            if (txt_in_link.Text == "") return;
            Process.Start(txt_in_link.Text);
        }

        private void btn_open_solution_link_Click(object sender, EventArgs e)
        {
            if (txt_in_solution_link.Text == "") return;
            Process.Start(txt_in_solution_link.Text);
        }

        private void txt_in_func_TextChanged(object sender, EventArgs e)
        {
            SplitFuncParam();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void btn_open_old_path_Click(object sender, EventArgs e)
        {
           Process.Start("explorer.exe", txt_path_old_problems.Text + "\\" + txt_path_curr_folder.Text);
        }

        private void btn_open_new_path_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", txt_path_new_problems.Text + "\\" + txt_path_prev_folder.Text);
        }

        private void btn_open_define_file_Click(object sender, EventArgs e)
        {
            Process.Start(txt_path_define_h.Text);
        }

        private void btn_open_problemset_file_Click(object sender, EventArgs e)
        {
            Process.Start(txt_path_problemset_all.Text);
        }

        private void btn_open_solutions_file_Click(object sender, EventArgs e)
        {
            Process.Start(txt_path_solutions_md.Text);
        }

        private void btn_open_test_cpp_Click(object sender, EventArgs e)
        {
            Process.Start(txt_path_test_cpp.Text);
        }

        private void btn_open_hold_path_Click(object sender, EventArgs e)
        {
            Process.Start(txt_path_hold_problems.Text);
        }

        private void btn_hold_Click(object sender, EventArgs e)
        {
            string oldPath = txt_path_old_problems.Text + "\\" + txt_path_curr_folder.Text;
            string newPath = txt_path_hold_problems.Text + "\\" + txt_path_curr_folder.Text;

            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            // 拷贝剩下的文件
            CopyDirectory(oldPath, newPath);

            // 删除原来文件夹
            if (Directory.Exists(oldPath))
            {
                Directory.Delete(oldPath, true);
            }

            reset();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            string newPath = txt_path_main.Text + @"\test\Test\";
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            Create_File_Solution_cpp(newPath);
            Create_File_TestCases_txt(newPath);
            //Modify_File_Define_IdName_h();
            //Modify_File_Test_cpp();
        }

    }
}
