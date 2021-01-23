using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Timers;
using System.Threading.Tasks;

namespace Caffeine
{
    public class AwayMode
    {
        private bool Enabled { get; set; }
        private LASTINPUTINFO LastInput;
        private Rectangle ScreenBounds { get; set; }
        private System.Timers.Timer DelayTimer { get; set; }
        private Random Rng { get; set; }

        public AwayMode() // Constructor, Enable Away Mode
        {
            this.Enabled = true;
            this.Rng = new Random(); // Random number generator
            this.LastInput = new LASTINPUTINFO(); // Create struct
            this.LastInput.cbSize = (uint)Marshal.SizeOf(this.LastInput); // Set cbSize parameter in struct with size of this struct
            this.ScreenBounds = Screen.PrimaryScreen.Bounds; // Get screen bounds
            this.ResetTimer();
        }
        private async void DelayTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!this.Enabled) return;
            WinAPI.GetLastInputInfo(ref this.LastInput);
            uint idletime = (WinAPI.GetTickCount() - this.LastInput.dwTime); // Check to see if user is idle
            if (idletime > this.DelayTimer.Interval) // User is idle, proceed with simulating keyboard/mouse input
            {
                WinAPI.SendKey((byte)this.Rng.Next(65, 90)); // Key Press, use A-Z randomly, converts integer vkey value to byte
                WinAPI.SetCursorPos(this.Rng.Next(0, this.ScreenBounds.Width), this.Rng.Next(0, this.ScreenBounds.Height)); // Move mouse to random area of primary screen
                await Task.Delay(500);
            }
            this.ResetTimer();
        }
        private async void ResetTimer()
        {
            if (!this.Enabled) return;
            this.DelayTimer = new System.Timers.Timer(Rng.Next(55000, 65000)); // 55 - 65 sec
            this.DelayTimer.AutoReset = false;
            this.DelayTimer.Elapsed += DelayTimer_Elapsed; // Set elapsed event method
            this.DelayTimer.Start(); // start timer
        }
        public async void Disable() // Disables Away Mode
        {
            this.Enabled = false;
            this.DelayTimer?.Stop();
        }
    }
}
