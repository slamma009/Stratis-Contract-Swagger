using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using api.Models;
using api.Services;
using api.StratisResponses;
using Microsoft.AspNetCore.Mvc;
using Mono.Cecil;
using Newtonsoft.Json;

namespace api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ContractsController : Controller
    {        
        [HttpPost]
        public IActionResult GetContractInfo([FromBody] StringRequest request)
        {
            // Call blockchain to get the smart contract code
            string json = WebRequestHelper.GET("http://localhost:38220/api/SmartContracts/code?address=" + request.request);

            // Convert the JSON to a CodeResponse object
            CodeResponse response = JsonConvert.DeserializeObject<CodeResponse>(json);

            if (response == null || string.IsNullOrEmpty(response.bytecode))
                return BadRequest();

            // Grab the byte code from the response object
            string contractByteString = response.bytecode;

            // Convert string to byte array
            byte[] contractByteCode = HexToByteArray(contractByteString);

            List<StratisMethodInfo> result = new List<StratisMethodInfo>();

            using (ModuleDefinition moduleDefinition = ModuleDefinition.ReadModule(new MemoryStream(contractByteCode)))
            {
                // Get the contract definition, and all methods on it
                TypeDefinition contractType = GetContractType(moduleDefinition);
                MethodDefinition[] methods = contractType.Methods.ToArray();


                foreach (MethodDefinition definition in methods)
                {
                    // No need to show the contracts constructor method
                    if (definition.Name != ".ctor")
                    {
                        // Build a new Method Info project
                        StratisMethodInfo newInfo = new StratisMethodInfo(definition.Name);

                        List<StratisParamterInfo> newParamters = new List<StratisParamterInfo>();

                        // Loop through all paramters and add them the list
                        for (var i = 0; i < definition.Parameters.Count; ++i)
                        {
                            newParamters.Add(
                                new StratisParamterInfo(
                                    definition.Parameters[i].ParameterType.Name, 
                                    definition.Parameters[i].Name
                                )
                            );
                        }

                        // Add the paramters list to the method info object. Then add the object to the result list.
                        newInfo.Paramters = newParamters.ToArray();
                        result.Add(newInfo);
                    }
                }
            }
            return Ok(result.ToArray());
        }
        public static byte[] HexToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        private static TypeDefinition GetContractType(ModuleDefinition moduleDefinition)
        {
            return moduleDefinition.Types.FirstOrDefault(x => x.FullName != "<Module>");
        }

    }
}