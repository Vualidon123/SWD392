using MongoDB.Driver;
using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;
using System.Linq.Expressions;

namespace SPTS_Reader.Data
{
	public class NotificationRepository : Repository<Notification>, INotificationRepository
	{
		private readonly IMongoCollection<Notification> _collection;

		public NotificationRepository(MongoDbContext context) : base(context)
		{
			_collection = context.Notifications;
		}

		public async Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(Guid userId)
		{
			var filter = Builders<Notification>.Filter.Eq(n => n.UserId, userId);
			return await _collection.Find(filter).ToListAsync();
		}
	}
}
