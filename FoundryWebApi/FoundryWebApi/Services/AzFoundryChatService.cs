using Azure.AI.Projects;
using Azure.Identity;
using OpenAI.Responses;
using System.ClientModel;
using WebApi.Models;

#pragma warning disable OPENAI001 // pour using OpenAI.Responses;

namespace WebApi.Services
{
    public class AzFoundryChatService : IAzFoundryChatService
    {
        private readonly IConfiguration _config;
        private readonly AIProjectClient _projectClient;
        private readonly string? _modelName;
        private readonly ILogger<AzFoundryChatService> _logger;

        public AzFoundryChatService(
            IConfiguration config, 
            AIProjectClient projectClient,
            ILogger<AzFoundryChatService> logger)
        {
            _config = config;
            _projectClient = projectClient;
            _logger = logger;

            _modelName = _config["Foundry-Project-01:ModelName"]
                ?? throw new InvalidOperationException("ModelName not configured");

        }

        public async Task<PromptResponse> GetResponseAsync(PromptRequest request)
        {
            _logger.LogInformation("Sending prompt to Azure AI Foundry");

            var client = _projectClient
                .ProjectOpenAIClient
                .GetProjectResponsesClientForModel(_modelName);

            ClientResult<ResponseResult> result = await client.CreateResponseAsync(
                request.TextRequest);

            var response = result.Value.GetOutputText();

            return new PromptResponse
            {
                Text = response
            };
        }
    }
}

