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

        public Dictionary<int, Question> GetQuestionsByIdAsync(IEnumerable<int> questionsId, CancellationToken token)
        {
            return _contextFactory.InvokeSafe(c => c.QuestionsReader.Where(i => questionsId.Contains(i.Id)).ToDictionary(x => x.Id));
        }

        public Dictionary<int, Survey> GetSurveysByIdAsync(IEnumerable<int> surveysId, CancellationToken token)
        {
            return _contextFactory.InvokeSafe(c => c.SurveysReader.Where(i => surveysId.Contains(i.Id)).ToDictionary(x => x.Id));
        }

        internal List<Question> Questions()
        {
            return _contextFactory.InvokeSafe(c => c.QuestionsReader.ToList());
        }
        internal List<Survey> Surveys()
        {
            return _contextFactory.InvokeSafe(c => c.SurveysReader.ToList());
        }
        internal Question GetQuestion(int id)
        {
            return _contextFactory.InvokeSafe(c => c.QuestionsReader.FirstOrDefault(x => x.Id == id));
        }

        internal Survey GetSurvey(int id)
        {
            return _contextFactory.InvokeSafe(c => c.SurveysReader.FirstOrDefault(x => x.Id == id));
        }

        internal Question AddQuestion(Question question)
        {
            using (var _dbContext = _contextFactory.CreateDbContext())
            {
                var item = _dbContext.QuestionsReader.AddEntry(question);
                _dbContext.SaveChanges();
                return item;
            }
        }

        internal Question EditQuestion(int questionId, Question questionUpdated)
        {
            using (var _dbContext = _contextFactory.CreateDbContext())
            {
                var item = _dbContext.QuestionsReader.FirstOrDefault(x => x.Id == questionId);
                if (item.Name != questionUpdated.Name) { item.Rename(questionUpdated.Name); }
                _dbContext.SaveChanges();
                return item;
            }
        }

        internal List<Question> GetQuestionsBySurvey(int id)
        {
            return _contextFactory.InvokeSafe(c => c.QuestionsReader.Where(x => x.SurveyId == id).ToList());
        }
        internal Survey GetSurveyByQuestion(int id)
        {
            return _contextFactory.InvokeSafe(c => c.QuestionsReader.OriginalDbSet.Include(x => x.Survey).First(x => x.Id == id).Survey);
        }
    }
}
