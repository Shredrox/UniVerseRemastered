namespace UniVerse.Core.DTOs.Responses;

public record JobOfferResponseDto(
    int Id,
    string Title,
    string Company,
    string Description,
    string Requirements,
    string Location,
    string Type,
    string Salary);