using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Timers;
using System.Threading.Tasks;

namespace Caffeine
{
    public class AfkMode
    {
        private bool Enabled;
        private LASTINPUTINFO LastInput;
        private Rectangle ScreenBounds;
        private System.Timers.Timer DelayTimer;
        private Random Rng;

        public AfkMode() // Constructor, Enable AFK Mode
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
            Win32API.GetLastInputInfo(ref this.LastInput);
            uint idletime = (Win32API.GetTickCount() - this.LastInput.dwTime); // Check to see if user is idle
            if (idletime > this.DelayTimer.Interval) // User is idle, proceed with simulating keyboard/mouse input
            {
                Array vkeys = typeof(VirtualKey).GetEnumValues(); // Get possible enumeration values
                VirtualKey key = (VirtualKey)vkeys.GetValue(Rng.Next(0, vkeys.Length)); // Pick random key
                Win32API.SendKey(key); // Send Key Press with randomly selected key
                Win32API.SetCursorPos(this.Rng.Next(0, this.ScreenBounds.Width), this.Rng.Next(0, this.ScreenBounds.Height)); // Move mouse to random area of primary screen
                await Task.Delay(500); // 500 ms delay before resetting timer
            }
            this.ResetTimer();
        }
        private void ResetTimer()
        {
            if (!this.Enabled) return;
            this.DelayTimer = new System.Timers.Timer(Rng.Next(55000, 65000)); // 55 - 65 sec, random
            this.DelayTimer.AutoReset = false;
            this.DelayTimer.Elapsed += DelayTimer_Elapsed; // Set elapsed event method
            this.DelayTimer.Start(); // start timer
        }
        public void Disable() // Disables AFK Mode
        {
            this.Enabled = false;
            this.DelayTimer?.Stop();
        }
    }
}
