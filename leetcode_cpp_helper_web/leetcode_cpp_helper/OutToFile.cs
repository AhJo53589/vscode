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

namespace leetcode_cpp_helper
{
    public partial class frmMain : Form
    {
        readonly string strComment = "//";
        readonly string strEnter = "\r\n";

        ///////////////////////////////////////////////////////////////////////////////////////
        /// Common
        private void Modify_File_Test_cpp(string strFile, string strId, bool useDefault, string strDir = "")
        {
            if (strFile == "") return;
            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Test.cpp] file not exist!");
                return;
            }

            string strUseDefault = "#define USE_DEFAULT_INCLUDE";
            string strOld = "#define SOLUTION_ID						SOLUTION_CPP_FOLDER_NAME_ID_";
            if (!useDefault)
            {
                strOld = "#define SOLUTION_CPP_FULL_PATH			\"../../" + strDir + "/";
            }
            string strInsert = strOld + strId;
            if (!useDefault)
            {
                strInsert = strOld + strId + "/SOLUTION.cpp\"";
            }
            string strText = "";

            FileStream fs = new FileStream(strFile, FileMode.Open);
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    if (str.IndexOf(strUseDefault) != -1)
                    {
                        strText += (useDefault) ? strUseDefault : strComment + strUseDefault;
                        strText += strEnter;
                        continue;
                    }
                    if (str.IndexOf(strOld) != -1)
                    {
                        strText += strInsert + strEnter;    // insert content here
                        continue;
                    }

