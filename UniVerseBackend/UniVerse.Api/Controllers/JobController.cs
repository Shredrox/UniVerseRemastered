using Microsoft.AspNetCore.Mvc;
using UniVerse.Core.DTOs.Requests;
using UniVerse.Core.Interfaces.IServices;

namespace UniVerseBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController(IJobService jobService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllJobs()
    {
        return Ok(await jobService.GetAllJobs());
    }
    
    [HttpGet("{jobId:int}")]
    public async Task<IActionResult> GetJobById(int jobId)
    {
        return Ok(await jobService.GetJobById(jobId));
    }
    
    [HttpGet("{jobId:int}/is-applied/{username}")]
    public async Task<IActionResult> GetJobIsApplied(int jobId, string username)
    {
        return Ok(await jobService.IsAppliedToJob(jobId, username));
    }
    
    [HttpPost("create-job")]
    public async Task<IActionResult> CreateJob([FromBody] CreateJobOfferRequestDto request)
    {
        await jobService.CreateJob(request);
        return Ok("Job offer created.");
    }
    
    [HttpPost("{jobId:int}/apply/{username}")]
    public async Task<IActionResult> ApplyToJob(int jobId, string username)
    {
        await jobService.ApplyToJob(jobId, username);
        return Ok("Applied to job offer.");
    }
    
    [HttpPost("{jobId:int}/cancel-application/{username}")]
    public async Task<IActionResult> CancelApplicationToJob(int jobId, string username)
    {
        await jobService.CancelApplicationToJob(jobId, username);
        return Ok("Application to job offer canceled.");
    }
    
    [HttpPut("{jobId:int}")]
    public async Task<IActionResult> UpdateJob(int jobId, [FromBody] UpdateJobOfferRequestDto request)
    {
        await jobService.UpdateJob(jobId, request);
        return Ok("Job offer updated.");
    }
    
    [HttpDelete("{jobId:int}")]
    public async Task<IActionResult> DeleteJob(int jobId)
    {
        await jobService.DeleteJob(jobId);
        return Ok("Job offer deleted.");
    }
}