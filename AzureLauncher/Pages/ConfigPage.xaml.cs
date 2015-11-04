using AzureLauncher.Logic;
using Microsoft.Win32;
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

namespace AzureLauncher.Pages
{
    /// <summary>
    /// Interaction logic for ConfigPage.xaml
    /// </summary>
    public partial class ConfigPage : Page
    {
        public ConfigPage()
        {
            InitializeComponent();
        }

        private void BrowseBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.DefaultExt = ".exe";
            ofd.Filter = "Ragnarok Online Client (.exe)|*.exe";

            Nullable<bool> result = ofd.ShowDialog();

            if(result == true)
            {
                var file = ofd.FileName;
                PathBox.Text = file;
                Core.GamePath = file;
                Properties.Settings.Default.GamePath = file;
                Properties.Settings.Default.Save();
            }
        }
    }
}
