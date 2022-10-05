using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Cityinfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Security.Cryptography.X509Certificates;
using Cityinfo.API.Services;
using AutoMapper;

namespace Cityinfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase

    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, 
            IMailService mailService,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? 
                throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? 
                throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ?? 
                throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointofInterestDto>>> GetPointsOfInterest(
            int cityId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation(
                $"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();
            }
            var pointsOfInterestForCity = await _cityInfoRepository
                     .GetPointsOfInterestForCityAsync(cityId);
            return Ok(_mapper.Map<IEnumerable<PointofInterestDto>>(pointsOfInterestForCity));
            }
        
            
        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointofInterestDto>> GetPointOfInterest(
            int cityId, int pointOfInterestId)
        {
           if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }
            var pointOfInterest = await _cityInfoRepository
                 .GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointofInterestDto>(pointOfInterest)); 
        }

        [HttpPost]
        public async Task<ActionResult<PointofInterestDto>> CreatePointOfIntesert(
            int cityId,
            PointOfInterestForCreationDto pointOfInterest)
        {
           if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = _mapper.Map < Entities.PointOfInterest>(pointOfInterest);

            await _cityInfoRepository.AddPointOfInterestForCityAsync(
                cityId, finalPointOfInterest);

            await _cityInfoRepository.SaveChangesAsync();

            var createdPointOfInterestToReturn =
                _mapper.Map<Models.PointofInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = createdPointOfInterestToReturn.Id
                },
                createdPointOfInterestToReturn);
        }
        //[HttpPut("{pointofinterestid}")]
        //public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId,
        //    PointOfInterestForUpdateDto pointOfInterest)
        //{
        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
        //    var pointOfInterestFromStore = city.PointsofInterest
        //        .FirstOrDefault(c => c.Id == pointOfInterestId);
        //    if (pointOfInterestFromStore == null)
        //    {
        //        return NotFound();
        //    }

        //    pointOfInterestFromStore.Name = pointOfInterest.Name;
        //    pointOfInterestFromStore.Description = pointOfInterest.Description;

        //    return NoContent();
        //}

        //[HttpPatch("{pointofinterestid}")]
        //public ActionResult PartiallyUpdatePointOfInterst(
        //    int cityId, int pointOfInterestId,
        //    JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        //{
        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
        //    var pointOfInterestFromStore = city.PointsofInterest
        //        .FirstOrDefault(c => c.Id == pointOfInterestId);
        //    if (pointOfInterestFromStore == null)
        //    {
        //        return NotFound();
        //    }
        //    var pointOfInterestToPatch =
        //        new PointOfInterestForUpdateDto()
        //        {
        //            Name = pointOfInterestFromStore.Name,
        //            Description = pointOfInterestFromStore.Description
        //        };

        //    patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (!TryValidateModel(pointOfInterestToPatch))
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
        //    pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;

        //    return NoContent();

        //}

        //[HttpDelete("{pointofinterestid}")]
        //public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        //{
        //    var city = _citiesDataStore.Cities
        //        .FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
        //    var pointOfInterestFromStore = city.PointsofInterest
        //       .FirstOrDefault(c => c.Id == pointOfInterestId);
        //    if (pointOfInterestFromStore == null)
        //    {
        //        return NotFound();
        //    }

        //    city.PointsofInterest.Remove(pointOfInterestFromStore);
        //    _mailService.Send(
        //        "Point of interst deleted.",
        //        $"Point of interest {pointOfInterestFromStore.Name} with id {pointOfInterestFromStore.Id} was deleted.");
        //    return NoContent();
        //}
    }
}
