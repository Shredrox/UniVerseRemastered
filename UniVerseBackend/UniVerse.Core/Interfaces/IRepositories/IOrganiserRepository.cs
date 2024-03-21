using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IRepositories;

public interface IOrganiserRepository
{
    Task<Organiser?> GetOrganiserByName(string username);
}