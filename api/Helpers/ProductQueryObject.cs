
namespace api.Helpers
{
    public class ProductQueryObject
    {
        public int? SizeId { get; set; }
        public int? CategoryId { get; set; }
        public int? ColourId { get; set; }
        public int? MaterialId { get; set; }
        public int? GenderId { get; set; }
        public int? BrandId { get; set; }
        public string? SortBy {get; set;}
        public bool IsDecending {get; set;} = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}