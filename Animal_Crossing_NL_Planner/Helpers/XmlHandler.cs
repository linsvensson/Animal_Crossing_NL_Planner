using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Windows;
using System.Xml.Schema;

namespace Animal_Xing_Planner
{
    /// <summary>
    /// Handles serializing and deserializing Xml
    /// </summary>
    public static class XmlHandler
    {
        #region Loading
        /// <summary>
        /// Loads villagers.xml
        /// </summary>
        public static List<Villager> LoadVillagers()
        {
            List<Villager> nodes = new List<Villager>();
            List<XmlVillager> xmlObjects = new List<XmlVillager>();

            XmlSerializer serializer = new XmlSerializer(xmlObjects.GetType());
            if (!File.Exists(@"data/villagers.xml"))
            {
                if (Globals.MsgBox.Show(null, "Could not load villagers.xml, make sure it's in the data folder", "Fatal Error", MessageBoxButton.OK, MessageBoxIconType.Error) == MessageBoxResult.OK)
                {
                    Environment.Exit(0);
                    return null;
                }
            }

            StreamReader stream = new StreamReader(@"data/villagers.xml");
            try { xmlObjects = (List<XmlVillager>)serializer.Deserialize(stream); }
            catch (Exception ex)
            {
                Globals.MsgBox.Show(null, "Could not load villagers.xml, make sure it's in the data folder", "Warning", MessageBoxButton.OK, MessageBoxIconType.Warning);
                Globals.Logger.Fatal("Could not load villagers.xml, make sure it's in the data folder " + ex.Message);
            }
            finally { stream.Close(); }

            for (int i = 0; i < xmlObjects.Count; i++)
            {
                Villager tempNode = new Villager(xmlObjects[i].Name, xmlObjects[i].Personality, xmlObjects[i].Species, xmlObjects[i].Birthday);
                nodes.Add(tempNode);
            }

            Globals.TakeOutGarbage();

            return nodes;
        }

