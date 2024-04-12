using Messanger.Model;
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

namespace Messanger
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UpdateChecker _updateChecker;
        private readonly INotificationService _notificationService;
        public MainWindow()
        {
            InitializeComponent();
            _notificationService = new WpfNotificationService(this);
            _updateChecker = new UpdateChecker(_notificationService);

            CheckForUpdateAsync();
            VersionLabel.Content = $"v{UpdateChecker.AppVersion}";
        }

        private async Task CheckForUpdateAsync()
        {
            try
            {
                Version latestVersion = await _updateChecker.CheckForUpdateAsync();
                if (latestVersion > UpdateChecker.AppVersion)
                {
                    _notificationService.ShowNotification($"New version {latestVersion} is available! Click to download.", UpdateChecker.GitHubHtmlUrl);
                }
                else
                {
                    _notificationService.ShowNotification("You are using the latest version.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

      

    }
}