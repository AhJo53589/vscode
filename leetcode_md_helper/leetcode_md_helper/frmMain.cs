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
        private string m_strId;
        private string m_strPath;
        private string m_strTitleE;
        private string m_strTitleC;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Test path
            //txt_FilePath.Text = @"C:/AhJo53589/leetcode-cn";
            txt_FilePath.Text = System.Windows.Forms.Application.StartupPath;

            txtOut_ReadmeFilePath.Text = txt_FilePath.Text + @"/README.md";
            txtOut_ProblemsFilePath.Text = txt_FilePath.Text + @"/Problems.md";
            txtOut_SolutionsFilePath.Text = txt_FilePath.Text + @"/Solutions.md";
            txtOut_UpdateFilePath.Text = txt_FilePath.Text + @"/Update.md";
            txtOut_CommitFilePath.Text = txt_FilePath.Text + @"/git_commit.bat";

            m_strDifficult = "简单";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtIn_IdTitleC.Text = "";
            txtIn_IdTitleE.Text = "";
            txtIn_Link.Text = "";
            txtIn_Description.Text = "";
            txtIn_SolutionLink.Text = "";
            txtIn_Answer.Text = "";
            txtOut_AnswerFilePath.Text = "";
            lblOut_Readme.Visible = false;
            lblOut_Problems.Visible = false;
            lblOut_Solutions.Visible = false;
            lblOut_Update.Visible = false;
            lblOut_Answer.Visible = false;
            lblOut_Commit.Visible = false;
            btnGenerate.Enabled = false;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            txtTitle_TextChanged(sender, e);
            if (m_strPath == "")
            {
                int iProblemsCount = Modify_ProblemsFile();
                Modify_ReadmeFile(iProblemsCount);
            }
            else
            {
                Modify_ProblemsFile();
            }
            Modify_UpdateFile();
            Create_AnswerFile();
            Modify_SolutionsFile();
            Create_CommitFile();
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            if (txtIn_IdTitleC.Text != "" && txtIn_Link.Text != "")
            {
                GetPathAndTitleE_FromLink();
                string strIdTitleE = m_strId + "." + m_strTitleE;

                string str = txt_FilePath.Text + m_strPath;
                str += @"/problems/" + strIdTitleE + @"/README.md";

                if (m_strPath != "")
                {
                    txtOut_ProblemsFilePath.Text = txt_FilePath.Text + m_strPath + @"/README.md";
                }

                txtIn_IdTitleE.Text = strIdTitleE;
                Clipboard.SetText(strIdTitleE);
                txtOut_AnswerFilePath.Text = str;

                btnGenerate.Enabled = true;
            }
            else
            {
                txtIn_IdTitleE.Text = "";
                btnGenerate.Enabled = false;
            }
        }

        private void btnFixFile_Click(object sender, EventArgs e)
        {
            //string strFile = txtOut_ProblemsFilePath.Text;

            //if (!File.Exists(strFile))
            //{
            //    MessageBox.Show(@"[README.md] file not exist!");
            //    return;
            //}

            //string strText = "";
            //int iMark = 0;

            //FileStream fs = new FileStream(strFile, FileMode.Open);
            //using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            //{
            //    while (!sr.EndOfStream)
            //    {
            //        string str = sr.ReadLine();
            //        if (iMark == 0)
            //        {
            //            if (str == "# leetcode-cn") iMark = 1;  // find title
            //        }
            //        else if (iMark == 1)
            //        {
            //            if (str == "## Problems & Solutions") iMark = 20;  // find title
            //        }
            //        else if (iMark == 20)
            //        {
            //            if (str == "") continue;
            //            string strDifficult = GetDifficult_FromDirectoryString(str);
            //            string strIdTitle = GetIdTitleEC_FromDirectoryString(str);
            //            string strAnswer = GetAnswerFilePath_FromDirectoryString(str);

            //            str = "* ";
            //            str += strDifficult;
            //            str += " ";
            //            str += strIdTitle;
            //            str += " | [详情](";
            //            str += strAnswer;
            //            str += ")";
            //        }

            //        // copy this line
            //        strText += str + "\n";
            //    }
            //    sr.Close();

            //    UTF8Encoding utf8 = new UTF8Encoding(false);
            //    File.WriteAllText(strFile, strText, utf8);
            //}
            //lblOut_Readme.Visible = true;
        }

        private void rbIn_Difficult_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            m_strDifficult = rb.Text;
        }

        private void btnCopy_Link_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GenerateDirectoryString_WithSelectedSolution());
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
            Clipboard.SetText(GenerateString_Answer_2());
        }

        private void btnCopy_ID_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtIn_IdTitleE.Text);
        }

    }
}
