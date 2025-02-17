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

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            
            SerialReader.Visibility = Visibility.Visible;
            TCP_IP.Visibility = Visibility.Collapsed;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            // Hide previous content, such as ComboBox and other elements inside SerialReader
            SerialReader.Visibility = Visibility.Collapsed;

            TCP_IP.Visibility = Visibility.Visible;

           

        }


    }
}
