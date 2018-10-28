using GraphQL;
using GraphQL.Types;
using WiseCatalog.Data.Schemas;

namespace WiseCatalog.Data
{
    public class ApplicationMutableSchema : Schema
    {
        public ApplicationMutableSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<QuestionQuery>();
            Mutation = resolver.Resolve<QuestionMutation>();
        }
    }
}
