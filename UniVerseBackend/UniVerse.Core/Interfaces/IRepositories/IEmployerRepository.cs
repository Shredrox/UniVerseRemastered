using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IRepositories;

public interface IEmployerRepository
{
    Task<Employer?> GetEmployerByName(string username);
}