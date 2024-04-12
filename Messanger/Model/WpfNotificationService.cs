using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messanger.Model
{
    public class WpfNotificationService : INotificationService
    {
        private readonly MainWindow _mainWindow;

        public WpfNotificationService(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void ShowNotification(string message, string link = null)
        {
            if (link != null)
            {
                var hyperlink = new System.Windows.Documents.Hyperlink
                {
                    NavigateUri = new Uri(link)
                };
                hyperlink.Inlines.Add(message);
                hyperlink.RequestNavigate += (sender, e) =>
                {
                    System.Diagnostics.Process.Start(e.Uri.ToString());
                };
                _mainWindow.NotificationLabel.Content = hyperlink;
            }
            else
            {
                _mainWindow.NotificationLabel.Content = message;
            }
        }
    }

}
