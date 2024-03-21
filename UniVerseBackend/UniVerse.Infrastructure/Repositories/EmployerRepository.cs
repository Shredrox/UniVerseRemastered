using Microsoft.EntityFrameworkCore;
using UniVerse.Core.Entities;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Infrastructure.Data;

namespace UniVerse.Infrastructure.Repositories;

public class EmployerRepository(UniVerseDbContext context) : IEmployerRepository
{
    public async Task<Employer?> GetEmployerByName(string username)
    {
        return await context.Employers
            .FirstOrDefaultAsync(e => e.User.UserName == username);
    }
}