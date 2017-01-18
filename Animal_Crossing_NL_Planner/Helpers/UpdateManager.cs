using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;
using System.Xml;

namespace Animal_Xing_Planner
{
    public static class UpdateManager
    {
        public static string AppVersionInstalled, Version, DownloadLink;

        public static bool CheckforUpdate()
        {
            try
            {
                if (NeedUpdate())
                {
                    if (Globals.MsgBox.Show(Globals.Main, "Newer version available, would you like to download it?", "Update", MessageBoxButton.YesNo, MessageBoxIconType.Question) == MessageBoxResult.Yes)
                        SendToDownload();

                    return true;
                }
            }
            catch (Exception ex) { Globals.Logger.Warn("Update check failed: " + ex.Message); }
            return false;
        }

        public static bool NeedUpdate()
        {
            string URLString = "https://dl.dropboxusercontent.com/u/41918503/updateCheckACNL.xml";

            // build a http web request stream
            var request = WebRequest.Create(URLString);

            // request the cast and build the stream
            var response = request.GetResponse();
            var inputstream = response.GetResponseStream();

            if (inputstream != null)
            {
                var reader = new XmlTextReader(inputstream);
                while (reader.Read())
                {
                    if (reader.NodeType != XmlNodeType.Element) continue;
                    switch (reader.Name)
                    {
                        case "version":
                            {
                                Version = reader.ReadString();
                                break;
                            }
                        case "download":
                            {
                                DownloadLink = reader.ReadString();
                                break;
                            }
                    }
                }

                reader.Close();
            }

            var assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            AppVersionInstalled = fvi.FileVersion;

            return CompareVersions(AppVersionInstalled, Version) < 0;
        }

        public static void SendToDownload()
        {
            try
            {
                Uri download = new Uri(DownloadLink);
                Hyperlink link = new Hyperlink();
                link.NavigateUri = download;
                Process.Start(link.NavigateUri.AbsoluteUri);
            }
            catch (Exception ex)
            {
                Globals.Logger.Warn("Could not open download hyperlink: " + ex.Message);
                Globals.MsgBox.Show(Globals.Main, "Could not open download link " + DownloadLink, "Error", MessageBoxButton.OK, MessageBoxIconType.Error);
            }
        }

        /// <summary>
        /// Compare versions of form "1,2,3,4" or "1.2.3.4". Throws FormatException
        /// in case of invalid version.
        /// </summary>
        /// <param name="strA">the first version</param>
        /// <param name="strB">the second version</param>
        /// <returns>less than zero if strA is less than strB, equal to zero if
        /// strA equals strB, and greater than zero if strA is greater than strB</returns>
        public static int CompareVersions(string strA, string strB)
        {
            var vA = new Version(strA);
            var vB = new Version(strB);

            return vA.CompareTo(vB);
        }
    }
}
