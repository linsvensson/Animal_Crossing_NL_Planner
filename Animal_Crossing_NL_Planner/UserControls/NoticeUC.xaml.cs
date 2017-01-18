using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Animal_Xing_Planner
{
    /// <summary>
    /// Interaction logic for ReminderUC.xaml
    /// </summary>
    public partial class NoticeUC : UserControl
    {
        public CustomWindow ParentWindow;
        private List<string> names = new List<string>();
        private bool somethingWrong = false;
        private bool noTT = false;

        public NoticeUC()
        {
            Resources.Add("AccentColor", Globals.CurrentAccent);

            InitializeComponent();

            for (int i = 0; i < 24; i++)
            {
                if (i < 10)
                {
                    hourComboBox.Items.Add("0" + i);
                    hourComboBox1.Items.Add("0" + i);
                    hourComboBox2.Items.Add("0" + i);
                }
                else
                {
                    hourComboBox.Items.Add(i);
                    hourComboBox1.Items.Add(i);
                    hourComboBox2.Items.Add(i);
                }
            }

            for (int i = 0; i < 60; i++)
            {
                if (i < 10)
                {
                    minuteComboBox.Items.Add("0" + i);
                    minuteComboBox1.Items.Add("0" + i);
                    minuteComboBox2.Items.Add("0" + i);
                }
                else
                {
                    minuteComboBox.Items.Add(i);
                    minuteComboBox1.Items.Add(i);
                    minuteComboBox2.Items.Add(i);
                }
            }
        }

        public void Initialize(CustomWindow parent)
        {
            ParentWindow = parent;

            Globals.ClearControls(this);
            villagerNameComboBox.Items.Clear();
            villagerNameComboBox1.Items.Clear();

            for (int i = 0; i < Globals.UserSettings.CurrentProfile.Villagers.Count; i++)
                names.Add(Globals.UserSettings.CurrentProfile.Villagers[i].Name);

            names.Sort();
            for (int i = 0; i < names.Count; i++)
            {
                villagerNameComboBox.Items.Add(names[i]);
                villagerNameComboBox1.Items.Add(names[i]);
            }
            names.Clear();

            typeTabControl.SelectedIndex = 0;
        }

        private bool Check()
        {
            try
            {
                somethingWrong = false;
                noTT = false;

                string setTime = string.Empty;
                string timeNow = DateTime.Now.ToString("HH:mm:ss");
                TimeSpan now = TimeSpan.Parse(timeNow);
                DateTime date = DateTime.Now;

                if (typeTabControl.SelectedItem == meetingTabItem)
                {
                    if (string.IsNullOrEmpty(villagerNameComboBox.Text) || string.IsNullOrEmpty(hourComboBox.Text) || string.IsNullOrEmpty(minuteComboBox.Text))
                        somethingWrong = true;
                    else
                    {
                        setTime = hourComboBox.Text + ":" + minuteComboBox.Text + ":00";
                        TimeSpan stop = TimeSpan.Parse(setTime);

                        if (stop.CompareTo(now) <= 0)
                            noTT = true;
                    }

                    if (homeRadioButton.IsChecked == false && awayRadioButton.IsChecked == false)
                        somethingWrong = true;
                }

                else if (typeTabControl.SelectedItem == deliveryTabItem)
                {
                    if (string.IsNullOrEmpty(villagerNameComboBox1.Text) || string.IsNullOrEmpty(itemTextBox.Text))
                        somethingWrong = true;

                    if (!string.IsNullOrEmpty(hourComboBox2.Text) && !string.IsNullOrEmpty(minuteComboBox2.Text))
                    {
                        setTime = hourComboBox2.Text + ":" + minuteComboBox2.Text + ":00";
                        TimeSpan stop = TimeSpan.Parse(setTime);

                        if (stop.CompareTo(now) <= 0)
                            noTT = true;
                    }
                }

                else if (typeTabControl.SelectedItem == eventTabItem)
                {
                    if (string.IsNullOrEmpty(nameTextBox.Text) || string.IsNullOrEmpty(datePicker.Text) || string.IsNullOrEmpty(hourComboBox1.Text) || string.IsNullOrEmpty(minuteComboBox1.Text))
                        somethingWrong = true;

                    else
                    {
                        setTime = hourComboBox1.Text + ":" + minuteComboBox1.Text + ":00";
                        TimeSpan stop = TimeSpan.Parse(setTime);

                        if (stop.CompareTo(now) <= 0 && datePicker.SelectedDate <= DateTime.Now)
                            noTT = true;
                    }
                }

                if (noTT)
                {
                    Globals.MsgBox.Show(ParentWindow, "You cannot set a notice at the current time or back in time.", "No TT pls", MessageBoxButton.OK, MessageBoxIconType.Warning);
                    return false;
                }

                else if (somethingWrong)
                    return false;

                else
                    return true;
            }
            catch (Exception ex) { Globals.Logger.Warn("Error checking controls in NoticeUC: " + ex.Message); }
            return false;
        }

        private void AddNotice()
        {
            Check();

            if (somethingWrong)
                Globals.MsgBox.Show(ParentWindow, "You've left one or more fields empty.", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Warning);

            else if (!somethingWrong)
            {
                try
                {
                    Notice notice = new Notice();

                    if (typeTabControl.SelectedItem == meetingTabItem)
                    {
                        notice.Type = NoticeType.Meeting;
                        notice.Name = villagerNameComboBox.Text;
                        notice.Date = DateTime.Now.ToShortDateString();

                        if (!string.IsNullOrEmpty(hourComboBox.Text) && !string.IsNullOrEmpty(minuteComboBox.Text))
                            notice.StopTime = hourComboBox.Text + ":" + minuteComboBox.Text + ":00";

                        if (homeRadioButton.IsChecked == true)
                            notice.Place = homeRadioButton.Content.ToString();
                        else if (awayRadioButton.IsChecked == true)
                            notice.Place = awayRadioButton.Content.ToString();

                        notice.Info = notice.Place;
                    }

                    else if (typeTabControl.SelectedItem == deliveryTabItem)
                    {
                        notice.Type = NoticeType.Delivery;
                        notice.Name = villagerNameComboBox1.Text;
                        notice.Item = itemTextBox.Text;
                        notice.Date = DateTime.Now.ToShortDateString();

                        if (!string.IsNullOrEmpty(hourComboBox2.Text) && !string.IsNullOrEmpty(minuteComboBox2.Text))
                            notice.StopTime = hourComboBox2.Text + ":" + minuteComboBox2.Text + ":00";

                        notice.Info = notice.Item;
                    }

                    else if (typeTabControl.SelectedItem == eventTabItem)
                    {
                        notice.Type = NoticeType.Event;
                        notice.Name = nameTextBox.Text;
                        notice.Description = descriptionTextBox.Text;
                        notice.Date = datePicker.Text;
                        if (!string.IsNullOrEmpty(hourComboBox1.Text) && !string.IsNullOrEmpty(minuteComboBox1.Text))
                            notice.StopTime = hourComboBox1.Text + ":" + minuteComboBox1.Text + ":00";

                        notice.Info = notice.Date;
                    }

                    if (notice.StopTime == null)
                        notice.StopTime = string.Empty;

                    if (!noTT)
                    {
                        Globals.AddNotice(notice);

                        if (Globals.MsgBox.Show(ParentWindow, "Notice successfully added!", "Success", MessageBoxButton.OK, MessageBoxIconType.Info) == MessageBoxResult.OK)
                        {
                            ParentWindow.HideWindow();
                            ParentWindow.Owner.Activate();
                        } 
                    }
                }
                catch (Exception ex) { Globals.Logger.Warn("Error creating new notice: " + ex.Message); }
            }
        }

        #region Control Events
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddNotice();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.HideWindow();
            ParentWindow.Owner.Activate();
        }

        private void typeTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (typeTabControl.SelectedItem == meetingTabItem)
                villagerNameComboBox.Focus();

            else if (typeTabControl.SelectedItem == deliveryTabItem)
                villagerNameComboBox1.Focus();

            else if (typeTabControl.SelectedItem == eventTabItem)
                nameTextBox.Focus();
        }
        #endregion
    }
}
