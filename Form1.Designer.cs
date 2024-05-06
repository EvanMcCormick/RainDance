namespace Raindance
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_IhubRepoPath = new System.Windows.Forms.TextBox();
            this.lbl_IhubRepoPath = new System.Windows.Forms.Label();
            this.btn_raindance = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_selectall = new System.Windows.Forms.Button();
            this.gb_stop = new System.Windows.Forms.GroupBox();
            this.clb_stop = new System.Windows.Forms.CheckedListBox();
            this.gb_delete = new System.Windows.Forms.GroupBox();
            this.clb_delete = new System.Windows.Forms.CheckedListBox();
            this.gb_run = new System.Windows.Forms.GroupBox();
            this.clb_run = new System.Windows.Forms.CheckedListBox();
            this.btn_browse = new System.Windows.Forms.Button();
            this.fbdlg_RepoPath = new System.Windows.Forms.FolderBrowserDialog();
            this.rtxt_terminal = new System.Windows.Forms.RichTextBox();
            this.gb_stop.SuspendLayout();
            this.gb_delete.SuspendLayout();
            this.gb_run.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_IhubRepoPath
            // 
            this.txt_IhubRepoPath.Location = new System.Drawing.Point(137, 26);
            this.txt_IhubRepoPath.Name = "txt_IhubRepoPath";
            this.txt_IhubRepoPath.Size = new System.Drawing.Size(280, 20);
            this.txt_IhubRepoPath.TabIndex = 0;
            this.txt_IhubRepoPath.TextChanged += new System.EventHandler(this.txt_IhubRepoPath_TextChanged);
            // 
            // lbl_IhubRepoPath
            // 
            this.lbl_IhubRepoPath.AutoSize = true;
            this.lbl_IhubRepoPath.Location = new System.Drawing.Point(24, 29);
            this.lbl_IhubRepoPath.Name = "lbl_IhubRepoPath";
            this.lbl_IhubRepoPath.Size = new System.Drawing.Size(116, 13);
            this.lbl_IhubRepoPath.TabIndex = 2;
            this.lbl_IhubRepoPath.Text = "Path to IhubApp Repo:";
            // 
            // btn_raindance
            // 
            this.btn_raindance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_raindance.Location = new System.Drawing.Point(333, 245);
            this.btn_raindance.Name = "btn_raindance";
            this.btn_raindance.Size = new System.Drawing.Size(139, 23);
            this.btn_raindance.TabIndex = 5;
            this.btn_raindance.Text = "Rain Dance";
            this.btn_raindance.UseVisualStyleBackColor = true;
            this.btn_raindance.Click += new System.EventHandler(this.btn_raindance_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(27, 245);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_clear.TabIndex = 6;
            this.btn_clear.Text = "clear all";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_selectall
            // 
            this.btn_selectall.Location = new System.Drawing.Point(108, 245);
            this.btn_selectall.Name = "btn_selectall";
            this.btn_selectall.Size = new System.Drawing.Size(75, 23);
            this.btn_selectall.TabIndex = 7;
            this.btn_selectall.Text = "select all";
            this.btn_selectall.UseVisualStyleBackColor = true;
            this.btn_selectall.Click += new System.EventHandler(this.btn_selectall_Click);
            // 
            // gb_stop
            // 
            this.gb_stop.Controls.Add(this.clb_stop);
            this.gb_stop.Location = new System.Drawing.Point(27, 57);
            this.gb_stop.Name = "gb_stop";
            this.gb_stop.Size = new System.Drawing.Size(150, 182);
            this.gb_stop.TabIndex = 8;
            this.gb_stop.TabStop = false;
            this.gb_stop.Text = "End Process";
            // 
            // clb_stop
            // 
            this.clb_stop.CheckOnClick = true;
            this.clb_stop.FormattingEnabled = true;
            this.clb_stop.Items.AddRange(new object[] {
            "code",
            "nodejs"});
            this.clb_stop.Location = new System.Drawing.Point(10, 19);
            this.clb_stop.MultiColumn = true;
            this.clb_stop.Name = "clb_stop";
            this.clb_stop.Size = new System.Drawing.Size(134, 154);
            this.clb_stop.TabIndex = 5;
            this.clb_stop.SelectedIndexChanged += new System.EventHandler(this.clb_stop_SelectedIndexChanged);
            // 
            // gb_delete
            // 
            this.gb_delete.Controls.Add(this.clb_delete);
            this.gb_delete.Location = new System.Drawing.Point(183, 57);
            this.gb_delete.Name = "gb_delete";
            this.gb_delete.Size = new System.Drawing.Size(144, 182);
            this.gb_delete.TabIndex = 9;
            this.gb_delete.TabStop = false;
            this.gb_delete.Text = "Delete";
            // 
            // clb_delete
            // 
            this.clb_delete.CheckOnClick = true;
            this.clb_delete.FormattingEnabled = true;
            this.clb_delete.Items.AddRange(new object[] {
            ".cache",
            ".nx",
            "bin",
            "obj",
            "easycaching"});
            this.clb_delete.Location = new System.Drawing.Point(6, 19);
            this.clb_delete.Name = "clb_delete";
            this.clb_delete.Size = new System.Drawing.Size(132, 154);
            this.clb_delete.TabIndex = 0;
            // 
            // gb_run
            // 
            this.gb_run.Controls.Add(this.clb_run);
            this.gb_run.Location = new System.Drawing.Point(333, 57);
            this.gb_run.Name = "gb_run";
            this.gb_run.Size = new System.Drawing.Size(139, 182);
            this.gb_run.TabIndex = 10;
            this.gb_run.TabStop = false;
            this.gb_run.Text = "Run Process";
            // 
            // clb_run
            // 
            this.clb_run.CheckOnClick = true;
            this.clb_run.FormattingEnabled = true;
            this.clb_run.Items.AddRange(new object[] {
            "yarn",
            "yarn build:cache",
            "yarn build:app:styles",
            "yarn build:lighthouse",
            "yarn prestart",
            "npx nx reset",
            "restart VSC"});
            this.clb_run.Location = new System.Drawing.Point(6, 19);
            this.clb_run.Name = "clb_run";
            this.clb_run.Size = new System.Drawing.Size(127, 154);
            this.clb_run.TabIndex = 0;
            // 
            // btn_browse
            // 
            this.btn_browse.Location = new System.Drawing.Point(421, 21);
            this.btn_browse.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(50, 27);
            this.btn_browse.TabIndex = 11;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // rtxt_terminal
            // 
            this.rtxt_terminal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxt_terminal.BackColor = System.Drawing.Color.Black;
            this.rtxt_terminal.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.rtxt_terminal.Location = new System.Drawing.Point(27, 273);
            this.rtxt_terminal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rtxt_terminal.Name = "rtxt_terminal";
            this.rtxt_terminal.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtxt_terminal.Size = new System.Drawing.Size(439, 141);
            this.rtxt_terminal.TabIndex = 12;
            this.rtxt_terminal.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 437);
            this.Controls.Add(this.rtxt_terminal);
            this.Controls.Add(this.btn_browse);
            this.Controls.Add(this.gb_run);
            this.Controls.Add(this.gb_delete);
            this.Controls.Add(this.gb_stop);
            this.Controls.Add(this.btn_selectall);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_raindance);
            this.Controls.Add(this.lbl_IhubRepoPath);
            this.Controls.Add(this.txt_IhubRepoPath);
            this.Name = "Form1";
            this.Text = "Rain Dance";
            this.gb_stop.ResumeLayout(false);
            this.gb_delete.ResumeLayout(false);
            this.gb_run.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_IhubRepoPath;
        private System.Windows.Forms.Label lbl_IhubRepoPath;
        private System.Windows.Forms.Button btn_raindance;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_selectall;
        private System.Windows.Forms.GroupBox gb_stop;
        private System.Windows.Forms.CheckedListBox clb_stop;
        private System.Windows.Forms.GroupBox gb_delete;
        private System.Windows.Forms.CheckedListBox clb_delete;
        private System.Windows.Forms.GroupBox gb_run;
        private System.Windows.Forms.CheckedListBox clb_run;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.FolderBrowserDialog fbdlg_RepoPath;
        public System.Windows.Forms.RichTextBox rtxt_terminal;
    }
}

