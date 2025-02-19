namespace Zeiss.ProductApi.Services
{
    public interface ISqlSequenceProvider
    {
        Task<int> GetNextValueForSequenceAsync(string sequenceName);
    }
}
