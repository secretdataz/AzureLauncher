using AzureLauncher.Logic;
using AzureLauncher.Pages;
using MahApps.Metro.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AzureLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            InitPage();
            InitAccounts();
            Core.GamePath = Properties.Settings.Default.GamePath;
        }

        private void InitPage()
        {
            PageManager.PageContent = Container;

            //Add Pages
            PageManager.Pages.Add(new LoginPage());
            PageManager.Pages.Add(new ConfigPage());
            //End Pages Add

            PageManager.SwitchPage(typeof(LoginPage));
        }

        private void InitAccounts()
        {
            Core.LoadAccounts();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            PageManager.SwitchPage(typeof(LoginPage));
        }

        private void ConfigBtn_Click(object sender, RoutedEventArgs e)
        {
            PageManager.SwitchPage(typeof(ConfigPage));
        }
    }
}
