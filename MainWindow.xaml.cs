using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IDT_Reader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<DemoDataModel> DemoData { get; set; }
        public MainWindow()
        {
            InitializeComponent();  // Only call this once.
            DataContext = this;

            // Generate demo data
            DemoData = new ObservableCollection<DemoDataModel>();
            for (int i = 1; i <= 100; i++) // Add 100 rows for testing scrollbar
            {
                DemoData.Add(new DemoDataModel
                {
                    SerialNumber = i,
                    EPC = $"EPC-{i:000}",
                    ReadCount = i * 10,
                    RSSI = $"{-60 + i % 5} dBm",
                    TimeStamp = $"{System.DateTime.Now.AddMilliseconds(i * 10):HH:mm:ss.fff}"
                });
            }
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;

            if (dataGrid != null)
            {
                // After a row is clicked, make the DataGrid readonly to disable editing
                dataGrid.IsReadOnly = true;
            }
        }

        private void on_Read(object sender, RoutedEventArgs e)
        {

        }

        private void on_Clear(object sender, RoutedEventArgs e)
        {

        }

        private void on_Export(object sender, RoutedEventArgs e)
        {

        }

        private void on_Disconnect(object sender, RoutedEventArgs e)
        {

        }

        private void on_Weather(object sender, RoutedEventArgs e)
        {

        }

        private void on_Setting(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void on_ByEPC(object sender, RoutedEventArgs e)
        {

        }

        private void on_WithoutEPC(object sender, RoutedEventArgs e)
        {

        }

        private void on_Reset(object sender, RoutedEventArgs e)
        {

        }

        private void on_Detect(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {

        }
    }

    public class DemoDataModel
    {
        public int SerialNumber { get; set; }
        public string EPC { get; set; }
        public int ReadCount { get; set; }
        public string RSSI { get; set; }
        public string TimeStamp { get; set; }

    }


       
    }

