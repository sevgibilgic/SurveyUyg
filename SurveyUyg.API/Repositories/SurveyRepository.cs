using Microsoft.EntityFrameworkCore;
using SurveyUyg.API.Models;

namespace SurveyUyg.API.Repositories
{
    public class SurveyRepository
    {
        private readonly AppDbContext _context;
        public SurveyRepository(AppDbContext context) => _context = context;

        public List<Survey> GetList()
        {
            return _context.Surveys
                .Include(s => s.Questions)
                    .ThenInclude(q => q.QuestionOptions)
                .ToList();
        }

        public Survey GetById(int id)
        {
            var survey = _context.Surveys
                .Include(s => s.Questions)
                    .ThenInclude(q => q.QuestionOptions)
                .AsNoTracking()
                .FirstOrDefault(s => s.SurveyId == id);

            return survey;
        }

        public void Add(Survey survey) { _context.Surveys.Add(survey); _context.SaveChanges(); }
        public void Update(Survey survey) { _context.Surveys.Update(survey); _context.SaveChanges(); }
        public void Delete(int id)
        {
            var s = _context.Surveys.Find(id);
            if (s != null) { _context.Surveys.Remove(s); _context.SaveChanges(); }
        }
    }
}