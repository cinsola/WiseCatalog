using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiseCatalog.Data.DTO;
using WiseCatalog.Data.Repository;
using WiseCatalog.Data.Schemas;

namespace WiseCatalog.Data.Schemas
{
    public class QuestionQuery : ObjectGraphType
    {
        public QuestionQuery(SurveyRepository _surveyRepository)
        {
            Field<QuestionType>(
                "question",
                    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
                    resolve: context =>
                    _surveyRepository.GetQuestion(context.GetArgument<int>("id")));

            Field<ListGraphType<QuestionType>>(
                "questions",
                resolve: context => _surveyRepository.Questions());

            Field<SurveyType>(
                "survey",
                    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
                    resolve: context =>
                    _surveyRepository.GetSurvey(context.GetArgument<int>("id")));

            Field<ListGraphType<SurveyType>>(
                "surveys",
                resolve: context => _surveyRepository.Surveys());

        }
    }
}
