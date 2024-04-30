using Api.Data;
using Api.Models.Entities;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly Semana01Context _db;

    public TaskRepository(Semana01Context db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Tasks>> GetByUserIdAsync(int userId)
    {
        return await _db.Tasks.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
    }
}
