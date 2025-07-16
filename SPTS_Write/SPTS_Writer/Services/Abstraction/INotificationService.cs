using SPTS_Writer.Entities;

namespace SPTS_Writer.Services.Abstraction
{
	public interface INotificationService
	{
		Task AddNotificationAsync(Notification notification);
		Task<IEnumerable<Notification>> GetAllNotificationsAsync();

		Task<Notification?> GetWelcomeTemplateAsync();
	}
}
