using SurveyUyg.API.Models;
using SurveyUyg.API.DTOs;

namespace SurveyUyg.API.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly AppDbContext _context;
        public AnswerRepository(AppDbContext context) => _context = context;

        public bool SaveAnswers(SurveyResponseDto responseDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var response = new Response
                {
                    SurveyId = responseDto.SurveyId,
                    AppUserId = responseDto.UserId,
                    SubmittedAt = DateTime.Now
                };
                _context.Responses.Add(response);
                _context.SaveChanges();

                foreach (var item in responseDto.Answers)
                {
                    var newAnswer = new Answer
                    {
                        QuestionId = item.QuestionId,
                        SelectedOptionId = (item.SelectedOptionId.HasValue && item.SelectedOptionId > 0) ? item.SelectedOptionId : null,
                        AnswerText = item.AnswerText,
                        AppUserId = responseDto.UserId,
                        ResponseId = response.ResponseId,
                        CreatedAt = DateTime.Now
                    };
                    _context.Answers.Add(newAnswer);
                }

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}