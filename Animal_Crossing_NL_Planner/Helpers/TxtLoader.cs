using System.Collections.Generic;
using System.IO;

namespace Animal_Xing_Planner
{
    public static class TxtLoader
    {
        public static List<string> Load(string path)
        {
            List<string> names = new List<string>();
            string[] parts;

            // Load the file
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs);

            // Read
            string line = reader.ReadToEnd();

            // split the string to a string array
            parts = line.Split(',');

            for (int i = 0; i < parts.Length; i++)
                names.Add(parts[i]);

            return names;
        }
    }
}
