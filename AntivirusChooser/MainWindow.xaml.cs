using AntivirusChooser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;
using System.Xml.Serialization;

namespace AntivirusChooser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Node> Nodes { get; set; }

        public static string ApplicationPath
        {
            get => Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        public MainWindow()
        {
            InitializeComponent();
            StackPanelStart.Visibility = Visibility.Visible;
            StackPanelSurvey.Visibility = Visibility.Collapsed;
            InitializeNodes();
            DataContext = Nodes.First();
            ChangeButtons();
        }

        private void InitializeNodes()
        {
            var formatter = new XmlSerializer(typeof(List<Node>), new XmlRootAttribute("Nodes"));
            var nodesFile = Path.Combine(ApplicationPath, "TreeSolution.xml");
            using (var fs = new FileStream(nodesFile, FileMode.OpenOrCreate))
            {
                Nodes = (List<Node>)formatter.Deserialize(fs);
            }
        }

        private void ChangeButtons()
        {
            var node = (DataContext as Node);
            if (node.Type == NodeType.Question)
            {
                StackPanelYesNo.Visibility = Visibility.Visible;
                StackPanelOk.Visibility = Visibility.Collapsed;
            }
            else
            {
                StackPanelYesNo.Visibility = Visibility.Collapsed;
                StackPanelOk.Visibility = Visibility.Visible;
            }
            TextBlockNodeText.Text = node?.Text;
            ButtonYes.Content = node?.Yes?.Text;
            ButtonNo.Content = node?.No?.Text;
            ButtonOk.Content = "ОК";
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            var node = DataContext as Node;
            DataContext = Nodes.Single(n => n.Id == node.Yes.ToNodeId);
            ChangeButtons();
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            var node = DataContext as Node;
            DataContext = Nodes.Single(n => n.Id == node.No.ToNodeId);
            ChangeButtons();
        }

        private void ButtonStartSurvey_Click(object sender, RoutedEventArgs e)
        {
            StackPanelStart.Visibility = Visibility.Collapsed;
            StackPanelSurvey.Visibility = Visibility.Visible;
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            DataContext = Nodes.Single(n => n.Id == 1);
            ChangeButtons();
            StackPanelStart.Visibility = Visibility.Visible;
            StackPanelSurvey.Visibility = Visibility.Collapsed;
        }
    }
}
