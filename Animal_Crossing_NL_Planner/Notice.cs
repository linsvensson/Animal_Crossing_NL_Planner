using System;
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
        private System.Windows.Media.ImageSource _icon;
        [XmlIgnore]
        public System.Windows.Media.ImageSource Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
            }
        }
        [XmlIgnore]
        [NonSerialized]
        private System.Windows.Media.ImageSource _villagercon;
        [XmlIgnore]
        public System.Windows.Media.ImageSource VillagerIcon
        {
            get
            {
                return _villagercon;
            }
            set
            {
                _villagercon = value;
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
                if (string.IsNullOrEmpty(StopTime)) return;
                if (Globals.Main.NoticeListView.Items.Count == 0) return;

                string timeNow = DateTime.Now.ToString("HH:mm:00");
                TimeSpan stop = TimeSpan.Parse(StopTime);
                TimeSpan now = TimeSpan.Parse(timeNow);

                if (Date != DateTime.Now.ToShortDateString()) return;
                if (stop.CompareTo(now) > 0) return;

                NoticeEventArgs popUp = new NoticeEventArgs(Description);
                NoticeUpdate(this, popUp);
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
        /// Sets the right icon depending on NoticeType and Villager
        /// </summary>
        public void SetIcons()
        {
            try
            {
                string tempName = Name.ToLower();

                switch (Type)
                {
                    case NoticeType.Delivery:
                        Icon = Globals.GetBitmapImage("present", "notice/");
                        VillagerIcon = Globals.GetBitmapImage(tempName, "villagers/", ".gif");
                        break;
                    case NoticeType.Event:
                        Icon = Globals.GetBitmapImage("cracker", "notice/");
                        break;
                    case NoticeType.Birthday:
                        Icon = Globals.GetBitmapImage("cake", "notice/");
                        VillagerIcon = Globals.GetBitmapImage(tempName, "villagers/", ".gif");
                        break;
                    case NoticeType.Meeting:
                        Icon = Globals.GetBitmapImage("coffee", "notice/");
                        VillagerIcon = Globals.GetBitmapImage(tempName, "villagers/", ".gif");
                        break;
                }
            }
            catch (Exception ex) { Globals.Logger.Warn("Could not set notice icon(s): " + ex.Message); }
        }

        public void NoticeUpdate(object sender, NoticeEventArgs events)
        {
            Updated?.Invoke(this, events);
        }
    }
}
