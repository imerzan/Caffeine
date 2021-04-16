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
        private bool _Enabled;
        public bool Enabled
        {
            get
            {
                return this._Enabled;
            }
            set
            {
                this._Enabled = value;
                switch (value)
                {
                    case true:
                        this.ResetTimer();
                        break;
                    case false:
                        this.DelayTimer?.Stop();
                        break;
                }
            }
        }
        private System.Timers.Timer DelayTimer;
        private LASTINPUTINFO LastInput;
        private readonly Rectangle ScreenBounds = Screen.PrimaryScreen.Bounds; // Get screen bounds
        private readonly Random Rng = new Random(); // Random number generator
        private readonly Array Vkeys = typeof(VirtualKey).GetEnumValues(); // Get Virtual Keys

        public AfkMode(bool isEnabled = true) // Constructor
        {
            this.LastInput.cbSize = (uint)Marshal.SizeOf(this.LastInput); // Set cbSize parameter in struct with size of this struct
            this.Enabled = isEnabled;
        }
        private async void DelayTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (!this.Enabled) return;
                Win32API.GetLastInputInfo(ref this.LastInput); // Sets field this.LastInput.dwTime (ticks of last input)
                long idletime = (long)Win32API.GetTickCount() - (long)this.LastInput.dwTime; // Get difference between Current Ticks & Ticks of Last Input (Idle Time)
                if (idletime > this.DelayTimer.Interval) // // Check to see if user is idle, proceed with simulating keyboard/mouse input
                {
                    Win32API.SendKey((VirtualKey)this.Vkeys.GetValue(this.Rng.Next(0, this.Vkeys.Length))); // Send Key Press with randomly selected key
                    Win32API.SetCursorPos(this.Rng.Next(0, this.ScreenBounds.Width), this.Rng.Next(0, this.ScreenBounds.Height)); // Move mouse to random area of primary screen
                    await Task.Delay(500); // 500 ms delay before resetting timer
                }
            }
            catch { } // Silently ignore any exceptions (This program is designed to run quietly, and should cause no disturbance or provide any evidence that it is running when user is AFK)
            finally
            {
                this.ResetTimer(); // Set next timer
            }
        }
        private void ResetTimer()
        {
            if (!this.Enabled) return;
            this.DelayTimer = new System.Timers.Timer(this.Rng.Next(45000, 65000)); // 45 - 65 sec, random
            this.DelayTimer.AutoReset = false;
            this.DelayTimer.Elapsed += DelayTimer_Elapsed; // Set elapsed event method
            this.DelayTimer.Start(); // start timer
        }
    }
}
