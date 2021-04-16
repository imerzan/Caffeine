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
            this.CheckboxTooltip1.SetToolTip(checkbox_AfkMode, "Enabling afk mode will start a repeating timer for 45-65 seconds (random).\nIf no input is received during this period of time, Caffeine will simulate a single random keypress (A-Z, 0-9),\nand move the mouse cursor to a random area on the primary monitor."); // Set tooltip
            this.CheckboxTooltip2.SetToolTip(checkBox_KeepDisplayAwake, "This will prevent the display/monitor from going to sleep/energy save mode.");
            Win32API.PreventSleep(); // Prevent Windows from going to sleep while the main thread is active
        }

        private void checkBox_KeepDisplayAwake_CheckedChanged(object sender, EventArgs e)
        {
            this.keepDisplayAwakeToolStripMenuItem.Checked = this.checkBox_KeepDisplayAwake.Checked; // Set Tooltip Item State
            if (this.checkBox_KeepDisplayAwake.Checked)
            {
                Win32API.PreventSleep(true); // Prevent Display & Windows Sleep
            }
            else
            {
                Win32API.PreventSleep(); // Prevent Windows Sleep
            }
        }
        private void checkbox_AfkMode_CheckedChanged(object sender, EventArgs e) // Toggle AFK Mode
        {
            this.afkModeToolStripMenuItem.Checked = this.checkbox_AfkMode.Checked; // Set Tooltip Item State
            if (this.checkbox_AfkMode.Checked)
            {
                this.afkMode = new AfkMode(); // Enable AFK MODE
            }
            else
            {
                this.afkMode.Enabled = false; // Disable AFK MODE
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
        private void keepDisplayAwakeToolStripMenuItem_Click(object sender, EventArgs e) // Keep Display Awake Menu Item, Toggle
        {
            this.checkBox_KeepDisplayAwake.Checked = !this.checkBox_KeepDisplayAwake.Checked; // Invokes checkBox_KeepDisplayAwake_CheckedChanged()
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
        private void linkLabel_About_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/imerzan/Caffeine"); // Navigate to page when clicked
        }

    }
}
