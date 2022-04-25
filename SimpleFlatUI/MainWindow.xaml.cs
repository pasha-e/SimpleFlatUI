using Newtonsoft.Json.Linq;
using SimpleFlatUI.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace SimpleFlatUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileLogger _logger;

        public MainWindow()
        {
            InitializeComponent();

            Init();
        }

        private async void Init()
        {
            _logger = new FileLogger();

            _logger.AddMessage(nameof(MainWindow), new LogMessage(LogMessageType.Information,"Application started"));

            var url = "https://www.cbr-xml-daily.ru/daily_json.js";

            //var tmp = this.DataContext;
            
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }             
        }
    }
}

