using System.Windows;
using System.Windows.Documents;

namespace Animal_Xing_Planner
{
    /// <summary>
    /// Interaction logic for AboutUC.xaml
    /// </summary>
    public partial class HelpUC
    {
        public CustomWindow ParentWindow;

        public HelpUC()
        {
            InitializeComponent();

            // Create a FlowDocument
            FlowDocument mcFlowDoc = new FlowDocument();

            // Create a paragraph with text
            Paragraph para = new Paragraph();
            para.Inlines.Add("This application is made to easier keep track of events etc when playing Animal Crossing New Leaf. To start using it, just click the settings button and create a profile.");

            Paragraph para1 = new Paragraph();
            para1.Inlines.Add("* To add a notice on the board, click the + button, and to delete one press the - button.\n\n");
            para1.Inlines.Add("Notices\n");
            para1.Inlines.Add("* 'Meeting' is basically when a villager wants to meet up. You put in the name of the villager, time and place.\n");
            para1.Inlines.Add("* 'Delivery' is used when you need to deliver something to a villager. You put in the name of the villager, item to deliver and time(optional).\n");
            para1.Inlines.Add("* 'Event' can be used for pretty much anything. Here, you can also set a date so that you can for example set up a notice for the next Fishing Tourney.\n");

            Paragraph para2 = new Paragraph();
            para2.Inlines.Add("If you don't like the color scheme you can just go in to 'settings' then click the 'theme'-tab to choose colours.");

            // Add the paragraph to blocks of paragraph
            mcFlowDoc.Blocks.Add(para);
            mcFlowDoc.Blocks.Add(para1);
            mcFlowDoc.Blocks.Add(para2);

            // Set contents
            RichTextBox.Document = mcFlowDoc;
        }

        public void Initialize(CustomWindow parent)
        {
            IsEnabled = true;
            ParentWindow = parent;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            ParentWindow.HideWindow();
        }
    }
}
