using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyUyg.API.DTOs;
using SurveyUyg.API.Models;
using SurveyUyg.API.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace SurveyUyg.API.Controllers
{
    [Authorize (Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly SurveyRepository _surveyRepository;
        private readonly IMapper _mapper;
        ResultDto _result = new ResultDto();

        public SurveysController(SurveyRepository surveyRepository, IMapper mapper)
        {
            _surveyRepository = surveyRepository;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        public List<SurveyListDto> List()
        {
            var surveys = _surveyRepository.GetList();
            var surveyDtos = _mapper.Map<List<SurveyListDto>>(surveys);
            return surveyDtos;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var survey = _surveyRepository.GetById(id);
            if (survey == null) return NotFound("Anket bulunamadı.");

            var surveyDto = _mapper.Map<SurveyDetailDto>(survey);
            return Ok(surveyDto);
        }

        [HttpPost]
        public ResultDto Add([FromBody] SurveyCreateDto model)
        {
            var survey = _mapper.Map<Survey>(model);

            survey.CreatedAt = DateTime.Now;

            _surveyRepository.Add(survey);

            _result.Status = true;
            _result.Message = "Anket Başarıyla Eklendi";
            return _result;
        }

        [HttpPut]
        public ResultDto Update(SurveyDetailDto model)
        {
            var existingSurvey = _surveyRepository.GetById(model.SurveyId);
            if (existingSurvey == null)
            {
                _result.Status = false;
                _result.Message = "Anket Bulunamadı";
                return _result;
            }

            _mapper.Map(model, existingSurvey);

            _surveyRepository.Update(existingSurvey);

            _result.Status = true;
            _result.Message = "Anket Güncellendi";
            return _result;
        }

        [HttpDelete("{id}")]
        public ResultDto Delete(int id)
        {
            _surveyRepository.Delete(id);
            
            _result.Status = true;
            _result.Message = "Anket Silindi";
            return _result;
        }
    }
}