using System;
using System.Runtime.InteropServices;
using System.Timers;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Caffeine
{
    /// <summary>
    /// Provides an API to simulate hardware inputs to the Keyboard/Mouse.
    /// Can be enabled while using the system (non-afk).
    /// Will check if there is any recent input/activity from the user before performing automated functions.
    /// </summary>
    public sealed class AfkMode : IDisposable
    {
        /// <summary>
        /// Minimum Interval Range (in milliseconds).
        /// </summary>
        private const int IntervalMin = 45000;
        /// <summary>
        /// Maximum Interval Range (in milliseconds).
        /// </summary>
        private const int IntervalMax = 65000;

        private static readonly IReadOnlyCollection<VirtualKey> _keys;
        private readonly Random _rng = new Random();
        private readonly Stopwatch _sw = new Stopwatch();
        private readonly Rectangle _bounds = Screen.PrimaryScreen.Bounds;
        private readonly IntPtr _hWin;
        private readonly System.Timers.Timer _callbackTimer;
        private readonly LASTINPUTINFO _lastInput;

        private uint _lastChange;
        private TimeSpan _interval;

        static AfkMode()
        {
            _keys = typeof(VirtualKey).GetEnumValues().Cast<VirtualKey>().ToList();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="hWin">Window Handle to send Keyboard Input to.</param>
        public AfkMode(IntPtr hWin)
        {
            _hWin = hWin;
            _lastInput = new LASTINPUTINFO((uint)Marshal.SizeOf(typeof(LASTINPUTINFO)));
            SetRandomInterval(ref _interval);
            _callbackTimer = new System.Timers.Timer(250); // 250ms
            _callbackTimer.AutoReset = false;
            _callbackTimer.Elapsed += timer_Callback; // Set callback
            _callbackTimer.Start(); // start timer
        }

        /// <summary>
        /// Periodic callback to check last input, and if an interval has elapsed.
        /// </summary>
        private void timer_Callback(object sender, ElapsedEventArgs e)
        {
            try
            {
                Win32API.GetLastInputInfo(in _lastInput);
                if (_lastInput.dwTime != _lastChange) // User is not idle -> Reset Stopwatch
                {
                    _lastChange = _lastInput.dwTime;
                    _sw.Restart();
                }
                else // User is idle -> See if Stopwatch Interval has elapsed
                {
                    if (_sw.Elapsed > _interval) // Proceed with simulating keyboard/mouse input
                    {
                        Win32API.MoveMouse(_rng.Next(0, _bounds.Width), _rng.Next(0, _bounds.Height)); // Move mouse to random area of primary screen
                        // Set Caffeine as Forground Window for Keybd Input
                        // Will still work if Caffeine is minimzed to Tray
                        // This will prevent erroneous text entry into other applications
                        if (Win32API.SetForegroundWindow(_hWin))
                        {
                            var rngKey = _keys.ElementAt(_rng.Next(_keys.Count)); // Get random key
                            Win32API.SendKey(rngKey); // Send Key Input to Caffeine Window
                        }
                        ResetInterval(); // Prepare next interval
                    }
                }
            }
            catch { } // Swallow any exceptions (This program is designed to run quietly, and should cause no disturbance or provide any evidence that it is running when user is AFK)
            finally // Restart callback timer
            {
                lock (_callbackTimer)
                {
                    if (!_disposed)
                        _callbackTimer.Start();
                }
            }
        }

        /// <summary>
        /// Reset for the next interval duration.
        /// </summary>
        private void ResetInterval()
        {
            SetRandomInterval(ref _interval);
            Win32API.GetLastInputInfo(in _lastInput);
            _lastChange = _lastInput.dwTime;
        }

        /// <summary>
        /// Sets a new interval and restarts the Stopwatch.
        /// </summary>
        /// <returns>TimeSpan for the next interval.</returns>
        private void SetRandomInterval(ref TimeSpan interval)
        {
            interval = TimeSpan.FromMilliseconds(_rng.Next(IntervalMin, IntervalMax));
            _sw.Restart();
        }

        #region IDisposable
        private bool _disposed = false;
        public void Dispose()
        {
            lock (_callbackTimer)
            {
                _callbackTimer.Dispose();
                _disposed = true;
            }
        }
        #endregion
    }
}