using Azure.AI.Extensions.OpenAI;
using Azure.AI.Projects;
using Azure.Identity;
using OpenAI.Responses;
using System.ClientModel;
using WebApi.Models;

#pragma warning disable OPENAI001 // pour using OpenAI.Responses;

namespace WebApi.Services
{
    public class AzFoundryService : IAzFoundryService
    {
        private readonly IConfiguration _config;
        private readonly AIProjectClient _projectClient;
        private readonly string? _modelName;
        private readonly ILogger<AzFoundryService> _logger;

        public AzFoundryService(
            IConfiguration config, 
            ILogger<AzFoundryService> logger)
        {
            _config = config;
            _logger = logger;

            _modelName = _config["Foundry-Project-01:ModelName"]
                ?? throw new InvalidOperationException("ModelName not configured");

            var endpoint = _config["Foundry-Project-01:Endpoint"]
                ?? throw new InvalidOperationException("Endpoint not configured");

            _projectClient = new AIProjectClient(
                endpoint: new Uri(endpoint),
                tokenProvider: new DefaultAzureCredential()
             );

        }

        public async Task<PromptResponse> GetResponseAsync(PromptRequest request)
        {
            _logger.LogInformation("Sending prompt to Azure AI Foundry");

            var client = _projectClient
                .ProjectOpenAIClient
                .GetProjectResponsesClientForModel(_modelName);

            ClientResult<ResponseResult> result = await client.CreateResponseAsync(
                request.Text);

            var response = result.Value.GetOutputText();

            return new PromptResponse
            {
                Text = response
            };
        }
    }
}

