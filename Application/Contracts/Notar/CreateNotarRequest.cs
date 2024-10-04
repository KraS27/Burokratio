namespace Application.DTO.Notar
{
    public record class CreateNotarRequest(string name,
        string division, string country, string city, string street, string postalCode,
        double latitude, double longitude,
        string email,
        string phoneNumber)
    {

    }
}
