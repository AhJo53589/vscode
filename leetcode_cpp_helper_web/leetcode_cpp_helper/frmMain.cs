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
using CefSharp;
using CefSharp.OffScreen;

namespace leetcode_cpp_helper
{
    public partial class frmMain : Form
    {
        private string m_strDifficult;
        public ChromiumWebBrowser browser;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            DirectoryInfo pathInfo = new DirectoryInfo(System.Windows.Forms.Application.StartupPath);
            txt_path_main.Text = pathInfo.Parent.FullName;
            
            debug_config();
            release_cpponly_config();
            release_leetcodeweb_config();

            Reset();
            Clear();
            InitBrowser();

            debug_init();
        }

        [Conditional("DEBUG")]
        private void debug_config()
        {
            // Test path
            txt_path_main.Text = @"C:\AhJo53589\leetcode-cn";
            //txt_path_main.Text = @"C:\AhJo53589\nowcoder";
        }

        [Conditional("DEBUG")]
        private void debug_init()
        {
            //txt_launcher_lc_path_download.Text = "https://leetcode-cn.com/problems/kth-largest-element-in-a-stream/";
            txt_new_cpp_in_func.Text = "string run(stringstream &ss) {}";
            txt_new_cpp_in_func_testcase.Text = "-5 -4 0 1 2 3 4 13 14 15 17 20 22 99";
        }

        [Conditional("CPPONLY")]
        private void release_cpponly_config()
        {
            lbl_launcher_main_id.Visible = false;
            txt_launcher_main_id.Visible = false;
            txt_launcher_main_name.Visible = false;
            txt_launcher_main_path.Visible = false;
            btn_launcher_main_open.Visible = false;
            txt_launcher_main_lc_path.Visible = false;
            btn_launcher_main_lc_open.Visible = false;
            btn_launcher_main_load.Visible = false;

            tabControl1.TabPages.Remove(tabPage3);
            tabControl1.TabPages.Remove(tabPage4);
        }

        [Conditional("LCWEB")]
        private void release_leetcodeweb_config()
        {
            lbl_launcher_lc_path_download.Visible = true;
            txt_launcher_lc_path_download.Visible = true;
            btn_launcher_lc_path_download.Visible = true;
        }

        [Conditional("LCWEB")]
        private void InitBrowser()
        {
            Cef.Initialize(new CefSettings());
            browser = new ChromiumWebBrowser("");
            browser.FrameLoadEnd += WebBrowser_FrameLoadEnd;    //加载完成
        }

