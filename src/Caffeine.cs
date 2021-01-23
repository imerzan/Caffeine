using System;
using System.Windows.Forms;

namespace Caffeine
{
    public partial class Caffeine : Form
    {
        private AwayMode Worker { get; set; }
        public Caffeine() // GUI Constructor
        {
            InitializeComponent();
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1; // Set Tray Icon
            this.Resize += Window_Resize; // Minimize Event Handler
            WinAPI.PreventSleep();  // Prevent Windows from going to sleep while the main thread is active
        }
        private void checkbox_AwayMode_CheckedChanged(object sender, EventArgs e) // Toggle Away Mode
        {
            if (this.checkbox_AwayMode.Checked) // Away Mode Enabled
            {
                this.Worker = new AwayMode();
                this.awayModeToolStripMenuItem.Checked = true;
            }
            else // Away Mode Disabled
            {
                Worker?.Disable();
                this.awayModeToolStripMenuItem.Checked = false;
            }
        }

        private void Window_Resize(object sender, EventArgs e) // Hide GUI when minimized
        {
            if (this.WindowState == FormWindowState.Minimized) this.GuiShow(false);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) // Tray icon double click, Show GUI
        {
            this.GuiShow(true);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) // Open Menu Item, Show GUI
        {
            this.GuiShow(true);
        }
        private void awayModeToolStripMenuItem_Click(object sender, EventArgs e) // Away Mode Menu Item, Toggle Away Mode
        {
            this.checkbox_AwayMode.Checked = !this.checkbox_AwayMode.Checked; // Invokes checkbox_AwayMode_CheckedChanged()
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) // Exit Menu Item
        {
            this.notifyIcon1.Visible = false;
            Application.Exit();
        }
        private void GuiShow(bool IsVisible) // Shows/Hides GUI Window & Tray Icon
        {
            if (IsVisible) // Show GUI
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.BringToFront();
                this.notifyIcon1.Visible = false;
            }
            else // Hide GUI
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
            }
        }
    }
}
