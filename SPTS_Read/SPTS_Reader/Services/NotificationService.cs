using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;
using SPTS_Reader.Services.Abstraction;

namespace SPTS_Reader.Services
{
	public class NotificationService : INotificationService
	{
		private readonly INotificationRepository _notificationRepository;

		public NotificationService(INotificationRepository notificationRepository)
		{
			_notificationRepository = notificationRepository;
		}

		public async Task AddNotificationAsync(Notification notification)
		{
			await _notificationRepository.AddAsync(notification);
			await _notificationRepository.SaveChangesAsync();
		}

		public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
		{
			return await _notificationRepository.GetAllAsync();
		}

		// 🆕 Tìm thông báo mẫu để ghép tên user
		public async Task<Notification?> GetWelcomeTemplateAsync()
		{
			var notifications = await _notificationRepository.GetAllAsync();
			return notifications.FirstOrDefault(); // Lấy thông báo đầu tiên
		}
		public async Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(Guid userId)
		{
			return await _notificationRepository.GetNotificationsByUserIdAsync(userId);
		}

	}

}
