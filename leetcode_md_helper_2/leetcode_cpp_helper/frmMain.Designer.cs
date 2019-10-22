namespace leetcode_cpp_helper
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
            this.txt_in_id = new System.Windows.Forms.TextBox();
            this.txt_in_title = new System.Windows.Forms.TextBox();
            this.txt_in_func = new System.Windows.Forms.TextBox();
            this.txt_in_old_path = new System.Windows.Forms.TextBox();
            this.btn_get_first_folder = new System.Windows.Forms.Button();
            this.btn_copy_folder = new System.Windows.Forms.Button();
            this.txt_in_new_path = new System.Windows.Forms.TextBox();
            this.txt_in_curr_folder = new System.Windows.Forms.TextBox();
            this.txt_out_return_type = new System.Windows.Forms.TextBox();
            this.txt_out_func_name = new System.Windows.Forms.TextBox();
            this.txt_out_param = new System.Windows.Forms.TextBox();
            this.txt_out_param_value = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_in_id
            // 
            this.txt_in_id.Location = new System.Drawing.Point(12, 43);
            this.txt_in_id.Name = "txt_in_id";
            this.txt_in_id.Size = new System.Drawing.Size(67, 28);
            this.txt_in_id.TabIndex = 0;
            // 
            // txt_in_title
            // 
            this.txt_in_title.Location = new System.Drawing.Point(86, 43);
            this.txt_in_title.Name = "txt_in_title";
            this.txt_in_title.Size = new System.Drawing.Size(702, 28);
            this.txt_in_title.TabIndex = 1;
            // 
            // txt_in_func
            // 
            this.txt_in_func.Location = new System.Drawing.Point(12, 77);
            this.txt_in_func.Name = "txt_in_func";
            this.txt_in_func.Size = new System.Drawing.Size(776, 28);
            this.txt_in_func.TabIndex = 2;
            // 
            // txt_in_old_path
            // 
            this.txt_in_old_path.Location = new System.Drawing.Point(12, 187);
            this.txt_in_old_path.Name = "txt_in_old_path";
            this.txt_in_old_path.Size = new System.Drawing.Size(579, 28);
            this.txt_in_old_path.TabIndex = 3;
            // 
            // btn_get_first_folder
            // 
            this.btn_get_first_folder.Location = new System.Drawing.Point(12, 304);
            this.btn_get_first_folder.Name = "btn_get_first_folder";
            this.btn_get_first_folder.Size = new System.Drawing.Size(191, 77);
            this.btn_get_first_folder.TabIndex = 4;
            this.btn_get_first_folder.Text = "Get First Folder";
            this.btn_get_first_folder.UseVisualStyleBackColor = true;
            this.btn_get_first_folder.Click += new System.EventHandler(this.btn_get_first_folder_Click);
            // 
            // btn_copy_folder
            // 
            this.btn_copy_folder.Location = new System.Drawing.Point(597, 304);
            this.btn_copy_folder.Name = "btn_copy_folder";
            this.btn_copy_folder.Size = new System.Drawing.Size(191, 77);
            this.btn_copy_folder.TabIndex = 5;
            this.btn_copy_folder.Text = "Copy Folder";
            this.btn_copy_folder.UseVisualStyleBackColor = true;
            this.btn_copy_folder.Click += new System.EventHandler(this.btn_copy_folder_Click);
            // 
            // txt_in_new_path
            // 
            this.txt_in_new_path.Location = new System.Drawing.Point(12, 221);
            this.txt_in_new_path.Name = "txt_in_new_path";
            this.txt_in_new_path.Size = new System.Drawing.Size(776, 28);
            this.txt_in_new_path.TabIndex = 6;
            // 
            // txt_in_curr_folder
            // 
            this.txt_in_curr_folder.Location = new System.Drawing.Point(597, 187);
            this.txt_in_curr_folder.Name = "txt_in_curr_folder";
            this.txt_in_curr_folder.Size = new System.Drawing.Size(191, 28);
            this.txt_in_curr_folder.TabIndex = 7;
            // 
            // txt_out_return_type
            // 
            this.txt_out_return_type.Location = new System.Drawing.Point(12, 111);
            this.txt_out_return_type.Name = "txt_out_return_type";
            this.txt_out_return_type.Size = new System.Drawing.Size(147, 28);
            this.txt_out_return_type.TabIndex = 8;
            // 
            // txt_out_func_name
            // 
            this.txt_out_func_name.Location = new System.Drawing.Point(165, 111);
            this.txt_out_func_name.Name = "txt_out_func_name";
            this.txt_out_func_name.Size = new System.Drawing.Size(147, 28);
            this.txt_out_func_name.TabIndex = 9;
            // 
            // txt_out_param
            // 
            this.txt_out_param.Location = new System.Drawing.Point(318, 111);
            this.txt_out_param.Name = "txt_out_param";
            this.txt_out_param.Size = new System.Drawing.Size(470, 28);
            this.txt_out_param.TabIndex = 10;
            // 
            // txt_out_param_value
            // 
            this.txt_out_param_value.Location = new System.Drawing.Point(318, 145);
            this.txt_out_param_value.Name = "txt_out_param_value";
            this.txt_out_param_value.Size = new System.Drawing.Size(470, 28);
            this.txt_out_param_value.TabIndex = 11;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 390);
            this.Controls.Add(this.txt_out_param_value);
            this.Controls.Add(this.txt_out_param);
            this.Controls.Add(this.txt_out_func_name);
            this.Controls.Add(this.txt_out_return_type);
            this.Controls.Add(this.txt_in_curr_folder);
            this.Controls.Add(this.txt_in_new_path);
            this.Controls.Add(this.btn_copy_folder);
            this.Controls.Add(this.btn_get_first_folder);
            this.Controls.Add(this.txt_in_old_path);
            this.Controls.Add(this.txt_in_func);
            this.Controls.Add(this.txt_in_title);
            this.Controls.Add(this.txt_in_id);
            this.Name = "frmMain";
            this.Text = "leetcode_cpp_helper";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_in_id;
        private System.Windows.Forms.TextBox txt_in_title;
        private System.Windows.Forms.TextBox txt_in_func;
        private System.Windows.Forms.TextBox txt_in_old_path;
        private System.Windows.Forms.Button btn_get_first_folder;
        private System.Windows.Forms.Button btn_copy_folder;
        private System.Windows.Forms.TextBox txt_in_new_path;
        private System.Windows.Forms.TextBox txt_in_curr_folder;
        private System.Windows.Forms.TextBox txt_out_return_type;
        private System.Windows.Forms.TextBox txt_out_func_name;
        private System.Windows.Forms.TextBox txt_out_param;
        private System.Windows.Forms.TextBox txt_out_param_value;
    }
}

