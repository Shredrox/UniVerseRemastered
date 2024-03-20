using Microsoft.EntityFrameworkCore;
using UniVerse.Core.Entities;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Infrastructure.Data;

namespace UniVerse.Infrastructure.Repositories;

public class JobRepository(UniVerseDbContext context) : IJobRepository
{
    public async Task<List<JobOffer>> GetJobs()
    {
        return await context.JobOffers.ToListAsync();
    }

    public async Task<JobOffer?> GetJobOfferById(int id)
    {
        return await context.JobOffers.FindAsync(id);
    }

    public async Task<List<JobOffer>> GetJobOfferByTitle(string title)
    {
        return await context.JobOffers
            .Where(j => j.Title == title)
            .ToListAsync();
    }

    public async Task InsertJobOffer(JobOffer jobOffer)
    {
        context.JobOffers.Add(jobOffer);
        await context.SaveChangesAsync();
    }

    public async Task UpdateJobOffer(JobOffer jobOffer)
    {
        context.Entry(jobOffer).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteJobOffer(int jobOfferId)
    {
        var jobOffer = await context.JobOffers.FindAsync(jobOfferId);
        
        if (jobOffer is null)
        {
            throw new NotFoundException();
        }
        
        context.JobOffers.Remove(jobOffer);
        await context.SaveChangesAsync();
    }
}