namespace leetcode_md_helper
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGenerate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.txtIn_SolutionLink = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtIn_Answer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIn_Description = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.cmbIn_Difficult = new System.Windows.Forms.ComboBox();
            this.txtIn_Link = new System.Windows.Forms.TextBox();
            this.txtIn_IdTitleC = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblOut_IdTitleE = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtIn_IdTitleE = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_FilePath = new System.Windows.Forms.TextBox();
            this.lblOut_Answer = new System.Windows.Forms.Label();
            this.lblOut_Update = new System.Windows.Forms.Label();
            this.lblOut_Readme = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOut_AnswerFilePath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOut_UpdateFilePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOut_ReadmeFilePath = new System.Windows.Forms.TextBox();
            this.btnFixFile = new System.Windows.Forms.Button();
            this.lblOut_Problems = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtOut_ProblemsFilePath = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnGenerate.Enabled = false;
            this.btnGenerate.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGenerate.Location = new System.Drawing.Point(12, 453);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(936, 128);
            this.btnGenerate.TabIndex = 13;
            this.btnGenerate.Text = "↓ GENERATE ↓";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnFixFile);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtIn_SolutionLink);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtIn_Answer);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtIn_Description);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.cmbIn_Difficult);
            this.panel1.Controls.Add(this.txtIn_Link);
            this.panel1.Controls.Add(this.txtIn_IdTitleC);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(934, 435);
            this.panel1.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(34, 262);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 18);
            this.label11.TabIndex = 36;
            this.label11.Text = "题解链接";
            // 
            // txtIn_SolutionLink
            // 
            this.txtIn_SolutionLink.Location = new System.Drawing.Point(123, 260);
            this.txtIn_SolutionLink.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIn_SolutionLink.Name = "txtIn_SolutionLink";
            this.txtIn_SolutionLink.Size = new System.Drawing.Size(744, 28);
            this.txtIn_SolutionLink.TabIndex = 35;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(70, 298);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 18);
            this.label9.TabIndex = 34;
            this.label9.Text = "答题";
            // 
            // txtIn_Answer
            // 
            this.txtIn_Answer.Location = new System.Drawing.Point(123, 296);
            this.txtIn_Answer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIn_Answer.Multiline = true;
            this.txtIn_Answer.Name = "txtIn_Answer";
            this.txtIn_Answer.Size = new System.Drawing.Size(744, 130);
            this.txtIn_Answer.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 29;
            this.label5.Text = "题目描述";
            // 
            // txtIn_Description
            // 
            this.txtIn_Description.Location = new System.Drawing.Point(123, 122);
            this.txtIn_Description.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIn_Description.Multiline = true;
            this.txtIn_Description.Name = "txtIn_Description";
            this.txtIn_Description.Size = new System.Drawing.Size(744, 130);
            this.txtIn_Description.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 27;
            this.label3.Text = "题目链接";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 18);
            this.label2.TabIndex = 26;
            this.label2.Text = "ID.中文标题";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 25;
            this.label1.Text = "难度";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(640, 12);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(228, 30);
            this.btnClear.TabIndex = 24;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cmbIn_Difficult
            // 
            this.cmbIn_Difficult.FormattingEnabled = true;
            this.cmbIn_Difficult.Items.AddRange(new object[] {
            "简单",
            "中等",
            "困难"});
            this.cmbIn_Difficult.Location = new System.Drawing.Point(124, 12);
            this.cmbIn_Difficult.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbIn_Difficult.Name = "cmbIn_Difficult";
            this.cmbIn_Difficult.Size = new System.Drawing.Size(132, 26);
            this.cmbIn_Difficult.TabIndex = 23;
            this.cmbIn_Difficult.Text = "简单";
            // 
            // txtIn_Link
            // 
            this.txtIn_Link.Location = new System.Drawing.Point(123, 45);
            this.txtIn_Link.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIn_Link.Name = "txtIn_Link";
            this.txtIn_Link.Size = new System.Drawing.Size(744, 28);
            this.txtIn_Link.TabIndex = 21;
            this.txtIn_Link.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // txtIn_IdTitleC
            // 
            this.txtIn_IdTitleC.Location = new System.Drawing.Point(123, 86);
            this.txtIn_IdTitleC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIn_IdTitleC.Name = "txtIn_IdTitleC";
            this.txtIn_IdTitleC.Size = new System.Drawing.Size(744, 28);
            this.txtIn_IdTitleC.TabIndex = 22;
            this.txtIn_IdTitleC.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.lblOut_Problems);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.txtOut_ProblemsFilePath);
            this.panel2.Controls.Add(this.lblOut_IdTitleE);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.txtIn_IdTitleE);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txt_FilePath);
            this.panel2.Controls.Add(this.lblOut_Answer);
            this.panel2.Controls.Add(this.lblOut_Update);
            this.panel2.Controls.Add(this.lblOut_Readme);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtOut_AnswerFilePath);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtOut_UpdateFilePath);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtOut_ReadmeFilePath);
            this.panel2.Location = new System.Drawing.Point(12, 588);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(934, 268);
            this.panel2.TabIndex = 22;
            // 
            // lblOut_IdTitleE
            // 
            this.lblOut_IdTitleE.AutoSize = true;
            this.lblOut_IdTitleE.Location = new System.Drawing.Point(874, 53);
            this.lblOut_IdTitleE.Name = "lblOut_IdTitleE";
            this.lblOut_IdTitleE.Size = new System.Drawing.Size(62, 18);
            this.lblOut_IdTitleE.TabIndex = 40;
            this.lblOut_IdTitleE.Text = "Copy！";
            this.lblOut_IdTitleE.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 18);
            this.label10.TabIndex = 39;
            this.label10.Text = "ID.英文标题";
            // 
            // txtIn_IdTitleE
            // 
            this.txtIn_IdTitleE.Location = new System.Drawing.Point(123, 50);
            this.txtIn_IdTitleE.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIn_IdTitleE.Name = "txtIn_IdTitleE";
            this.txtIn_IdTitleE.ReadOnly = true;
            this.txtIn_IdTitleE.Size = new System.Drawing.Size(744, 28);
            this.txtIn_IdTitleE.TabIndex = 38;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 37;
            this.label6.Text = "当前路径";
            // 
            // txt_FilePath
            // 
            this.txt_FilePath.Location = new System.Drawing.Point(123, 14);
            this.txt_FilePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_FilePath.Name = "txt_FilePath";
            this.txt_FilePath.ReadOnly = true;
            this.txt_FilePath.Size = new System.Drawing.Size(744, 28);
            this.txt_FilePath.TabIndex = 36;
            // 
            // lblOut_Answer
            // 
            this.lblOut_Answer.AutoSize = true;
            this.lblOut_Answer.Location = new System.Drawing.Point(874, 197);
            this.lblOut_Answer.Name = "lblOut_Answer";
            this.lblOut_Answer.Size = new System.Drawing.Size(35, 18);
            this.lblOut_Answer.TabIndex = 35;
            this.lblOut_Answer.Text = "OK!";
            this.lblOut_Answer.Visible = false;
            // 
            // lblOut_Update
            // 
            this.lblOut_Update.AutoSize = true;
            this.lblOut_Update.Location = new System.Drawing.Point(874, 161);
            this.lblOut_Update.Name = "lblOut_Update";
            this.lblOut_Update.Size = new System.Drawing.Size(35, 18);
            this.lblOut_Update.TabIndex = 34;
            this.lblOut_Update.Text = "OK!";
            this.lblOut_Update.Visible = false;
            // 
            // lblOut_Readme
            // 
            this.lblOut_Readme.AutoSize = true;
            this.lblOut_Readme.Location = new System.Drawing.Point(874, 89);
            this.lblOut_Readme.Name = "lblOut_Readme";
            this.lblOut_Readme.Size = new System.Drawing.Size(35, 18);
            this.lblOut_Readme.TabIndex = 33;
            this.lblOut_Readme.Text = "OK!";
            this.lblOut_Readme.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 197);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.TabIndex = 32;
            this.label8.Text = "答题文件";
            // 
            // txtOut_AnswerFilePath
            // 
            this.txtOut_AnswerFilePath.Location = new System.Drawing.Point(123, 194);
            this.txtOut_AnswerFilePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOut_AnswerFilePath.Multiline = true;
            this.txtOut_AnswerFilePath.Name = "txtOut_AnswerFilePath";
            this.txtOut_AnswerFilePath.ReadOnly = true;
            this.txtOut_AnswerFilePath.Size = new System.Drawing.Size(744, 48);
            this.txtOut_AnswerFilePath.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 18);
            this.label7.TabIndex = 30;
            this.label7.Text = "日志文件";
            // 
            // txtOut_UpdateFilePath
            // 
            this.txtOut_UpdateFilePath.Location = new System.Drawing.Point(123, 158);
            this.txtOut_UpdateFilePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOut_UpdateFilePath.Name = "txtOut_UpdateFilePath";
            this.txtOut_UpdateFilePath.ReadOnly = true;
            this.txtOut_UpdateFilePath.Size = new System.Drawing.Size(744, 28);
            this.txtOut_UpdateFilePath.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 28;
            this.label4.Text = "主要文件";
            // 
            // txtOut_ReadmeFilePath
            // 
            this.txtOut_ReadmeFilePath.Location = new System.Drawing.Point(123, 86);
            this.txtOut_ReadmeFilePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOut_ReadmeFilePath.Name = "txtOut_ReadmeFilePath";
            this.txtOut_ReadmeFilePath.ReadOnly = true;
            this.txtOut_ReadmeFilePath.Size = new System.Drawing.Size(744, 28);
            this.txtOut_ReadmeFilePath.TabIndex = 17;
            // 
            // btnFixFile
            // 
            this.btnFixFile.Enabled = false;
            this.btnFixFile.Location = new System.Drawing.Point(404, 12);
            this.btnFixFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnFixFile.Name = "btnFixFile";
            this.btnFixFile.Size = new System.Drawing.Size(228, 30);
            this.btnFixFile.TabIndex = 37;
            this.btnFixFile.Text = "Fix";
            this.btnFixFile.UseVisualStyleBackColor = true;
            this.btnFixFile.Click += new System.EventHandler(this.btnFixFile_Click);
            // 
            // lblOut_Problems
            // 
            this.lblOut_Problems.AutoSize = true;
            this.lblOut_Problems.Location = new System.Drawing.Point(874, 125);
            this.lblOut_Problems.Name = "lblOut_Problems";
            this.lblOut_Problems.Size = new System.Drawing.Size(35, 18);
            this.lblOut_Problems.TabIndex = 43;
            this.lblOut_Problems.Text = "OK!";
            this.lblOut_Problems.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(36, 125);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 18);
            this.label13.TabIndex = 42;
            this.label13.Text = "题目文件";
            // 
            // txtOut_ProblemsFilePath
            // 
            this.txtOut_ProblemsFilePath.Location = new System.Drawing.Point(123, 122);
            this.txtOut_ProblemsFilePath.Margin = new System.Windows.Forms.Padding(4);
            this.txtOut_ProblemsFilePath.Name = "txtOut_ProblemsFilePath";
            this.txtOut_ProblemsFilePath.ReadOnly = true;
            this.txtOut_ProblemsFilePath.Size = new System.Drawing.Size(744, 28);
            this.txtOut_ProblemsFilePath.TabIndex = 41;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 872);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnGenerate);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Leetcode_MD_Helper";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ComboBox cmbIn_Difficult;
        private System.Windows.Forms.TextBox txtIn_Link;
        private System.Windows.Forms.TextBox txtIn_IdTitleC;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtOut_ReadmeFilePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIn_Description;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOut_UpdateFilePath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOut_AnswerFilePath;
        private System.Windows.Forms.Label lblOut_Answer;
        private System.Windows.Forms.Label lblOut_Update;
        private System.Windows.Forms.Label lblOut_Readme;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtIn_Answer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_FilePath;
        private System.Windows.Forms.Label lblOut_IdTitleE;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtIn_IdTitleE;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtIn_SolutionLink;
        private System.Windows.Forms.Button btnFixFile;
        private System.Windows.Forms.Label lblOut_Problems;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtOut_ProblemsFilePath;
    }
}

