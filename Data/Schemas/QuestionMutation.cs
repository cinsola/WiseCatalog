using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiseCatalog.Data.DTO;
using WiseCatalog.Data.Repository;

namespace WiseCatalog.Data.Schemas
{
    public class QuestionMutation : ObjectGraphType
    {
        public QuestionMutation(SurveyRepository _surveyRepository)
        {
            Field<QuestionType>(
                "createQuestion",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<QuestionInputType>> { Name = "question" }
                ),
                resolve: context =>
                {
                    var question = context.GetArgument<Question>("question");
                    return _surveyRepository.AddQuestion(question);
                });
        }
    }
}
