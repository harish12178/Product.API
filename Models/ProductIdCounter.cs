using System.ComponentModel.DataAnnotations;

namespace Zeiss.ProductApi.Models
{
    public class ProductIdCounter
    {
        public int Id { get; set; }
        public int LastGeneratedId { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; } // RowVersion column for concurrency control
    }
}
