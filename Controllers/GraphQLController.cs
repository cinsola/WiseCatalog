using System;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using WiseCatalog.Data;
using WiseCatalog.Data.DTO;

namespace wise_catalog.Controllers
{
    [Route("[controller]")]
    public class GraphQLController : Controller
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;
        //private readonly DataLoaderDocumentListener _documentListener;

        public GraphQLController(ISchema schema, IDocumentExecuter documentExecuter /*,DataLoaderDocumentListener documentListener*/)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
            //_documentListener = documentListener;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLRequest query)
        {
            if (query == null) { throw new ArgumentNullException(nameof(query)); }
            var inputs = query.Variables.ToInputs();
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query,
                Inputs = inputs
            };
            //executionOptions.Listeners.Add(_documentListener);
            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);
            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}