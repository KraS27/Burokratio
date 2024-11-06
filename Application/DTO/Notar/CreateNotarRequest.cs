namespace Application.DTO.Notar
{
    public record  CreateNotarRequest(
        string Name,
        string Password,
        string Division, string Country, string City, string Street, string PostalCode,
        double Latitude, double Longitude,
        string Email,
        string PhoneNumber)
    {

    }
}
