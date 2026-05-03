using WebApi.Models;

namespace WebApi.Services
{
    public interface IAzFoundryService
    {
        Task<PromptResponse> GetResponseAsync(PromptRequest request);
    }
}
