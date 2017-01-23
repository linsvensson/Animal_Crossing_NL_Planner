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
        private readonly List<string> _names = new List<string>();
        private bool _bSomethingWrong;
        private bool _bTimeTravel;
        public CustomWindow ParentWindow;

        public NoticeUC()
        {
            Resources.Add("AccentColor", Globals.CurrentAccent);

            InitializeComponent();

            for (int i = 0; i < 24; i++)
            {
                if (i < 10)
                {
                    MHourComboBox.Items.Add("0" + i);
                    EHourComboBox.Items.Add("0" + i);
                    DHourComboBox.Items.Add("0" + i);
                }
                else
                {
                    MHourComboBox.Items.Add(i);
                    EHourComboBox.Items.Add(i);
                    DHourComboBox.Items.Add(i);
                }
            }

            for (int i = 0; i < 60; i++)
            {
                if (i < 10)
                {
                    MMinuteComboBox.Items.Add("0" + i);
                    EMinuteComboBox.Items.Add("0" + i);
                    DMinuteComboBox.Items.Add("0" + i);
                }
                else
                {
                    MMinuteComboBox.Items.Add(i);
                    EMinuteComboBox.Items.Add(i);
                    DMinuteComboBox.Items.Add(i);
                }
            }
        }

        public void Initialize(CustomWindow parent)
        {
            ParentWindow = parent;

            Globals.ClearControls(this);
            MVillagerNameComboBox.Items.Clear();
            DVillagerNameComboBox.Items.Clear();

            for (int i = 0; i < Globals.UserSettings.CurrentProfile.Villagers.Count; i++)
                _names.Add(Globals.UserSettings.CurrentProfile.Villagers[i].Name);

            _names.Sort();
            for (int i = 0; i < _names.Count; i++)
            {
                MVillagerNameComboBox.Items.Add(_names[i]);
                DVillagerNameComboBox.Items.Add(_names[i]);
            }
            _names.Clear();

            TypeTabControl.SelectedIndex = 0;
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

                if (Equals(TypeTabControl.SelectedItem, MeetingTabItem))
                {
                    if (string.IsNullOrEmpty(MVillagerNameComboBox.Text) || string.IsNullOrEmpty(MHourComboBox.Text) || string.IsNullOrEmpty(MMinuteComboBox.Text))
                        _bSomethingWrong = true;
                    else
                    {
                        setTime = MHourComboBox.Text + ":" + MMinuteComboBox.Text + ":00";
                        TimeSpan stop = TimeSpan.Parse(setTime);

                        if (stop.CompareTo(now) <= 0)
                            _bTimeTravel = true;
                    }

                    if (HomeRadioButton.IsChecked == false && AwayRadioButton.IsChecked == false)
                        _bSomethingWrong = true;
                }

                else if (Equals(TypeTabControl.SelectedItem, DeliveryTabItem))
                {
                    if (string.IsNullOrEmpty(DVillagerNameComboBox.Text) || string.IsNullOrEmpty(ItemTextBox.Text))
                        _bSomethingWrong = true;

                    if (!string.IsNullOrEmpty(DHourComboBox.Text) && !string.IsNullOrEmpty(DMinuteComboBox.Text))
                    {
                        setTime = DHourComboBox.Text + ":" + DMinuteComboBox.Text + ":00";
                        TimeSpan stop = TimeSpan.Parse(setTime);

                        if (stop.CompareTo(now) <= 0)
                            _bTimeTravel = true;
                    }
                }

                else if (Equals(TypeTabControl.SelectedItem, EventTabItem))
                {
                    if (string.IsNullOrEmpty(NameTextBox.Text) || string.IsNullOrEmpty(DatePicker.Text) || string.IsNullOrEmpty(EHourComboBox.Text) || string.IsNullOrEmpty(EMinuteComboBox.Text))
                        _bSomethingWrong = true;

                    else
                    {
                        setTime = EHourComboBox.Text + ":" + EMinuteComboBox.Text + ":00";
                        TimeSpan stop = TimeSpan.Parse(setTime);

                        if (stop.CompareTo(now) <= 0 && DatePicker.SelectedDate <= DateTime.Now)
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

                    if (Equals(TypeTabControl.SelectedItem, MeetingTabItem))
                    {
                        notice.Type = NoticeType.Meeting;
                        notice.Name = MVillagerNameComboBox.Text;
                        notice.Date = DateTime.Now.ToShortDateString();

                        if (!string.IsNullOrEmpty(MHourComboBox.Text) && !string.IsNullOrEmpty(MMinuteComboBox.Text))
                            notice.StopTime = MHourComboBox.Text + ":" + MMinuteComboBox.Text + ":00";

                        if (HomeRadioButton.IsChecked == true)
                            notice.Place = HomeRadioButton.Content.ToString();
                        else if (AwayRadioButton.IsChecked == true)
                            notice.Place = AwayRadioButton.Content.ToString();

                        notice.Info = notice.Place;
                    }

                    else if (Equals(TypeTabControl.SelectedItem, DeliveryTabItem))
                    {
                        notice.Type = NoticeType.Delivery;
                        notice.Name = DVillagerNameComboBox.Text;
                        notice.Item = ItemTextBox.Text;
                        notice.Date = DateTime.Now.ToShortDateString();

                        if (!string.IsNullOrEmpty(DHourComboBox.Text) && !string.IsNullOrEmpty(DMinuteComboBox.Text))
                            notice.StopTime = DHourComboBox.Text + ":" + DMinuteComboBox.Text + ":00";

                        notice.Info = notice.Item;
                    }

                    else if (Equals(TypeTabControl.SelectedItem, EventTabItem))
                    {
                        notice.Type = NoticeType.Event;
                        notice.Name = NameTextBox.Text;
                        notice.Description = DescriptionTextBox.Text;
                        notice.Date = DatePicker.Text;
                        if (!string.IsNullOrEmpty(EHourComboBox.Text) && !string.IsNullOrEmpty(EMinuteComboBox.Text))
                            notice.StopTime = EHourComboBox.Text + ":" + EMinuteComboBox.Text + ":00";

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
            if (Equals(TypeTabControl.SelectedItem, MeetingTabItem))
                MVillagerNameComboBox.Focus();

            else if (Equals(TypeTabControl.SelectedItem, DeliveryTabItem))
                DVillagerNameComboBox.Focus();

            else if (Equals(TypeTabControl.SelectedItem, EventTabItem))
                NameTextBox.Focus();
        }
        #endregion
    }
}
