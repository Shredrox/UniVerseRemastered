namespace UniVerse.Core.DTOs.Requests;

public record CreateJobOfferRequestDto(
    string Title, 
    string Company,
    string Description, 
    string Requirements, 
    string Location, 
    string Type, 
    string Salary, 
    string EmployerName);
