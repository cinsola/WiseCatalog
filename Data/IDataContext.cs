using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WiseCatalog.Data.DTO;

namespace WiseCatalog.Data
{
    public interface IDataContext : IDisposable
    {
        IDataWrite<Question> QuestionsReader { get; }
        IDataWrite<Survey> SurveysReader { get; }
        void EnsureSeedData(IServiceScope scope);
        int SaveChanges();
    }
}
