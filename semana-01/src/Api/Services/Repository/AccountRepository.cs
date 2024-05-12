using Api.Data;
using Api.Models.Domain.Entities;
using Api.Models.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.Repository;

public class AccountRepository : IAccountRepository
{
	private readonly Semana01Context _db;

	public AccountRepository(Semana01Context db)
	{
		_db = db;
	}

	public async Task<IEnumerable<Account>> GetAllAsync()
	{
		return await _db.Accounts.AsNoTracking().ToListAsync();
	}

	public async Task<Account> GetByEmailAndPasswordAsync(Account account)
	{
		return await _db.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Email == account.Email && x.Password == account.Password) ?? null!;
	}

	public async Task<bool> AddAsync(Account account)
	{
		var accountExists = await _db.Accounts.AsNoTracking().AnyAsync(x => x.Email == account.Email);

		if (accountExists)
			return false;

		await _db.Accounts.AddAsync(account);
		await _db.SaveChangesAsync();

		return true;
	}
}
