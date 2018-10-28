using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseCatalog.Data.DTO;
using WiseCatalog.Data.Repository;

namespace WiseCatalog.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private SurveyRepository _surveysRepository;
        public SampleDataController(SurveyRepository surveysRepository)
        {
            _surveysRepository = surveysRepository;
        }

        [HttpGet("[action]")]
        public IEnumerable<Survey> Test()
        {
            return _surveysRepository.Surveys().ToList();
        }
    }
}
