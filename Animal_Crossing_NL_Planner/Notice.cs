using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Animal_Xing_Planner
{
    [Serializable]
    public enum NoticeType
    {
        Meeting,
        Delivery,
        Event,
        Birthday,
    }

    [Serializable]
    public class NoticeEventArgs : EventArgs
    {
        public string Message { get; set; }

        public NoticeEventArgs(string message)
        {
            Message = message;
        }
    }

    [Serializable]
    public class Notice
    {
        #region Members;
        [XmlIgnore]
        [NonSerialized]
        private System.Windows.Media.ImageSource icon;
        [XmlIgnore]
        public System.Windows.Media.ImageSource Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
            }
        }
        public string Name { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Item { get; set; }
        public string StopTime { get; set; }
        public NoticeType Type { get; set; }

        public string Info { get; set; }

        public delegate void NoticeHandler(object sender, NoticeEventArgs events);
        [field: NonSerialized]
        public event NoticeHandler Updated;
        #endregion

        public Notice()
        {
            Name = string.Empty;
            Place = string.Empty;
            Description = string.Empty;
            Date = string.Empty;
            Item = string.Empty;
            StopTime = string.Empty;
            Info = string.Empty;
        }

        public void Update()
        {
            try
            {
                if (!string.IsNullOrEmpty(StopTime))
                {
                    if (Globals.Main.noticeListView.Items.Count != 0)
                    {
                        string timeNow = DateTime.Now.ToString("HH:mm:00");
                        TimeSpan stop = TimeSpan.Parse(StopTime);
                        TimeSpan now = TimeSpan.Parse(timeNow);

                        if (Date == DateTime.Now.ToShortDateString())
                        {
                            if (stop.CompareTo(now) <= 0)
                            {
                                NoticeEventArgs popUp = new NoticeEventArgs(Description);
                                NoticeUpdate(this, popUp);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Globals.Logger.Error("Error updating notice: " + ex.Message); }
        }

        /// <summary>
        /// Unsubscribe from the update event
        /// </summary>
        public void Unsubscribe()
        {
            Updated -= Globals.Main.notice_Updated;
        }

        /// <summary>
        /// Sets the right icon depending on NoticeType
        /// </summary>
        public void SetIcon()
        {
            try
            {
                switch (Type)
                {
                    case NoticeType.Delivery:
                        Icon = Globals.GetBitmapImage("present", "notice/");
                        break;
                    case NoticeType.Event:
                        Icon = Globals.GetBitmapImage("cracker", "notice/");
                        break;
                    case NoticeType.Birthday:
                        Icon = Globals.GetBitmapImage("cake", "notice/");
                        break;
                    default:
                        Icon = Globals.GetBitmapImage("coffee", "notice/");
                        break;
                }
            }
            catch (Exception ex) { Globals.Logger.Warn("Could not set notice icon: " + ex.Message); }
        }

        public void NoticeUpdate(object sender, NoticeEventArgs events)
        {
            Updated?.Invoke(this, events);
        }
    }
}
