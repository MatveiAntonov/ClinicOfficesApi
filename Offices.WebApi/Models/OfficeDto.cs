using Offices.Domain.Entities;

namespace Offices.WebApi.Models;

public class OfficeDto
{
    public string Id { get; set; } = String.Empty;
    public Address Address { get; set; } = new Address();
    public string RegistryPhoneNumber { get; set; } = String.Empty;
    public bool IsActive { get; set; }
}