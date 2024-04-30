using Api.Models.Entities;

namespace Api.Services.Interfaces;

public interface IAccountRepository
{
    Task<IEnumerable<Account>> GetAllAsync();
    Task<Account> GetByEmailAndPasswordAsync(string email, string password);
    Task AddAsync(Account account);
}
