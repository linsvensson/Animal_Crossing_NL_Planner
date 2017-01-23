using System;
using System.Windows;
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
    public partial class MessageBox
    {
        private MessageBoxButton _buttons = MessageBoxButton.OK;
        private MessageBoxIconType _msgIcon = MessageBoxIconType.None;

        public bool IsShowing;

        public MessageBox()
        {
            InitializeComponent();
        }

        #region Internal Properties
        internal MessageBoxButton Buttons
        {
            get { return _buttons; }
            set
            {
                _buttons = value;
                // Set all Buttons Visibility Properties
                SetButtonsVisibility();
            }
        }

        internal MessageBoxIconType MsgIcon
        {
            get { return _msgIcon; }
            set
            {
                _msgIcon = value;
                // Set all Buttons Visibility Properties
                SetIconVisibility();
            }
        }

        internal MessageBoxResult Result { get; set; } = MessageBoxResult.None;
        #endregion

        #region SetIcon Method
        internal void SetIconVisibility()
        {
            switch (_msgIcon)
            {
                case MessageBoxIconType.None:
                    IconImage = null;
                    break;
                case MessageBoxIconType.Info:
                    IconImage.Source = Globals.GetBitmapImage("info", "messagebox/");
                    break;
                case MessageBoxIconType.Error:
                    IconImage.Source = Globals.GetBitmapImage("error", "messagebox/");
                    break;
                case MessageBoxIconType.Question:
                    IconImage.Source = Globals.GetBitmapImage("question", "messagebox/");
                    break;
                case MessageBoxIconType.Warning:
                    IconImage.Source = Globals.GetBitmapImage("warning", "messagebox/");
                    break;
                default:
                    return;
            }
        }
        #endregion

        #region SetButtonsVisibility Method
        internal void SetButtonsVisibility()
        {
            switch (_buttons)
            {
                case MessageBoxButton.OK:
                    OkBtn.Visibility = Visibility.Visible;
                    CancelBtn.Visibility = Visibility.Collapsed;
                    YesBtn.Visibility = Visibility.Collapsed;
                    NoBtn.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.OKCancel:
                    OkBtn.Visibility = Visibility.Visible;
                    CancelBtn.Visibility = Visibility.Visible;
                    YesBtn.Visibility = Visibility.Collapsed;
                    NoBtn.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.YesNo:
                    OkBtn.Visibility = Visibility.Collapsed;
                    CancelBtn.Visibility = Visibility.Collapsed;
                    YesBtn.Visibility = Visibility.Visible;
                    NoBtn.Visibility = Visibility.Visible;
                    break;
                case MessageBoxButton.YesNoCancel:
                    OkBtn.Visibility = Visibility.Collapsed;
                    CancelBtn.Visibility = Visibility.Visible;
                    YesBtn.Visibility = Visibility.Visible;
                    NoBtn.Visibility = Visibility.Visible;
                    break;
                default:
                    return;
            }
        }
        #endregion

        #region Button Click Events
        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            Hide();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            Hide();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            Hide();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            Hide();
        }
        #endregion

        #region Deactivated Event
        private void Window_Deactivated(object sender, EventArgs e)
        {
            // If only an OK button is displayed, 
            // allow the user to just move away from this dialog box
            if (Buttons == MessageBoxButton.OK)
                Hide();
        }
        #endregion

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
                if (owner != null || Owner == null)
                    Owner = owner;

                if (Globals.CurrentAccent != null)
                    TitleTextBlock.Foreground = Globals.CurrentAccent;
                TitleTextBlock.Text = caption;
                MessageTextBlock.Text = message;
                Buttons = buttons;
                MsgIcon = icon;

                IsShowing = true;

                ShowDialog();
                var result = Result;

                return result;
            }
            catch
            {
                // ignored
            }
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
