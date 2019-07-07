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
            this.label10 = new System.Windows.Forms.Label();
            this.txtIn_IdTitleE = new System.Windows.Forms.TextBox();
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
            this.label6 = new System.Windows.Forms.Label();
            this.txt_FilePath = new System.Windows.Forms.TextBox();
            this.lblOut_Answer = new System.Windows.Forms.Label();
            this.lblOut_Log = new System.Windows.Forms.Label();
            this.lblOut_Directory = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOut_AnswerFilePath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOut_LogFilePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOut_DirectoryFilePath = new System.Windows.Forms.TextBox();
            this.lblOut_IdTitleE = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Enabled = false;
            this.btnGenerate.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGenerate.Location = new System.Drawing.Point(8, 325);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(623, 85);
            this.btnGenerate.TabIndex = 13;
            this.btnGenerate.Text = "↓ GENERATE ↓";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblOut_IdTitleE);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtIn_IdTitleE);
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
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(623, 302);
            this.panel1.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 86);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 12);
            this.label10.TabIndex = 36;
            this.label10.Text = "ID.英文标题";
            // 
            // txtIn_IdTitleE
            // 
            this.txtIn_IdTitleE.Location = new System.Drawing.Point(82, 84);
            this.txtIn_IdTitleE.Name = "txtIn_IdTitleE";
            this.txtIn_IdTitleE.ReadOnly = true;
            this.txtIn_IdTitleE.Size = new System.Drawing.Size(511, 21);
            this.txtIn_IdTitleE.TabIndex = 35;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(49, 208);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 34;
            this.label9.Text = "答题";
            // 
            // txtIn_Answer
            // 
            this.txtIn_Answer.Location = new System.Drawing.Point(82, 205);
            this.txtIn_Answer.Multiline = true;
            this.txtIn_Answer.Name = "txtIn_Answer";
            this.txtIn_Answer.Size = new System.Drawing.Size(511, 88);
            this.txtIn_Answer.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 114);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 29;
            this.label5.Text = "题目描述";
            // 
            // txtIn_Description
            // 
            this.txtIn_Description.Location = new System.Drawing.Point(82, 111);
            this.txtIn_Description.Multiline = true;
            this.txtIn_Description.Name = "txtIn_Description";
            this.txtIn_Description.Size = new System.Drawing.Size(511, 88);
            this.txtIn_Description.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 33);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "题目链接";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "ID.中文标题";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "难度";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(427, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(166, 20);
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
            this.cmbIn_Difficult.Location = new System.Drawing.Point(83, 8);
            this.cmbIn_Difficult.Name = "cmbIn_Difficult";
            this.cmbIn_Difficult.Size = new System.Drawing.Size(89, 20);
            this.cmbIn_Difficult.TabIndex = 23;
            this.cmbIn_Difficult.Text = "简单";
            // 
            // txtIn_Link
            // 
            this.txtIn_Link.Location = new System.Drawing.Point(82, 30);
            this.txtIn_Link.Name = "txtIn_Link";
            this.txtIn_Link.Size = new System.Drawing.Size(511, 21);
            this.txtIn_Link.TabIndex = 21;
            this.txtIn_Link.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // txtIn_IdTitleC
            // 
            this.txtIn_IdTitleC.Location = new System.Drawing.Point(82, 57);
            this.txtIn_IdTitleC.Name = "txtIn_IdTitleC";
            this.txtIn_IdTitleC.Size = new System.Drawing.Size(511, 21);
            this.txtIn_IdTitleC.TabIndex = 22;
            this.txtIn_IdTitleC.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txt_FilePath);
            this.panel2.Controls.Add(this.lblOut_Answer);
            this.panel2.Controls.Add(this.lblOut_Log);
            this.panel2.Controls.Add(this.lblOut_Directory);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtOut_AnswerFilePath);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtOut_LogFilePath);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtOut_DirectoryFilePath);
            this.panel2.Location = new System.Drawing.Point(8, 432);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(623, 130);
            this.panel2.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 15);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 37;
            this.label6.Text = "当前路径";
            // 
            // txt_FilePath
            // 
            this.txt_FilePath.Location = new System.Drawing.Point(82, 12);
            this.txt_FilePath.Name = "txt_FilePath";
            this.txt_FilePath.ReadOnly = true;
            this.txt_FilePath.Size = new System.Drawing.Size(511, 21);
            this.txt_FilePath.TabIndex = 36;
            // 
            // lblOut_Answer
            // 
            this.lblOut_Answer.AutoSize = true;
            this.lblOut_Answer.Location = new System.Drawing.Point(598, 90);
            this.lblOut_Answer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOut_Answer.Name = "lblOut_Answer";
            this.lblOut_Answer.Size = new System.Drawing.Size(23, 12);
            this.lblOut_Answer.TabIndex = 35;
            this.lblOut_Answer.Text = "OK!";
            this.lblOut_Answer.Visible = false;
            // 
            // lblOut_Log
            // 
            this.lblOut_Log.AutoSize = true;
            this.lblOut_Log.Location = new System.Drawing.Point(598, 66);
            this.lblOut_Log.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOut_Log.Name = "lblOut_Log";
            this.lblOut_Log.Size = new System.Drawing.Size(23, 12);
            this.lblOut_Log.TabIndex = 34;
            this.lblOut_Log.Text = "OK!";
            this.lblOut_Log.Visible = false;
            // 
            // lblOut_Directory
            // 
            this.lblOut_Directory.AutoSize = true;
            this.lblOut_Directory.Location = new System.Drawing.Point(598, 42);
            this.lblOut_Directory.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOut_Directory.Name = "lblOut_Directory";
            this.lblOut_Directory.Size = new System.Drawing.Size(23, 12);
            this.lblOut_Directory.TabIndex = 33;
            this.lblOut_Directory.Text = "OK!";
            this.lblOut_Directory.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 89);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 32;
            this.label8.Text = "答题文件";
            // 
            // txtOut_AnswerFilePath
            // 
            this.txtOut_AnswerFilePath.Location = new System.Drawing.Point(82, 87);
            this.txtOut_AnswerFilePath.Multiline = true;
            this.txtOut_AnswerFilePath.Name = "txtOut_AnswerFilePath";
            this.txtOut_AnswerFilePath.ReadOnly = true;
            this.txtOut_AnswerFilePath.Size = new System.Drawing.Size(511, 33);
            this.txtOut_AnswerFilePath.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 65);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 30;
            this.label7.Text = "日志文件";
            // 
            // txtOut_LogFilePath
            // 
            this.txtOut_LogFilePath.Location = new System.Drawing.Point(82, 63);
            this.txtOut_LogFilePath.Name = "txtOut_LogFilePath";
            this.txtOut_LogFilePath.ReadOnly = true;
            this.txtOut_LogFilePath.Size = new System.Drawing.Size(511, 21);
            this.txtOut_LogFilePath.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 42);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "目录文件";
            // 
            // txtOut_DirectoryFilePath
            // 
            this.txtOut_DirectoryFilePath.Location = new System.Drawing.Point(82, 39);
            this.txtOut_DirectoryFilePath.Name = "txtOut_DirectoryFilePath";
            this.txtOut_DirectoryFilePath.ReadOnly = true;
            this.txtOut_DirectoryFilePath.Size = new System.Drawing.Size(511, 21);
            this.txtOut_DirectoryFilePath.TabIndex = 17;
            // 
            // lblOut_IdTitleE
            // 
            this.lblOut_IdTitleE.AutoSize = true;
            this.lblOut_IdTitleE.Location = new System.Drawing.Point(598, 86);
            this.lblOut_IdTitleE.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOut_IdTitleE.Name = "lblOut_IdTitleE";
            this.lblOut_IdTitleE.Size = new System.Drawing.Size(23, 12);
            this.lblOut_IdTitleE.TabIndex = 37;
            this.lblOut_IdTitleE.Text = "OK!";
            this.lblOut_IdTitleE.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 573);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnGenerate);
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.TextBox txtOut_DirectoryFilePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIn_Description;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOut_LogFilePath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOut_AnswerFilePath;
        private System.Windows.Forms.Label lblOut_Answer;
        private System.Windows.Forms.Label lblOut_Log;
        private System.Windows.Forms.Label lblOut_Directory;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtIn_Answer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtIn_IdTitleE;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_FilePath;
        private System.Windows.Forms.Label lblOut_IdTitleE;
    }
}

