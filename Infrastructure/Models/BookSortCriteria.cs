using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class BookSortCriteria
    {
        public PriceSortOrders? PriceSortOrder { get; set; }
        public DateSortOrders? DateSortOrder { get; set; }
    }

    public enum PriceSortOrders
    {
        [Display(Name = "Cao nhất")]
        LowestToHighest,
        [Display(Name = "Thấp nhất")]
        HighestToLowest
    }

    public enum DateSortOrders
    {
        [Display(Name = "Mới nhất")]
        NewestToOldest,
        [Display(Name = "Cũ nhất")]
        OldestToNewest
    }
}
