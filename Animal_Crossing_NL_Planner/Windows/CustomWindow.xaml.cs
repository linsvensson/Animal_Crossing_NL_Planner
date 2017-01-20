using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Animal_Xing_Planner
{

    public enum CContent
    {
        Profile,
        Reminder,
    };

    /// <summary>
    /// Interaction logic for CustomWindow.xaml
    /// </summary>
    public partial class CustomWindow
    {
        private CContent _content;

        public ProfileUc ProfileUc = new ProfileUc();
        public NoticeUC NoticeUc = new NoticeUC();
        public HelpUc HelpUc = new HelpUc();

        public bool IsShowing;

        public CustomWindow()
        {
            InitializeComponent();
        }

        public void Show(Elysium.Controls.Window owner, CContent content)
        {
                Show(owner, content, CAction.None);
        }

        public void Show(Elysium.Controls.Window owner, CContent content, CAction action)
        {
            this._content = content;

            if (content == CContent.Profile)
            {
                Content = ProfileUc;
                ProfileUc.Initialize(this, action);
                Title = "Profile";
            }
            else
            {
                Content = NoticeUc;
                NoticeUc.Initialize(this);
                Title = "New Notice";
            }

            if (owner != null || Owner == null)
                Owner = owner;

            IsShowing = true;
            IsEnabled = true;

            try { ShowDialog(); }
            catch
            {
                // ignored
            }
        }

        public void ShowHelp(Elysium.Controls.Window owner)
        {
            Content = HelpUc;
            HelpUc.Initialize(this);
            Title = "Help";

            if (owner != null || Owner == null)
                Owner = owner;

            IsShowing = true;
            IsEnabled = true;
            try { ShowDialog(); }
            catch
            {
                // ignored
            }
        }

        public void HideWindow()
        {
            IsShowing = false;
            IsEnabled = false;
            Hide();
            Owner.Activate();
        }

        private void cWindow_DragOver(object sender, DragEventArgs e)
        {
            if (_content != CContent.Profile) return;

            e.Handled = true;
            e.Effects = DragDropEffects.None;
            if (!e.Data.GetDataPresent(DataFormats.FileDrop, true)) return;

            var filenames = e.Data.GetData(DataFormats.FileDrop, true) as string[];
            if (filenames != null && filenames.Length == 1 && Globals.IsValidImageExtension(filenames[0]))
            {
                e.Effects = DragDropEffects.Copy;
            }
        }

        private void cWindow_Drop(object sender, DragEventArgs e)
        {
            if (_content != CContent.Profile) return;
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);

            // Assuming you have one file that you care about, pass it off to whatever
            // handling code you have defined.
            if (filenames == null) return;
            BitmapImage tmpImage = new BitmapImage((new Uri(filenames[0])));

            ProfileUc.profileImg.Source = tmpImage;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            if (!Globals.ShutDown)
                e.Cancel = true;
        }

        private void cWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Globals.ShutDown) return;
            e.Cancel = true;
            HideWindow();
        }
    }
}
