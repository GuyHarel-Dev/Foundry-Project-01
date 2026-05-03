using Microsoft.AspNetCore.Mvc;

using Azure.Identity;
using Azure.AI.Projects;
using Azure.AI.Extensions.OpenAI;
using OpenAI.Responses;
using WebApi.Services;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly IConfiguration _config; 
        private readonly IAzFoundryService _azFoundryService;

        public AiController(IConfiguration config, IAzFoundryService azFoundryService)
        {
            _config = config;
            _azFoundryService = azFoundryService;
        }


        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var response = await _azFoundryService.GetResponseAsync(new WebApi.Models.PromptRequest
            {
                Text = "What is the size of France in square miles?"
            });

            return Ok(response.Text);

        }
    }
}


