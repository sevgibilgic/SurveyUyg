using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyUyg.API.DTOs;
using SurveyUyg.API.Models;
using SurveyUyg.API.Repositories;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly QuestionRepository _questionRepository;
    private readonly IMapper _mapper;
    private ResultDto _result = new ResultDto();

    public QuestionsController(QuestionRepository questionRepository, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public List<QuestionListDto> List()
    {
        var questions = _questionRepository.GetList();
        return _mapper.Map<List<QuestionListDto>>(questions);
    }

    [AllowAnonymous]
    [HttpGet("BySurvey/{surveyId}")]
    public List<QuestionListDto> GetBySurvey(int surveyId)
    {
        var questions = _questionRepository.GetList().Where(x => x.SurveyId == surveyId).ToList();
        return _mapper.Map<List<QuestionListDto>>(questions);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ResultDto Add(QuestionCreateDto model)
    {
        var question = _mapper.Map<Question>(model);
        _questionRepository.Add(question);

        _result.Status = true;
        _result.Message = "Soru Başarıyla Eklendi";
        return _result;
    }

    [HttpDelete("{id}")]
    public ResultDto Delete(int id)
    {
        _questionRepository.Delete(id);
        _result.Status = true;
        _result.Message = "Soru Silindi";
        return _result;
    }
}