        /// <summary>
        /// Loads various collectibles
        /// </summary>
        public static List<Collectible> LoadCollectibles()
        {
            List<Collectible> nodes = new List<Collectible>();

            // Bugs
            List<XmlBug> xmlBugs = new List<XmlBug>();
            XmlSerializer serializer = new XmlSerializer(xmlBugs.GetType());
            StreamReader stream = new StreamReader(@"data/bugs.xml");
            if (!File.Exists(@"data/bugs.xml"))
            {
                if (Globals.MsgBox.Show(null, "Could not load bugs.xml, make sure it's in the data folder", "Fatal Error", MessageBoxButton.OK, MessageBoxIconType.Error) == MessageBoxResult.OK)
                {
                    Environment.Exit(0);
                    return null;
                }
            }

            try { xmlBugs = (List<XmlBug>)serializer.Deserialize(stream); }
            catch (Exception ex)
            {
                Globals.MsgBox.Show(null, "Could not load bugs.xml, make sure it's in the data folder", "Warning", MessageBoxButton.OK, MessageBoxIconType.Warning);
                Globals.Logger.Fatal("Could not load bugs.xml, make sure it's in the data folder " + ex.Message);
            }
            finally { stream.Close(); }

            for (int i = 0; i < xmlBugs.Count; i++)
            {
                XmlUri link = new Uri(xmlBugs[i].Info);
                Collectible tempNode = new Collectible(xmlBugs[i].Name, xmlBugs[i].Month, xmlBugs[i].Location, xmlBugs[i].Value, "n/a", link, xmlBugs[i].ImageURL, xmlBugs[i].HasGot, "Bug");
                nodes.Add(tempNode);
            }

            // Fish
            List<XmlFish> xmlFish = new List<XmlFish>();
            serializer = new XmlSerializer(xmlFish.GetType());
            stream.Dispose();
            stream = new StreamReader(@"data/fish.xml");
            if (!File.Exists(@"data/fish.xml"))
            {
                if (Globals.MsgBox.Show(null, "Could not load fish.xml, make sure it's in the data folder", "Fatal Error", MessageBoxButton.OK, MessageBoxIconType.Error) == MessageBoxResult.OK)
                {
                    Environment.Exit(0);
                    return null;
                }
            }

            try { xmlFish = (List<XmlFish>)serializer.Deserialize(stream); }
            catch (Exception ex)
            {
                Globals.MsgBox.Show(null, "Could not load fish.xml, make sure it's in the data folder", "Warning", MessageBoxButton.OK, MessageBoxIconType.Warning);
                Globals.Logger.Fatal("Could not load fish.xml, make sure it's in the data folder " + ex.Message);
            }
            finally { stream.Close(); }

            for (int i = 0; i < xmlFish.Count; i++)
            {
                XmlUri link = new Uri(xmlFish[i].Info);
                Collectible tempNode = new Collectible(xmlFish[i].Name, xmlFish[i].Month, xmlFish[i].Location, xmlFish[i].Value, xmlFish[i].Shadow, link, xmlFish[i].ImageURL, xmlFish[i].HasGot, "Fish");
                nodes.Add(tempNode);
            }

            // Seafood
            List<XmlSeafood> xmlSeafood = new List<XmlSeafood>();
            serializer = new XmlSerializer(xmlSeafood.GetType());
            stream = new StreamReader(@"data/seafood.xml");
            if (!File.Exists(@"data/seafood.xml"))
            {
                if (Globals.MsgBox.Show(null, "Could not load seafood.xml, make sure it's in the data folder", "Fatal Error", MessageBoxButton.OK, MessageBoxIconType.Error) == MessageBoxResult.OK)
                {
                    Environment.Exit(0);
                    return null;
                }
            }

            try { xmlSeafood = (List<XmlSeafood>)serializer.Deserialize(stream); }
            catch (Exception ex)
            {
                Globals.MsgBox.Show(null, "Could not load seafood.xml, make sure it's in the data folder", "Warning", MessageBoxButton.OK, MessageBoxIconType.Warning);
                Globals.Logger.Fatal("Could not load seafood.xml, make sure it's in the data folder " + ex.Message);
            }
            finally { stream.Close(); }
            stream.Dispose();

            for (int i = 0; i < xmlSeafood.Count; i++)
            {
                XmlUri link = new Uri(xmlSeafood[i].Info);
                Collectible tempNode = new Collectible(xmlSeafood[i].Name, xmlSeafood[i].Month, xmlSeafood[i].Location, xmlSeafood[i].Value, xmlSeafood[i].Shadow, link, xmlSeafood[i].ImageURL, xmlSeafood[i].HasGot, "Seafood");
                nodes.Add(tempNode);
            }

            Globals.TakeOutGarbage();

            return nodes;
        }

        /// <summary>
        /// Loads profiles.xml
        /// </summary>
        public static List<Profile> LoadProfiles()
        {
            List<Profile> nodes = new List<Profile>();
            List<XmlProfile> xmlObjects = new List<XmlProfile>();

            XmlSerializer serializer = new XmlSerializer(xmlObjects.GetType());
            string dir = @"saveData";
            StreamReader stream = new StreamReader(dir + "/profiles.xml");

            try { xmlObjects = (List<XmlProfile>)serializer.Deserialize(stream); }
            catch (Exception ex)
            {
                Globals.MsgBox.Show(null, "Could not load profiles.xml, make sure it's in the saveData folder", "Warning", MessageBoxButton.OK, MessageBoxIconType.Warning);
                Globals.Logger.Fatal("Could not load profiles.xml, make sure it's in the saveData folder " + ex.Message);
                //Globals.SetProfile(null);
            }

            finally
            {
                stream.Close();
            }

            for (int i = 0; i < xmlObjects.Count; i++)
            {
                if (string.IsNullOrEmpty(xmlObjects[i].Fruit))
                    xmlObjects[i].Fruit = "cherry";
                Profile tempNode = new Profile(xmlObjects[i].Mayor, xmlObjects[i].Town, xmlObjects[i].Fruit, xmlObjects[i].FC, xmlObjects[i].DC, xmlObjects[i].TagLine, xmlObjects[i].Villagers);
                tempNode.ProfileImagePath = xmlObjects[i].ProfileImagePath;
                tempNode.Collectibles = xmlObjects[i].Collectibles;
                tempNode.Notices = xmlObjects[i].Notices;
                nodes.Add(tempNode);
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Villagers.ForEach(item => item.SetIcon());
                nodes[i].Notices.ForEach(item => item.SetIcon());
            }

            return nodes;
        }
        #endregion

