using Azure.AI.Extensions.OpenAI;
using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenAI.Responses;
using WebApi.Models;
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
            try
            {
                var response = await _azFoundryService.GetResponseAsync(
                    new PromptRequest
                    {
                        Text = "What is the size of France in square miles?"
                    });

                return Ok(response.Text);
            }
            catch (TimeoutException)
            {
                return StatusCode(504, "AI service timeout");
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401, "Unauthorized to access AI service");
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred");
            }

        }
    }
}


