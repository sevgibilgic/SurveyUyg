using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyUyg.API.DTOs;
using SurveyUyg.API.Models;
using SurveyUyg.API.Repositories;

namespace SurveyUyg.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly GenericRepository<Survey> _repo;
        private readonly IMapper _mapper;
        private readonly ResultDto _result = new ResultDto();

        public SurveysController(GenericRepository<Survey> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("ListActive")]
        public List<SurveyListDto> ListActive()
        {
            var surveys = _repo.GetList();
            return _mapper.Map<List<SurveyListDto>>(surveys);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var testIds = _repo.GetQueryable().Select(x => x.SurveyId).ToList();

            var survey = _repo.GetQueryable()
                              .Include(x => x.Questions)
                              .FirstOrDefault(x => x.SurveyId == id);

            if (survey == null)
            {
                return NotFound(new
                {
                    Hata = $"ID {id} bulunamadı.",
                    VeritabanındakiMevcutIDler = testIds
                });
            }

            return Ok(survey);
        }
        [HttpPost]
        public ResultDto Add(SurveyCreateDto model)
        {
            var survey = _mapper.Map<Survey>(model);

            survey.AppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            survey.CreatedAt = DateTime.Now;

            _repo.Add(survey);
            return new ResultDto { Status = true, Message = "Tamamlandı" };
        }

        [HttpPut]
        public ResultDto Update(SurveyDetailDto model)
        {
            var existingSurvey = _repo.GetById(model.SurveyId);
            if (existingSurvey == null)
            {
                _result.Status = false;
                _result.Message = "Anket Bulunamadı";
                return _result;
            }

            _mapper.Map(model, existingSurvey);
            _repo.Update(existingSurvey);

            _result.Status = true;
            _result.Message = "Anket Güncellendi";
            return _result;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var survey = _repo.GetById(id);
                if (survey == null) return NotFound("Anket zaten yok.");

                _repo.Delete(id);
                return Ok(new { status = true, message = "Silindi" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, message = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [Authorize]
        [HttpGet("MySurveys")]
        public IActionResult GetMySurveys()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Kullanıcı kimliği bulunamadı.");
            }

            var mySurveys = _repo.GetQueryable()
                                 .Where(x => x.AppUserId == userId)
                                 .ToList();

            var dto = _mapper.Map<List<SurveyListDto>>(mySurveys);
            return Ok(dto);
        }
    }
}