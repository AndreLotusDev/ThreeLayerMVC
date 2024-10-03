using ThreeLayer.Business.Notifications;

namespace ThreeLayer.Business.Interfaces
{
    public interface INotifier
    {
        bool IsThereAnyNotification();
        List<Notification> GetNotifications();
        IReadOnlyList<Notification> GetNotificationsAsReadOnly();
        void Handle(Notification notification);
    }
}
