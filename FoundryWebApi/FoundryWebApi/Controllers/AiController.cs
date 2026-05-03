using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly IConfiguration _config; 
        private readonly IAzFoundryChatService _azFoundryChatService;

        public AiController(IConfiguration config, IAzFoundryChatService azFoundryService)
        {
            _config = config;
            _azFoundryChatService = azFoundryService;
        }

        [HttpPost("prompt")]
        public async Task<IActionResult> Prompt([FromBody] PromptRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.TextRequest))
            {
                return BadRequest("Prompt text cannot be empty.");
            }
            var response = await _azFoundryChatService.GetResponseAsync(request);
            return Ok(response);
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            try
            {
                var response = await _azFoundryChatService.GetResponseAsync(
                    new PromptRequest
                    {
                        TextRequest = "What is the size of France in square miles?"
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


