using Elysium;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace Animal_Xing_Planner
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Elysium.Controls.Window
    {
        public bool IsShowing;
        private ResourceDictionary resource = new ResourceDictionary();
        private string[] tpcColours = new string[5] { "Green", "Pink", "Blue", "Red", "Orange" };

        public SettingsWindow()
        {
            Resources.Add("AccentColor", Globals.CurrentAccent);

            InitializeComponent();
            DataContext = Globals.Main;
            tpcComboBox.ItemsSource = tpcColours;
            ShowCurrentTPCColour();
        }

        public void Show(Elysium.Controls.Window owner)
        {
            if (owner != null || Owner == null)
                Owner = owner;

            IsShowing = true;
            settingsTabControl.SelectedIndex = 0;
            ShowDialog();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!Globals.ShutDown)
            {
                e.Cancel = true;
                IsShowing = false;
                Hide();

                Owner.Activate();
            }
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

                Globals.Logger.Info("Deleted profile " + profile.Mayor + " " + profile.Town + " " + profile.FC);
                Globals.SetProfile(null);

                Globals.ResetChecklist();
                profileListView.Items.Refresh();
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
            if (soundSlider.Value == 0)
                Globals.bSound = false;
            else
                Globals.bSound = true;
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
            if (profileListView.SelectedItem != null)
            {
                Globals.CWindow.Show(this, CContent.Profile, CAction.Edit);
            }

            else
                Globals.MsgBox.Show(this, "You haven't selected a profile!", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Warning);
        }

        private void useButton_Click(object sender, RoutedEventArgs e)
        {
            if (profileListView.SelectedItem != null)
                Globals.SetProfile(profileListView.SelectedItem as Profile);

            else
                Globals.MsgBox.Show(this, "You haven't selected a profile!", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Warning);
        }

        private void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (profileListView.SelectedItem != null)
                DeleteProfile(Globals.SettingsWindow.profileListView.SelectedItem as Profile);
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (profileListView.SelectedItem != null)
                DeleteProfile(Globals.SettingsWindow.profileListView.SelectedItem as Profile);

            else
                Globals.MsgBox.Show(this, "You haven't selected a profile!", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Warning);

        }

        private void trayCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (trayCheckBox.IsChecked == true)
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

            for (int i = 0; i < profileListView.Items.Count; i++)
            {
                Profile tempProfile = profileListView.Items[i] as Profile;
                if (tempProfile == null)
                    return;
                if (tempProfile.Mayor == Globals.UserSettings.CurrentProfile.Mayor)
                {
                    profileListView.SelectedItem = profileListView.Items[i];
                    break;
                }
            }
        }

        private void HighlightCurrentAccent()
        {
            if (Globals.CurrentAccent == null)
                return;

            if (Globals.CurrentAccent.Equals(AccentBrushes.Green))
                greenButton.Focus();
            else if (Globals.CurrentAccent.Equals(AccentBrushes.Sky))
                blueButton.Focus();
            else if (Globals.CurrentAccent.Equals(AccentBrushes.Pink))
                pinkButton.Focus();
            else if (Globals.CurrentAccent.Equals(AccentBrushes.Red))
                redButton.Focus();
            else
                orangeButton.Focus();
        }

        private void ShowCurrentTPCColour()
        {
            string colour = Properties.Settings.Default.TPC;

            for (int i = 0; i < tpcComboBox.Items.Count; i++)
                if (tpcComboBox.Items[i].Equals(colour))
                    tpcComboBox.SelectedItem = tpcComboBox.Items[i];
        }

        private void themeTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            //if (!tpcComboBox.IsDropDownOpen)
            //    HighlightCurrentAccent();
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

                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = "TPC";
                dialog.DefaultExt = ".jpg";
                var result = dialog.ShowDialog();
                if (!result.HasValue)
                    return;

                Globals.SettingsWindow.villagerButton.Visibility = Visibility.Hidden;
                byte[] screenshot = Globals.GetJpgImage(Globals.Main.TPCGrid);
                FileStream fileStream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter binaryWriter = new BinaryWriter(fileStream);
                binaryWriter.Write(screenshot);
                binaryWriter.Close();
                Globals.SettingsWindow.villagerButton.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Globals.SettingsWindow.villagerButton.Visibility = Visibility.Visible;
                Globals.MsgBox.Show(this, "Unable to save TPC, check the log for further info", "Could not save", MessageBoxButton.OK, MessageBoxIconType.Error);
                Globals.Logger.Warn("Unable to save TPC: " + ex.Message);
            }
        }

        private void tpcComboBox_DropDownClosed(object sender, EventArgs e)
        {
            Globals.SetTPCColour(tpcComboBox.Text);
        }

        private void defaultSoundButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.Main.SoundPlayer.Stream = Properties.Resources.reminder;
            soundTextBox.Text = "Default";
            Properties.Settings.Default.CustomSound = string.Empty;
            Properties.Settings.Default.Save();
        }

        private void villagerButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.ToggleVillagers();

            villagerButton.Content = FindResource(villagerButton.Content == FindResource("Visible") ? "Hidden" : "Visible");
        }

        private void soundTextBox_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "wav files (*.wav)|*.wav";
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
