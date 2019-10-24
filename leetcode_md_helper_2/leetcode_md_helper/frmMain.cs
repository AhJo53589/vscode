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
        private string m_strDifficult;
        private string m_strCodeSelect;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Test path
            txt_path_main.Text = @"C:\AhJo53589\leetcode-cn\leetcode-cn_2";
            //txt_path_main.Text = System.Windows.Forms.Application.StartupPath;

            Reset();
            m_strDifficult = rb_in_difficult_1.Text;
            m_strCodeSelect = rb_in_test_0.Text;
        }

        private void Reset()
        {
            txt_path_readme_md.Text = txt_path_main.Text + @"\README.md";
            txt_path_update_md.Text = txt_path_main.Text + @"\Update.md";
            txt_path_problemset_all.Text = txt_path_main.Text + @"\problemset\all\README.md";
            txt_path_solutions_md.Text = txt_path_main.Text + @"\Solutions.md";
            txt_path_commit_bat.Text = txt_path_main.Text + @"\git_commit.bat";
        }

        private void Clear()
        {
            rb_in_difficult_1.Checked = true;
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
            rb_in_test_1.Checked = true;

            txt_path_contest_problemset.Text = "";
            txt_path_answer_readme_md.Text = "";
            txt_path_solution_cpp.Text = "";
            txt_path_tests_txt.Text = "";

            lbl_out_readme_md.Visible = false;
            lbl_out_update_md.Visible = false;
            lbl_out_problemset_all.Visible = false;
            lbl_out_contest_problems.Visible = false;
            lbl_out_solutions_md.Visible = false;
            lbl_out_answer_readme_md.Visible = false;
            lbl_out_solution_cpp.Visible = false;
            lbl_out_tests_txt.Visible = false;
            lbl_out_commit_bat.Visible = false;

            btnGenerate.Enabled = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            // 是否是比赛
            if (txt_path_contest.Text != "")
            {
                if (!Directory.Exists(txt_path_main.Text + txt_path_contest.Text))
                {
                    Directory.CreateDirectory(txt_path_main.Text + txt_path_contest.Text);
                }
                if (!File.Exists(txt_path_contest_problemset.Text))
                {
                    Create_File_Contest_Problems_Readme_md();
                }
                else
                {
                    Modify_File_Contest_Problems_Readme_md();
                }
            }

            int iProblemsCount = Modify_File_ProblemsetAll_Readme_md();
            Modify_File_Readme_md(iProblemsCount);
            Modify_File_Update_md();
            Modify_File_Solutions_md();

            string newPath = txt_path_main.Text + @"/problems/" + txt_in_titleE.Text;
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            Create_File_Answer_Readme_md();
            Copy_File_Solution_cpp();       // TODO: Solution.cpp的 return "tests_1.txt"; 需要替换路径
            Copy_File_Tests_txt();

            Create_CommitFile();
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_in_id_titleC.Text != "" && txt_in_link.Text != "")
                {
                    SplitPathAndTitleE_From_Link();

                    if (txt_path_contest.Text != "")
                    {
                        txt_path_contest_problemset.Text = txt_path_main.Text + txt_path_contest.Text + @"\README.md";
                    }
                    else
                    {
                        txt_path_contest_problemset.Text = "";
                    }
                    txt_path_answer_readme_md.Text = txt_path_main.Text + @"/problems/" + txt_in_titleE.Text + @"\README.md";
                    txt_path_solution_cpp.Text = txt_path_main.Text + @"/problems/" + txt_in_titleE.Text + @"\SOLUTION.cpp";
                    txt_path_tests_txt.Text = txt_path_main.Text + @"/problems/" + txt_in_titleE.Text + @"\tests.txt";

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

        private void rb_in_difficult_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            m_strDifficult = rb.Text;
        }

        private void rb_in_test_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            m_strCodeSelect = rb.Text;
        }

        private void btnCopy_SolutionLink_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GenerateString_SolutionLink());
        }

        private void btnCopy_Answer_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GenerateString_Answer());
        }

        private void btnCopy_Answer_2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GenerateString_Answer_Other());
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
