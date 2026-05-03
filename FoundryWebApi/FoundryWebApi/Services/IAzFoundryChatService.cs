using WebApi.Models;

namespace WebApi.Services
{
    public interface IAzFoundryChatService
    {
        Task<PromptResponse> GetResponseAsync(PromptRequest request, CancellationToken cancellationToken = default);
    }
}
