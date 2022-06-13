using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model
{
    public class NotificationMessage
    {
        public string returnUrl { get; set; }


        public string title { get; set; }


        public string message { get; set; }


        public string status { get; set; }


        public int timeout { get; set; }

    }
    public static class NotificationMessageUI
    {
        public static NotificationMessage Response(int _resultCode, string _message, string _returnUrl = null)
        {
            NotificationMessage notificationMessage = null;
            switch (_resultCode)
            {
                case 100:
                    notificationMessage = Success(_message, _returnUrl);
                    break;
                case 101:
                    notificationMessage = Error(_message);
                    break;
                case 200:
                    notificationMessage = Success(_message, _returnUrl);
                    break;
                case 300:
                    notificationMessage = Warning(_message, _returnUrl);
                    break;
                case 400:
                    notificationMessage = Error(_message);
                    break;
                default:
                    notificationMessage = Warning(_message, _returnUrl);
                    break;
            }
            return notificationMessage;
        }
        private static NotificationMessage Success(string msg = "", string returnUrl = null)
        {
            NotificationMessage result = new NotificationMessage
            {
                returnUrl = (returnUrl ?? string.Empty),
                message = msg,
                title = "Bilgilendirme",
                status = "success",
                timeout = 10
            };
            return result;
        }

        private static NotificationMessage Error(string msg = "İstek işlenirken sorun oluştu. Lütfen tekrar deneyin.")
        {
            NotificationMessage result = new NotificationMessage
            {
                returnUrl = "",
                message = msg,
                title = "Sistem Uyarısı",
                status = "error",
                timeout = 10
            };
            return result;
        }

        private static NotificationMessage Warning(string msg = "", string returnUrl = null)
        {
            NotificationMessage result = new NotificationMessage
            {
                returnUrl = (returnUrl ?? string.Empty),
                message = msg,
                title = "İşlem Eksik Gerçekleşti",
                status = "warning",
                timeout = 10
            };
            return result;
        }
    }


}
