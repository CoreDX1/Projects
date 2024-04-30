using Api.Models.Entities;

namespace Api.Services.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<Tasks>> GetByUserIdAsync(int userId);
}

