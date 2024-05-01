using Api.Models.Domain.Entities;

namespace Api.Models.Domain.Interfaces;

public interface IAccountRepository
{
    Task<IEnumerable<Account>> GetAllAsync();
    Task<Account?> GetByEmailAndPasswordAsync(Account account);
    Task<bool> AddAsync(Account account);
}
