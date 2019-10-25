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
        private void SplitPathAndTitleE_From_Link()
        {
            string[] s = txt_in_id_titleC.Text.Split('.');
            txt_in_id.Text = s[0];

            txt_in_titleC.Text = s[1];
            while (txt_in_titleC.Text[0] == ' ') txt_in_titleC.Text = txt_in_titleC.Text.Substring(1);


            // https://leetcode-cn.com/contest/weekly-contest-159/problems/check-if-it-is-a-straight-line/
            // https://leetcode-cn.com/contest/season/2019-fall/problems/guess-numbers/
            s = txt_in_link.Text.Split('/');
            bool bContest = false;
            txt_path_contest.Text = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == "problems" && i < s.Length - 1)
                {
                    txt_in_titleE.Text = s[i + 1];
                    break;
                }
                if (s[i] == "contest")
                {
                    bContest = true;
                }
                if (bContest)
                {
                    txt_path_contest.Text += "\\" + s[i];
                }
            }
        }

        private int GetId_From_InfoForm_Problem(string str)
        {
            string[] s = str.Split('|');
            int.TryParse(s[2], out int id);
            return id;
        }

        private int GetFinishStatus_From_InfoForm_Problem(string str)
        {
            if (str.IndexOf("√") == -1) return 0;
            return 1;
        }

        private string GenerateString_InfoForm_Problem()
        {
            // 有题解
            // | √ | 1 | [two-sum](../../problems/two-sum) | [两数之和](../../problems/two-sum/README.md) | [cpp](../../problems/two-sum/SOLUTION.cpp) | [查看](https://leetcode-cn.com/problems/two-sum/solution/liang-shu-zhi-he-by-leetcode-2/) | 简单 |
            // 无题解
            // | √ | 1 | [two-sum](../../problems/two-sum) | [两数之和](../../problems/two-sum/README.md) | [cpp](../../problems/two-sum/SOLUTION.cpp) |  | 简单 |
            string strText = "";
            strText += "| ";
            if (cb_in_finish.Checked == true)
            {
                strText += "√";
            }
            else
            {
                strText += " ";
            }
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
            strText += " | " + m_strDifficult;
            strText += " | ";
            return strText;
        }

        private string GenerateString_InfoForm_Contest()
        {
            //| 2019 / 9 / 29 | [第 156 场周赛](./contest/weekly-contest-156/README.md) | 3 / 4 | 192 / 1432 |
            string strText = "";
            strText += "| ";
            strText += " | " + "[" + GenerateString_InfoForm_Contest_Title() + "]";

            string[] s = txt_path_contest.Text.Split('\\');
            strText += "(./" + s[1] + "/" + s[2] + "/README.md)";

            strText += " | ";
            strText += " | ";
            strText += " | ";
            return strText;
        }

        private string GenerateString_InfoForm_Contest_Title()
        {
            // https://leetcode-cn.com/contest/weekly-contest-159/problems/check-if-it-is-a-straight-line/
            // https://leetcode-cn.com/contest/biweekly-contest-11/problems/missing-number-in-arithmetic-progression/
            // TODO: https://leetcode-cn.com/contest/season/2019-fall/problems/guess-numbers/

            string strText = "";
            string[] s = txt_path_contest.Text.Split('\\');
            s = s[2].Split('-');
            if (s[0] == "weekly")
            {
                strText += "第 " + s[2] + " 场周赛";
            }
            else if (s[0] == "biweekly")
            {
                strText += "第 " + s[2] + " 场双周赛";
            }
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
            // https://leetcode-cn.com/contest/weekly-contest-159/problems/check-if-it-is-a-straight-line/
            // https://leetcode-cn.com/contest/season/2019-fall/problems/guess-numbers/
            string strLink = "";
            if (txt_path_contest.Text != "")
            {
                bool bContest = false;
                string[] s = txt_in_link.Text.Split('/');
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == "contest")
                    {
                        bContest = true;
                    }
                    if (s[i] == "problems")
                    {
                        bContest = false;
                    }
                    if (!bContest && i < s.Length - 1)
                    {
                        strLink += s[i] + "/";
                    }
                }
            }
            else
            {
                strLink = txt_in_link.Text;
            }
            // example: 
            // `（简单）` [1.two-sum 两数之和](https://leetcode-cn.com/problems/two-sum/)
            string strText = "";
            strText += "`（" + m_strDifficult + "）` ";
            strText += "[" + txt_in_id.Text + "." + txt_in_titleE.Text + " " + txt_in_titleC.Text + "]";
            strText += "(" + strLink + ")";

            if (txt_path_contest.Text != "")
            {
                strText += "\r\n\r\n";
                strText += "[contest]";
                strText += "(" + txt_in_link.Text + ")";
            }
            return strText;
        }

        private string GenerateString_SolutionLink()
        {
            string strText = "";
            if (txt_in_solution_link.Text == "") return strText;

            strText += "[发布的题解]";
            strText += "(" + txt_in_solution_link.Text + ")";
            return strText;
        }

        private string GenerateString_Answer()
        {
            string strText = "";
            strText += "### 答题\r\n";
            strText += "``` C++\r\n";
            strText += txt_in_answer.Text + "\r\n";
            strText += "```\r\n";
            return strText;
        }

        private string GenerateString_Answer_Other()
        {
            string strText = "";
            if (txt_in_answer_other.Text == "") return strText;

            strText += "### 其它\r\n";
            strText += "``` C++\r\n";
            strText += txt_in_answer_other.Text + "\r\n";
            strText += "```\r\n";
            return strText;
        }
    }
}