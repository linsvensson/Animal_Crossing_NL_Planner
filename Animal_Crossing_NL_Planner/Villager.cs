using System;
using System.Linq;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Animal_Xing_Planner
{
    [Serializable]
    public class Villager
    {
        public string Name { get; set; }
        public string Personality { get; set; }
        public string Species { get; set; }
        public string Birthday { get; set; }
        [XmlIgnore]
        [NonSerialized]
        private ImageSource icon;
        [XmlIgnore]
        public ImageSource Icon
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

        public Villager()
        {
        }

        public Villager(string name, string personality, string species, string birthday)
        {
            Name = name;
            Personality = personality;
            Species = species;
            Birthday = birthday;

            SetIcon();
        }

        public void SetIcon()
        {
            string tempName = Name.ToLower();
            //if (tempName.Contains())
            Icon = Globals.GetBitmapImage(tempName, "villagers/", ".gif");
        }
    }
}