        private void WebBrowser_FrameLoadEnd(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                String html = browser.GetSourceAsync().Result;
                txt_new_cpp_in_func.Text = GetCode_From_Page(html);
                txt_generate_md_answer.Text = txt_new_cpp_in_func.Text;
                txt_new_cpp_in_func_testcase.Text = GetTestcase_From_Page(html);
                string diff = GetDifficult_From_Page(html);
                rb_generate_md__difficult_1.Checked = (diff == "easy");
                rb_generate_md_difficult_2.Checked = (diff == "medium");
                rb_generate_md_difficult_3.Checked = (diff == "hard");
                txt_generate_md_link.Text = txt_launcher_lc_path_download.Text;
                txt_launcher_lc_path_download.Text = "";
                txt_generate_md_id_titleC.Text = GetIdTitleC_From_Page(html);
                txt_new_cpp_id_temp.Text = txt_generate_md_split_id.Text;
                txt_generate_md_description.Text = GetDescription_From_Page(html);
                lbl_launcher_lc_path_download_hint.Visible = false;
            }));
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
            txt_new_cpp_in_func.Text = "";
            txt_new_cpp_in_func_testcase.Text = "";
        }

        private void Clear_Generate_MD()
        {
            // Generate MD tabpage
            rb_generate_md__difficult_1.Checked = true;
            cb_in_finish.Checked = true;
            txt_generate_md_link.Text = "";
            txt_generate_md_contest.Text = "";
            txt_generate_md_id_titleC.Text = "";
            txt_generate_md_split_id.Text = "";
            txt_generate_md_split_titleE.Text = "";
            txt_generate_md_split_titleC.Text = "";
            txt_generate_md_solution_link.Text = "";
            txt_generate_md_description.Text = "";
            txt_generate_md_answer.Text = "";
            txt_generate_md_answer_other.Text = "";
            txt_generate_md_source_dir.Text = "problems_test";
            cb_in_skip_md.Checked = false;
            txt_generate_md_source_id.Text = "0";

            m_strDifficult = rb_generate_md__difficult_1.Text;
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
        private void txt_launcher_main_id_TextChanged(object sender, EventArgs e)
        {
            Find_In_File_Define_IdName_h();
        }

        private void txt_launcher_main_name_TextChanged(object sender, EventArgs e)
        {
            txt_launcher_main_path.Text = txt_path_problems.Text + "\\" + txt_launcher_main_name.Text;
            txt_launcher_main_lc_path.Text = "https://leetcode-cn.com/problems/" + txt_launcher_main_name.Text;
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

        private void btn_launcher_main_load_Click(object sender, EventArgs e)
        {
            try
            {
                Modify_File_Test_cpp(txt_path_test_cpp.Text, txt_launcher_main_id.Text, true);
            }
            catch
            {

            }
        }

        private void txt_launcher_temp_dir_TextChanged(object sender, EventArgs e)
        {
            txt_launcher_temp_path.Text = txt_path_problems_test.Text + "\\" + txt_launcher_temp_name.Text;
        }

        private void btn_launcher_temp_open_Click(object sender, EventArgs e)
        {
            try
            {
	            Process.Start("explorer.exe", txt_path_main.Text + "\\" + txt_launcher_temp_dir.Text + "\\" + txt_launcher_temp_name.Text);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_launcher_temp_load_Click(object sender, EventArgs e)
        {
            try
            {
                Modify_File_Test_cpp(txt_path_test_cpp.Text, txt_launcher_temp_name.Text, false, txt_launcher_temp_dir.Text);
            }
            catch
            {

            }
        }

        private void btn_launcher_lc_path_download_Click(object sender, EventArgs e)
        {
            GetPage_From_URL(txt_launcher_lc_path_download.Text);
            lbl_launcher_lc_path_download_hint.Visible = true;
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

        private void btn_new_cpp_create_Click(object sender, EventArgs e)
        {
            string newPath = txt_path_main.Text + "\\" + txt_new_cpp_dir_temp.Text + "\\" + txt_new_cpp_id_temp.Text;
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            Create_File_Solution_cpp(newPath, @"\SOLUTION.cpp", txt_new_cpp_in_func.Text, cb_new_cpp_custom.Checked);
            Create_File_TestCases_txt(newPath, @"\tests.txt", txt_new_cpp_in_func_testcase.Text);

            Modify_File_Test_cpp(txt_path_test_cpp.Text, txt_new_cpp_id_temp.Text, false, txt_new_cpp_dir_temp.Text);

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
                if (txt_generate_md_id_titleC.Text != "" && txt_generate_md_link.Text != "")
                {
                    SplitPathAndTitleE_From_Link();

                    if (txt_generate_md_contest.Text != "")
                    {
                        txt_path_contest_problems.Text = txt_path_main.Text + txt_generate_md_contest.Text + @"\README.md";
                    }
                    else
                    {
                        txt_path_contest_problems.Text = "";
                    }
                }
            }
            catch
            {

            }
        }

        private void txt_in_splite_TextChanged(object sender, EventArgs e)
        {
            txt_path_answer_readme_md.Text = txt_path_main.Text + @"\problems\" + txt_generate_md_split_titleE.Text + @"\README.md";
            txt_path_solution_cpp.Text = txt_path_main.Text + @"\problems\" + txt_generate_md_split_titleE.Text + @"\SOLUTION.cpp";
            txt_path_tests_txt.Text = txt_path_main.Text + @"\problems\" + txt_generate_md_split_titleE.Text + @"\tests.txt";

            btnGenerate.Enabled = (txt_generate_md_split_id.Text != "" && txt_generate_md_split_titleE.Text != "" && txt_generate_md_split_titleC.Text != "");
        }

        private void btn_Copy_SolutionLink_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GenerateString_SolutionLink());
        }

        private void btn_Copy_Description_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txt_generate_md_description.Text);
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
            int iProblemsCount = Modify_File_ProblemsetAll_Readme_md(txt_path_problemset_all.Text, txt_generate_md_split_id.Text, "../../problems/", m_strDifficult, cb_in_finish.Checked, txt_generate_md_solution_link.Text);
            Modify_File_Readme_md(txt_path_readme_md.Text, txt_generate_md_split_id.Text, iProblemsCount, "./problems/", m_strDifficult, cb_in_finish.Checked, txt_generate_md_solution_link.Text);
            Modify_File_Update_md(txt_path_update_md.Text);
            Modify_File_Solutions_md(txt_path_solutions_md.Text, txt_generate_md_split_id.Text, "./problems/", m_strDifficult, cb_in_finish.Checked, txt_generate_md_solution_link.Text);

            // 比赛目录
            if (txt_generate_md_contest.Text != "")
            {
                Modify_File_Contest_md(txt_path_contest_md.Text);
                if (!Directory.Exists(txt_path_main.Text + txt_generate_md_contest.Text))
                {
                    Directory.CreateDirectory(txt_path_main.Text + txt_generate_md_contest.Text);
                }
                if (!File.Exists(txt_path_contest_problems.Text))
                {
                    Create_File_Contest_Problems_Readme_md(txt_generate_md_split_id.Text, "../../problems/", m_strDifficult, cb_in_finish.Checked, txt_generate_md_solution_link.Text);
                }
                else
                {
                    Modify_File_Contest_Problems_Readme_md(txt_path_contest_problems.Text, txt_generate_md_split_id.Text, "../../problems/", m_strDifficult, cb_in_finish.Checked, txt_generate_md_solution_link.Text);
                }
            }

            // 问题目录
            string oldPath = txt_path_main.Text + "\\" + txt_generate_md_source_dir.Text + "\\" + txt_generate_md_source_id.Text;
            string newPath = txt_path_problems.Text + "\\" + txt_generate_md_split_titleE.Text;
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
            Modify_File_Test_cpp(txt_path_test_cpp.Text, txt_generate_md_split_id.Text, true);
            Modify_File_Define_IdName_h(txt_path_define_h.Text, txt_generate_md_split_id.Text);

            // 提交文件
            Create_CommitFile(txt_path_commit_bat.Text);
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
