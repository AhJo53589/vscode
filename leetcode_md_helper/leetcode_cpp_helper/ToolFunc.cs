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
        private void GetPathAndTitleE_FromLink()
        {
            string[] s = txtIn_IdTitleC.Text.Split('.');
            m_strId = s[0];
            m_strTitleC = s[1];
            while (m_strTitleC[0] == ' ') m_strTitleC = m_strTitleC.Substring(1);
            s = txtIn_Link.Text.Split('/');
            if (s[3] == "problems")
            {
                m_strPath = "";
                m_strTitleE = s[4];
            }
            else if (s[3] == "contest")
            {
                if (s[4] == "season")
                {
                    m_strPath = "/" + s[4] + "/" + s[5];
                    m_strTitleE = s[7];
                }
                else
                {
                    m_strPath = "/" + s[3] + "/" + s[4];
                    m_strTitleE = s[6];
                }
            }
        }

        private string GetDifficult_FromDirectoryString(string str)
        {
            // example: 
            // * `（简单）`  [1.TwoSum 两数之和](./problems/1.TwoSum/README.md)
            return str.Substring(2, 6);
        }

        private string GetIdTitleEC_FromDirectoryString(string str)
        {
            // example: 
            // * `（简单）`  [1.TwoSum 两数之和](./problems/1.TwoSum/README.md)
            string[] s = str.Split('[');
            s = s[1].Split(']');
            return s[0];
        }

        private string GetAnswerFilePath_FromDirectoryString(string str)
        {
            // example: 
            // * `（简单）`  [1.TwoSum 两数之和](./problems/1.TwoSum/README.md)
            string[] s = str.Split('(');
            s = s[1].Split(')');
            return s[0];
        }

        private int GetDirectoryNo_FromDirectoryString(string str)
        {
            // example: 
            // * `（简单）`  [1.TwoSum 两数之和](./problems/1.TwoSum/README.md)
            string[] s = str.Split('[');
            s = s[1].Split('.');
            int i;
            int.TryParse(s[0], out i);
            return i;
        }

        private string GenerateDirectoryString()
        {
            // example: 
            //* `（简单）`  [198.Rob 打家劫舍] (./problems/198.Rob/README.md)
            string strOutput = "* `（";
            strOutput += m_strDifficult;
            strOutput += "）`  [";   // 2个空格
            strOutput += txtIn_IdTitleE.Text;
            strOutput += " ";
            strOutput += m_strTitleC;
            strOutput += "](." + m_strPath + "/problems/";
            strOutput += txtIn_IdTitleE.Text;
            strOutput += "/README.md)";
            return strOutput;
        }

        private string GenerateDirectoryString_WithSelectedSolution()
        {
            // example: 
            //* `（简单）`  [198.Rob 打家劫舍] (./problems/198.Rob/README.md) | [发布的题解] (https://leetcode-cn.com/problems/house-robber/solution/da-jia-jie-she-by-ikaruga) | 
            string strOutput = GenerateDirectoryString();
            strOutput += " | [发布的题解](";
            strOutput += txtIn_SolutionLink.Text;
            strOutput += ") |";
            return strOutput;
        }

        private string GenerateString_Difficult_Link()
        {
            string strText = "";
            strText = "# `（";
            strText += m_strDifficult;
            strText += "）`  [";
            strText += txtIn_IdTitleE.Text;
            strText += " ";
            strText += m_strTitleC;
            strText += "](";
            strText += txtIn_Link.Text;
            strText += ")";
            strText += "\n\n";
            return strText;
        }

        private string GenerateString_SolutionLink()
        {
            string strText = "";
            if (txtIn_SolutionLink.Text != "")
            {
                strText += "[发布的题解](";
                strText += txtIn_SolutionLink.Text;
                strText += ")\n\n";
            }
            return strText;
        }

        private string GenerateString_Answer()
        {
            string strText = "";
            strText += "### 答题\n";
            strText += "``` C++\n";
            strText += txtIn_Answer.Text + "\n";
            strText += "```\n";
            strText += "\n";
            return strText;
        }

        private string GenerateString_Answer_2()
        {
            string strText = "";
            if (txtIn_Answer_2.Text != "")
            {
                strText += "### 其它\n";
                strText += "``` C++\n";
                strText += txtIn_Answer_2.Text + "\n";
                strText += "```\n";
                strText += "\n";
            }
            return strText;
        }
    }
}