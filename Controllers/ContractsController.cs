using System;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class ContractsController : Controller
    {
        // GET api/contacts
        [HttpGet]
        public IActionResult Get()
        {
            var result = new [] {
                new { 
                    MethodName = "TestMethod", 
                    Paramters = new [] {
                        new {
                            ParamType = "int", 
                            ParamName = "TestParameter"
                        } 
                    } 
                }
            };

            return Ok(result);
        }
    }
}