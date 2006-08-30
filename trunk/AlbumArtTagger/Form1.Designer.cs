namespace AlbumArtTagger
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
            this.btnCommand = new System.Windows.Forms.Button();
            this.bg1 = new System.ComponentModel.BackgroundWorker();
            this.pgbProgress = new System.Windows.Forms.ProgressBar();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSongInfo = new System.Windows.Forms.Label();
            this.chkConfirmImage = new System.Windows.Forms.CheckBox();
            this.selPlaylist = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCommand
            // 
            this.btnCommand.Location = new System.Drawing.Point(202, 12);
            this.btnCommand.Name = "btnCommand";
            this.btnCommand.Size = new System.Drawing.Size(75, 23);
            this.btnCommand.TabIndex = 0;
            this.btnCommand.Text = "Start";
            this.btnCommand.UseVisualStyleBackColor = true;
            this.btnCommand.Click += new System.EventHandler(this.button1_Click);
            // 
            // bg1
            // 
            this.bg1.WorkerReportsProgress = true;
            this.bg1.WorkerSupportsCancellation = true;
            this.bg1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.bg1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.bg1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // pgbProgress
            // 
            this.pgbProgress.Location = new System.Drawing.Point(6, 69);
            this.pgbProgress.Name = "pgbProgress";
            this.pgbProgress.Size = new System.Drawing.Size(259, 23);
            this.pgbProgress.Step = 1;
            this.pgbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgbProgress.TabIndex = 1;
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.Location = new System.Drawing.Point(6, 50);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(0, 13);
            this.lblPercentage.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSongInfo);
            this.panel1.Controls.Add(this.pgbProgress);
            this.panel1.Controls.Add(this.lblPercentage);
            this.panel1.Location = new System.Drawing.Point(7, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(270, 102);
            this.panel1.TabIndex = 3;
            // 
            // lblSongInfo
            // 
            this.lblSongInfo.AutoSize = true;
            this.lblSongInfo.Location = new System.Drawing.Point(6, 6);
            this.lblSongInfo.Name = "lblSongInfo";
            this.lblSongInfo.Size = new System.Drawing.Size(0, 13);
            this.lblSongInfo.TabIndex = 5;
            // 
            // chkConfirmImage
            // 
            this.chkConfirmImage.AutoSize = true;
            this.chkConfirmImage.Checked = true;
            this.chkConfirmImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConfirmImage.Location = new System.Drawing.Point(7, 41);
            this.chkConfirmImage.Name = "chkConfirmImage";
            this.chkConfirmImage.Size = new System.Drawing.Size(205, 17);
            this.chkConfirmImage.TabIndex = 4;
            this.chkConfirmImage.Text = "Confirm Album Covers Before Tagging";
            this.chkConfirmImage.UseVisualStyleBackColor = true;
            this.chkConfirmImage.CheckedChanged += new System.EventHandler(this.chkConfirmImage_CheckedChanged);
            // 
            // selPlaylist
            // 
            this.selPlaylist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selPlaylist.FormattingEnabled = true;
            this.selPlaylist.Location = new System.Drawing.Point(75, 13);
            this.selPlaylist.MaxDropDownItems = 100;
            this.selPlaylist.Name = "selPlaylist";
            this.selPlaylist.Size = new System.Drawing.Size(121, 21);
            this.selPlaylist.TabIndex = 5;
            this.selPlaylist.SelectedIndexChanged += new System.EventHandler(this.selPlaylist_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Playlist";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 168);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selPlaylist);
            this.Controls.Add(this.chkConfirmImage);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCommand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "iTunes Album Art Tagger";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCommand;
        private System.ComponentModel.BackgroundWorker bg1;
        private System.Windows.Forms.ProgressBar pgbProgress;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkConfirmImage;
        private System.Windows.Forms.Label lblSongInfo;
        private System.Windows.Forms.ComboBox selPlaylist;
        private System.Windows.Forms.Label label1;
    }
}

