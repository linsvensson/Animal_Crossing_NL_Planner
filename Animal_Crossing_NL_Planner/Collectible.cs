using System;
using System.ComponentModel;
using static Animal_Xing_Planner.XmlHandler;

namespace Animal_Xing_Planner
{
    [Serializable]
    public class Collectible
    {
        private bool isChecked;
        public bool Checked
        {
            get { return isChecked; }
            set { isChecked = value; OnPropertyChanged("Checked"); }
        }
        public string Name { get; set; }
        public string Month { get; set; }
        public string Location { get; set; }
        public string Value { get; set; }
        public XmlUri Info { get; set; }
        public string ImageURL { get; set; }
        public string Type { get; set; }
        public string Shadow { get; set; }

        public string Text { get; set; }
        public string Image { get; set; }

        public Collectible()
        {
        }

        public Collectible(string name, string month, string location, string value, string shadow, XmlUri info, string imgURL, bool hasGot, string type)
        {
            Name = name;
            Month = month;
            Location = location;
            Value = value;
            Shadow = shadow;
            Info = info;
            ImageURL = imgURL;
            Checked = hasGot;
            Type = type;

            Text = "More Info";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
