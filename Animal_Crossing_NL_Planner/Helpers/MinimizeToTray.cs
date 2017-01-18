using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;

namespace Animal_Xing_Planner
{
    /// <summary>
    /// Class implementing support for "minimize to tray" functionality.
    /// </summary>
    public static class MinimizeToTray
    {
        private static MinimizeToTrayInstance trayInstance = new MinimizeToTrayInstance();

        /// <summary>
        /// Enables "minimize to tray" behavior for the specified Window.
        /// </summary>
        /// <param name="window">Window to enable the behavior for.</param>
        public static void Enable(Elysium.Controls.Window window)
        {
            trayInstance.Enable(window);
        }

        /// <summary>
        /// Diables "minimize to tray" behavior for the specified Window.
        /// </summary>
        /// <param name="window">Window to enable the behavior for.</param>
        public static void Disable(Elysium.Controls.Window window)
        {
                trayInstance.Disable();
        }

        /// <summary>
        /// Class implementing "minimize to tray" functionality for a Window instance.
        /// </summary>
        private class MinimizeToTrayInstance
        {
            private Elysium.Controls.Window _window;
            private NotifyIcon _notifyIcon;
            private bool _balloonShown;

            /// <summary>
            /// Enables minimize to tray
            /// </summary>
            /// <param name="window">Window instance to attach to.</param>
            public void Enable(Elysium.Controls.Window window)
            {
                Debug.Assert(window != null, "window parameter is null.");
                _window = window;
                _window.StateChanged += new EventHandler(HandleStateChanged);
            }

            /// <summary>
            /// Disables minimize to tray
            /// </summary>
            public void Disable()
            {
                // Remove all event handlers
                if (_notifyIcon != null)
                {
                    _notifyIcon.MouseClick -= HandleNotifyIconOrBalloonClicked;
                    _notifyIcon.BalloonTipClicked -= HandleNotifyIconOrBalloonClicked;
                    _window.StateChanged -= HandleStateChanged;

                    _notifyIcon.Visible = false;
                    _notifyIcon.Dispose();
                }

                _window.StateChanged -= HandleStateChanged;
                _window = null;
            }

            /// <summary>
            /// Initializes a new instance of the MinimizeToTrayInstance class.
            /// </summary>
            public MinimizeToTrayInstance()
            {
            }

            /// <summary>
            /// Handles the Window's StateChanged event.
            /// </summary>
            /// <param name="sender">Event source.</param>
            /// <param name="e">Event arguments.</param>
            private void HandleStateChanged(object sender, EventArgs e)
            {
                if (_notifyIcon == null)
                {
                    // Initialize NotifyIcon instance "on demand"
                    _notifyIcon = new NotifyIcon();
                    _notifyIcon.Icon = Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);
                    _notifyIcon.MouseClick += new MouseEventHandler(HandleNotifyIconOrBalloonClicked);
                    _notifyIcon.BalloonTipClicked += new EventHandler(HandleNotifyIconOrBalloonClicked);
                }
                // Update copy of Window Title in case it has changed
                _notifyIcon.Text = _window.Title;

                // Show/hide Window and NotifyIcon
                var minimized = (_window.WindowState == WindowState.Minimized);
                _window.ShowInTaskbar = !minimized;
                _notifyIcon.Visible = minimized;
                if (minimized && !_balloonShown)
                {
                    // If this is the first time minimizing to the tray, show the user what happened
                    _notifyIcon.ShowBalloonTip(1000, null, _window.Title, ToolTipIcon.None);
                    _balloonShown = true;
                }
            }

            /// <summary>
            /// Handles a click on the notify icon or its balloon.
            /// </summary>
            /// <param name="sender">Event source.</param>
            /// <param name="e">Event arguments.</param>
            private void HandleNotifyIconOrBalloonClicked(object sender, EventArgs e)
            {
                // Restore the Window
                _window.WindowState = WindowState.Normal;
            }
        }
    }
}
