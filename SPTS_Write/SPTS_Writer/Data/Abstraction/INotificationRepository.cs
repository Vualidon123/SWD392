﻿using SPTS_Writer.Entities;

namespace SPTS_Writer.Data.Abstraction
{
	public interface INotificationRepository : IRepository<Notification>
	{
		Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(Guid userId);
	}
}
