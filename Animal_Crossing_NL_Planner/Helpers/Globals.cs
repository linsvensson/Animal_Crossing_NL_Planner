﻿using Elysium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Animal_Xing_Planner
{
    public static class Globals
    {
        public static MainWindow Main;
        public static MessageBox MsgBox;
        public static CustomWindow CWindow;
        public static SettingsWindow SettingsWindow;

        public static bool bSound;
        public static string TimeSetup;

        public static Log Logger;

        public static SolidColorBrush CurrentAccent, CurrentAccentOpacity;

        public static bool MinToTray;
        public static bool ShutDown = false;

        public static ImageSourceConverter ImgConvert = new ImageSourceConverter();
        public static UserSettings UserSettings = null;

        private static ResourceDictionary resource = new ResourceDictionary();
        public static void Initialize(MainWindow main)
        {
            Main = main;
            MsgBox = new MessageBox();
            CWindow = new CustomWindow();
            SettingsWindow = new SettingsWindow();

            SetupProfiles();

            Main.Resources.Add("Accent", CurrentAccent);
            SettingsWindow.Resources.Add("Accent", CurrentAccent);
            CWindow.Resources.Add("Accent", CurrentAccent);
            MsgBox.Resources.Add("Accent", CurrentAccent);

            // Set up and configure logger
            Logger = new Log();
            Logger.ConfigureLogger();
            //Logger.Init("Application starting...");

            Application.Current.DispatcherUnhandledException += (sender, args) =>
            {
                Logger.Error(args.Exception);
            };

            // Theme setup
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Accent))
                ChangeTheme(Properties.Settings.Default.Accent, Theme.Light);

            else
                ChangeTheme("Green", Theme.Light);

            SetTPCColour(Properties.Settings.Default.TPC);

            MinToTray = Properties.Settings.Default.MinimizeToTray;
            if (MinToTray)
            {
                MinimizeToTray.Enable(main);
                SettingsWindow.trayCheckBox.IsChecked = true;
            }
            else
                SettingsWindow.trayCheckBox.IsChecked = false;

            // Sound related stuff
            bSound = Properties.Settings.Default.SoundOn;
            if (bSound)
                SettingsWindow.soundSlider.Value = 1;
            else
                SettingsWindow.soundSlider.Value = 0;

            Main.SoundPlayer = new SoundPlayer();
            SetCustomSound(Properties.Settings.Default.CustomSound);
        }

        /// <summary>
        /// Garbage collection
        /// </summary>
        public static void TakeOutGarbage()
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            catch { }
        }

        public static void ToggleVillagers()
        {
            // Hide
            if (Properties.Settings.Default.VillagerToggle)
            {
                for (int j = 0; j < Main.Controls.Count; j++)
                {
                    Main.Controls["v" + j + "Image"].Visibility = Visibility.Hidden;
                    Properties.Settings.Default.VillagerToggle = false;
                    Properties.Settings.Default.Save();
                }
            }

            // Show
            else
            {
                for (int j = 0; j < Main.Controls.Count; j++)
                {
                    Main.Controls["v" + j + "Image"].Visibility = Visibility.Visible;
                    Properties.Settings.Default.VillagerToggle = true;
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Unsubscribe from all notices
        /// </summary>
        public static void UnsubscribeNotices()
        {
            try
            {
                for (int i = 0; i < Main.Profiles.Count; i++)
                    if (Main.Profiles[i].Notices.Count != 0)
                        for (int j = 0; j < Main.Profiles[i].Notices.Count; j++)
                            Main.Profiles[i].Notices[j].Unsubscribe();
            }
            catch { }
        }

        public static bool SetCustomSound(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (File.Exists(path))
                {
                    string[] words = path.Split('\\');

                    Main.SoundPlayer.SoundLocation = path;
                    Properties.Settings.Default.CustomSound = path;
                    Properties.Settings.Default.Save();
                    SettingsWindow.soundTextBox.Text = words[words.Length - 1];
                    return true;
                }
            }
            Properties.Settings.Default.CustomSound = string.Empty;
            Properties.Settings.Default.Save();
            Main.SoundPlayer.Stream = Properties.Resources.reminder;
            SettingsWindow.soundTextBox.Text = "Default";
            return false;
        }

        #region Notice
        public static void NoticeCheck()
        {
            Main.noticeListView.Items.Clear();

            if (UserSettings.CurrentProfile == null)
                return;
            if (UserSettings.CurrentProfile.Notices == null)
                return;
            if (UserSettings.CurrentProfile.Notices.Count == 0)
                return;

            List<Notice> noticesToRemove = new List<Notice>();

            for (int i = 0; i < UserSettings.CurrentProfile.Notices.Count; i++)
            {
                Notice notice = UserSettings.CurrentProfile.Notices[i];
                try
                {
                    if (!string.IsNullOrEmpty(notice.StopTime))
                    {
                        string timeNow = DateTime.Now.ToString("HH:mm:00");
                        string dateNow = DateTime.Now.Date.ToString("yyyy-MM-dd");

                        TimeSpan stop = TimeSpan.Parse(notice.StopTime);
                        TimeSpan now = TimeSpan.Parse(timeNow);

                        string[] split = notice.Date.Split('-');
                        DateTime noticeDate = new DateTime(Convert.ToInt32(split[0]), Convert.ToInt32(split[1]), Convert.ToInt32(split[2]), stop.Hours, stop.Minutes, stop.Seconds);
                        string noticeDateNow = noticeDate.ToString("yyyy-MM-dd");

                        if (DateTime.Parse(noticeDateNow) == DateTime.Parse(dateNow))
                        {
                            if (stop.CompareTo(now) < 0)
                            {
                                noticesToRemove.Add(notice);

                                if (notice.Type == NoticeType.Meeting)
                                    MsgBox.Show(null, "You've missed a meeting with " + notice.Name + " at " + notice.Place + " for " + notice.StopTime + "!", "Missed notice", MessageBoxButton.OK, MessageBoxIconType.Info);

                                else if (notice.Type == NoticeType.Delivery)
                                    MsgBox.Show(null, "You've missed a delivery of " + notice.Item + " to " + notice.Name + " for " + notice.StopTime + "!", "Missed notice", MessageBoxButton.OK, MessageBoxIconType.Info);

                                else if (notice.Type == NoticeType.Event)
                                    MsgBox.Show(null, "You've missed the event " + notice.Name + ", " + notice.Description + " for " + notice.StopTime + "!", "Missed notice", MessageBoxButton.OK, MessageBoxIconType.Info);
                            }

                            else
                            {
                                Main.noticeListView.Items.Add(notice);
                                notice.Updated += new Notice.NoticeHandler(Main.notice_Updated);
                            }
                        }

                        else if (DateTime.Parse(noticeDateNow) < DateTime.Parse(dateNow))
                        {
                            if (notice.Type == NoticeType.Meeting)
                                MsgBox.Show(null, "You've missed a meeting with " + notice.Name + " at " + notice.Place + " for " + notice.StopTime + "!", "Missed notice", MessageBoxButton.OK, MessageBoxIconType.Info);

                            else if (notice.Type == NoticeType.Delivery)
                                MsgBox.Show(null, "You've missed a delivery of " + notice.Item + " to " + notice.Name + " for " + notice.StopTime + "!", "Missed notice", MessageBoxButton.OK, MessageBoxIconType.Info);

                            else if (notice.Type == NoticeType.Event)
                                MsgBox.Show(null, "You've missed the event " + notice.Name + ", " + notice.Description + " for " + notice.StopTime + "!", "Missed notice", MessageBoxButton.OK, MessageBoxIconType.Info);

                            noticesToRemove.Add(notice);
                        }

                        else
                        {
                            Main.noticeListView.Items.Add(notice);
                            notice.Updated += new Notice.NoticeHandler(Main.notice_Updated);
                        }
                    }

                    else
                        Main.noticeListView.Items.Add(notice);
                }

                catch (Exception ex)
                {
                    Logger.Error("Error in function NoticeCheck: " + ex.Message);
                    noticesToRemove.Add(notice);
                }
            }

            if (noticesToRemove.Count != 0)
            {
                for (int i = 0; i < noticesToRemove.Count; i++)
                    RemoveNotice(noticesToRemove[i]);
            }
        }

        /// <summary>
        /// Check if any of the villagers in the current profile has a birthday
        /// </summary>
        public static void BirthdayCheck()
        {
            if (UserSettings.CurrentProfile == null)
                return;
            if (UserSettings.CurrentProfile.Villagers == null)
                return;

            string dateNow = DateTime.Now.ToShortDateString();

            for (int i = 0; i < UserSettings.CurrentProfile.Villagers.Count; i++)
            {
                string bday = DateTime.Now.Year + "-" + UserSettings.CurrentProfile.Villagers[i].Birthday;

                if (bday == dateNow)
                {
                    if (MsgBox.Show(null, "" + UserSettings.CurrentProfile.Villagers[i].Name + " has a birthday today!\nDo you want to add this to your notice board for later?", "Birthday!", MessageBoxButton.YesNo, MessageBoxIconType.Info) == MessageBoxResult.Yes)
                    {
                        Notice notice = new Notice();
                        notice.Type = NoticeType.Birthday;
                        notice.Name = UserSettings.CurrentProfile.Villagers[i].Name;
                        notice.Date = dateNow;
                        notice.StopTime = "18:00";

                        AddNotice(notice);
                    }

                    else
                        MsgBox.Hide();
                }
            }
        }

        /// <summary>
        /// Adds a notice to the current profile
        /// </summary>
        /// <param name="notice">Notice to add</param>
        public static void AddNotice(Notice notice)
        {
            notice.SetIcon();

            UserSettings.CurrentProfile.Notices.Add(notice);
            UserSettings.Save();

            int index = Main.Profiles.FindIndex(x => x.FC == UserSettings.CurrentProfile.FC);
            if (!Main.Profiles[index].Notices.Contains(notice))
                Main.Profiles[index].Notices.Add(notice);
            SaveProfiles();

            if (!string.IsNullOrEmpty(notice.StopTime))
                notice.Updated += new Notice.NoticeHandler(Main.notice_Updated);

            Main.noticeListView.Items.Add(notice);
        }

        /// <summary>
        /// Removes a notice from the current profile
        /// </summary>
        /// <param name="notice">Notice to remove</param>
        public static void RemoveNotice(Notice notice)
        {
            UserSettings.CurrentProfile.Notices.Remove(notice);
            UserSettings.Save();

            int index = Main.Profiles.FindIndex(x => x.FC == UserSettings.CurrentProfile.FC);
            Main.Profiles[index].Notices.Remove(notice);
            SaveProfiles();

            if (!string.IsNullOrEmpty(notice.StopTime))
                notice.Unsubscribe();
            Main.noticeListView.Items.Remove(notice);
        }
        #endregion

        #region Profile
        public static void SetupProfiles()
        {
            UserSettings = new UserSettings();
            UserSettings.Reload(); // Refresh (get) data from persisted storage.

            string dir = @"saveData";
            if (File.Exists(dir + "/profiles.xml"))
            {
                List<Profile> profiles = new List<Profile>();

                profiles = XmlHandler.LoadProfiles();

                // Make sure the xml hasn't been tampered with, can't have more than one profile with the same FC
                foreach (Profile i in profiles)
                {
                    int index = profiles.FindIndex(x => x.FC == i.FC);
                    foreach (Profile j in profiles)
                    {
                        if (i != j && i.FC.Equals(j.FC))
                        {
                            j.FC = "0000-0000-0000";
                            Logger.Info("Found multiple profiles with the same FC, fixing...");
                        }
                    }
                }

                Main.Profiles = profiles;
            }

            else
                UserSettings.CurrentProfile = null;

            if (Main.Profiles == null)
                Main.Profiles = new List<Profile>();

            if (UserSettings.CurrentProfile != null)
            {
                // Make sure we're getting notices from the xml and not currentprofile
                int index = Main.Profiles.FindIndex(x => x.FC == UserSettings.CurrentProfile.FC);
                if (index != -1)
                    UserSettings.CurrentProfile = Main.Profiles[index];
                else
                    UserSettings.CurrentProfile = null;
            }
        }

        /// <summary>
        /// Sets a new current profile
        /// </summary>
        /// <param name="profile">New profile to set</param>
        public static void SetProfile(Profile profile)
        {
            //if (UserSettings.CurrentProfile != null)
            //    if (profile.FC.Equals(UserSettings.CurrentProfile.FC))
            //        return;

            if (profile == null || string.IsNullOrEmpty(profile.Town))
            {
                Main.messageLabel.Content = "No profile set, create one in settings!";
                Main.townLabel.Content = string.Empty;
                Main.mayorLabel.Content = string.Empty;
                Main.fcLabel.Content = string.Empty;
                Main.dcLabel.Content = string.Empty;
                Main.profileImage.Source = null;
                Main.profileImage.UpdateLayout();
                Main.noticeListView.Items.Clear();
                Main.checklistDataGrid.IsEnabled = false;
                Main.fruitImage.Source = null;
                Main.fruitImage.UpdateLayout();
            }

            else
            {
                Main.messageLabel.Content = profile.TagLine;
                Main.townLabel.Content = profile.Town;
                Main.mayorLabel.Content = "Mayor " + profile.Mayor;
                Main.fcLabel.Content = profile.FC;
                Main.dcLabel.Content = profile.DC;
                Main.checklistDataGrid.IsEnabled = true;

                Main.profileImage.Source = null;
                Main.profileImage.UpdateLayout();
                Main.fruitImage.Source = null;
                Main.fruitImage.UpdateLayout();

                // Reset villager images
                for (int j = 0; j < Main.Controls.Count; j++)
                {
                    Image img = Main.Controls["v" + j + "Image"] as Image;
                    img.Source = null;
                    img.UpdateLayout();
                }

                if (profile.Villagers.Count != 0)
                {
                    for (int i = 0; i < profile.Villagers.Count; i++)
                    {
                        Image img = Main.Controls["v" + i + "Image"] as Image;
                        img.Source = profile.Villagers[i].Icon;
                        img.UpdateLayout();
                    }
                }

                if (profile.ProfileImagePath != null)
                {
                    try
                    {
                        Main.profileImage.Source = ImgConvert.ConvertFromString(profile.ProfileImagePath) as ImageSource;
                        Main.profileImage.UpdateLayout();
                    }
                    catch { }
                }

                if (profile.Fruit != null)
                {
                    try
                    {
                        Main.fruitImage.Source = GetBitmapImage(profile.Fruit, "fruit/") as ImageSource;
                        Main.fruitImage.UpdateLayout();
                    }
                    catch { }
                }
            }

            UserSettings.CurrentProfile = profile;
            UserSettings.Save();

            if (Main.bDoneLoading)
            {
                NoticeCheck();
                BirthdayCheck();
                LoadChecklist();
            }

            SettingsWindow.profileListView.Items.Refresh();
            SettingsWindow.HighlightCurrentProfile();

            //if (profile != null)
            //    Logger.Info("Set profile " + profile.Mayor + " " + profile.Town + " " + profile.FC);
        }

        /// <summary>
        /// Save profiles to xml through the XmlHandler class
        /// </summary>
        public static void SaveProfiles()
        {
            XmlHandler.Save(Main.Profiles);
        }
        #endregion

        #region Checklist
        /// <summary>
        /// Loads the checklist from the current profile
        /// </summary>
        public static void LoadChecklist()
        {
            ResetChecklist();
            if (UserSettings.CurrentProfile != null)
            {
                // If the current profile has no list of collectibles, create one and break out
                if (UserSettings.CurrentProfile.Collectibles == null)
                {
                    UserSettings.CurrentProfile.Collectibles = new List<Collectible>();
                    return;
                }

                if (Main.Collectibles == null || Main.Collectibles.Count == 0)
                {
                    //Logger.Warn("Unable to load checklist, Main.Collectibles is null or contains no items.");
                    return;
                }

                // Find each collectible from the profile and check them in the datagrid
                if (UserSettings.CurrentProfile.Collectibles.Count != 0)
                    for (int i = 0; i < UserSettings.CurrentProfile.Collectibles.Count; i++)
                        for (int j = 0; j < Main.Collectibles.Count; j++)
                            if (UserSettings.CurrentProfile.Collectibles[i].Name == Main.Collectibles[j].Name)
                                Main.Collectibles[j].Checked = true;
            }
        }

        /// <summary>
        /// Unticks every single item in the checklist
        /// </summary>
        public static void ResetChecklist()
        {
            try
            {
                if (Main.Collectibles == null)
                    return;
                for (int i = 0; i < Main.Collectibles.Count; i++)
                    Main.Collectibles[i].Checked = false;
                Main.checklistDataGrid.Items.Refresh();
                Main.checklistDataGrid.UnselectAll();
            }
            catch (Exception ex) { Logger.Warn("Error resetting checklist: " + ex.Message); }
        }

        /// <summary>
        /// Removes a collectible from the current profile
        /// </summary>
        public static void RemoveCollectible()
        {
            if (Main.Collectibles == null || Main.Collectibles.Count == 0)
                return;

            Collectible selectedItem = Main.checklistDataGrid.SelectedItem as Collectible;
            if (selectedItem == null)
                return;
            Collectible item = UserSettings.CurrentProfile.Collectibles.Find(x => x.Name == selectedItem.Name);
            if (item == null)
                return;

            if (UserSettings.CurrentProfile.Collectibles.Contains(item))
            {
                UserSettings.CurrentProfile.Collectibles.Remove(item);
                UserSettings.Save();

                item = Main.Collectibles.Find(x => x.Name == item.Name);
                item.Checked = false;

                try { SaveProfiles(); }
                catch (Exception ex) { Logger.Warn("Could not save profiles after removing collectible: " + ex.Message); }
            }
            Main.checklistDataGrid.UnselectAll();
        }

        /// <summary>
        /// Adds a collectible to the current profile
        /// </summary>
        public static void AddCollectible()
        {
            if (Main.Collectibles == null || Main.Collectibles.Count == 0)
                return;

            Collectible selectedItem = Main.checklistDataGrid.SelectedItem as Collectible;
            if (selectedItem == null)
                return;

            Collectible item = UserSettings.CurrentProfile.Collectibles.Find(x => x.Name == selectedItem.Name);
            if (item == null)
            {
                selectedItem.Checked = true;
                UserSettings.CurrentProfile.Collectibles.Add(selectedItem);
                UserSettings.Save();

                int index = Main.Profiles.FindIndex(x => x.FC == UserSettings.CurrentProfile.FC);
                if (!Main.Profiles[index].Collectibles.Contains(selectedItem))
                    Main.Profiles[index].Collectibles.Add(selectedItem);

                try { SaveProfiles(); }
                catch (Exception ex) { Logger.Warn("Could not save profiles after adding collectible: " + ex.Message); }
            }

            else
                item.Checked = true;
            Main.checklistDataGrid.UnselectAll();
        }
        #endregion

        #region Theme
        public static SolidColorBrush GetAccent(string name)
        {
            if (name == "Red" || name == AccentBrushes.Red.Color.ToString())
                return AccentBrushes.Red;
            else if (name == "Green" || name == AccentBrushes.Green.Color.ToString())
                return AccentBrushes.Green;
            else if (name == "Sky" || name == AccentBrushes.Sky.Color.ToString())
                return AccentBrushes.Sky;
            else if (name == "Pink" || name == AccentBrushes.Pink.Color.ToString())
                return AccentBrushes.Pink;
            else
                return AccentBrushes.Mango;
        }

        static SolidColorBrush brush = new SolidColorBrush();
        public static void ChangeTheme(string accent, Theme theme)
        {
            brush.Color = Colors.White;
            if (accent != null)
                CurrentAccent = GetAccent(accent);

            if (CurrentAccentOpacity == null)
                CurrentAccentOpacity = new SolidColorBrush(CurrentAccent.Color);

            if (!Main.Resources.Contains("CurrentAccentOpacity"))
                Main.Resources.Add("AccentOpacity", CurrentAccentOpacity);

            // If the property is frozen, clone it and make the changes needed
            if (CurrentAccentOpacity.IsFrozen)
                CurrentAccentOpacity = CurrentAccentOpacity.Clone();
            CurrentAccentOpacity.Color = CurrentAccent.Color;
            CurrentAccentOpacity.Opacity = 0.2;

            Main.Resources["Accent"] = CurrentAccent;
            SettingsWindow.Resources["Accent"] = CurrentAccent;
            CWindow.Resources["Accent"] = CurrentAccent;
            Main.Resources["CurrentAccentOpacity"] = CurrentAccentOpacity;

            Manager.Apply(Application.Current, Theme.Light, CurrentAccent, brush);
        }

        public static void SetTPCColour(string colour)
        {
            string resource = string.Empty;
            string mayorColour = string.Empty;
            string townColour = string.Empty;

            if (colour.Equals("Pink"))
            {
                resource += "tpc_pink";
                townColour = "#FFD82E93";
                mayorColour = "#FFE863D0";
            }
            else if (colour.Equals("Green"))
            {
                resource += "tpc";
                townColour = "#FF218304";
                mayorColour = "#FF2DC500";
            }
            else if (colour.Equals("Orange"))
            {
                resource += "tpc_orange";
                townColour = "#FFC35F19";
                mayorColour = "#FFF37001";
            }
            else if (colour.Equals("Red"))
            {
                resource += "tpc_red";
                townColour = "#FFB40E0E";
                mayorColour = "#FFE41616";
            }
            else
            {
                resource += "tpc_blue";
                townColour = "#FF043E83";
                mayorColour = "#FF0063C5";
            }

            Properties.Settings.Default.TPC = colour;
            Properties.Settings.Default.Save();

            try
            {
                Main.tpcImage.Source = GetBitmapImage(resource, "tpc/") as ImageSource;
                var newColour = (Color)ColorConverter.ConvertFromString(mayorColour);
                var brush = new SolidColorBrush(newColour);
                Main.mayorLabel.Foreground = brush;
                var newColour2 = (Color)ColorConverter.ConvertFromString(townColour);
                var townBrush = new SolidColorBrush(newColour2);
                Main.townLabel.Foreground = townBrush;
                Main.tpcImage.UpdateLayout();
            }

            catch { }
        }
        #endregion

        /// <summary>
        /// Gets a bitmapimage from resources
        /// </summary>
        /// <param name="resourceName">Name of the resource</param>
        /// <param name="folder">Optional, folder to find the resource in</param>
        /// <param name="extension">Optional, extension (.jpg, .png etc)</param>
        /// <returns></returns>
        public static BitmapImage GetBitmapImage(string resourceName, string folder="", string extension = ".png")
        {
            try
            {
                Uri uri = new Uri("pack://application:,,,/Resources/" + folder + resourceName + extension);
                return new BitmapImage(uri);
            }

            catch { return new BitmapImage(); }
        }

        #region Checks
        public static bool IsValidImageExtension(string filename)
        {
            var extension = Path.GetExtension(filename);
            var extensions = new[] { ".png", ".jpg", ".jpeg", ".gif" };
            return extensions.Any(e => extension.Equals(e, StringComparison.OrdinalIgnoreCase));
        }

        public static bool CheckControls(DependencyObject obj)
        {
            bool somethingWrong = false;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                if (obj is TextBox)
                    if (string.IsNullOrEmpty(((TextBox)obj).Text))
                        somethingWrong = true;

                if (obj is ComboBox)
                    if (string.IsNullOrEmpty(((ComboBox)obj).Text))
                        somethingWrong = true;

                if (obj is DatePicker)
                    if (string.IsNullOrEmpty(((DatePicker)obj).Text))
                        somethingWrong = true;

                CheckControls(VisualTreeHelper.GetChild(obj, i));
            }

            if (somethingWrong)
                return false;

            else
                return true;
        }
        #endregion

        public static void ClearControls(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                if (obj is TextBox)
                    ((TextBox)obj).Text = null;

                if (obj is ComboBox)
                    ((ComboBox)obj).Text = null;

                if (obj is RadioButton)
                    ((RadioButton)obj).IsChecked = false;

                ClearControls(VisualTreeHelper.GetChild(obj, i));
            }
        }

        ///
        /// Gets a JPG "screenshot" of the current UIElement
        ///
        /// UIElement to screenshot
        /// Scale to render the screenshot
        /// JPG Quality
        /// Byte array of JPG data
        public static byte[] GetJpgImage(this UIElement source, double scale = 1, int quality = 100)
        {
            double actualHeight = source.RenderSize.Height;
            double actualWidth = source.RenderSize.Width;

            double renderHeight = actualHeight * scale;
            double renderWidth = actualWidth * scale;

            RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
            VisualBrush sourceBrush = new VisualBrush(source);

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            using (drawingContext)
            {
                drawingContext.PushTransform(new ScaleTransform(scale, scale));
                drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Point(actualWidth, actualHeight)));
            }
            renderTarget.Render(drawingVisual);

            JpegBitmapEncoder jpgEncoder = new JpegBitmapEncoder();
            jpgEncoder.QualityLevel = quality;
            jpgEncoder.Frames.Add(BitmapFrame.Create(renderTarget));

            byte[] _imageArray;

            using (MemoryStream outputStream = new MemoryStream())
            {
                jpgEncoder.Save(outputStream);
                _imageArray = outputStream.ToArray();
            }

            return _imageArray;
        }
    }

    #region Converters
    public class TextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value.ToString().Length > 0)
            {
                string comicVine = "More Info";
                return comicVine;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Uri comicVine = new Uri((string)value);
            return comicVine;
        }
    }

    public class UrlToImageSourceConverter : IValueConverter
    {
        public static UrlToImageSourceConverter Instance = new UrlToImageSourceConverter();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new BitmapImage(new Uri(value.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region ValidationRules
    public class EmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value as string;
            if (string.IsNullOrEmpty(str))
                return new ValidationResult(false, "This cannot be left empty");

            return new ValidationResult(true, null);
        }
    }

    public class FCValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value as string;

            if (string.IsNullOrEmpty(str))
                return new ValidationResult(false, "Please enter a Friend Code");

            if (str == "0000-0000-0000")
                return new ValidationResult(false, "This is not a valid Friend Code");

            var parts = str.Split('-');
            if (parts.Length != 3)
                return new ValidationResult(false, "A valid FC should look like this '0000-0000-0000'");

            foreach (var p in parts)
            {
                int intPart;
                if (!int.TryParse(p, NumberStyles.Integer,
                  cultureInfo.NumberFormat, out intPart))
                    return new ValidationResult(false, "A Friend Code should only consist of numbers and dashes");

                if (p.Length < 4 || p.Length > 4)
                    return new ValidationResult(false, null);
            }

            return new ValidationResult(true, null);
        }
    }

    public class DCValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value as string;

            if (string.IsNullOrEmpty(str))
                return new ValidationResult(false, "Please enter an Dream Code");

            if (str == "0000-0000-0000")
                return new ValidationResult(false, "This is not a valid Dream Code");

            var parts = str.Split('-');
            if (parts.Length != 3)
                return new ValidationResult(false, "A valid DC should look like this '0000-0000-0000'");

            foreach (var p in parts)
            {
                int intPart;
                if (!int.TryParse(p, NumberStyles.Integer,
                  cultureInfo.NumberFormat, out intPart))
                    return new ValidationResult(false, "A Dream Code should only consist of numbers and dashes");

                if (p.Length < 4 || p.Length > 4)
                    return new ValidationResult(false, null);
            }

            return new ValidationResult(true, null);
        }
    }
#endregion
}