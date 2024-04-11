using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messanger.Model
{
    public class NotificationManager
    {
        private List<Notification> notifications;

        public NotificationManager()
        {
            notifications = new List<Notification>();
        }

        public void AddNotification(string message)
        {
            Notification notification = new Notification(message);
            notifications.Add(notification);
        }

        public string GetLatestNotification()
        {
            if (notifications.Count > 0)
            {
                return notifications[notifications.Count - 1].ToString();
            }
            else
            {
                return "No notifications available";
            }
        }

        public List<Notification> GetAllNotifications()
        {
            return notifications;
        }
    }

}
