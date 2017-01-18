using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Animal_Xing_Planner
{
    public enum MessageBoxIconType
    {
        None,
        Info,
        Warning,
        Error,
        Question,
    }

    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBox : Elysium.Controls.Window
    {
        public MessageBox()
        {
            InitializeComponent();
        }

        private MessageBoxButton _Buttons = MessageBoxButton.OK;
        private MessageBoxResult _Result = MessageBoxResult.None;
        private MessageBoxIconType _MsgIcon = MessageBoxIconType.None;

        #region internal Properties
        internal MessageBoxButton Buttons
        {
            get { return _Buttons; }
            set
            {
                _Buttons = value;
                // Set all Buttons Visibility Properties
                SetButtonsVisibility();
            }
        }

        internal MessageBoxIconType MsgIcon
        {
            get { return _MsgIcon; }
            set
            {
                _MsgIcon = value;
                // Set all Buttons Visibility Properties
                SetIconVisibility();
            }
        }

        internal MessageBoxResult Result
        {
            get { return _Result; }
            set { _Result = value; }
        }
        #endregion

        #region SetIcon Method
        internal void SetIconVisibility()
        {
            switch (_MsgIcon)
            {
                case MessageBoxIconType.None:
                    iconImage = null;
                    break;
                case MessageBoxIconType.Info:
                    iconImage.Source = Globals.GetBitmapImage("info", "messagebox/");
                    break;
                case MessageBoxIconType.Error:
                    iconImage.Source = Globals.GetBitmapImage("error", "messagebox/");
                    break;
                case MessageBoxIconType.Question:
                    iconImage.Source = Globals.GetBitmapImage("question", "messagebox/");
                    break;
                case MessageBoxIconType.Warning:
                    iconImage.Source = Globals.GetBitmapImage("warning", "messagebox/");
                    break;
            }
        }
        #endregion

        #region SetButtonsVisibility Method
        internal void SetButtonsVisibility()
        {
            switch (_Buttons)
            {
                case MessageBoxButton.OK:
                    btnOk.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Collapsed;
                    btnNo.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.OKCancel:
                    btnOk.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Visible;
                    btnYes.Visibility = Visibility.Collapsed;
                    btnNo.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.YesNo:
                    btnOk.Visibility = Visibility.Collapsed;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Visible;
                    btnNo.Visibility = Visibility.Visible;
                    break;
                case MessageBoxButton.YesNoCancel:
                    btnOk.Visibility = Visibility.Collapsed;
                    btnCancel.Visibility = Visibility.Visible;
                    btnYes.Visibility = Visibility.Visible;
                    btnNo.Visibility = Visibility.Visible;
                    break;
            }
        }
        #endregion

        #region Button Click Events
        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            this.Hide();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            this.Hide();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            this.Hide();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            this.Hide();
        }
        #endregion

        #region Windows Drag Event
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // if (e.ButtonState == MouseButtonState.Pressed)
            // this.DragMove();
        }
        #endregion

        #region Deactivated Event
        private void Window_Deactivated(object sender, EventArgs e)
        {
            // If only an OK button is displayed, 
            // allow the user to just move away from this dialog box
            if (Buttons == MessageBoxButton.OK)
                this.Hide();
        }
        #endregion

        public bool IsShowing;

        public MessageBoxResult Show(string message)
        {
            return Show(null, message, string.Empty, MessageBoxButton.OK, MessageBoxIconType.None);
        }

        public MessageBoxResult Show(string message, string caption)
        {
            return Show(null, message, caption, MessageBoxButton.OK, MessageBoxIconType.None);
        }

        public MessageBoxResult Show(Elysium.Controls.Window owner, string message, string caption, MessageBoxButton buttons, MessageBoxIconType icon)
        {
            try
            {
                MessageBoxResult result = MessageBoxResult.None;

                if (owner != null || Owner == null)
                    Owner = owner;

                if (Globals.CurrentAccent != null)
                    title.Foreground = Globals.CurrentAccent as SolidColorBrush;
                title.Text = caption;
                tbMessage.Text = message;
                Buttons = buttons;
                MsgIcon = icon;

                IsShowing = true;

                ShowDialog();
                result = Result;

                return result;
            }
            catch { }
            return MessageBoxResult.Cancel;
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Result == MessageBoxResult.None)
                e.Cancel = true;

            IsShowing = false;
        }


    }
}
