﻿using System;
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
            txt_in_curr_folder.Text = "";
            txt_in_old_path.Text = @"C:\AhJo53589\leetcode-cn\leetcode-cn_2\_problems";
            txt_in_new_path.Text = @"C:\AhJo53589\leetcode-cn\leetcode-cn_2\problems";
            txt_in_define_file.Text = @"C:\AhJo53589\leetcode-cn\leetcode-cn_2\test\Common\Define_IdName.h";
            txt_in_problemset_file.Text = @"C:\AhJo53589\leetcode-cn\leetcode-cn_2\problemset\all\README.md";
            txt_in_test_cpp.Text = @"C:\AhJo53589\leetcode-cn\leetcode-cn_2\test\Test\Test.cpp";

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

            btn_get_first_folder.Enabled = true;
            btn_copy_folder.Enabled = true;
            btn_delete_folder.Enabled = true;
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

        private void Copy_AnswerFile(string oldPath, string newPath)
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
                        str = "# " + GenerateString_DiffTitleLink();
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

        private void Create_SolutionCppFile(string newPath)
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
            strText += "\treturn {\r\n";
            strText += "};\r\n";
            strText += "}\r\n\r\n";

            string strComment = "";
            if (txt_in_func_testcase.Text == "")
            {
                strComment = "//";
            }
            strText += strComment + "#define USE_GET_TEST_CASES_FILESTREAM" + "\r\n";
            strText += strComment + "string _get_test_cases_filestream()" + "\r\n";
            strText += strComment + "{\r\n";
            strText += strComment + "\treturn " + "\"..\\..\\problems\\" + txt_in_titleE.Text + "\\tests.txt\";" + "\r\n";
            strText += strComment + "}\r\n\r\n";

            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);

            Process.Start(strFile);
        }

        private void Create_TestCasesFile(string newPath)
        {
            if (txt_in_func_testcase.Text == "") return;

            string strFile = newPath + @"\tests.txt";

            string strText = txt_in_func_testcase.Text;

            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);

            //Process.Start(strFile);
        }


        private void Modify_DefineFile()
        {
            if (txt_in_define_file.Text == "") return;

            string strFile = txt_in_define_file.Text;

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

        private int Modify_ProblemsFile()
        {
            if (txt_in_problemset_file.Text == "") return 0;

            int iProblemsCount = 0;
            string strFile = txt_in_problemset_file.Text;

            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[problemset/all/README.md] file not exist!");
                return iProblemsCount;
            }

            string strInsert = GenerateString_InfoForm_Problem();
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

        private void Modify_TestFile()
        {
            if (txt_in_test_cpp.Text == "") return;

            string strFile = txt_in_test_cpp.Text;

            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Test.cpp] file not exist!");
                return;
            }

            string strOld = "#include SOLUTION_CPP_PATH_NAME(SOLUTION_CPP_FOLDER_NAME_ID_";
            string strInsert = strOld + txt_in_id.Text + ")";
            int iInsertNo = 0;
            int.TryParse(txt_in_id.Text, out iInsertNo);
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
            int id = 0;
            int.TryParse(s[2], out id);
            return id;
        }

        private string GenerateString_InfoForm_Problem()
        {
            // | √ | 1 | two-sum | [两数之和](../../problems/two-sum/README.md) | [查看](https://leetcode-cn.com/problems/two-sum/solution/liang-shu-zhi-he-by-leetcode-2/) | 简单 |
            // | √ | 1 | two-sum | [两数之和](../../problems/two-sum/README.md) |  | 简单 |
            string strText = "";
            strText += "| √";
            strText += " | " + txt_in_id.Text;
            strText += " | " + txt_in_titleE.Text;
            strText += " | " + GenerateString_TitleAndFileLink();
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

        private string GenerateString_TitleAndFileLink()
        {
            // example: 
            // [两数之和](../../problems/two-sum/README.md)
            string strText = "";
            strText += "[" + txt_in_id.Text + "." + txt_in_titleE.Text + " " + txt_in_titleC.Text + "]";
            strText += "(../../problems/" + txt_in_titleE.Text + "/README.md)";
            return strText;
        }

        private string GenerateString_DiffTitleLink()
        {
            // example: 
            // `（简单）` [1.two-sum 两数之和](https://leetcode-cn.com/problems/two-sum/)
            string strText = "";
            strText += "`（" + txt_in_difficult.Text + "）` ";
            strText += "[" + txt_in_id.Text + "." + txt_in_titleE.Text + " " + txt_in_titleC.Text + "]";
            strText += "(" + txt_in_link.Text + ")";
            return strText;
        }

        private void btn_get_first_folder_Click(object sender, EventArgs e)
        {
            DirectoryInfo folder = new DirectoryInfo(txt_in_old_path.Text);
            FileSystemInfo[] fileinfo = folder.GetFileSystemInfos();
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo)
                {
                    txt_in_curr_folder.Text = i.Name;
                    string first_path = txt_in_old_path.Text + "\\" + i.Name;
                    System.Diagnostics.Process.Start("explorer.exe", first_path);
                    Open_AnswerFile(first_path);
                    break;
                }
            }

            string oldPath = txt_in_old_path.Text + "\\" + txt_in_curr_folder.Text;
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

            btn_get_first_folder.Enabled = false;
        }

        private void btn_copy_folder_Click(object sender, EventArgs e)
        {
            string oldPath = txt_in_old_path.Text + "\\" + txt_in_curr_folder.Text;
            string newPath = txt_in_new_path.Text + "\\" + txt_in_titleE.Text;
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            Copy_AnswerFile(oldPath, newPath);
            Create_SolutionCppFile(newPath);
            Create_TestCasesFile(newPath);
            Modify_DefineFile();
            Modify_ProblemsFile();
            Modify_TestFile();

            System.Diagnostics.Process.Start("explorer.exe", newPath);
            btn_copy_folder.Enabled = false;
        }

        private void btn_delete_folder_Click(object sender, EventArgs e)
        {
            string oldPath = txt_in_old_path.Text + "\\" + txt_in_curr_folder.Text;
            string newPath = txt_in_new_path.Text + "\\" + txt_in_titleE.Text;

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

            CopyDirectory(oldPath, newPath);

            if (Directory.Exists(oldPath))
            {
                Directory.Delete(oldPath, true);
            }

            btn_delete_folder.Enabled = false;
            reset();
        }

        private void btn_func_split_Click(object sender, EventArgs e)
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
                if (s[1][0] == '*' || s[1][0] == '&')
                {
                    temp += s[1].Substring(1);
                }
                else
                {
                    temp += sValue[i] = s[1];
                }
                if (i != sValue.Length - 1) temp += ",";
            }
            temp += ")";
            txt_out_param_value.Text = temp;
        }

        private void btn_open_link_Click(object sender, EventArgs e)
        {
            if (txt_in_link.Text == "") return;
            System.Diagnostics.Process.Start(txt_in_link.Text);
        }

        private void btn_open_solution_link_Click(object sender, EventArgs e)
        {
            if (txt_in_solution_link.Text == "") return;
            System.Diagnostics.Process.Start(txt_in_solution_link.Text);
        }

        private void txt_in_func_TextChanged(object sender, EventArgs e)
        {
            btn_func_split_Click(sender, e);
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            reset();
        }
    }
}