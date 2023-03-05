using AutoMapper.Configuration.Annotations;
using Offices.Domain.Entities;

namespace Offices.WebApi.Models;

public class OfficeDto
{
	public string Id { get; set; } = String.Empty;
    public string RegistryPhoneNumber { get; set; } = String.Empty;
    public bool IsActive { get; set; }
	public string City { get; set; } = string.Empty;
	public string Region { get; set; } = string.Empty;
	public string Street { get; set; } = string.Empty;
	public string PostalCode { get; set; } = string.Empty;
	public int HouseNumber { get; set; }
	public IFormFile PhotoBytes { get; set; }
}