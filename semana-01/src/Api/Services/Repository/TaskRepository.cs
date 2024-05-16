using System.Runtime.CompilerServices;
using Api.Data;
using Api.Models.Domain.Entities;
using Api.Models.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.services.Repository;

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

	public async Task<Tasks> DeleteTask(int id)
	{
		var task = await _db.Tasks.FindAsync(id);

		if (task == null)
		{
			return new Tasks();
		}

		_db.Tasks.Remove(task);
		await _db.SaveChangesAsync();
		return task;
	}

	public async Task<Tasks> AddTask(Tasks task, Account user)
	{
		Tasks newTask =
			new()
			{
				UserId = user.UserId,
				Title = task.Title,
				Description = task.Description,
				DueDate = new DateOnly(),
				Completed = false,
				CratedAt = new DateTime(),
				UpdateAt = new DateTime()
			};

		await _db.AddAsync(newTask);
		await _db.SaveChangesAsync();

		return newTask;
	}
}
