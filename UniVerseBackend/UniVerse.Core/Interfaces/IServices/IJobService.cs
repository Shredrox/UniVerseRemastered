using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.DTOs.Responses;
using UniVerse.Core.Entities;

namespace UniVerse.Core.Interfaces.IServices;

public interface IJobService
{
    Task<List<JobOfferResponseDto>> GetAllJobs();
    Task<JobOfferResponseDto?> GetJobById(int jobId);
    Task<bool> IsAppliedToJob(int jobId, string username);
    Task CreateJob(CreateJobOfferRequestDto request);
    Task ApplyToJob(int jobId, string username);
    Task CancelApplicationToJob(int jobId, string username);
    Task UpdateJob(int jobId, UpdateJobOfferRequestDto request);
    Task DeleteJob(int jobId);
}