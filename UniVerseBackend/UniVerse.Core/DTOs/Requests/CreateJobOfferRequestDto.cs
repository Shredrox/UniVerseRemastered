namespace UniVerse.Core.DTOs.Requests;

public record CreateJobOfferRequestDto(
    string Title, 
    string Description, 
    string Requirements, 
    string Location, 
    string Type, 
    string Salary, 
    string EmployerName);
