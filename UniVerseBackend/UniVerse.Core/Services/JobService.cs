using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.Entities;
using UniVerse.Core.Exceptions;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerse.Core.Services;

public class JobService(
    IJobRepository jobRepository,
    IUserRepository userRepository) : IJobService
{
    public async Task<List<JobOffer>> GetAllJobs()
    {
        return await jobRepository.GetJobs();
    }

    public async Task<JobOffer?> GetJobById(int jobId)
    {
        return await jobRepository.GetJobOfferById(jobId);
    }

    public async Task<bool> IsAppliedToJob(int jobId, string username)
    {
        var jobOffer = await jobRepository.GetJobOfferById(jobId);
        var user = await userRepository.GetUserByUsername(username);

        if (user is null || jobOffer is null)
        {
            throw new NotFoundException();
        }
        
        return jobOffer.Applicants.Any(a => a.Equals(user));
    }

    public async Task CreateJob(CreateJobOfferRequestDto request)
    {
        var user = await userRepository.GetUserByUsername(request.EmployerName);

        if (user is null)
        {
            throw new NotFoundException();
        }

        var jobOffer = new JobOffer
        {
            Title = request.Title,
            Description = request.Description,
            Location = request.Location,
            Salary = request.Salary,
            Requirements = request.Requirements,
            Type = request.Type,
            Employer = user
        };

        await jobRepository.InsertJobOffer(jobOffer);
    }

    public async Task ApplyToJob(int jobId, string username)
    {
        var jobOffer = await jobRepository.GetJobOfferById(jobId);
        var user = await userRepository.GetUserByUsername(username);

        if (user is null || jobOffer is null)
        {
            throw new NotFoundException();
        }
        
        jobOffer.Applicants.Add(user);
        await jobRepository.UpdateJobOffer(jobOffer);
    }

    public async Task CancelApplicationToJob(int jobId, string username)
    {
        var jobOffer = await jobRepository.GetJobOfferById(jobId);
        var user = await userRepository.GetUserByUsername(username);

        if (user is null || jobOffer is null)
        {
            throw new NotFoundException();
        }
        
        jobOffer.Applicants.Remove(user);
        await jobRepository.UpdateJobOffer(jobOffer);
    }

    public async Task UpdateJob(int jobId, UpdateJobOfferRequestDto request)
    {
        var jobOffer = await jobRepository.GetJobOfferById(jobId);

        if (jobOffer is null)
        {
            throw new NotFoundException();
        }

        if (!string.IsNullOrEmpty(request.Title))
        {
            jobOffer.Title = request.Title;
        }
        if (!string.IsNullOrEmpty(request.Description))
        {
            jobOffer.Description = request.Description;
        }
        if (!string.IsNullOrEmpty(request.Requirements))
        {
            jobOffer.Requirements = request.Requirements;
        }
        if (!string.IsNullOrEmpty(request.Salary))
        {
            jobOffer.Salary = request.Salary;
        }
        if (!string.IsNullOrEmpty(request.Location))
        {
            jobOffer.Location = request.Location;
        }
        if (!string.IsNullOrEmpty(request.Type))
        {
            jobOffer.Type = request.Type;
        }

        await jobRepository.UpdateJobOffer(jobOffer);
    }

    public async Task DeleteJob(int jobId)
    {
        await jobRepository.DeleteJobOffer(jobId);
    }
}