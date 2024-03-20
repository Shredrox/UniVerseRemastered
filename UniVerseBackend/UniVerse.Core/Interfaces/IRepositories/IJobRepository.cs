using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IRepositories;

public interface IJobRepository
{
    Task<List<JobOffer>> GetJobs();
    Task<JobOffer?> GetJobOfferById(int id);
    Task<List<JobOffer>> GetJobOfferByTitle(string title);
    Task InsertJobOffer(JobOffer jobOffer);
    Task UpdateJobOffer(JobOffer jobOffer);
    Task DeleteJobOffer(int jobOfferId);
}