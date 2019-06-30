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
        private readonly ApplicationDbContextFactory _contextFactory;
        public SurveyRepository(ApplicationDbContextFactory _dbContextFactory)
        {
            this._contextFactory = _dbContextFactory;
        }

        public async Task<Dictionary<int, Question>> GetQuestionsByIdAsync(IEnumerable<int> questionsId, CancellationToken token)
        {
            return await _contextFactory.InvokeSafe(c => c.Questions.Where(i => questionsId.Contains(i.Id)).ToDictionaryAsync(x => x.Id));
        }

        public async Task<Dictionary<int, Survey>> GetSurveysByIdAsync(IEnumerable<int> surveysId, CancellationToken token)
        {
            return await _contextFactory.InvokeSafe(c => c.Surveys.Where(i => surveysId.Contains(i.Id)).ToDictionaryAsync(x => x.Id));
        }

        internal List<Question> Questions()
        {
            return _contextFactory.InvokeSafe(c => c.Questions.ToList());
        }
        internal List<Survey> Surveys()
        {
            return _contextFactory.InvokeSafe(c => c.Surveys.ToList());
        }
        internal Question GetQuestion(int id)
        {
            return _contextFactory.InvokeSafe(c => c.Questions.Find(id));
        }

        internal Survey GetSurvey(int id)
        {
            return _contextFactory.InvokeSafe(c => c.Surveys.Find(id));
        }

        internal Question AddQuestion(Question question)
        {
            using (var _dbContext = _contextFactory.CreateDbContext())
            {
                var item = _dbContext.Questions.Add(question);
                _dbContext.SaveChanges();
                return item.Entity;
            }
        }

        internal List<Question> GetQuestionsBySurvey(int id)
        {
            return _contextFactory.InvokeSafe(c => c.Questions.Where(x => x.SurveyId == id).ToList());
        }
        internal Survey GetSurveyByQuestion(int id)
        {
            return _contextFactory.InvokeSafe(c => c.Questions.Include(x => x.Survey).First(x => x.Id == id).Survey);
        }
    }
}
