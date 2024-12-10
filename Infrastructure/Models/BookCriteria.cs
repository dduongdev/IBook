namespace Infrastructure.Models
{
    public class BookCriteria
    {
        public PriceSortOrders? ProceSortOrder { get; set; }
        public DateSortOrders? DateSortOrder { get; set; }
        public string? BookTitleSearchQuery { get; set; }
        public int? CategoryId { get; set; }
        public int? PublisherId { get; set; }
    }

    public enum PriceSortOrders
    {
        LowestToHighest,
        HighestToLowest
    }

    public enum DateSortOrders
    {
        NewestToOldest,
        OldestToNewest
    }
}
