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
            debug_config();
            Reset();
            Clear();
        }

        [Conditional("DEBUG")]
        private void debug_config()
        {
            // Test path
            txt_path_main.Text = @"C:\AhJo53589\leetcode-cn";

            lbl_new_cpp_lc_link.Visible = true;
            txt_new_cpp_lc_link.Visible = true;
            btn_new_cpp_lc_link.Visible = true;
        }

        private void Reset()
        {
            // Directory tab tabpage
            txt_path_problems.Text = txt_path_main.Text + @"\problems";
            txt_path_problems_test.Text = txt_path_main.Text + @"\problems_test";

            txt_path_readme_md.Text = txt_path_main.Text + @"\README.md";
            txt_path_update_md.Text = txt_path_main.Text + @"\Update.md";
            txt_path_problemset_all.Text = txt_path_main.Text + @"\problemset\all\README.md";
            txt_path_contest_md.Text = txt_path_main.Text + @"\Contest.md";
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
            txt_launcher_main_id.Text = "";
            txt_launcher_main_name.Text = "";
            txt_launcher_temp_dir.Text = "problems_test";
            txt_launcher_temp_name.Text = "0";
        }

        private void Clear_New_Cpp()
        {
            // New Cpp tabpage
            txt_new_cpp_dir_temp.Text = "problems_test";
            txt_new_cpp_id_temp.Text = "0";
            cb_new_cpp_custom.Checked = false;
            txt_new_cpp_lc_link.Text = "https://leetcode-cn.com/problems/two-sum";
            txt_new_cpp_in_func.Text = "";
            txt_new_cpp_in_func_testcase.Text = "";
        }

        private void Clear_Generate_MD()
        {
            // Generate MD tabpage
            rb_in_difficult_1.Checked = true;
            cb_in_finish.Checked = true;
            txt_in_link.Text = "";
            txt_in_contest.Text = "";
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

            btn_open_readme_md.BackColor = System.Drawing.Color.Transparent;
            btn_open_update_md.BackColor = System.Drawing.Color.Transparent;
            btn_open_problemset_all.BackColor = System.Drawing.Color.Transparent;
            btn_open_contest_md.BackColor = System.Drawing.Color.Transparent;
            btn_open_contest_problems.BackColor = System.Drawing.Color.Transparent;
            btn_open_solutions_md.BackColor = System.Drawing.Color.Transparent;
            btn_open_answer_readme_md.BackColor = System.Drawing.Color.Transparent;
            btn_open_solution_cpp.BackColor = System.Drawing.Color.Transparent;
            btn_open_tests_txt.BackColor = System.Drawing.Color.Transparent;
            btn_open_test_cpp.BackColor = System.Drawing.Color.Transparent;
            btn_open_define_h.BackColor = System.Drawing.Color.Transparent;
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        /// event
        ///////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////
        /// Launcher
        private void txt_launcher_id_TextChanged(object sender, EventArgs e)
        {
            Find_In_File_Define_IdName_h();
        }

        private void txt_launcher_main_name_TextChanged(object sender, EventArgs e)
        {
            txt_launcher_main_path.Text = "problems\\" + txt_launcher_main_name.Text;
        }

        private void btn_launcher_main_open_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", txt_launcher_main_path.Text);
            }
            catch (System.Exception ex)
            {

            }
        }

        private void btn_launcher_main_lc_open_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", txt_launcher_main_lc_path.Text);
            }
            catch (System.Exception ex)
            {

            }
        }

        private void btn_launcher_main_active_Click(object sender, EventArgs e)
        {
            try
            {
                Modify_File_Test_cpp(true, txt_launcher_main_id.Text);
            }
            catch
            {

            }
        }

        private void txt_launcher_temp_dir_TextChanged(object sender, EventArgs e)
        {
            txt_launcher_temp_path.Text = txt_launcher_temp_dir.Text + "\\" + txt_launcher_temp_name.Text;
        }

        private void btn_launcher_temp_open_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", txt_path_main.Text + "\\" + txt_launcher_temp_dir.Text + "\\" + txt_launcher_temp_name.Text);
        }

        private void btn_launcher_temp_active_Click(object sender, EventArgs e)
        {
            try
            {
                Modify_File_Test_cpp(false, txt_launcher_temp_name.Text, txt_launcher_temp_dir.Text);
            }
            catch
            {

            }
        }

        private void btn_launcher_test_sln_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", txt_path_main.Text + @"\test\Leetcode.sln");
            }
            catch (System.Exception ex)
            {

            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////
        /// New Cpp
        private void btn_new_cpp_clear_Click(object sender, EventArgs e)
        {
            Clear_New_Cpp();
        }

        private void btn_new_cpp_lc_link_Click(object sender, EventArgs e)
        {
            string strPage = GetPage_From_URL(txt_new_cpp_lc_link.Text);
            txt_new_cpp_in_func.Text = strPage;
            string strCode = GetCode_From_Page(strPage);
            txt_new_cpp_in_func_testcase.Text += strCode;
        }

        private void btn_new_cpp_create_Click(object sender, EventArgs e)
        {
            string newPath = txt_path_main.Text + "\\" + txt_new_cpp_dir_temp.Text + "\\" + txt_new_cpp_id_temp.Text;
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            Create_File_Solution_cpp(newPath, @"\SOLUTION.cpp", txt_new_cpp_in_func.Text, cb_new_cpp_custom.Checked);
            Create_File_TestCases_txt(newPath, @"\tests.txt", txt_new_cpp_in_func_testcase.Text);

            Modify_File_Test_cpp(false, txt_new_cpp_id_temp.Text, txt_new_cpp_dir_temp.Text);

            Process.Start("explorer.exe", newPath);
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        /// Generate MD     
        private void btn_generate_md_clear_Click(object sender, EventArgs e)
        {
            Clear_Generate_MD();
            Clear_Directory();
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

                    if (txt_in_contest.Text != "")
                    {
                        txt_path_contest_problems.Text = txt_path_main.Text + txt_in_contest.Text + @"\README.md";
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

        private void btn_Copy_SolutionLink_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GenerateString_SolutionLink());
        }

        private void btn_Copy_Description_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txt_in_description.Text);
        }

        private void btn_Copy_Answer_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GenerateString_Answer());
        }

        private void btn_Copy_Answer_2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GenerateString_Answer_Other());
        }

        private void btn_Generate_Click(object sender, EventArgs e)
        {
            // 集合文件
            int iProblemsCount = Modify_File_ProblemsetAll_Readme_md();
            Modify_File_Readme_md(iProblemsCount);
            Modify_File_Contest_md(txt_path_contest_md.Text);
            Modify_File_Update_md(txt_path_update_md.Text);
            Modify_File_Solutions_md(txt_path_solutions_md.Text);

            // 比赛目录
            if (txt_in_contest.Text != "")
            {
                if (!Directory.Exists(txt_path_main.Text + txt_in_contest.Text))
                {
                    Directory.CreateDirectory(txt_path_main.Text + txt_in_contest.Text);
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
            btn_open_solution_cpp.BackColor = System.Drawing.Color.Aqua;
            btn_open_tests_txt.BackColor = System.Drawing.Color.Aqua;

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
            try
            {
	            Process.Start("explorer.exe", txt_path_main.Text + "\\");
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_open_problems_dir_Click(object sender, EventArgs e)
        {
            try
            {
	            Process.Start("explorer.exe", txt_path_problems.Text + "\\");
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_open_problems_test_dir_Click(object sender, EventArgs e)
        {
            try
            {
	            Process.Start("explorer.exe", txt_path_problems_test.Text + "\\");
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_open_readme_md_Click(object sender, EventArgs e)
        {
            try
            {
	            Process.Start(txt_path_readme_md.Text);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_open_update_md_Click(object sender, EventArgs e)
        {
            try
            {
	            Process.Start(txt_path_update_md.Text);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_open_problemset_file_Click(object sender, EventArgs e)
        {
            try
            {
	            Process.Start(txt_path_problemset_all.Text);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_open_contest_md_Click(object sender, EventArgs e)
        {
            try
            {
	            Process.Start(txt_path_contest_md.Text);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_open_contest_problems_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(txt_path_contest_problems.Text);
            }
            catch (System.Exception ex)
            {

            }
        }

        private void btn_open_solutions_file_Click(object sender, EventArgs e)
        {
            try
            {
	            Process.Start(txt_path_solutions_md.Text);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_open_commit_bat_Click(object sender, EventArgs e)
        {
            try
            {
	            Process.Start(txt_path_readme_md.Text);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_open_answer_readme_md_Click(object sender, EventArgs e)
        {
            try
            {
	            Process.Start(txt_path_answer_readme_md.Text);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_open_solution_cpp_Click(object sender, EventArgs e)
        {
            try
            {
	            Process.Start(txt_path_solution_cpp.Text);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_open_tests_txt_Click(object sender, EventArgs e)
        {
            try
            {
	            Process.Start(txt_path_tests_txt.Text);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_open_test_cpp_Click(object sender, EventArgs e)
        {
            try
            {
	            Process.Start(txt_path_test_cpp.Text);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_open_define_file_Click(object sender, EventArgs e)
        {
            try
            {
	            Process.Start(txt_path_define_h.Text);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

    }
}
