using System;
using System.Windows;

namespace AntivirusChooser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SurveyControl SurveyControl { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            InitializeSurveyControl();
            StackPanelStart.Visibility = Visibility.Visible;
            ContentControlSurvey.Visibility = Visibility.Collapsed;
        }

        private void InitializeSurveyControl()
        {
            SurveyControl = new SurveyControl();
            SurveyControl.SurveyCompletion += ChangeControlsVisibility;
        }

        private void ChangeControlsVisibility(object sender, EventArgs e)
        {
            if (StackPanelStart.Visibility == Visibility.Visible)
            {
                StackPanelStart.Visibility = Visibility.Collapsed;
            }
            else
            {
                StackPanelStart.Visibility = Visibility.Visible;
            }

            if (ContentControlSurvey.Visibility == Visibility.Visible)
            {
                ContentControlSurvey.Visibility = Visibility.Collapsed;
            }
            else
            {
                ContentControlSurvey.Visibility = Visibility.Visible;
            }
        }

        private void ButtonStartSurvey_Click(object sender, RoutedEventArgs e)
        {
            ChangeControlsVisibility(this, EventArgs.Empty);
            ContentControlSurvey.Content = SurveyControl;
        }
    }
}
