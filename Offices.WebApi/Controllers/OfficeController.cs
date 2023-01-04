using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Offices.Application.Interfaces;
using Offices.Domain.Entities;
using Offices.WebApi.Models;

namespace Offices.WebApi.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class OfficeController : ControllerBase
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IMapper _mapper;
    public OfficeController(IOfficeRepository officeRepository, IMapper mapper)
    {
        _officeRepository = officeRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OfficeDto>>> GetAll()
    {
        var offices = await _officeRepository.GetAllOffices(default(CancellationToken));
        if (offices is not null)
        {
            var officesDto = new List<OfficeDto>();
            foreach (var office in offices)
            {
                officesDto.Add(_mapper.Map<OfficeDto>(office));
            }
            return Ok(officesDto);
        }
        else
        {
            return NoContent();
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OfficeDto>> GetService(string id)
    {
        var office = await _officeRepository.GetOffice(id, default(CancellationToken));
        if (office is not null)
        {
            var officeDto = _mapper.Map<OfficeDto>(office);
            return Ok(officeDto);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<ActionResult<OfficeDto>> CreateService([FromForm] OfficeDto officeDto)
    {
        var officeEntity = _mapper.Map<Office>(officeDto);

        var office = await _officeRepository.CreateOffice(officeEntity, default(CancellationToken));
        if (office is not null)
        {
            var officeReturn = _mapper.Map<OfficeDto>(office);
            return Created($"/services/{officeReturn.Id}", officeReturn);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<ActionResult<OfficeDto>> UpdateService([FromForm] OfficeDto officeDto)
    {
        var officeEntity = _mapper.Map<Office>(officeDto);

        var office = await _officeRepository.UpdateOffice(officeEntity, default(CancellationToken));
        if (office is not null)
        {
            var officeResult = _mapper.Map<OfficeDto>(office);
            return Ok(officeResult);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<OfficeDto>> DeleteService(string id)
    {
        var service = await _officeRepository.DeleteOffice(id, default(CancellationToken));
        if (service is not null)
        {
            var serviceResult = _mapper.Map<OfficeDto>(service);
            return Ok(service);
        }
        else
        {
            return BadRequest();
        }
    }
}
