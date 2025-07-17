using SPTS_Reader.Entities;

namespace SPTS_Reader.Services.Abstraction
{
	public interface INotificationService
	{
		Task AddNotificationAsync(Notification notification);
		Task<IEnumerable<Notification>> GetAllNotificationsAsync();

		Task<Notification?> GetWelcomeTemplateAsync();

		Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(Guid userId); // 🆕

	}
}
