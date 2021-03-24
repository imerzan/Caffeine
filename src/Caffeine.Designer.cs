namespace Caffeine
{
    partial class Caffeine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Caffeine));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.afkModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.checkbox_AfkMode = new System.Windows.Forms.CheckBox();
            this.CheckboxTooltip1 = new System.Windows.Forms.ToolTip(this.components);
            this.linkLabel_About = new System.Windows.Forms.LinkLabel();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Caffeine";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.afkModeToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(130, 76);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // afkModeToolStripMenuItem
            // 
            this.afkModeToolStripMenuItem.Name = "afkModeToolStripMenuItem";
            this.afkModeToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.afkModeToolStripMenuItem.Text = "AFK Mode";
            this.afkModeToolStripMenuItem.Click += new System.EventHandler(this.afkModeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(126, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Windows will not go to sleep \r\nwhile this program is running.";
            // 
            // checkbox_AfkMode
            // 
            this.checkbox_AfkMode.AutoSize = true;
            this.checkbox_AfkMode.Location = new System.Drawing.Point(25, 78);
            this.checkbox_AfkMode.Name = "checkbox_AfkMode";
            this.checkbox_AfkMode.Size = new System.Drawing.Size(112, 17);
            this.checkbox_AfkMode.TabIndex = 2;
            this.checkbox_AfkMode.Text = "Toggle AFK Mode";
            this.checkbox_AfkMode.UseVisualStyleBackColor = true;
            this.checkbox_AfkMode.CheckedChanged += new System.EventHandler(this.checkbox_AfkMode_CheckedChanged);
            // 
            // CheckboxTooltip1
            // 
            this.CheckboxTooltip1.AutoPopDelay = 10000;
            this.CheckboxTooltip1.InitialDelay = 500;
            this.CheckboxTooltip1.ReshowDelay = 100;
            this.CheckboxTooltip1.ShowAlways = true;
            // 
            // linkLabel_About
            // 
            this.linkLabel_About.AutoSize = true;
            this.linkLabel_About.Location = new System.Drawing.Point(12, 136);
            this.linkLabel_About.Name = "linkLabel_About";
            this.linkLabel_About.Size = new System.Drawing.Size(144, 13);
            this.linkLabel_About.TabIndex = 3;
            this.linkLabel_About.TabStop = true;
            this.linkLabel_About.Text = "github.com/imerzan/Caffeine";
            this.linkLabel_About.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_About_LinkClicked);
            // 
            // Caffeine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 158);
            this.Controls.Add(this.linkLabel_About);
            this.Controls.Add(this.checkbox_AfkMode);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Caffeine";
            this.Text = "Caffeine";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkbox_AfkMode;
        private System.Windows.Forms.ToolStripMenuItem afkModeToolStripMenuItem;
        private System.Windows.Forms.ToolTip CheckboxTooltip1;
        private System.Windows.Forms.LinkLabel linkLabel_About;
    }
}

