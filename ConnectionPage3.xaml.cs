﻿using System;
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
    /// </summary>
    public partial class ConnectionPage3 : Window
    {
        public ConnectionPage3()
        {
            InitializeComponent();
        }

        private void on_changereader(object sender, RoutedEventArgs e)
        {
            ConnectionPage2 ChangeReader = new ConnectionPage2();
            this.Content = ChangeReader;
            //ChangeReader.Show();
        }

        private void on_connect(object sender, RoutedEventArgs e)
        {
            
        }

        private void on_next(object sender, RoutedEventArgs e)
        {
            MainWindow ReadeWindow = new MainWindow(); 
            ReadeWindow.Show();
        }
    }
}
