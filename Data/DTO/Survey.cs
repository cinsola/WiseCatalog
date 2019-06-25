using GraphQL.DataLoader;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiseCatalog.Data.Repository;

namespace WiseCatalog.Data.DTO
{
    public class Survey : HistoricizedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public ICollection<Question> Questions { get; set; }
    }

    public class SurveyType : HistoricizedDtoType<Survey>
    {
        public SurveyType(SurveyRepository _surveyRepository)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field<ListGraphType<QuestionType>, IEnumerable<Question>>()
                .Name("Questions")
                .Resolve(ctx => {
                    return _surveyRepository.GetQuestionsBySurvey(ctx.Source.Id);
                });
        }
    }

}
