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
    private readonly GenericRepository<Question> _repo;
    private readonly IMapper _mapper;
    private ResultDto _result = new ResultDto();

    public QuestionsController(GenericRepository<Question> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public List<QuestionListDto> List()
    {
        var questions = _repo.GetList();
        return _mapper.Map<List<QuestionListDto>>(questions);
    }

    [AllowAnonymous]
    [HttpGet("BySurvey/{surveyId}")]
    public List<QuestionListDto> GetBySurvey(int surveyId)
    {
        var questions = _repo.GetList().Where(x => x.SurveyId == surveyId).ToList();
        return _mapper.Map<List<QuestionListDto>>(questions);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public ResultDto Add(QuestionCreateDto model)
    {
        var question = _mapper.Map<Question>(model);

        _repo.Add(question);

        _result.Status = true;
        _result.Message = "Soru Başarıyla Eklendi";
        return _result;
    }

    [HttpDelete("{id}")]
    public ResultDto Delete(int id)
    {
        _repo.Delete(id);

        _result.Status = true;
        _result.Message = "Soru Silindi";
        return _result;
    }
}