                    // copy this line
                    strText += str + strEnter;
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);
            }
            btn_open_test_cpp.BackColor = System.Drawing.Color.Aqua;
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        /// Launcher
        private void Find_In_File_Define_IdName_h()
        {
            string strFile = txt_path_define_h.Text;
            if (strFile == "") return;

            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Define_IdName.h] file not exist!");
                return;
            }

            txt_launcher_main_name.Text = "";

            string strSearch = "#define SOLUTION_CPP_FOLDER_NAME_ID_" + txt_launcher_main_id.Text + " ";

            FileStream fs = new FileStream(strFile, FileMode.Open);
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    if (str.IndexOf(strSearch) != -1)
                    {
                        string[] s = str.Split('\t');
                        txt_launcher_main_name.Text = s[s.Length - 1];
                        break;
                    }
                }
                sr.Close();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        /// New Cpp
        private string GetCode_ParamArg_RemoveRef(string input)
        {
            if (input.Length == 0) return input;
            if (/*input[0] == '*' || */input[0] == '&')
            {
                return input.Substring(1);
            }
            if (/*input[input.Length - 1] == '*' || */input[input.Length - 1] == '&')
            {
                return input.Substring(0, input.Length - 1);
            }
            return input;
        }

        private string GetCode_ParamArg_FillArg(in List<string> input, int bg = 0)
        {
            string output = "";
            for (int i = bg; i < input.Count; i += 2)
            {
                output += GetCode_ParamArg_RemoveRef(input[i + 1]);
                if (i + 2 < input.Count)
                {
                    output += ", ";
                }
            }
            return output;
        }

        private string GetCode_Default_SolutionRunCode(in List<string> input)
        {
            string output = "";
            output += input[0];
            output += " " + "_solution_run" + "(";
            for (int i = 2; i < input.Count; i += 2)
            {
                output += input[i] + " " + input[i + 1];
                if (i + 2 < input.Count)
                {
                    output += ", ";
                }
            }
            output += ")";
            return output;
        }

        private string GetCode_Default_CallFunc(in string strClassName, in List<string> input)
        {
            string output = "";

            string strArg = "";
            for (int i = 2; i < input.Count; i += 2)
            {
                strArg += GetCode_ParamArg_RemoveRef(input[i + 1]);
                if (i + 2 < input.Count)
                {
                    strArg += ", ";
                }
            }

            if (strClassName == null || strClassName == "")
            {
                // Sample:
                // return twoSum(nums, target);
                output += "\treturn " + input[1] + "(" + strArg + ");" + strEnter;
            }
            else
            {
                // Sample:
                // Solution sln;
                // return sln.twoSum(nums, target);
                output += "\t" + strClassName + " " + "sln;" + strEnter;
                output += "\treturn sln." + input[1] + "(" + strArg + ");" + strEnter;
            }

            return output;
        }

        private string GetCode_Custom_CallConstructor(in string strTab, in string strClassName, in List<string> input)
        {
            // Sample:
            //if (sf[i] == "KthLargest")
            //{
            //    TestCases stc(sp[i]);
            //    int k = stc.get<int>();
            //    vector<int> nums = stc.get<vector<int>>();
            //    obj = new KthLargest(k, nums);
            //    ans.push_back("null");
            //}

            string output = "";
            output += strTab + "if (sf[i] == \"" + strClassName + "\")" + strEnter;
            output += strTab + "{" + strEnter;

            if (input.Count != 0)
            {
                output += strTab + "\t" + "TestCases stc(sp[i]);" + strEnter;

                for (int i = 0; i < input.Count; i += 2)
                {
                    output += strTab + "\t" + GetCode_ParamArg_RemoveRef(input[i]) + " " + GetCode_ParamArg_RemoveRef(input[i + 1])
                        + " = stc.get<" + GetCode_ParamArg_RemoveRef(input[i]) + ">();" + strEnter;
                }
            }
            output += strTab + "\t" + "obj = new " + strClassName + "(" + GetCode_ParamArg_FillArg(input, 0) + ");" + strEnter;
            output += strTab + "\t" + "ans.push_back(\"null\");" + strEnter;

            output += strTab + "}" + strEnter;

            return output;
        }

        private string GetCode_Custom_CallFunc(in string strTab, in List<string> input)
        {
            // Sample:
            //else if (sf[i] == "add")
            //{
            //    TestCases stc(sp[i]);
            //    int val = stc.get<int>();
            //    int r = obj->add(val);
            //    ans.push_back(convert<string>(r));
            //}

            string output = "";
            output += strTab + "else if (sf[i] == \"" + input[1] + "\")" + strEnter;
            output += strTab + "{" + strEnter;

            if (input.Count >= 2)
            {
                output += strTab + "\t" + "TestCases stc(sp[i]);" + strEnter;

                for (int i = 2; i < input.Count; i += 2)
                {
                    output += strTab + "\t" + GetCode_ParamArg_RemoveRef(input[i]) + " " + GetCode_ParamArg_RemoveRef(input[i + 1])
                        + " = stc.get<" + GetCode_ParamArg_RemoveRef(input[i]) + ">();" + strEnter;
                }
            }
            if (input[0] == "void")
            {
                output += strTab + "\t" + "obj->" + input[1] + "(" + GetCode_ParamArg_FillArg(input, 2) + ");" + strEnter;
                output += strTab + "\t" + "ans.push_back(\"null\");" + strEnter;
            }
            else
            {
                output += strTab + "\t" + input[0] + " " + "r = obj->" + input[1] + "(" + GetCode_ParamArg_FillArg(input, 2) + ");" + strEnter;
                output += strTab + "\t" + "ans.push_back(convert<string>(r));" + strEnter;
            }
            output += strTab + "}" + strEnter;

            return output;
        }

        private void Create_File_Solution_cpp(string newPath, string fileName, string strCode, bool isCustom = false)
        {
            if (strCode == null || strCode == "") return;

            string strFile = newPath + fileName;
            string strText = strEnter;

            // 答题代码
            strText += "//////////////////////////////////////////////////////////////////////////" + strEnter;
            strText += strCode + strEnter + strEnter;

            // 转接函数
            strText += "//////////////////////////////////////////////////////////////////////////" + strEnter;
            if (!isCustom)
            {
                // get code
                GetFunc_Constructor(strCode, out string strClassName, out List<string> lsConstructor);
                GetFunc_Normal(strCode, strClassName, out List<string> lsFunc);
                if (lsFunc.Count == 0) return;
                SplitFunc_Normal(lsFunc[0], out List<string> lsParamArg);

                // _solution_run
                // Sample:
                // vector<int> _solution_run(vector<int>&nums, int target)
                strText += GetCode_Default_SolutionRunCode(lsParamArg) + strEnter;

                strText += "{" + strEnter;
                {   // 选择执行单个测试用例的代码              
                    strText += "\t" + strComment + "int caseNo = -1;" + strEnter;
                    strText += "\t" + strComment + "static int caseCnt = 0;" + strEnter;
                    strText += "\t" + strComment + "if (caseNo != -1 && caseCnt++ != caseNo) return {};" + strEnter + strEnter;
                }
                // Sample:
                // Solution sln;
                // return sln.twoSum(nums, target);
                strText += GetCode_Default_CallFunc(strClassName, lsParamArg);
                strText += "}" + strEnter + strEnter;

                // _solution_custom
                strText += strComment + "#define USE_SOLUTION_CUSTOM" + strEnter;
                strText += strComment + "string _solution_custom(TestCases &tc)" + strEnter;
                strText += strComment + "{" + strEnter;
                strText += strComment + "\t" + "return {};" + strEnter;
                strText += strComment + "}" + strEnter + strEnter;
            }
            else
            {
                // get code
                GetFunc_Constructor(strCode, out string strClassName, out List<string> lsConstructor);
                if (lsConstructor.Count == 0) return;
                SplitFuncParamArg(lsConstructor[0], out List<string> lsParamArg_C);
                GetFunc_Normal(strCode, strClassName, out List<string> lsFunc);

                //string strReturnType = output[0];
                //string strFuncName = output[1];
                //string strParam = output[2];
                //string strArg = output[3];

                strText += strComment + "int _solution_run(int)" + strEnter;
                strText += strComment + "{" + strEnter;
                strText += strComment + "}" + strEnter + strEnter;

                strText += "#define USE_SOLUTION_CUSTOM" + strEnter;
                strText += "string _solution_custom(TestCases &tc)" + strEnter;
                strText += "{" + strEnter;
                strText += "\t" + "vector<string> sf = tc.get<vector<string>>();" + strEnter;
                strText += "\t" + "vector<string> sp = tc.get<vector<string>>();" + strEnter;
                strText += "\t" + "vector<string> ans;" + strEnter + strEnter;

                strText += "\t" + strClassName + " " + "*obj = nullptr;" + strEnter;
                strText += "\t" + "for (auto i = 0; i < sf.size(); i++)" + strEnter;
                strText += "\t" + "{" + strEnter;

                // Call constructor
                // Sample:
                // if (sf[i] == "KthLargest") { ... }
                strText += GetCode_Custom_CallConstructor("\t\t", strClassName, lsParamArg_C);

                // Call func
                // Sample:
                // else if (sf[i] == "add") { ... }
                foreach (string strFunc in lsFunc)
                {
                    SplitFunc_Normal(strFunc, out List<string> lsParamArg);
                    strText += GetCode_Custom_CallFunc("\t\t", lsParamArg);
                }

                strText += "\t" + "}" + strEnter;
                strText += "\t" + "delete obj;" + strEnter + strEnter;

                strText += "\t" + "return convert<string>(ans);" + strEnter;
                strText += "}" + strEnter + strEnter;
            }

            // 测试用例
            strText += "//////////////////////////////////////////////////////////////////////////" + strEnter;
            strText += strComment + "#define USE_GET_TEST_CASES_IN_CPP" + strEnter;
            strText += strComment + "vector<string> _get_test_cases_string()" + strEnter;
            strText += strComment + "{" + strEnter;
            strText += strComment + "\t" + "return {};" + strEnter;
            strText += strComment + "}" + strEnter;

            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);
        }

        private void Create_File_TestCases_txt(string newPath, string fileName, string strText)
        {
            string strFile = newPath + fileName;

            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        /// Generate MD
        private int Modify_File_ProblemsetAll_Readme_md(string strFile, string strId, string strPath, string strDifficult, bool bFinish, string strSolutionLink)
        {
            if (strFile == "") return 0;
            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[problemset/all/README.md] file not exist!");
                return 0;
            }

            int iProblemsCount = 0;
            string strInsert = GenerateString_InfoForm_Problem(strPath, strId, strDifficult, bFinish, strSolutionLink);
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
                        // ## All
                        if (str.IndexOf("## ") != -1) iMark = 20;  // find title
                    }
                    else if (iMark == 20 || iMark == 21)
                    {
                        if (str == "") continue;
                        if (str.IndexOf("|") != -1) iMark++;
                    }
                    else if (iMark == 22)
                    {
                        if (str == "") continue;
                        // ## Season/2019-fall
                        if (str.IndexOf("## ") != -1)
                        {
                            strText += strInsert + strEnter;    // insert content here
                            iMark = 29;  // iMakr == 29, insert completed
                        }
                        else
                        {
                            iProblemsCount += GetFinishStatus_From_InfoForm_Problem(str);
                            string strReadNo = GetId_From_InfoForm_Problem(str);
                            if (strReadNo.CompareTo(strId) != -1)
                            {
                                strText += strInsert + strEnter;    // insert content here
                                iMark = 29;  // iMakr == 29, insert completed
                            }
                            if (strReadNo.CompareTo(strId) == 0) continue;
                        }
                    }
                    else if (iMark == 29)
                    {
                        // TODO: iProblemsCount
                        if (str == "") continue;
                        iProblemsCount++;
                    }

                    // copy this line
                    strText += str + strEnter;
                }
                if (iMark == 22)
                {
                    strText += strInsert + strEnter;    // insert content here
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
            btn_open_problemset_all.BackColor = System.Drawing.Color.Aqua;

            return iProblemsCount + 1;
        }

        private void Modify_File_Readme_md(string strFile, string strId, int iProblemsCount, string strPath, string strDifficult, bool bFinish, string strSolutionLink)
        {
            if (strFile == "") return;
            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[README.md] file not exist!");
                return;
            }

            string strInsert_SelectedSolution = GenerateString_InfoForm_Problem(strPath, strId, strDifficult, bFinish, strSolutionLink);
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
                        // # leetcode-cn
                        if (str.IndexOf("# ") != -1) iMark = 1;  // find title
                    }
                    else if (iMark == 1)
                    {
                        // copy solution to Selected Solutions
                        //if (str == "## Selected Solutions") iMark = 10;  // find title
                        if (str == "## Problemset / All") iMark = 20;  // find title
                    }
                    // disable [Selected Solutions] function
                    //else if (iMark == 10 || iMark == 11)
                    //{
                    //    if (str == "") continue;
                    //    if (str.IndexOf("|") != -1) iMark++;
                    //}
                    //else if (iMark == 12)
                    //{
                    //    if (txt_in_solution_link.Text == "")
                    //    {
                    //        iMark = 19;
                    //    }
                    //    else
                    //    {
                    //        if (str.IndexOf("|") != -1)
                    //        {
                    //            int iReadNo = GetId_From_InfoForm_Problem(str);
                    //            if (iReadNo >= iInsertNo)
                    //            {
                    //                strText += strInsert_SelectedSolution + strEnter;    // insert content here
                    //                iMark = 19;  // iMakr == 19, insert completed
                    //            }
                    //            if (iReadNo == iInsertNo) continue;
                    //        }
                    //        else
                    //        {
                    //            strText += strInsert_SelectedSolution + strEnter;    // insert content here
                    //            iMark = 19;  // find title
                    //        }
                    //    }
                    //}
                    //else if (iMark == 19)
                    //{
                    //    if (str == "## Problemset / All") iMark = 20;  // find title
                    //}
                    else if (iMark == 20)
                    {
                        string[] s1 = str.Split('（');
                        string[] s2 = str.Split('/');
                        str = s1[0] + "（";
                        str += iProblemsCount.ToString();
                        str += " /";
                        str += s2[1];

                        iMark = 100;
                    }

                    // copy this line
                    strText += str + strEnter;
                }
                if (iMark != 100)
                {
                    MessageBox.Show(@"[README.md] insert failed!");
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);
            }
            btn_open_readme_md.BackColor = System.Drawing.Color.Aqua;
        }

        private void Modify_File_Contest_md(string strFile)
        {
            if (strFile == "") return;
            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Contest.md] file not exist!");
                return;
            }

            if (txt_in_contest.Text != "" &&   
                !Directory.Exists(txt_path_main.Text + txt_in_contest.Text))
            {
                string strInsert_Contest = GenerateString_InfoForm_Contest() + strEnter;

                File.AppendAllText(strFile, strEnter + strInsert_Contest);
            }

            btn_open_contest_md.BackColor = System.Drawing.Color.Aqua;
        }

        private void Modify_File_Update_md(string strFile)
        {
            if (strFile == "") return;
            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Update.md] file not exist!");
                return;
            }

            // example: 
            // * 133.CloneGraph 克隆图
            string strInsert = "* " + txt_in_id.Text + "." + txt_in_titleE.Text + " " + txt_in_titleC.Text + strEnter;
            string strText = "";
            string strDate = "## " + DateTime.Now.ToString("yyyyMMdd");
            int iMark = 0;

            FileStream fs = new FileStream(strFile, FileMode.Open);
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    if (iMark == 2)
                    {
                        strText += strInsert + strEnter;
                        iMark = 3;  // iMakr == 3, insert completed
                    }
                    if (iMark == 1)
                    {
                        if (str == strDate)
                        {
                            iMark = 2;  // find today, insert content next line
                        }
                        else
                        {
                            // insert new date and content
                            strText += strDate + strEnter + strInsert + strEnter;
                            strText += strEnter;
                            strText += "---" + strEnter;
                            iMark = 3;  // iMakr == 3, insert completed
                        }

                    }
                    if (iMark == 0 && str == "---") iMark = 1;  // iMark == 1, find first of "---'

                    // copy this line
                    strText += str + strEnter;
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);

                btn_open_update_md.BackColor = System.Drawing.Color.Aqua;
            }
        }

        private void Modify_File_Solutions_md(string strFile, string strId, string strPath, string strDifficult, bool bFinish, string strSolutionLink)
        {
            if (txt_in_solution_link.Text == "") return;
            if (strFile == "") return;
            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Solutions.md] file not exist!");
                return;
            }

            string strInsert_SelectedSolution = GenerateString_InfoForm_Problem(strPath, strId, strDifficult, bFinish, strSolutionLink);
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
                        if (str.IndexOf("|") != -1) iMark++;
                    }
                    else if (iMark == 12)
                    {
                        if (str == "") continue;
                        string strReadNo = GetId_From_InfoForm_Problem(str);
                        if (strReadNo.CompareTo(strId) != -1)
                        {
                            strText += strInsert_SelectedSolution + strEnter;    // insert content here
                            iMark = 20;  // iMakr == 20, insert completed
                        }
                        if (strReadNo.CompareTo(strId) == 0) continue;
                    }
                    // copy this line
                    strText += str + strEnter;
                }
                if (iMark == 12)
                {
                    strText += strInsert_SelectedSolution + strEnter;    // insert content here
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
            btn_open_solutions_md.BackColor = System.Drawing.Color.Aqua;
        }

        private void Create_File_Contest_Problems_Readme_md(string strId, string strPath, string strDifficult, bool bFinish, string strSolutionLink)
        {
            string strFile = txt_path_contest_problems.Text;
            if (strFile == "") return;

            string strText = "";

            strText += "# " + GenerateString_InfoForm_Contest_Title() + strEnter + strEnter;
            strText += "[返回](../../README.md)" + strEnter + strEnter;

            strText += "## Problems & Solutions" + strEnter + strEnter;

            strText += "|     | #   | 名称                 | 题目                  | 答题          | 题解 | 难度 |" + strEnter;
            strText += "| --- | --- | -------------------- | --------------------- | ------------- | ---- | ---- |" + strEnter;

            strText += GenerateString_InfoForm_Problem(strPath, strId, strDifficult, bFinish, strSolutionLink) + strEnter;

            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);

            btn_open_contest_problems.BackColor = System.Drawing.Color.Aqua;
        }

        private void Modify_File_Contest_Problems_Readme_md(string strFile, string strId, string strPath, string strDifficult, bool bFinish, string strSolutionLink)
        {
            if (strFile == "") return;
            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[contest/../README.md] file not exist!");
                return;
            }

            string strInsert = GenerateString_InfoForm_Problem(strPath, strId, strDifficult, bFinish, strSolutionLink);
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
                        if (str == "## Problems & Solutions") iMark = 20;  // find title
                    }
                    else if (iMark == 20 || iMark == 21)
                    {
                        if (str == "") continue;
                        if (str.IndexOf("|") != -1) iMark++;
                    }
                    else if (iMark == 22)
                    {
                        if (str == "") continue;
                        string strReadNo = GetId_From_InfoForm_Problem(str);
                        if (strReadNo.CompareTo(strId) != -1)
                        {
                            strText += strInsert + strEnter;    // insert content here
                            iMark = 29;  // iMakr == 29, insert completed
                        }
                        if (strReadNo.CompareTo(strId) == 0) continue;
                    }
                    else if (iMark == 29)
                    {
                        if (str == "") continue;
                    }

                    // copy this line
                    strText += str + strEnter;
                }
                if (iMark == 22)
                {
                    strText += strInsert + strEnter;    // insert content here
                    iMark = 29;  // iMakr == 29, insert completed
                }
                if (iMark != 29)
                {
                    MessageBox.Show(@"[contest/../README.md] insert failed!");
                }
                sr.Close();

                UTF8Encoding utf8 = new UTF8Encoding(false);
                File.WriteAllText(strFile, strText, utf8);
            }
            btn_open_contest_problems.BackColor = System.Drawing.Color.Aqua;
        }

        private void Create_File_Answer_Readme_md(string strFile)
        {
            if (strFile == "") return;

            // example: 
            //# `（简单）`  [48.Rotate 旋转图像](https://leetcode-cn.com/problems/rotate-image/)
            string strText;
            strText = "# " + GenerateString_DiffIdTitleECLink() + strEnter + strEnter;

            // ### 题目描述
            strText += "### 题目描述" + strEnter;
            strText += txt_in_description.Text;
            strText += strEnter + strEnter;

            // ---
            strText += "---" + strEnter;

            // ### 思路
            strText += "### 思路" + strEnter;
            strText += "```" + strEnter + "```" + strEnter;
            strText += strEnter;

            // 题解链接
            strText += GenerateString_SolutionLink() + strEnter + strEnter;

            // ### 答题
            strText += GenerateString_Answer() + strEnter + strEnter;

            // ### 其它
            strText += GenerateString_Answer_Other() + strEnter + strEnter;

            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);
            btn_open_answer_readme_md.BackColor = System.Drawing.Color.Aqua;
        }

        private void Modify_File_Define_IdName_h(string strFile, string strId)
        {
            if (strFile == "") return;
            if (!File.Exists(strFile))
            {
                MessageBox.Show(@"[Define_IdName.h] file not exist!");
                return;
            }

            string strInsert_SelectedSolution = "#define SOLUTION_CPP_FOLDER_NAME_ID_" + txt_in_id.Text + " \t" + txt_in_titleE.Text;
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

                        string[] s = str.Split(' ');
                        s = s[1].Split('_');
                        string strReadNo = s[5];
                        if (strReadNo.CompareTo(strId) != -1)
                        {
                            strText += strInsert_SelectedSolution + strEnter;    // insert content here
                            iMark = 20;  // iMakr == 20, insert completed
                        }
                        if (strReadNo.CompareTo(strId) == 0) continue;
                    }
                    // copy this line
                    strText += str + strEnter;
                }
                if (iMark == 1)
                {
                    strText += strInsert_SelectedSolution + strEnter;    // insert content here
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

            btn_open_define_h.BackColor = System.Drawing.Color.Aqua;
        }

        private void Create_CommitFile(string strText)
        {
            if (strText == "") return;
            strText = "git pull" + strEnter;
            strText += "git add -A" + strEnter;
            strText += "git commit -m";
            strText += "\"" + txt_in_id.Text + "." + txt_in_titleE.Text + "\"" + strEnter;
            strText += "git push" + strEnter;

            string strFile = txt_path_commit_bat.Text;
            UTF8Encoding utf8 = new UTF8Encoding(false);
            File.WriteAllText(strFile, strText, utf8);
            btn_open_commit_bat.Visible = true;
        }


    }
}