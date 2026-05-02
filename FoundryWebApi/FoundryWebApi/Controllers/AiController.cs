using Microsoft.AspNetCore.Mvc;

using Azure.Identity;
using Azure.AI.Projects;
using Azure.AI.Extensions.OpenAI;
using OpenAI.Responses;

#pragma warning disable OPENAI001 // pour using OpenAI.Responses;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly IConfiguration _config; 

        public AiController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var ProjectEndpoint = _config["Foundry:ProjectEndpoint"] ?? "";

            AIProjectClient projectClient = new(
                endpoint: new Uri(ProjectEndpoint),
                tokenProvider: new DefaultAzureCredential());

            ProjectResponsesClient responseClient = projectClient.ProjectOpenAIClient.GetProjectResponsesClientForModel("gpt-4.1-mini"); // supports all Foundry direct models
            ResponseResult response = await responseClient.CreateResponseAsync(
                "What is the size of France in square miles?");

            return Ok(response.GetOutputText());
        }
    }
}


