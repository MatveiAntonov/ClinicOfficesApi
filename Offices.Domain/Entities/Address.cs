namespace Offices.Domain.Entities
{
    public class Address
    {
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public int HouseNumber { get; set; }
    }
}