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
using System.Text.RegularExpressions;
using System.Net;

namespace leetcode_cpp_helper
{
    public partial class frmMain : Form
    {
        private void DeEscape(ref string output)
        {
            output = output.Replace("&lt;", "<");
            output = output.Replace("&gt;", ">");
            output = output.Replace("&amp;", "&");
            output = output.Replace("&apos;", "\'");
            output = output.Replace("&quot;", "\"");
        }

        private void ConvertEnter(ref string output)
        {
            output = output.Replace("\n", strEnter);
        }

        private static void CopyDirectory(string srcPath, string destPath)
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
                MessageBox.Show(e.ToString());
            }
        }

        private void GetPage_From_URL(in string url)
        {
            browser.Load(url);
        }

        private string GetCode_From_Page(in string input)
        {
            string output = "";
            // get code
            // Sample: 
            // <input name="code" type="hidden" value="
            // class Solution ... "
            // ><input name="question"
            string pattern = "<input name=\"code\"[\\s\\S]+?><input name=\"question\"";
            //textBox1.Text = pattern + "\r\n";
            foreach (Match match in Regex.Matches(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline))
            {
                output = match.Value;
            }
            pattern = "class\\s[\\s\\S]+?><input";
            foreach (Match match in Regex.Matches(output, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline))
            {
                output = match.Value;
            }
            output = output.Replace("\"><input", "");
            DeEscape(ref output);
            ConvertEnter(ref output);

            txt_new_cpp_in_func.Text = output;

            ////////////////////////////////////////////
            pattern = "<input name=\"testcase\"[\\s\\S]+?><input name=\"lang\"";
            foreach (Match match in Regex.Matches(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline))
            {
                output = match.Value;
            }
            output = output.Replace("\"><input name=\"lang\"", "");
            output = output.Replace("<input name=\"testcase\" type=\"hidden\" value=\"", "");
            DeEscape(ref output);
            ConvertEnter(ref output);

            txt_new_cpp_in_func_testcase.Text = output;

            return output;
        }

        private void SplitFuncParamArg(in string input, out List<string> output)
        {
            output = new List<string>();

            // split param
            // Sample: 
            // (int k, vector<int>& nums)
            string[] s = input.Split('(');
            s = s[1].Split(')');

            if (s[0].Length == 0) return;

            // get arg
            // Sample: 
            // s[0] = int, s[1] = k
            // s[0] = vector<int>&, s[1] = nums
            string[] sValue = s[0].Split(',');
            for (int i = 0; i < sValue.Length; i++)
            {
                string temp = sValue[i].Trim();
                s = temp.Split(' ');
                output.Add(s[0]);
                output.Add(s[1]);
            }
        }

        private void SplitFunc_Normal(in string input, out List<string> output)
        {
            output = new List<string>();

            // remove space
            // Sample: 
            // vector<int> twoSum(vector<int> &nums, int target)
            input.Trim();

            // split return type
            // vector<int>
            string[] s = input.Split(' ');
            output.Add(s[0]);

            // split func name
            // twoSum
            s = s[1].Split('(');
            output.Add(s[0]);

            // split param
            // Sample: 
            // (int k, vector<int>& nums)
            // (k, nums)
            SplitFuncParamArg(input, out List<string> lsParamArg);
            output.AddRange(lsParamArg);
        }

        private void GetFunc_Constructor(in string input, out string strClassName, out List<string> output)
        {
            strClassName = "";
            output = new List<string>();

            // get class name
            // Sample: 
            // KthLargest
            string pattern = @"(?<=class\s).*\b";
            foreach (Match match in Regex.Matches(input, pattern))
            {
                strClassName = match.Value;
            }
            if (strClassName == "") return;

            // get constructor
            // Sample: 
            // KthLargest(int k, vector<int>& nums)
            string pattern_1 = @"[^~](" + strClassName + @"\((?:[_a-zA-Z][_a-zA-Z0-9<>*&]*\s[_a-zA-Z0-9<>*&]+(?:,\s*)?){0,}\))";
            foreach (Match match in Regex.Matches(input, pattern_1))
            {
                output.Add(match.Value);
            }
        }

        private void GetFunc_Normal(in string input, in string strClassName, out List<string> output)
        {
            output = new List<string>();

            // get func
            string pattern = @"[_a-zA-Z][_a-zA-Z0-9<>*&]*\s[_a-zA-Z][_a-zA-Z0-9]*\((?:[_a-zA-Z][_a-zA-Z0-9<>*&]*\s[_a-zA-Z0-9<>*&]+(?:,\s*)?){0,}\)";
            foreach (Match match in Regex.Matches(input, pattern))
            {
                if (new System.Text.RegularExpressions.Regex(@"\b" + strClassName + @"\b").IsMatch(match.Value)) continue;
                if (new System.Text.RegularExpressions.Regex(@"\bnew\b").IsMatch(match.Value)) continue;
                output.Add(match.Value);
            }
        }

        private void SplitPathAndTitleE_From_Link()
        {
            string[] s = txt_in_id_titleC.Text.Split('.');
            txt_in_id.Text = s[0];
            txt_generate_md_id.Text = txt_in_id.Text;

            txt_in_titleC.Text = s[1];
            while (txt_in_titleC.Text[0] == ' ') txt_in_titleC.Text = txt_in_titleC.Text.Substring(1);

            // Sample: 
            // https://leetcode-cn.com/contest/weekly-contest-159/problems/check-if-it-is-a-straight-line/
            // https://leetcode-cn.com/contest/season/2019-fall/problems/guess-numbers/
            s = txt_in_link.Text.Split('/');
            bool bContest = false;
            txt_in_contest.Text = "";
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
                    txt_in_contest.Text += "\\" + s[i];
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

        private string GenerateString_InfoForm_Problem(string path)
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
            strText += " | " + GenerateString_TitleAndFolderPath(path);
            strText += " | " + GenerateString_TitleAndFileLink(path);
            strText += " | " + GenerateString_CppFilePath(path);
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

            string[] s = txt_in_contest.Text.Split('\\');
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
            string[] s = txt_in_contest.Text.Split('\\');
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

        private string GenerateString_TitleAndFolderPath(string path)
        {
            // example: 
            // [two-sum](../../problems/two-sum)
            string strText = "";
            strText += "[" + txt_in_titleE.Text + "]";
            strText += "(" + path + txt_in_titleE.Text + ")";
            return strText;
        }

        private string GenerateString_TitleAndFileLink(string path)
        {
            // example: 
            // [两数之和](../../problems/two-sum/README.md)
            string strText = "";
            strText += "[" + txt_in_titleC.Text + "]";
            strText += "(" + path + txt_in_titleE.Text + "/README.md)";
            return strText;
        }

        private string GenerateString_CppFilePath(string path)
        {
            // example: 
            // [cpp](../../problems/two-sum/SOLUTION.cpp)
            string strText = "";
            strText += "[cpp]";
            strText += "(" + path + txt_in_titleE.Text + "/SOLUTION.cpp)";
            return strText;
        }

        private string GenerateString_DiffIdTitleECLink()
        {
            // https://leetcode-cn.com/contest/weekly-contest-159/problems/check-if-it-is-a-straight-line/
            // https://leetcode-cn.com/contest/season/2019-fall/problems/guess-numbers/
            string strLink = "";
            if (txt_in_contest.Text != "")
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

            if (txt_in_contest.Text != "")
            {
                strText += strEnter + strEnter;
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
            strText += "### 答题" + strEnter;
            strText += "``` C++" + strEnter;
            strText += txt_in_answer.Text + strEnter;
            strText += "```" + strEnter;
            return strText;
        }

        private string GenerateString_Answer_Other()
        {
            string strText = "";
            if (txt_in_answer_other.Text == "") return strText;

            strText += "### 其它" + strEnter;
            strText += "``` C++" + strEnter;
            strText += txt_in_answer_other.Text + strEnter;
            strText += "```" + strEnter;
            return strText;
        }
    }
}