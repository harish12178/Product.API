using Microsoft.EntityFrameworkCore;
using Zeiss.ProductApi.Data;

namespace Zeiss.ProductApi.Services
{
    public class SqlSequenceProvider : ISqlSequenceProvider
    {
        private readonly ProductDbContext _context;

        public SqlSequenceProvider(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetNextValueForSequenceAsync(string sequenceName)
        {
            return await _context.Database.SqlQueryRaw<int>($"SELECT NEXT VALUE FOR {sequenceName};").SingleAsync();
        }
    }
}
