using Microsoft.EntityFrameworkCore;
using SurveyUyg.API.Models;

namespace SurveyUyg.API.Repositories
{
    public class QuestionRepository
    {
        private readonly AppDbContext _context;
        public QuestionRepository(AppDbContext context) => _context = context;

        public List<Question> GetList() => _context.Questions.Include(q => q.QuestionOptions).ToList();

        public List<Question> GetBySurveyId(int surveyId) =>
            _context.Questions.Where(q => q.SurveyId == surveyId).Include(q => q.QuestionOptions).ToList();

        public Question GetById(int id) =>
            _context.Questions.Include(q => q.QuestionOptions).FirstOrDefault(q => q.QuestionId == id);

        public void Add(Question question)
        {
            question.Survey = null;

            _context.Questions.Add(question);
            _context.SaveChanges();
        }
        public void Update(Question question) { _context.Questions.Update(question); _context.SaveChanges(); }
        public void Delete(int id)
        {
            var q = _context.Questions.Find(id);
            if (q != null) { _context.Questions.Remove(q); _context.SaveChanges(); }
        }
    }
}