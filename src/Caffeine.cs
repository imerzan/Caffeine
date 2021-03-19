using System;
using System.Windows.Forms;

namespace Caffeine
{
    public partial class Caffeine : Form
    {
        private AfkMode afkMode;
        public Caffeine() // GUI Constructor
        {
            InitializeComponent();
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1; // Set Tray Icon
            this.Resize += Window_Resize; // Minimize Event Handler
            this.CheckboxTooltip1.SetToolTip(checkbox_AfkMode, "Enabling afk mode will start a repeating timer for 55-65 seconds (random).\nIf no input is received during this period of time, Caffeine will simulate a single random keypress (A-Z),\nand move the mouse cursor to a random area on the primary monitor."); // Set tooltip
            Win32API.PreventSleep();  // Prevent Windows from going to sleep while the main thread is active
        }
        private void checkbox_AwayMode_CheckedChanged(object sender, EventArgs e) // Toggle AFK Mode
        {
            if (this.checkbox_AfkMode.Checked) // AFK Mode Enabled
            {
                this.afkMode = new AfkMode();
                this.afkModeToolStripMenuItem.Checked = true;
            }
            else // AFK Mode Disabled
            {
                this.afkMode?.Disable();
                this.afkModeToolStripMenuItem.Checked = false;
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
        private void afkModeToolStripMenuItem_Click(object sender, EventArgs e) // AFK Mode Menu Item, Toggle Away Mode
        {
            this.checkbox_AfkMode.Checked = !this.checkbox_AfkMode.Checked; // Invokes checkbox_AwayMode_CheckedChanged()
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
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/imerzan/Caffeine"); // Navigate to page when clicked
        }
    }
}
