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
            txt_in_old_path.Text = @"C:\AhJo53589\leetcode-cn\leetcode-cn_2\_problems";
            txt_in_new_path.Text = @"C:\AhJo53589\leetcode-cn\leetcode-cn_2\problems";
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
                    OpenAnswerFile(first_path);
                    break;
                }
            }
        }

        private void btn_copy_folder_Click(object sender, EventArgs e)
        {
            string oldPath = txt_in_old_path.Text + "\\" + txt_in_curr_folder.Text;
            string newPath = txt_in_new_path.Text + "\\" + txt_in_title.Text;
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            Copy_AnswerFile(oldPath, newPath);
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

        private void OpenAnswerFile(string path)
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
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    str = GetIdTitleEC_FromDirectoryString(str);
                    string[] s = str.Split('.');
                    txt_in_id.Text = s[0];
                    s = s[1].Split(' ');
                    txt_in_title.Text = s[0];
                    break;
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
                        string[] s1 = str.Split('.');
                        string[] s2 = s1[1].Split(' ');
                        if (s2[0] != txt_in_title.Text)
                        {
                            str = s1[0] + "." + txt_in_title.Text + " " + s2[1];
                        }
                    }

                    // copy this line
                    strText += str + "\n";
                }
                sr.Close();

                strFile = newPath + @"\README.md";
                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);
            }

            Process.Start(strFile);
        }

        private void Create_cppFile(string oldPath, string newPath)
        {
            string strFile = newPath + "@SOLUTION.cpp";

            // 测试函数
            string strText = "//////////////////////////////////////////////////////////////////////////" + "\n";
            strText += txt_in_func.Text + "\n";
            strText += "{\n}\n\n";

            // 转接函数
            strText += "//////////////////////////////////////////////////////////////////////////" + "\n";
            strText += txt_out_return_type.Text + " " + "_solution_run" + txt_out_param.Text;
            strText += "{\n";
            strText += "\treturn " + txt_out_func_name.Text + txt_out_param_value.Text;
            strText += "}\n\n";

            strText += "//" + "#define USE_SOLUTION_CUSTOM" + "\n";
            strText += "//" + txt_out_return_type.Text + " " + "_solution_custom(TestCases &tc)" + "\n";
            strText += "//" + "{\n}\n\n";

            // 测试用例
            strText += "//////////////////////////////////////////////////////////////////////////" + "\n";
            strText += "vector<string> _get_test_cases_string()" + "\n";
            strText += "{\n";
            strText += "\treturn {};\n";
            strText += "}\n\n";

            strText += "//" + "#define USE_GET_TEST_CASES_FILESTREAM" + "\n";
            strText += "//" + "string _get_test_cases_filestream()" + "\n";
            strText += "//" + "{\n";
            strText += "//" + "\treturn " + "\"../../problems/" + txt_in_title.Text + "/tests.txt\";\n";
            strText += "//" + "}\n\n";



            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);


            Process.Start(strFile);
        }

        private string GetIdTitleEC_FromDirectoryString(string str)
        {
            // example: 
            // * `（简单）`  [1.TwoSum 两数之和](./problems/1.TwoSum/README.md)
            string[] s = str.Split('[');
            s = s[1].Split(']');
            return s[0];
        }

    }
}
