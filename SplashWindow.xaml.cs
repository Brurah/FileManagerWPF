using FileManager.Model;
using FileManager.Pages;
using FileManager.Util;
using SQLite;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace FileManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        DispatcherTimer closeTimer;
        Window page;
        public SplashWindow()
        {
            InitializeComponent();           
        }
        
        private async void CreateDatabase()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Constants.BD_NAME);
            await conn.CreateTableAsync<Folder>();    
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, Constants.BD_NAME)))
            {
                CreateDatabase();                
            }

            page = new MainWindow();
            closeTimer = new DispatcherTimer();
            closeTimer.Tick += closeTimer_Tick;
            closeTimer.Interval = new TimeSpan(0, 0, 3);
            closeTimer.Start();
        }

        void closeTimer_Tick(object sender, System.EventArgs e)
        {
            closeTimer.Stop();            
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            page.Show();
        }      
               

    }
}
