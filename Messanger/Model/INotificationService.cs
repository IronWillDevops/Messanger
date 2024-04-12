using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messanger.Model
{
    public interface INotificationService
    {
        void ShowNotification(string notificationMessage, string notificationLinkUrl =null);
       // void ShowNotification(object notificationMessage);
    }
}
