namespace Infrastructure.Services
{
    public class PaginationService<T>
    {
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalItemCount { get; private set; }
        public IEnumerable<T> Source { get; private set; }

        public PaginationService(IEnumerable<T> source, int pageSize)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source), "Source collection cannot be null.");
            TotalItemCount = source.Count();

            if (pageSize <= 0)
            {
                throw new ArgumentException("PageSize must be greater than 0.");
            }

            if (pageSize > TotalItemCount)
            {
                throw new ArgumentException($"PageSize must be less than or equal to the total number of items ({TotalItemCount}).");
            }

            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
        }

        public IEnumerable<T> GetItemsByPage(int pageIndex)
        {
            if (pageIndex <= 0)
            {
                throw new ArgumentException("Page index must be greater than 0.");
            }

            if (pageIndex > TotalPages)
            {
                throw new ArgumentException($"Page index must be less than or equal to the total number of pages ({TotalPages}).");
            }

            return Source.Skip((pageIndex - 1) * PageSize)
                .Take(PageSize).ToList();
        }
    }
}
