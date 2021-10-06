using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Swagger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
            Log.Information("Values Initialize");
            ValueSamples.Initialize();
        }

        // GET api/values  
        [HttpGet]
        public ActionResult<Dictionary<int, string>> Get()
        {
            Log.Information("Getting all the values");
            return ValueSamples.MyValue;
        }

        // GET api/values/5  

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            Log.Information("Getting Data with the id " + id);
            return ValueSamples.MyValue.GetValueOrDefault(id);
        }

        // POST api/values  
        [HttpPost]
        public void Post([FromBody] string value)
        {

            Log.Information("Inserting value with the data =" + value);
            var maxKey = ValueSamples.MyValue.Max(x => x.Key);

            ValueSamples.MyValue.Add(maxKey + 1, value);
        }

        // PUT api/values/5  
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

            ValueSamples.MyValue.Add(id, value);
        }

        // DELETE api/values/5  
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ValueSamples.MyValue.Remove(id);
        }
    }
}
