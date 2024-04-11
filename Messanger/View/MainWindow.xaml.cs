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
        private Version currentAppVersion;

        private UpdateChecker updateChecker;
        NotificationManager manager = new NotificationManager();

        public MainWindow()
        {
            InitializeComponent();
            currentAppVersion = GetCurrentAppVersion();
            VersionLabel.Content = $"v{currentAppVersion}";

            updateChecker = new UpdateChecker();
            updateChecker.UpdateAvailable += UpdateChecker_UpdateAvailable;
            CheckForUpdateAsync();
        }

        private void UpdateChecker_UpdateAvailable(object sender, Version latestVersion)
        {
            manager.AddNotification($"New version {latestVersion} is available! Click 'Update' to install.");
            NotificationLabel.Content = manager.GetLatestNotification();
            UpdateButton.Visibility = Visibility.Visible;
        }

        private void CheckForUpdates_Click(object sender, RoutedEventArgs e)
        {
            CheckForUpdateAsync();
        }

        private async void CheckForUpdateAsync()
        {


            // Получаем последнее уведомление

            try
            {
                Version latestVersion = await updateChecker.CheckForUpdateAsync();
                if (latestVersion > currentAppVersion)
                {
                    updateChecker.NotifyUpdateAvailable(latestVersion);
                }
                else
                {
                    manager.AddNotification("You are using the latest version.");
                }
            }
            catch (Exception ex)
            {
                // manager.AddNotification($"Failed to check for updates: {ex.Message}");
            }
            finally
            {
                NotificationLabel.Content = manager.GetLatestNotification();
            }
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(UpdateChecker.GitHubHtmlUrl);
            // Perform update logic here
        }

        private Version GetCurrentAppVersion()
        {
            return new Version(0, 0, 0, 1);
        }
    }



}