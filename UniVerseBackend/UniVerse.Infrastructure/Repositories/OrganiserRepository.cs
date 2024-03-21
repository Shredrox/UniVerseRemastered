using Microsoft.EntityFrameworkCore;
using UniVerse.Core.Entities;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Infrastructure.Data;

namespace UniVerse.Infrastructure.Repositories;

public class OrganiserRepository(UniVerseDbContext context) : IOrganiserRepository
{
    public async Task<Organiser?> GetOrganiserByName(string username)
    {
        return await context.Organisers
            .FirstOrDefaultAsync(e => e.User.UserName == username);
    }
}