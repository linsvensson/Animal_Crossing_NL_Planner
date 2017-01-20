using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices.AccountManagement;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows.Data;

namespace Animal_Xing_Planner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Variables
        private SortAdorner _curAdorner;
        readonly System.Windows.Forms.Timer _fDeltaTime = new System.Windows.Forms.Timer();

        public bool BDoneLoading;
        public SoundPlayer SoundPlayer;
        public Dictionary<string, FrameworkElement> Controls;
        public List<Collectible> Collectibles { get; set; }
        public List<Profile> Profiles { get; set; }
        public GridViewColumnHeader CurSortCol { get; set; }
        #endregion

        #region MainWindow
        public MainWindow()
        {
            Resources.Add("AccentColor", Globals.CurrentAccent);
            InitializeComponent();

            Top = Properties.Settings.Default.Top;
            Left = Properties.Settings.Default.Left;
            Height = Properties.Settings.Default.Height;
            Width = Properties.Settings.Default.Width;

            // Very quick and dirty - but it does the job
            if (Properties.Settings.Default.Maximised)
                WindowState = WindowState.Maximized;
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            Globals.Initialize(this);
            DataContext = this;

            // Villager TPC setup
            Controls = new Dictionary<string, FrameworkElement>();
            for (int i = 0; i < 10; i++)
                Controls.Add("v" + i + "Image", FindName("v" + i + "Image") as Image);

            if (!Properties.Settings.Default.VillagerToggle)
                for (int j = 0; j < Main.Controls.Count; j++)
                    Main.Controls["v" + j + "Image"].Visibility = Visibility.Hidden;

            // Every 10 seconds
            _fDeltaTime.Interval = (1000 * 10);
            _fDeltaTime.Tick += Update;
            _fDeltaTime.Start();

            // Get username
            if (!string.IsNullOrEmpty(UserPrincipal.Current.DisplayName))
                userNameLabel.Content = "Hello " + UserPrincipal.Current.DisplayName;
            else
                userNameLabel.Content = "Hello " + Environment.UserName;

            // Date for clock
            string day = DateTime.Now.DayOfWeek.ToString();
            char[] dayArr = day.ToCharArray();
            dateTextLabel.Content = DateTime.Now.ToString("dd / MM");
            dayTextLabel.Content = dayArr[0] + dayArr[1].ToString();

            // Timer for clock
            new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                timeTextLabel.Content = DateTime.Now.ToString("HH:mm");
            }, Dispatcher);

            UpdateManager.CheckforUpdate();

            // Load the checklist from xml
            Collectibles = new List<Collectible>();
            Collectibles = XmlHandler.LoadCollectibles();
            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            dpd?.AddValueChanged(checklistDataGrid, ChecklistItemSourceChanged);
            Globals.LoadChecklist();

            // We're done loading, set the current profile
            BDoneLoading = true;
            if (Globals.UserSettings.CurrentProfile != null)
                Globals.SetProfile(Globals.UserSettings.CurrentProfile);

            Globals.TakeOutGarbage();
        }

        private void ChecklistItemSourceChanged(object sender, EventArgs e)
        {
            if (Main.checklistDataGrid.ItemsSource == null) return;

            ICollectionView collection = CollectionViewSource.GetDefaultView(Main.checklistDataGrid.ItemsSource);
            if (collection.GroupDescriptions.Count == 0)
                collection.GroupDescriptions.Add(new PropertyGroupDescription("Type"));
        }

        private void Main_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // If noticeListView loses focus, reset its selected item
            if (noticeListView.SelectedItem != null)
                noticeListView.SelectedItem = null;
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            // Used for drag/dropping image
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }

        private void Main_Closing(object sender, CancelEventArgs e)
        {
            //Globals.Logger.Init("Shutting down...");
            Globals.ShutDown = true;
            Globals.SaveProfiles();          
            Globals.UnsubscribeNotices();

            SoundPlayer.Dispose();

            // Save settings
            Properties.Settings.Default.SoundOn = Globals.BSound;
            Properties.Settings.Default.Accent = Globals.CurrentAccent.Color.ToString();
            Properties.Settings.Default.MinimizeToTray = Globals.MinToTray;

            if (WindowState == WindowState.Maximized)
            {
                // Use the RestoreBounds as the current values will be 0, 0 and the size of the screen
                Properties.Settings.Default.Top = RestoreBounds.Top;
                Properties.Settings.Default.Left = RestoreBounds.Left;
                Properties.Settings.Default.Height = RestoreBounds.Height;
                Properties.Settings.Default.Width = RestoreBounds.Width;
                Properties.Settings.Default.Maximised = true;
            }
            else
            {
                Properties.Settings.Default.Top = Top;
                Properties.Settings.Default.Left = Left;
                Properties.Settings.Default.Height = Height;
                Properties.Settings.Default.Width = Width;
                Properties.Settings.Default.Maximised = false;
            }

            Properties.Settings.Default.Save();

            Globals.TakeOutGarbage();

            // Close all windows
            Globals.MsgBox.Close();
            Globals.CWindow.Close();
            Globals.SettingsWindow.Close();

            Application.Current.Shutdown();
        }
        #endregion

        #region Update
        public void Update(object sender, EventArgs e)
        {
            if (noticeListView.Items.Count == 0) return;

            foreach (Notice t in noticeListView.Items)
                t.Update();
        }
        #endregion

        #region Control Events
        private void newprofileButton_Click(object sender, RoutedEventArgs e)
        {
            try { Globals.CWindow.Show(); }
            catch
            {
                // ignored
            }
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            try { Globals.SettingsWindow.Show(this); }
            catch
            {
                // ignored
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Globals.UserSettings.CurrentProfile != null)
                    Globals.CWindow.Show(this, CContent.Reminder, CAction.None);
                else
                    Globals.MsgBox.Show(this, "No profile set, create one in settings", "Info", MessageBoxButton.OK, MessageBoxIconType.Info);
            }
            catch
            {
                // ignored
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            try { DeleteNotice(); }
            catch (Exception ex)
            {
                Globals.MsgBox.Show(this, "Unable to delete notice", "Error", MessageBoxButton.OK, MessageBoxIconType.Error);
                Globals.Logger.Warn("Error deleting notice: " + ex.Message);
            }
        }

        private void deleteItem_Click(object sender, RoutedEventArgs e)
        {
            try { DeleteNotice(); }
            catch (Exception ex)
            {
                Globals.MsgBox.Show(this, "Unable to delete notice", "Error", MessageBoxButton.OK, MessageBoxIconType.Error);
                Globals.Logger.Warn("Error deleting notice: " + ex.Message);
            }
        }

        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            try { Globals.CWindow.ShowHelp(this); }
            catch
            {
                // ignored
            }
        }

        private void itemExp_Expanded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the Selected Row Expander Object
                Expander expandCollapseObj = (Expander)sender;

                // Check the Expander Object is null or not
                if (expandCollapseObj == null) return;

                // Return the Contains which specified element
                DataGridRow dgrSelectedRowObj = DataGridRow.GetRowContainingElement(expandCollapseObj);

                // Check the DataGridRow Object is Null or Not
                if (dgrSelectedRowObj != null)
                    dgrSelectedRowObj.DetailsVisibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Globals.MsgBox.Show(this, "Unable to expand/collapse item, check the log for more details", "Error", MessageBoxButton.OK, MessageBoxIconType.Error);
                Globals.Logger.Error("Unable to expand/collapse item: " + ex.Message);
            }
        }

        private void itemExp_Collapsed(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the Selected Row Expander Object
                Expander expandCollapseObj = (Expander)sender;

                // Check the Expander Object is null or not
                if (expandCollapseObj != null)
                {
                    // Return the Contains which specified element
                    DataGridRow dgrSelectedRowObj = DataGridRow.GetRowContainingElement(expandCollapseObj);

                    // Check the DataGridRow Object is Null or not
                    if (dgrSelectedRowObj != null)
                        dgrSelectedRowObj.DetailsVisibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                Globals.MsgBox.Show(this, "Unable to expand/collapse item, check the log for more details", "Error", MessageBoxButton.OK, MessageBoxIconType.Error);
                Globals.Logger.Error("Unable to expand/collapse item: " + ex.Message);
            }
        }
    #endregion

        #region Custom Functions
        private void SortClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            if (column == null) return;

            string field = column.Tag as string;

            if (CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(CurSortCol).Remove(_curAdorner);
                noticeListView.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (Equals(CurSortCol, column) && Equals(_curAdorner.Direction, newDir))
                newDir = ListSortDirection.Descending;

            CurSortCol = column;
            _curAdorner = new SortAdorner(CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(CurSortCol).Add(_curAdorner);
            noticeListView.Items.SortDescriptions.Add(new SortDescription(field, newDir));
        }

        public void notice_Updated(object sender, NoticeEventArgs events)
        {
            Notice notice = sender as Notice;

            if (Globals.BSound)
                SoundPlayer.Play();

            if (WindowState == WindowState.Minimized)
                WindowState = WindowState.Normal;

            if (Globals.MsgBox.IsShowing)
                Globals.MsgBox.Hide();

            Globals.RemoveNotice(notice);

            if (notice == null) return;

            string timeString = notice.StopTime;

            switch (notice.Type)
            {
                case NoticeType.Event:
                    if (Globals.MsgBox.Show(this, notice.Description, notice.Type.ToString() + " - " + notice.Name, MessageBoxButton.OK, MessageBoxIconType.Info) == MessageBoxResult.OK)
                        if (Globals.BSound)
                            SoundPlayer.Stop();
                    break;
                case NoticeType.Delivery:
                    if (!string.IsNullOrEmpty(notice.StopTime))
                    {
                        if (Globals.MsgBox.Show(this, "You've got a delivery of " + notice.Item + " to " + notice.Name + "!", "", MessageBoxButton.OK, MessageBoxIconType.Info) == MessageBoxResult.OK)
                            if (Globals.BSound)
                                SoundPlayer.Stop();
                    }

                    else
                    if (Globals.MsgBox.Show(this, "You've got a delivery of " + notice.Item + " to " + notice.Name + " for " + timeString + "!", "", MessageBoxButton.OK, MessageBoxIconType.Info) == MessageBoxResult.OK)
                        if (Globals.BSound)
                            SoundPlayer.Stop();
                    break;
                case NoticeType.Meeting:
                    if (Globals.MsgBox.Show(this, "You've got a meeting with " + notice.Name + " right now at " + notice.Place.ToLower() + "!", notice.Type.ToString(), MessageBoxButton.OK, MessageBoxIconType.Info) == MessageBoxResult.OK)
                        if (Globals.BSound)
                            SoundPlayer.Stop();
                    break;
                case NoticeType.Birthday:
                    break;
                default:
                    return;
            }
        }

        private void DeleteNotice()
        {
            if (noticeListView.Items.Count == 0) return;

            if (Globals.MsgBox.Show(this, "Are you sure you want to delete this notice?", "Confirmation", MessageBoxButton.YesNo, MessageBoxIconType.Info) == MessageBoxResult.Yes)
                if (noticeListView.SelectedItem != null)
                    Globals.RemoveNotice(noticeListView.SelectedItem as Notice);
        }

        private void DG_Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Collectible item = checklistDataGrid.SelectedItem as Collectible;
                Hyperlink link = e.OriginalSource as Hyperlink;
                if (link == null) return;

                if (item != null) link.NavigateUri = item.Info;
                Process.Start(link.NavigateUri.AbsoluteUri);
            }

            catch (Exception ex)
            {
                Globals.MsgBox.Show(this, "Could not open link, are you connected to the internet?", "Error", MessageBoxButton.OK, MessageBoxIconType.Error);
                Globals.Logger.Warn("Could not open hyperlink: " + ex.Message);
            }
        }

        private void DG_CheckBox_Check(object sender, RoutedEventArgs e)
        {
            Globals.AddCollectible();
        }

        private void DG_CheckBox_Uncheck(object sender, RoutedEventArgs e)
        {
            Globals.RemoveCollectible();
        }
        #endregion

        #region Single Click Editing
        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;

            if (cell == null || cell.IsEditing || cell.IsReadOnly) return;
            if (!cell.IsFocused)
            {
                cell.Focus();
            }

            DataGrid dataGrid = FindVisualParent<DataGrid>(cell);
            if (dataGrid == null) return;

            if (dataGrid.SelectionUnit != DataGridSelectionUnit.FullRow)
            {
                if (!cell.IsSelected)
                {
                    cell.IsSelected = true;
                }
            }
            else
            {
                DataGridRow row = FindVisualParent<DataGridRow>(cell);
                if (row != null && !row.IsSelected)
                {
                    row.IsSelected = true;
                }
            }
        }

        private static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }
        #endregion
    }
}
