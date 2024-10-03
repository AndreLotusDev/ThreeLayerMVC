using ThreeLayer.Business.Interfaces;

namespace ThreeLayer.Business.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications = new();
        public Notifier()
        {
            
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public IReadOnlyList<Notification> GetNotificationsAsReadOnly()
        {
            return _notifications.AsReadOnly();
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public bool IsThereAnyNotification()
        {
            return _notifications.Any();
        }
    }
}
