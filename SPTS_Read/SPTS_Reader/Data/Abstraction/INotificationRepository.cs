using SPTS_Reader.Entities;

namespace SPTS_Reader.Data.Abstraction
{
	public interface INotificationRepository : IRepository<Notification>
	{
		Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(Guid userId);
	}
}
