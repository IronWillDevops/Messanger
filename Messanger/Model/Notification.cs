using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messanger.Model
{
    // Класс, представляющий уведомление
    public class Notification
    {
        public string Message { get; }
        public Uri Link { get; }

        // Конструктор для текстового уведомления
        public Notification(string message)
        {
            Message = message;
            Link = null;
        }

        // Конструктор для уведомления с ссылкой
        public Notification(string message, Uri linkUrl)
        {
            Message = message;
            Link = linkUrl;
        }
    }

}
