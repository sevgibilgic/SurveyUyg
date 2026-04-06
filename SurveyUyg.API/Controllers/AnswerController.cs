using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyUyg.API.DTOs;
using SurveyUyg.API.Repositories;

[Authorize (Roles = "Admin,Member")]
[ApiController]
[Route("api/[controller]")]
public class AnswersController : ControllerBase
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IMapper _mapper;

    public AnswersController(IAnswerRepository answerRepository, IMapper mapper)
    {
        _answerRepository = answerRepository;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult PostAnswers([FromBody] SurveyResponseDto responseDto)
    {
        if (responseDto == null || !responseDto.Answers.Any())
            
            return BadRequest("Cevaplar boş olamaz.");

        _answerRepository.SaveAnswers(responseDto);

        return Ok(new { message = "Anket başarıyla tamamlandı!" });
    }
}