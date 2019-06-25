using GraphQL.DataLoader;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiseCatalog.Data.Repository;

namespace WiseCatalog.Data.DTO
{
    public class Question : HistoricizedDto
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public Survey Survey { get; set; }
        public int SurveyId { get; set; }
        public bool Skippable { get; set; }
    }

    public class QuestionType: HistoricizedDtoType<Question>
    {
        public QuestionType(SurveyRepository _surveyRepository) : base()
        {
            Field(x => x.Id);
            Field(x => x.Order);
            Field(x => x.Name);
            Field(x => x.SurveyId);
            Field(x => x.Skippable);
            Field<SurveyType, Survey>()
                .Name("Survey")
                .Resolve(ctx => {
                    return _surveyRepository.GetSurveyByQuestion(ctx.Source.Id);
                });
        }
    }

    public class QuestionInputType : InputObjectGraphType
    {
        public QuestionInputType()
        {
            Name = "QuestionInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<IntGraphType>>("surveyId");
        }
    }
}
