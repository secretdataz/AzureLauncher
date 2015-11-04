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
using AzureLauncher.Logic;
using System.Collections.ObjectModel;

namespace AzureLauncher.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            DataContext = new ViewModel();
            RememberIdBox.IsChecked = Properties.Settings.Default.RemId;
            RememberPwdBox.IsChecked = Properties.Settings.Default.RemPwd;
            switch (Properties.Settings.Default.param)
            {
                case "1rag1":
                    rag.IsChecked = true;
                    break;
                case "1sak1":
                    sak.IsChecked = true;
                    break;
                default:
                    none.IsChecked = true;
                    break;                
            }
        }

        public void ToggleLoginBtn()
        {
            LoginBtn.IsEnabled = !LoginBtn.IsEnabled;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.LastId = IdBox.Text;
            Properties.Settings.Default.RemId = (bool)RememberIdBox.IsChecked;
            Properties.Settings.Default.RemPwd = (bool)RememberPwdBox.IsChecked;
            Properties.Settings.Default.Save();
            var acc = new Account();
            acc.Id = IdBox.Text;
            acc.Password = PwdBox.Password;

            if (RememberIdBox.IsChecked == true)
            {
                if (RememberPwdBox.IsChecked == false) acc.Password = null;
                Core.Remember(acc);
            }

            var param = "";
            if(rag.IsChecked == true)
            {
                param = "1rag1";
            }else if(sak.IsChecked == true)
            {
                param = "1sak1";
            }

            Core.LaunchGame(acc.Id, acc.Password, param);
        }

        private void ReplayBtn_Click(object sender, RoutedEventArgs e)
        {
            var param = "";
            if (rag.IsChecked == true)
            {
                param = "1rag1";
            }
            else if (sak.IsChecked == true)
            {
                param = "1sak1";
            }
            Core.LaunchReplay(param);
        }

        private void RememberId_Checked(object sender, RoutedEventArgs e)
        {
            RememberPwdBox.IsEnabled = true;
        }

        private void RememberId_Unchecked(object sender, RoutedEventArgs e)
        {
            RememberPwdBox.IsEnabled = false;
            RememberPwdBox.IsChecked = false;
        }

        private void IdBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var value = comboBox.SelectedItem as String;
            UpdatePwdBox(value);
        }

        public void UpdatePwdBox(string Id)
        {
            var acc = Core.Accounts.FirstOrDefault(x => x.Id == Id);
            if (acc != null)
            {
                IdBox.Text = acc.Id;
                PwdBox.Password = acc.Password;
            }
        }

        public string GetId()
        {
            return IdBox.Text;
        }

        public void SetId(string id)
        {
            IdBox.Text = id;
        }
        public class ViewModel
        {
            public ObservableCollection<string> CmbContent { get; set; }

            public ViewModel()
            {
                CmbContent = Core.Ids;
            }
        }
    }
}
