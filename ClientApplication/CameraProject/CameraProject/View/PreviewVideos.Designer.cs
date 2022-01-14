namespace CameraProject.View
{
    partial class PreviewVideos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewVideos));
            this.checkedVideos = new System.Windows.Forms.CheckedListBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.deleteButton = new System.Windows.Forms.Button();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkedVideos
            // 
            this.checkedVideos.FormattingEnabled = true;
            this.checkedVideos.Location = new System.Drawing.Point(439, 37);
            this.checkedVideos.Name = "checkedVideos";
            this.checkedVideos.Size = new System.Drawing.Size(111, 184);
            this.checkedVideos.TabIndex = 0;
            this.checkedVideos.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedVideos_ItemCheck);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(439, 227);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(111, 25);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(12, 12);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(410, 293);
            this.axWindowsMediaPlayer1.TabIndex = 1;
            // 
            // deleteButton
            // 
            this.deleteButton.Enabled = false;
            this.deleteButton.Location = new System.Drawing.Point(439, 258);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(111, 23);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // DownloadButton
            // 
            this.DownloadButton.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.DownloadButton.Enabled = false;
            this.DownloadButton.Location = new System.Drawing.Point(439, 287);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(111, 23);
            this.DownloadButton.TabIndex = 4;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // PreviewVideos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 317);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.checkedVideos);
            this.Name = "PreviewVideos";
            this.Text = "PreviewVideos";
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedVideos;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.Button refreshButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}