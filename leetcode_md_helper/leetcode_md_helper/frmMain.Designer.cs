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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.cmbIn_Difficult = new System.Windows.Forms.ComboBox();
            this.txtIn_Link = new System.Windows.Forms.TextBox();
            this.txtIn_Title = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtOut_Directory = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIn_Description = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOut_Log = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOut_Answer = new System.Windows.Forms.TextBox();
            this.lblOut_Answer = new System.Windows.Forms.Label();
            this.lblOut_Log = new System.Windows.Forms.Label();
            this.lblOut_Directory = new System.Windows.Forms.Label();
            this.txtIn_Answer = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Enabled = false;
            this.btnGenerate.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGenerate.Location = new System.Drawing.Point(12, 818);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(935, 128);
            this.btnGenerate.TabIndex = 13;
            this.btnGenerate.Text = "↓ GENERATE ↓";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtIn_Answer);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtPath);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtIn_Description);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.cmbIn_Difficult);
            this.panel1.Controls.Add(this.txtIn_Link);
            this.panel1.Controls.Add(this.txtIn_Title);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(935, 772);
            this.panel1.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 27;
            this.label3.Text = "题目链接";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 18);
            this.label2.TabIndex = 26;
            this.label2.Text = "ID.中文标题";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 25;
            this.label1.Text = "难度";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(665, 92);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(216, 61);
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
            this.cmbIn_Difficult.Location = new System.Drawing.Point(117, 92);
            this.cmbIn_Difficult.Margin = new System.Windows.Forms.Padding(4);
            this.cmbIn_Difficult.Name = "cmbIn_Difficult";
            this.cmbIn_Difficult.Size = new System.Drawing.Size(410, 26);
            this.cmbIn_Difficult.TabIndex = 23;
            this.cmbIn_Difficult.Text = "简单";
            // 
            // txtIn_Link
            // 
            this.txtIn_Link.Location = new System.Drawing.Point(117, 161);
            this.txtIn_Link.Margin = new System.Windows.Forms.Padding(4);
            this.txtIn_Link.Name = "txtIn_Link";
            this.txtIn_Link.Size = new System.Drawing.Size(764, 28);
            this.txtIn_Link.TabIndex = 22;
            this.txtIn_Link.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // txtIn_Title
            // 
            this.txtIn_Title.Location = new System.Drawing.Point(117, 125);
            this.txtIn_Title.Margin = new System.Windows.Forms.Padding(4);
            this.txtIn_Title.Name = "txtIn_Title";
            this.txtIn_Title.Size = new System.Drawing.Size(410, 28);
            this.txtIn_Title.TabIndex = 21;
            this.txtIn_Title.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblOut_Answer);
            this.panel2.Controls.Add(this.lblOut_Log);
            this.panel2.Controls.Add(this.lblOut_Directory);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtOut_Answer);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtOut_Log);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtOut_Directory);
            this.panel2.Location = new System.Drawing.Point(12, 977);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(935, 188);
            this.panel2.TabIndex = 22;
            // 
            // txtOut_Directory
            // 
            this.txtOut_Directory.Location = new System.Drawing.Point(117, 40);
            this.txtOut_Directory.Margin = new System.Windows.Forms.Padding(4);
            this.txtOut_Directory.Name = "txtOut_Directory";
            this.txtOut_Directory.ReadOnly = true;
            this.txtOut_Directory.Size = new System.Drawing.Size(764, 28);
            this.txtOut_Directory.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 28;
            this.label4.Text = "目录文件";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 29;
            this.label5.Text = "题目描述";
            // 
            // txtIn_Description
            // 
            this.txtIn_Description.Location = new System.Drawing.Point(117, 197);
            this.txtIn_Description.Margin = new System.Windows.Forms.Padding(4);
            this.txtIn_Description.Multiline = true;
            this.txtIn_Description.Name = "txtIn_Description";
            this.txtIn_Description.Size = new System.Drawing.Size(764, 270);
            this.txtIn_Description.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 32;
            this.label6.Text = "当前路径";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(117, 28);
            this.txtPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(764, 28);
            this.txtPath.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 18);
            this.label7.TabIndex = 30;
            this.label7.Text = "日志文件";
            // 
            // txtOut_Log
            // 
            this.txtOut_Log.Location = new System.Drawing.Point(117, 76);
            this.txtOut_Log.Margin = new System.Windows.Forms.Padding(4);
            this.txtOut_Log.Name = "txtOut_Log";
            this.txtOut_Log.ReadOnly = true;
            this.txtOut_Log.Size = new System.Drawing.Size(764, 28);
            this.txtOut_Log.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.TabIndex = 32;
            this.label8.Text = "答题文件";
            // 
            // txtOut_Answer
            // 
            this.txtOut_Answer.Location = new System.Drawing.Point(117, 112);
            this.txtOut_Answer.Margin = new System.Windows.Forms.Padding(4);
            this.txtOut_Answer.Multiline = true;
            this.txtOut_Answer.Name = "txtOut_Answer";
            this.txtOut_Answer.ReadOnly = true;
            this.txtOut_Answer.Size = new System.Drawing.Size(764, 48);
            this.txtOut_Answer.TabIndex = 31;
            // 
            // lblOut_Answer
            // 
            this.lblOut_Answer.AutoSize = true;
            this.lblOut_Answer.Location = new System.Drawing.Point(888, 115);
            this.lblOut_Answer.Name = "lblOut_Answer";
            this.lblOut_Answer.Size = new System.Drawing.Size(35, 18);
            this.lblOut_Answer.TabIndex = 35;
            this.lblOut_Answer.Text = "OK!";
            this.lblOut_Answer.Visible = false;
            // 
            // lblOut_Log
            // 
            this.lblOut_Log.AutoSize = true;
            this.lblOut_Log.Location = new System.Drawing.Point(888, 79);
            this.lblOut_Log.Name = "lblOut_Log";
            this.lblOut_Log.Size = new System.Drawing.Size(35, 18);
            this.lblOut_Log.TabIndex = 34;
            this.lblOut_Log.Text = "OK!";
            this.lblOut_Log.Visible = false;
            // 
            // lblOut_Directory
            // 
            this.lblOut_Directory.AutoSize = true;
            this.lblOut_Directory.Location = new System.Drawing.Point(888, 43);
            this.lblOut_Directory.Name = "lblOut_Directory";
            this.lblOut_Directory.Size = new System.Drawing.Size(35, 18);
            this.lblOut_Directory.TabIndex = 33;
            this.lblOut_Directory.Text = "OK!";
            this.lblOut_Directory.Visible = false;
            // 
            // txtIn_Answer
            // 
            this.txtIn_Answer.Location = new System.Drawing.Point(117, 475);
            this.txtIn_Answer.Margin = new System.Windows.Forms.Padding(4);
            this.txtIn_Answer.Multiline = true;
            this.txtIn_Answer.Name = "txtIn_Answer";
            this.txtIn_Answer.Size = new System.Drawing.Size(764, 270);
            this.txtIn_Answer.TabIndex = 33;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(66, 478);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 18);
            this.label9.TabIndex = 34;
            this.label9.Text = "答题";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 1175);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnGenerate);
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
        private System.Windows.Forms.TextBox txtIn_Title;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtOut_Directory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIn_Description;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOut_Log;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOut_Answer;
        private System.Windows.Forms.Label lblOut_Answer;
        private System.Windows.Forms.Label lblOut_Log;
        private System.Windows.Forms.Label lblOut_Directory;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtIn_Answer;
    }
}

