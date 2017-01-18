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
    public partial class MainWindow : Elysium.Controls.Window
    {
        #region Variables
        System.Windows.Forms.Timer fDeltaTime = new System.Windows.Forms.Timer();
        public bool bDoneLoading;
        public SoundPlayer SoundPlayer;
        public Dictionary<string, FrameworkElement> Controls;

        private List<Collectible> collectibles;
        public List<Collectible> Collectibles
        {
            get
            {
                return collectibles;
            }

            set
            {
                collectibles = value;
            }
        }
        private List<Profile> profiles;
        public List<Profile> Profiles
        {
            get
            {
                return profiles;
            }

            set
            {
                profiles = value;
            }
        }
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
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            Globals.Initialize(this);

            Controls = new Dictionary<string, FrameworkElement>();
            Controls.Add("v0Image", v0Image);
            Controls.Add("v1Image", v1Image);
            Controls.Add("v2Image", v2Image);
            Controls.Add("v3Image", v3Image);
            Controls.Add("v4Image", v4Image);
            Controls.Add("v5Image", v5Image);
            Controls.Add("v6Image", v6Image);
            Controls.Add("v7Image", v7Image);
            Controls.Add("v8Image", v8Image);
            Controls.Add("v9Image", v9Image);

            if (!Properties.Settings.Default.VillagerToggle)
                for (int j = 0; j < Main.Controls.Count; j++)
                    Main.Controls["v" + j + "Image"].Visibility = Visibility.Hidden;

            DataContext = this;

            // Every 10 seconds
            fDeltaTime.Interval = (1000 * 10);
            fDeltaTime.Tick += new EventHandler(Update);
            fDeltaTime.Start();

            // Get username
            if (!string.IsNullOrEmpty(UserPrincipal.Current.DisplayName))
                userNameLabel.Content = "Hello " + UserPrincipal.Current.DisplayName;
            else
                userNameLabel.Content = "Hello " + Environment.UserName;

            // Date for clock
            string day = DateTime.Now.DayOfWeek.ToString();
            char[] dayArr = day.ToCharArray();
            dateTextLabel.Content = DateTime.Now.ToString("dd / MM");
            dayTextLabel.Content = dayArr[0].ToString() + dayArr[1].ToString();

            // Timer for clock
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                timeTextLabel.Content = DateTime.Now.ToString("HH:mm");
            }, Dispatcher);

            UpdateManager.CheckforUpdate();

            // Load the checklist from xml
            Collectibles = new List<Collectible>();
            Collectibles = XmlHandler.LoadCollectibles();
            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (dpd != null)
                dpd.AddValueChanged(checklistDataGrid, ChecklistItemSourceChanged);
            Globals.LoadChecklist();

            // We're done loading, set the current profile
            bDoneLoading = true;
            if (Globals.UserSettings.CurrentProfile != null)
                Globals.SetProfile(Globals.UserSettings.CurrentProfile);

            Globals.TakeOutGarbage();
        }

        private void ChecklistItemSourceChanged(object sender, EventArgs e)
        {
            if (Main.checklistDataGrid.ItemsSource != null)
            {
                ICollectionView collection = CollectionViewSource.GetDefaultView(Main.checklistDataGrid.ItemsSource);
                if (collection.GroupDescriptions.Count == 0)
                    collection.GroupDescriptions.Add(new PropertyGroupDescription("Type"));
            }

        }

        private void Main_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (noticeListView.SelectedItem != null)
                noticeListView.SelectedItem = null;
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
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
            Properties.Settings.Default.SoundOn = Globals.bSound;
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

            Globals.MsgBox.Close();
            Globals.CWindow.Close();
            Globals.SettingsWindow.Close();

            Application.Current.Shutdown();
        }
        #endregion

        #region Update
        public void Update(object sender, EventArgs e)
        {
            if (noticeListView.Items.Count != 0)
                for (int i = 0; i < noticeListView.Items.Count; i++)
                {
                    Notice reminder = noticeListView.Items[i] as Notice;
                    reminder.Update();
                }
        }
        #endregion

        #region Control Events
        private void newprofileButton_Click(object sender, RoutedEventArgs e)
        {
            try { Globals.CWindow.Show(); }
            catch { }
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            try { Globals.SettingsWindow.Show(this); }
            catch { }
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
            catch { }
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
            catch { }
        }

        private void itemExp_Expanded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the Selected Row Expander Object
                Expander ExpandCollapseObj = (Expander)sender;

                // Check the Expander Object is null or Not
                if (ExpandCollapseObj != null)
                {
                    // Return the Contains which specified element
                    DataGridRow DgrSelectedRowObj = DataGridRow.GetRowContainingElement(ExpandCollapseObj);

                    // Check the DataGridRow Object is Null or Not
                    if (DgrSelectedRowObj != null)
                        DgrSelectedRowObj.DetailsVisibility = Visibility.Visible;
                }
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
                Expander ExpandCollapseObj = (Expander)sender;

                // Check the Expander Object is null or Not
                if (ExpandCollapseObj != null)
                {
                    // Return the Contains which specified element
                    DataGridRow DgrSelectedRowObj = DataGridRow.GetRowContainingElement(ExpandCollapseObj);

                    // Check the DataGridRow Object is Null or Not
                    if (DgrSelectedRowObj != null)
                        DgrSelectedRowObj.DetailsVisibility = Visibility.Collapsed;
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
    void OnItemToolTipOpening(object sender, ToolTipEventArgs e)
        {
            TextBlock item = sender as TextBlock;
            if (item != null)
            {
                // Determine right edge of text with respect to the TreeView.
                // If it is within TreeView bounds, then suppress the tooltip.
                Point itemScreenPosition = item.PointToScreen(new Point(0, 0));
                Point itemTreePosition = noticeListView.PointFromScreen(itemScreenPosition);
                double itemExtent = itemTreePosition.X + item.ActualWidth;
                if (itemExtent < 20)
                {
                    e.Handled = true;
                }
            }
        }

        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        private void SortClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            string field = column.Tag as string;

            if (_CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(_CurSortCol).Remove(_CurAdorner);
                noticeListView.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (_CurSortCol == column && _CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            _CurSortCol = column;
            _CurAdorner = new SortAdorner(_CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(_CurSortCol).Add(_CurAdorner);
            noticeListView.Items.SortDescriptions.Add(new SortDescription(field, newDir));
        }

        public void notice_Updated(object sender, NoticeEventArgs events)
        {
            Notice notice = sender as Notice;

            if (Globals.bSound)
                SoundPlayer.Play();

            if (WindowState == WindowState.Minimized)
                WindowState = WindowState.Normal;

            if (Globals.MsgBox.IsShowing)
                Globals.MsgBox.Hide();

            Globals.RemoveNotice(notice);

            string timeString = notice.StopTime;

            if (notice.Type == NoticeType.Event)
            {
                if (Globals.MsgBox.Show(this, notice.Description, notice.Type.ToString() + " - " + notice.Name, MessageBoxButton.OK, MessageBoxIconType.Info) == MessageBoxResult.OK)
                    if (Globals.bSound)
                        SoundPlayer.Stop();
            }

            else if (notice.Type == NoticeType.Delivery)
            {
                if (!string.IsNullOrEmpty(notice.StopTime))
                {
                    if (Globals.MsgBox.Show(this, "You've got a delivery of " + notice.Item + " to " + notice.Name + "!", "", MessageBoxButton.OK, MessageBoxIconType.Info) == MessageBoxResult.OK)
                        if (Globals.bSound)
                            SoundPlayer.Stop();
                }

                else
                    if (Globals.MsgBox.Show(this, "You've got a delivery of " + notice.Item + " to " + notice.Name + " for " + timeString + "!", "", MessageBoxButton.OK, MessageBoxIconType.Info) == MessageBoxResult.OK)
                    if (Globals.bSound)
                        SoundPlayer.Stop();
            }

            else if (notice.Type == NoticeType.Meeting)
            {
                if (Globals.MsgBox.Show(this, "You've got a meeting with " + notice.Name + " right now at " + notice.Place.ToLower() + "!", notice.Type.ToString(), MessageBoxButton.OK, MessageBoxIconType.Info) == MessageBoxResult.OK)
                    if (Globals.bSound)
                        SoundPlayer.Stop();
            }


        }

        private void DeleteNotice()
        {
            if (noticeListView.Items.Count != 0)
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
                link.NavigateUri = item.Info;
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
            if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
            {
                if (!cell.IsFocused)
                {
                    cell.Focus();
                }
                DataGrid dataGrid = FindVisualParent<DataGrid>(cell);
                if (dataGrid != null)
                {
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
            }
        }

        static T FindVisualParent<T>(UIElement element) where T : UIElement
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
