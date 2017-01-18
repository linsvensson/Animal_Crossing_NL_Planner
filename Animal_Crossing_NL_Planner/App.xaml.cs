using System.Windows;
using System.Windows.Media.Animation;

namespace Animal_Xing_Planner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public sealed partial class App : Application
    {
        private void StartupHandler(object sender, StartupEventArgs e)
        {
            Timeline.DesiredFrameRateProperty.OverrideMetadata(
    typeof(Timeline),
    new FrameworkPropertyMetadata { DefaultValue = 30 });
        }
    }
}