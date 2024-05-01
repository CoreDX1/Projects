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

    public async Task<Account?> GetByEmailAndPasswordAsync(Account account)
    {
        var foundAccount = await _db.Accounts.FirstOrDefaultAsync(x => x.Email == account.Email && x.Password == account.Password);

        return foundAccount;
    }

    public async Task<bool> AddAsync(Account account)
    {
        var accountExists = await _db.Accounts.AnyAsync(x => x.Email == account.Email);

        if (accountExists)
            return false;

        await _db.Accounts.AddAsync(account);
        await _db.SaveChangesAsync();

        return true;
    }
}
