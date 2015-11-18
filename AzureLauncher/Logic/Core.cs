using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using AzureLauncher.Pages;
using System.Windows;
using System.IO;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace AzureLauncher.Logic
{
    internal class Core
    {
        internal static string GamePath = "";
        internal static List<Account> Accounts = new List<Account>();
        internal static ObservableCollection<String> Ids;

        internal static void Remember(Account acc)
        {
            var existing = Accounts.FirstOrDefault(x => x.Id == acc.Id);
            if (existing != null)
            {
                Accounts.RemoveAll(x => x.Id == acc.Id);
            }
            Accounts.Add(acc);
            SaveAccounts();
            RefreshCmb();
        }

        internal static void RefreshCmb()
        {
            Ids = new ObservableCollection<string>();
            foreach(var acc in Accounts)
            {
                Ids.Add(acc.Id);
            }
            var lp = PageManager.GetPage(typeof(LoginPage)) as LoginPage;
            var vm = new LoginPage.ViewModel();
            vm.CmbContent = Ids;
            lp.DataContext = vm;
        }

        internal static void SaveAccounts()
        {
            var json = JsonConvert.SerializeObject(Accounts);
            File.WriteAllText("azure_accounts.json", json);
        }

        internal static void LoadAccounts()
        {
            try
            {
                var json = File.ReadAllText("azure_accounts.json");
                Accounts = JsonConvert.DeserializeObject<List<Account>>(json);
            }
            catch
            { }
            RefreshCmb();
            var lp = PageManager.GetPage(typeof(LoginPage)) as LoginPage;
            lp.SetId(Properties.Settings.Default.LastId);
            lp.UpdatePwdBox(lp.GetId());
        }

        internal static void LaunchGame(string Id, string Password, string Param)
        {
            if (!CheckGame()) return;
            SaveParam(Param);
            var PrcStartInfo = new ProcessStartInfo();
            PrcStartInfo.Arguments = "-t:" + Password + " " + Id + " " + Param;
            PrcStartInfo.FileName = GamePath;
            PrcStartInfo.UseShellExecute = true;
            PrcStartInfo.WorkingDirectory = Path.GetDirectoryName(GamePath);
            PrcStartInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                using (Process exe = Process.Start(PrcStartInfo))
                {
                    /*var loginPage = PageManager.GetPage(typeof(LoginPage)) as LoginPage;
                    loginPage.ToggleLoginBtn();
                    exe.WaitForExit();
                    loginPage.ToggleLoginBtn();*/                    
                }
            }
            catch
            {

            }
        }

        internal static void LaunchReplay(string param)
        {
            if (!CheckGame()) return;
            SaveParam(param);
            var PrcStartInfo = new ProcessStartInfo();
            PrcStartInfo.Arguments = "-t:Replay " + param;
            PrcStartInfo.FileName = GamePath;
            PrcStartInfo.UseShellExecute = true;
            PrcStartInfo.WorkingDirectory = Path.GetDirectoryName(GamePath);
            PrcStartInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                Process.Start(PrcStartInfo);
            }
            catch { }
        }

        internal static void SaveParam(string Param)
        {
            Properties.Settings.Default.param = Param;
            Properties.Settings.Default.Save();
        }

        internal static bool CheckGame()
        {
            if(String.IsNullOrEmpty(GamePath))// || !File.Exists(GamePath))
            {
                MessageBox.Show("ไม่พบตัวเกม กรุณาตั้งค่าที่อยู่ของตัวเกมก่อน");
                return false;
            }
            return true;
        }
    }
}
