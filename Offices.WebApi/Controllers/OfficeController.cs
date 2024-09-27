using AutoMapper;
using Events;
using MassTransit;
using MassTransit.Clients;
using MassTransit.Transports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Offices.Application.Interfaces;
using Offices.Domain.Entities;
using Offices.WebApi.Models;
using Photos.Application.Interfaces;

namespace Offices.WebApi.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class OfficeController : ControllerBase
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IPhotoRepository _photoRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<OfficeController> _logger;
	private readonly IRequestClient<PhotoAdded> _requestClient;


	public OfficeController(IOfficeRepository officeRepository, IPhotoRepository photoRepository, IMapper mapper, ILogger<OfficeController> logger,
		IRequestClient<PhotoAdded> requestClient)
    {
        _officeRepository = officeRepository;
        _photoRepository = photoRepository;
        _mapper = mapper;
        _logger = logger;
		_requestClient = requestClient;
	}

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OfficeDto>>> GetAll()
    {
        _logger.LogInformation("Fetching all office models from the storage");

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
            _logger.LogInformation("Storage is empty");

            return NoContent();
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OfficeDto>> GetOffice(string id)
    {
        _logger.LogInformation($"Fetching office model with id: {id} from the storage");

        var office = await _officeRepository.GetOffice(id, default(CancellationToken));
        if (office is not null)
        {
            var officeDto = _mapper.Map<OfficeDto>(office);
            return Ok(officeDto);
        }
        else
        {
            _logger.LogInformation($"No office with id: {id} in the storage");

            return BadRequest();
        }
    }

    [HttpPost]
    //[Authorize(Roles = "Receptionist")]
    public async Task<ActionResult<OfficeDto>> CreateOffice([FromForm] OfficeDto officeDto)
    {
        _logger.LogInformation($"Posting new office model to the storage");

        if (officeDto == null)
            return BadRequest();


        var officeEntity = _mapper.Map<Office>(officeDto);

		byte[] photoDto = { };

		using (var ms = new MemoryStream())
		{
			officeDto.PhotoBytes.CopyTo(ms);
			photoDto = ms.ToArray();
		}

		var photoRequest = new PhotoAdded
		{
			PhotoName = officeDto.PhotoBytes.FileName,
			PhotoData = photoDto
		};

		var photoResponse = await _requestClient.GetResponse<PhotoAddedResponse>(photoRequest);

		if (photoResponse != null)
        {
			var photo = new Photo
			{
				Id = photoResponse.Message.Id,
				PhotoUrl = photoResponse.Message.PhotoUrl,
				PhotoName = photoResponse.Message.PhotoName
			};

			await _photoRepository.CreatePhoto(photo, default(CancellationToken));

            officeEntity.PhotoId = photo.Id;
		}

		var office = await _officeRepository.CreateOffice(officeEntity, default(CancellationToken));
        if (office is not null)
        {
            var officeReturn = _mapper.Map<OfficeDto>(office);
            return Created($"/services/{officeReturn.Id}", officeReturn);
        }
        else
        {
            _logger.LogError($"Something went wrong while adding new office model to the storage");

            return BadRequest();
        }
    }

    [HttpPut]
    //[Authorize(Roles = "Receptionist")]
    public async Task<ActionResult<OfficeDto>> UpdateOffice([FromForm] OfficeDto officeDto)
    {
        _logger.LogInformation($"Updating office model with id: {officeDto.Id} in the storage");

        if (officeDto == null)
            return BadRequest();

        var officeEntity = _mapper.Map<Office>(officeDto);

        var office = await _officeRepository.UpdateOffice(officeEntity, default(CancellationToken));
        if (office is not null)
        {
            var officeResult = _mapper.Map<OfficeDto>(office);
            return Ok(officeResult);
        }
        else
        {
            _logger.LogError($"Something went wrong while updating office model in the storage");

            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Receptionist")]
    public async Task<ActionResult<OfficeDto>> DeleteOffice(string id)
    {
        _logger.LogInformation($"Deleting office model with id: {id} in the storage");

        if (_officeRepository.GetOffice(id, default(CancellationToken)) == null)
        {
            return BadRequest();
        };

        var service = await _officeRepository.DeleteOffice(id, default(CancellationToken));
        if (service is null)
        {
            return Ok(id);
        }
        else
        {
            _logger.LogError($"Something went wrong while deleting office model from the storage");

            return BadRequest();
        }
    }
}
