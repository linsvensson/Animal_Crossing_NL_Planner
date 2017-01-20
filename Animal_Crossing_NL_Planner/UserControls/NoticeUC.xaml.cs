using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Animal_Xing_Planner
{
    /// <summary>
    /// Interaction logic for ReminderUC.xaml
    /// </summary>
    public partial class NoticeUC
    {
        public CustomWindow ParentWindow;
        private readonly List<string> _names = new List<string>();
        private bool _bSomethingWrong;
        private bool _bTimeTravel = false;

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
                _names.Add(Globals.UserSettings.CurrentProfile.Villagers[i].Name);

            _names.Sort();
            for (int i = 0; i < _names.Count; i++)
            {
                villagerNameComboBox.Items.Add(_names[i]);
                villagerNameComboBox1.Items.Add(_names[i]);
            }
            _names.Clear();

            typeTabControl.SelectedIndex = 0;
        }

        private bool Check()
        {
            try
            {
                _bSomethingWrong = false;
                _bTimeTravel = false;

                string setTime = string.Empty;
                string timeNow = DateTime.Now.ToString("HH:mm:ss");
                TimeSpan now = TimeSpan.Parse(timeNow);

                if (Equals(typeTabControl.SelectedItem, meetingTabItem))
                {
                    if (string.IsNullOrEmpty(villagerNameComboBox.Text) || string.IsNullOrEmpty(hourComboBox.Text) || string.IsNullOrEmpty(minuteComboBox.Text))
                        _bSomethingWrong = true;
                    else
                    {
                        setTime = hourComboBox.Text + ":" + minuteComboBox.Text + ":00";
                        TimeSpan stop = TimeSpan.Parse(setTime);

                        if (stop.CompareTo(now) <= 0)
                            _bTimeTravel = true;
                    }

                    if (homeRadioButton.IsChecked == false && awayRadioButton.IsChecked == false)
                        _bSomethingWrong = true;
                }

                else if (Equals(typeTabControl.SelectedItem, deliveryTabItem))
                {
                    if (string.IsNullOrEmpty(villagerNameComboBox1.Text) || string.IsNullOrEmpty(itemTextBox.Text))
                        _bSomethingWrong = true;

                    if (!string.IsNullOrEmpty(hourComboBox2.Text) && !string.IsNullOrEmpty(minuteComboBox2.Text))
                    {
                        setTime = hourComboBox2.Text + ":" + minuteComboBox2.Text + ":00";
                        TimeSpan stop = TimeSpan.Parse(setTime);

                        if (stop.CompareTo(now) <= 0)
                            _bTimeTravel = true;
                    }
                }

                else if (Equals(typeTabControl.SelectedItem, eventTabItem))
                {
                    if (string.IsNullOrEmpty(nameTextBox.Text) || string.IsNullOrEmpty(datePicker.Text) || string.IsNullOrEmpty(hourComboBox1.Text) || string.IsNullOrEmpty(minuteComboBox1.Text))
                        _bSomethingWrong = true;

                    else
                    {
                        setTime = hourComboBox1.Text + ":" + minuteComboBox1.Text + ":00";
                        TimeSpan stop = TimeSpan.Parse(setTime);

                        if (stop.CompareTo(now) <= 0 && datePicker.SelectedDate <= DateTime.Now)
                            _bTimeTravel = true;
                    }
                }

                if (_bTimeTravel)
                {
                    Globals.MsgBox.Show(ParentWindow, "You cannot set a notice at the current time or back in time.", "No TT pls", MessageBoxButton.OK, MessageBoxIconType.Warning);
                    return false;
                }

                return !_bSomethingWrong;
            }
            catch (Exception ex) { Globals.Logger.Warn("Error checking controls in NoticeUC: " + ex.Message); }
            return false;
        }

        private void AddNotice()
        {
            Check();

            if (_bSomethingWrong)
                Globals.MsgBox.Show(ParentWindow, "You've left one or more fields empty.", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Warning);

            else if (!_bSomethingWrong)
            {
                try
                {
                    Notice notice = new Notice();

                    if (Equals(typeTabControl.SelectedItem, meetingTabItem))
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

                    else if (Equals(typeTabControl.SelectedItem, deliveryTabItem))
                    {
                        notice.Type = NoticeType.Delivery;
                        notice.Name = villagerNameComboBox1.Text;
                        notice.Item = itemTextBox.Text;
                        notice.Date = DateTime.Now.ToShortDateString();

                        if (!string.IsNullOrEmpty(hourComboBox2.Text) && !string.IsNullOrEmpty(minuteComboBox2.Text))
                            notice.StopTime = hourComboBox2.Text + ":" + minuteComboBox2.Text + ":00";

                        notice.Info = notice.Item;
                    }

                    else if (Equals(typeTabControl.SelectedItem, eventTabItem))
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

                    if (_bTimeTravel) return;
                    Globals.AddNotice(notice);

                    if (Globals.MsgBox.Show(ParentWindow, "Notice successfully added!", "Success", MessageBoxButton.OK, MessageBoxIconType.Info) == MessageBoxResult.OK)
                    {
                        ParentWindow.HideWindow();
                        ParentWindow.Owner.Activate();
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
            if (Equals(typeTabControl.SelectedItem, meetingTabItem))
                villagerNameComboBox.Focus();

            else if (Equals(typeTabControl.SelectedItem, deliveryTabItem))
                villagerNameComboBox1.Focus();

            else if (Equals(typeTabControl.SelectedItem, eventTabItem))
                nameTextBox.Focus();
        }
        #endregion
    }
}
