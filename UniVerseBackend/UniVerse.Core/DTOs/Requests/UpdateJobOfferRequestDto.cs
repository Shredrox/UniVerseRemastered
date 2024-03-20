namespace UniVerse.Core.DTOs.Requests;

public record UpdateJobOfferRequestDto(
    string Title, 
    string Description, 
    string Requirements, 
    string Location, 
    string Type, 
    string Salary);