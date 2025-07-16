using SPTS_Writer.Data.Abstraction;
using SPTS_Writer.Entities;

namespace SPTS_Writer.Data
{
	public class NotificationRepository : Repository<Notification>, INotificationRepository
	{
		public NotificationRepository(MongoDbContext context) : base(context)
		{
		}
	}
}
