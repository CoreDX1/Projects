using Api.Data;
using Api.Models.Entities;
using Api.Services.Interfaces;
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

    public async Task<Account> GetByEmailAndPasswordAsync(string email, string password)
    {
        return await _db.Accounts.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
    }

    public async Task AddAsync(Account account)
    {
        await _db.Accounts.AddAsync(account);
        await _db.SaveChangesAsync();
    }
}