        #region Saving
        /// <summary>
        /// Saves profiles.xml
        /// </summary>
        public static void Save(List<Profile> profiles)
        {
            if (profiles == null)
                return;
            if (profiles.Count == 0)
                return;

            List<XmlProfile> xmlObjects = new List<XmlProfile>();

            for (int i = 0; i < profiles.Count; i++)
            {
                XmlProfile tempNode = new XmlProfile();
                if (string.IsNullOrEmpty(profiles[i].Fruit))
                    profiles[i].Fruit = "cherry";

                tempNode.Initialize(profiles[i].Mayor, profiles[i].Town, profiles[i].Fruit, profiles[i].FC, profiles[i].DC, profiles[i].TagLine, profiles[i].Villagers);
                tempNode.ProfileImagePath = profiles[i].ProfileImagePath;
                tempNode.Notices = profiles[i].Notices;
                tempNode.Collectibles = profiles[i].Collectibles;
                xmlObjects.Add(tempNode);
            }

            string dir = @"saveData";

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            XmlSerializer serializer = new XmlSerializer(xmlObjects.GetType());
            StreamWriter stream = new StreamWriter(dir + "/profiles.xml");

            try { serializer.Serialize(stream, xmlObjects); }
            catch (Exception ex)
            {
                Globals.MsgBox.Show(null, "Could not save profiles.xml, check the log for more details", "Warning", MessageBoxButton.OK, MessageBoxIconType.Warning);
                Globals.Logger.Error("Could not save profiles.xml " + ex.Message);
            }
            finally { stream.Close(); }
        }
        #endregion

        #region Other needed xml crap
        public class XmlProfile
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

            public XmlProfile()
            {
            }

            public void Initialize(string mayor, string town, string fruit, string fc, string dc, string tagline, List<Villager> villagers)
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
        }

        public class XmlVillager
        {
            public string Name;
            public string Personality;
            public string Species;
            public string Birthday;

            public XmlVillager()
            {
            }

            public void Initialize(string name, string personality, string species, string birthday)
            {
                Name = name;
                Personality = personality;
                Species = species;
                Birthday = birthday;
            }
        }

        public class XmlCollectible
        {
            public bool HasGot;
            public string Name;
            public string Month;
            public string Info;
            public string ImageURL;
            public string Location;
            public string Value;
            public string Shadow;

            public XmlCollectible()
            {
            }

            public void Initialize(string name, string month, string location, string value, string shadow, string info, string imgURL, bool hasGot)
            {
                Name = name;
                Month = month;
                Location = location;
                Info = info;
                Shadow = shadow;
                ImageURL = imgURL;
                Value = value;
                HasGot = hasGot;
            }
        }

        [Serializable]
        public class XmlUri
        {
            private Uri _Value;

            public XmlUri() { }
            public XmlUri(Uri source) { _Value = source; }

            public static implicit operator Uri(XmlUri o)
            {
                return o == null ? null : o._Value;
            }

            public static implicit operator XmlUri(Uri o)
            {
                return o == null ? null : new XmlUri(o);
            }

            public XmlSchema GetSchema()
            {
                return null;
            }

            public void ReadXml(XmlReader reader)
            {
                _Value = new Uri(reader.ReadElementContentAsString());
            }

            public void WriteXml(XmlWriter writer)
            {
                writer.WriteValue(_Value.ToString());
            }
        }

        public class XmlBug : XmlCollectible
        {
            public XmlBug()
            { }
        }

        public class XmlFish : XmlCollectible
        {
            public XmlFish()
            { }
        }

        public class XmlSeafood : XmlCollectible
        {
            public XmlSeafood()
            { }
        }
        #endregion
    }
}
