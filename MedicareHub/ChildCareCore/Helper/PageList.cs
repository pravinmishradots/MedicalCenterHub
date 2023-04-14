

namespace ChildCareCore.Helper
{
    public class PageList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext => CurrentPage < TotalPages;
        public bool HasPrevious => CurrentPage > 1;

        public PageList(List<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = count;
            PageSize = pageSize;
            TotalCount = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);

        }

        public static PageList<T> ToPageList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var item = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PageList<T>(item, count, pageNumber, pageSize);
        }


    }
}


