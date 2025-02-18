using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IDT_Reader
{
    /// <summary>
    /// Interaction logic for ConnectionPage2.xaml
    /// </summary>
    public partial class ConnectionPage2 : Window
    {
        public ConnectionPage2()
        {
            InitializeComponent();
        }

        private void wnd_Connection_Loaded(object sender, RoutedEventArgs e)
        {
            SerialReader.Visibility = Visibility.Collapsed;
            TCP_IP.Visibility = Visibility.Collapsed;
        }

        private void rdbtn_serialreader_Checked(object sender, RoutedEventArgs e)
        {
            
            SerialReader.Visibility = Visibility.Visible;
            TCP_IP.Visibility = Visibility.Collapsed;

            ComBox_ComPort.Items.Clear();
            ComBox_ComPort.Items.Add("AUTO");
            for (int i = 1; i < 21; i++)
                ComBox_ComPort.Items.Add($"COM{i}");
            ComBox_ComPort.SelectedIndex = 0;

            ComBox_BaudRate.ItemsSource =PublicVariables.baudRates;
            ComBox_BaudRate.SelectedIndex = 3;

        }

        private void rdbtn_TCP_Checked(object sender, RoutedEventArgs e)
        {
            SerialReader.Visibility = Visibility.Collapsed;

            TCP_IP.Visibility = Visibility.Visible;
        }

        private void On_Skip(object sender, RoutedEventArgs e)
        {

        }

        private void on_Next(object sender, RoutedEventArgs e)
        {

        }
    }
}
