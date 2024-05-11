using Api.Models.Domain.Entities;

namespace Api.Models.Domain.Interfaces;

public interface ITaskRepository
{
	Task<IEnumerable<Tasks>> GetByUserIdAsync(int userId);
	public Task<Tasks> DeleteTask(int id);
}
