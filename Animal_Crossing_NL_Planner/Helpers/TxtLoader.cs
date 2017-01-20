using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Animal_Xing_Planner
{
    public static class TxtLoader
    {
        public static List<string> Load(string path)
        {
            // Load the file
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs);

            // Read
            string line = reader.ReadToEnd();

            // split the string to a string array
            var parts = line.Split(',');

            return parts.ToList();
        }
    }
}
