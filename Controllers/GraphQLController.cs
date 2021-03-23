using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BooksApi.GraphQL;

namespace BooksApi.Controllers
{
    [Route("[controller]")]  
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private ISchema _schema { get; }
        private IDocumentExecuter _documentExecuter { get; }
        private IDocumentWriter _documentWriter { get; }

        public GraphQLController(
            ISchema schema, 
            IDocumentExecuter documentExecuter, 
            IDocumentWriter documentWriter
        )
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
            _documentWriter = documentWriter;
        }

        [HttpPost]  
        public async Task<IActionResult> PostAsync([FromBody] GraphQLQuery query)  
        {  
            try
            {
                if (query == null) 
                    throw new ArgumentNullException(nameof(query)); 
                
                var varsDict = new Dictionary<string, object>();
                if (query.Variables != null)
                    varsDict = query.Variables.ToObject<Dictionary<string, object>>();

                var inputs = InputsExtensions.ToInputs(varsDict);
                var executionOptions = new ExecutionOptions  
                {  
                    Schema = _schema,  
                    Query = query.Query,
                    Inputs = inputs  
                };
    
                var result = await _documentExecuter.ExecuteAsync(executionOptions);  
    
                if (result.Errors?.Count > 0)  
                    return BadRequest(result);

                var value = await _documentWriter.WriteToStringAsync(result);
                return Ok(JsonConvert.DeserializeObject<JObject>(value));  
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

    }
}