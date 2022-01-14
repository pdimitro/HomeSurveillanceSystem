namespace CameraProject.View
{
    partial class MainMenu
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
            this.components = new System.ComponentModel.Container();
            this.cameraActiveCheckbox = new System.Windows.Forms.CheckBox();
            this.viewHistoryButton = new System.Windows.Forms.Button();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.systemTimeLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStripContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cameraActiveCheckbox
            // 
            this.cameraActiveCheckbox.AutoSize = true;
            this.cameraActiveCheckbox.Location = new System.Drawing.Point(367, 93);
            this.cameraActiveCheckbox.Name = "cameraActiveCheckbox";
            this.cameraActiveCheckbox.Size = new System.Drawing.Size(146, 17);
            this.cameraActiveCheckbox.TabIndex = 1;
            this.cameraActiveCheckbox.Text = "Activete selected camera";
            this.cameraActiveCheckbox.UseVisualStyleBackColor = true;
            this.cameraActiveCheckbox.CheckedChanged += new System.EventHandler(this.cameraActiveCheckbox_CheckedChanged);
            // 
            // viewHistoryButton
            // 
            this.viewHistoryButton.Location = new System.Drawing.Point(395, 234);
            this.viewHistoryButton.Name = "viewHistoryButton";
            this.viewHistoryButton.Size = new System.Drawing.Size(102, 24);
            this.viewHistoryButton.TabIndex = 2;
            this.viewHistoryButton.Text = "View history";
            this.viewHistoryButton.UseVisualStyleBackColor = true;
            this.viewHistoryButton.Click += new System.EventHandler(this.viewHistoryButton_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.BackgroundImage = global::CameraProject.Properties.Resources.novideo;
            this.toolStripContainer1.ContentPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(336, 239);
            this.toolStripContainer1.Location = new System.Drawing.Point(28, 59);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(336, 264);
            this.toolStripContainer1.TabIndex = 4;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 337);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(535, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "Camera server running..";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(300, 341);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Camera server running..";
            // 
            // systemTimeLabel
            // 
            this.systemTimeLabel.AutoSize = true;
            this.systemTimeLabel.Location = new System.Drawing.Point(20, 343);
            this.systemTimeLabel.Name = "systemTimeLabel";
            this.systemTimeLabel.Size = new System.Drawing.Size(66, 13);
            this.systemTimeLabel.TabIndex = 8;
            this.systemTimeLabel.Text = "System time:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(92, 337);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 9;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Camera 1",
            "Camera 2(currently not available)",
            "Camera 3(currently not available)",
            "Camera 4(currently not available)"});
            this.comboBox1.Location = new System.Drawing.Point(376, 66);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 10;
            // 
            // toolTip1
            // 
            this.toolTip1.Tag = "Please login to access this functionality";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(535, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.loginToolStripMenuItem.Text = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(395, 264);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 359);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.systemTimeLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.viewHistoryButton);
            this.Controls.Add(this.cameraActiveCheckbox);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainMenu_MouseMove);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cameraActiveCheckbox;
        private System.Windows.Forms.Button viewHistoryButton;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label systemTimeLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.Button button1;
    }
}