using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using System;
using System.Xml.Linq;

namespace AntivirusChooser
{
    /// <summary>
    /// Interaction logic for SurveyControl.xaml
    /// </summary>
    public partial class SurveyControl : UserControl
    {
        public EventHandler SurveyCompletion;

        private List<Node> Nodes { get; set; }

        public SurveyControl()
        {
            InitializeComponent();
            InitializeNodes();
            DataContext = Nodes.Single(n => n.Id == 1);
            ChangeButtons();
        }

        private void InitializeNodes()
        {
            var formatter = new XmlSerializer(typeof(List<Node>), new XmlRootAttribute("Nodes"));
            var nodesFile = Path.Combine(App.ApplicationPath, "TreeSolution.xml");
            using (var fs = new FileStream(nodesFile, FileMode.OpenOrCreate))
            {
                Nodes = (List<Node>)formatter.Deserialize(fs);
            }
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

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            DataContext = Nodes.Single(n => n.Id == 1);
            ChangeButtons();
            SurveyCompletion?.Invoke(this, EventArgs.Empty);
        }

        private void ChangeButtons()
        {
            var node = DataContext as Node;
            if (node.Type == NodeType.Question)
            {
                StackPanelYesNo.Visibility = Visibility.Visible;
                StackPanelOk.Visibility = Visibility.Collapsed;
                TextBlockChoiceText.Visibility = Visibility.Collapsed;
            }
            else
            {
                StackPanelYesNo.Visibility = Visibility.Collapsed;
                StackPanelOk.Visibility = Visibility.Visible;
                TextBlockChoiceText.Visibility = Visibility.Visible;
            }

            TextBlockNodeText.Text = node?.Text;
            ButtonYes.Content = node?.Yes?.Text;
            ButtonNo.Content = node?.No?.Text;
            ButtonOk.Content = "ОК";
        }
    }
}
