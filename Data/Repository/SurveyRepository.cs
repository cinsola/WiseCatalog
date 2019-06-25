using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WiseCatalog.Data.DTO;

namespace WiseCatalog.Data.Repository
{
    public class SurveyRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public SurveyRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<Dictionary<int, Question>> GetQuestionsByIdAsync(IEnumerable<int> questionsId, CancellationToken token)
        {
            return await _dbContext.Questions.Where(i => questionsId.Contains(i.Id)).ToDictionaryAsync(x => x.Id);
        }

        public async Task<Dictionary<int, Survey>> GetSurveysByIdAsync(IEnumerable<int> surveysId, CancellationToken token)
        {
            return await _dbContext.Surveys.Where(i => surveysId.Contains(i.Id)).ToDictionaryAsync(x => x.Id);
        }

        internal IQueryable<Question> Questions()
        {
            return _dbContext.Questions;
        }
        internal IQueryable<Survey> Surveys()
        {
            return _dbContext.Surveys;
        }
        internal Question GetQuestion(int id)
        {
            return _dbContext.Questions.Find(id);
        }

        internal Survey GetSurvey(int id)
        {
            return _dbContext.Surveys.Find(id);
        }

        internal Question AddQuestion(Question question)
        {
            var item = _dbContext.Questions.Add(question);
            _dbContext.SaveChanges();
            return item.Entity;
        }

        internal IEnumerable<Question> GetQuestionsBySurvey(int id)
        {
            return _dbContext.Questions.Where(x => x.SurveyId == id).ToList();
        }
        internal Survey GetSurveyByQuestion(int id)
        {
            return _dbContext.Questions.Include(x => x.Survey).First(x => x.Id == id).Survey;
        }
    }
}
