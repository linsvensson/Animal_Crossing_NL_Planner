using System;
using System.Collections.Generic;

namespace Animal_Xing_Planner
{
    [Serializable]
    public class Profile
    {
        public string Mayor { get; set; }
        public string Town { get; set; }
        public string Fruit { get; set; }
        public string FC { get; set; }
        public string DC { get; set; }
        public string TagLine { get; set; }
        public string ProfileImagePath { get; set; }
        public List<Villager> Villagers { get; set; }
        public List<Collectible> Collectibles { get; set; }
        public List<Notice> Notices { get; set; }

        public Profile(string mayor, string town, string fruit, string fc, string dc, string tagline, List<Villager> villagers)
        {
            Mayor = mayor;
            Town = town;
            Fruit = fruit;
            FC = fc;
            DC = dc;
            TagLine = tagline;
            Villagers = villagers;

            Collectibles = new List<Collectible>();
            Notices = new List<Notice>();
        }

        public Profile()
        {
            FC = "0000-0000-0000";
            DC = "0000-0000-0000";
            Mayor = string.Empty;
            Town = string.Empty;
        }
    }
}
