using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Animal_Xing_Planner.Properties;
using Elysium;
using Elysium.Parameters;
using Microsoft.Win32;
using Window = Elysium.Controls.Window;

namespace Animal_Xing_Planner
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow
    {
        private readonly string[] _tpcColours = { "Green", "Pink", "Blue", "Red", "Orange" };
        public bool IsShowing;

        public SettingsWindow()
        {
            Resources.Add("AccentColor", Globals.CurrentAccent);

            InitializeComponent();
            DataContext = Globals.Main;
            TpcComboBox.ItemsSource = _tpcColours;
            ShowCurrentTpcColour();
        }

        public void Show(Window owner)
        {
            if (owner != null || Owner == null)
                Owner = owner;

            IsShowing = true;
            SettingsTabControl.SelectedIndex = 0;
            ShowDialog();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Globals.ShutDown) return;

            e.Cancel = true;
            IsShowing = false;
            Hide();

            Owner.Activate();
        }

        private void DeleteProfile(Profile profile)
        {
            if (Globals.MsgBox.Show(this, "Are you sure you want to delete this profile?", "Confirmation", MessageBoxButton.YesNo, MessageBoxIconType.Info) == MessageBoxResult.Yes)
            {
                int index = Globals.Main.Profiles.FindIndex(x => x == profile);
                Globals.Main.Profiles.RemoveAt(index);
                Globals.SaveProfiles();
                Globals.UserSettings.CurrentProfile = null;
                Globals.UserSettings.Save();

                Globals.Logger.Info("Deleted profile " + profile.Mayor + " " + profile.Town + " " + profile.Fc);
                Globals.SetProfile(null);

                Globals.ResetChecklist();
                ProfileListView.Items.Refresh();
                Globals.SettingsWindow.HighlightCurrentProfile();
            }
        }

        #region Control Events
        private void greenButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.ChangeTheme("Green", Theme.Light);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            IsShowing = false;
            Hide();
            Owner.Activate();
        }

        private void soundSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Globals.BSound = SoundSlider.Value != 0;
        }

        private void blueButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.ChangeTheme("Sky", Theme.Light);
        }

        private void pinkButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.ChangeTheme("Pink", Theme.Light);
        }

        private void orangeButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.ChangeTheme("Orange", Theme.Light);
        }

        private void redButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.ChangeTheme("Red", Theme.Light);
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.CWindow.Show(this, CContent.Profile, CAction.New);
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProfileListView.SelectedItem != null)
            {
                Globals.CWindow.Show(this, CContent.Profile, CAction.Edit);
            }

            else
                Globals.MsgBox.Show(this, "You haven't selected a profile!", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Warning);
        }

        private void useButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProfileListView.SelectedItem != null)
                Globals.SetProfile(ProfileListView.SelectedItem as Profile);

            else
                Globals.MsgBox.Show(this, "You haven't selected a profile!", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Warning);
        }

        private void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ProfileListView.SelectedItem != null)
                DeleteProfile(Globals.SettingsWindow.ProfileListView.SelectedItem as Profile);
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProfileListView.SelectedItem != null)
                DeleteProfile(Globals.SettingsWindow.ProfileListView.SelectedItem as Profile);

            else
                Globals.MsgBox.Show(this, "You haven't selected a profile!", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Warning);

        }

        private void trayCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (TrayCheckBox.IsChecked == true)
            {
                // Enable "minimize to tray" behavior for main Window
                MinimizeToTray.Enable(Globals.Main);
                Globals.MinToTray = true;
            }

            else
            {
                // Disable "minimize to tray" behavior for main Window
                MinimizeToTray.Disable(Globals.Main);
                Globals.MinToTray = false;
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!UpdateManager.CheckforUpdate())
                Globals.MsgBox.Show(Globals.Main, "Your app is up to date!", "Update", MessageBoxButton.OK, MessageBoxIconType.Info);
        }
        #endregion

        private void profileTabItem_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            HighlightCurrentProfile();
        }

        public void HighlightCurrentProfile()
        {
            if (Globals.UserSettings.CurrentProfile == null)
                return;

            foreach (Profile t in ProfileListView.Items)
            {
                if (t == null) return;
                if (t.Mayor != Globals.UserSettings.CurrentProfile.Mayor) continue;
                ProfileListView.SelectedItem = t;
                break;
            }
        }

        private void HighlightCurrentAccent()
        {
            if (Globals.CurrentAccent == null)
                return;

            if (Globals.CurrentAccent.Equals(AccentBrushes.Green))
                GreenButton.Focus();
            else if (Globals.CurrentAccent.Equals(AccentBrushes.Sky))
                BlueButton.Focus();
            else if (Globals.CurrentAccent.Equals(AccentBrushes.Pink))
                PinkButton.Focus();
            else if (Globals.CurrentAccent.Equals(AccentBrushes.Red))
                RedButton.Focus();
            else
                OrangeButton.Focus();
        }

        private void ShowCurrentTpcColour()
        {
            string colour = Settings.Default.TPC;

            for (int i = 0; i < TpcComboBox.Items.Count; i++)
                if (TpcComboBox.Items[i].Equals(colour))
                    TpcComboBox.SelectedItem = TpcComboBox.Items[i];
        }

        private void themeTabItem_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            HighlightCurrentAccent();
        }

        private void saveTPCButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Globals.UserSettings.CurrentProfile == null)
                {
                    Globals.MsgBox.Show(this, "No profile set, create one in settings", "Info", MessageBoxButton.OK, MessageBoxIconType.Info);
                    return;
                }

                SaveFileDialog dialog = new SaveFileDialog
                {
                    FileName = "TPC",
                    DefaultExt = ".jpg"
                };
                var result = dialog.ShowDialog();
                if (!result.HasValue)
                    return;

                byte[] screenshot = Globals.Main.TpcGrid.GetJpgImage();
                FileStream fileStream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter binaryWriter = new BinaryWriter(fileStream);
                binaryWriter.Write(screenshot);
                binaryWriter.Close();
            }
            catch (Exception ex)
            {
                Globals.MsgBox.Show(this, "Unable to save TPC, check the log for further info", "Could not save", MessageBoxButton.OK, MessageBoxIconType.Error);
                Globals.Logger.Warn("Unable to save TPC: " + ex.Message);
            }
        }

        private void tpcComboBox_DropDownClosed(object sender, EventArgs e)
        {
            Globals.SetTpcColour(TpcComboBox.Text);
        }

        private void defaultSoundButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.Main.SoundPlayer.Stream = Properties.Resources.reminder;
            SoundTextBox.Text = "Default";
            Settings.Default.CustomSound = string.Empty;
            Settings.Default.Save();
        }

        private void villagerButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.ToggleVillagers();

            VillagerButton.Content = FindResource(VillagerButton.Content == FindResource("Visible") ? "Hidden" : "Visible");
        }

        private void soundTextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog {Filter = "wav files (*.wav)|*.wav"};
                var result = dialog.ShowDialog();
                if (!result.HasValue)
                    return;

                Globals.SetCustomSound(dialog.FileName);
            }
            catch (Exception ex)
            {
                Globals.MsgBox.Show(this, "Unable to load custom sound, check the log for further info", "Could not load", MessageBoxButton.OK, MessageBoxIconType.Error);
                Globals.Logger.Warn("Unable to load custom sound: " + ex.Message);
            }
        }

    }
}
