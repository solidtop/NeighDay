using Newtonsoft.Json;

namespace NeighDay.Server.Common
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public PagedList(List<T> list, int count, int page, int pageSize) 
        { 
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = page;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(list);
        }

        public static PagedList<T> ToPagedList(List<T> source, int page, int pageSize)
        {
            var count = source.Count;
            var items = source.Skip(page - 1).Take(pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<T>(items, count, page, pageSize);
        }

        public static string ToMetadata(PagedList<T> list)
        {
            return JsonConvert.SerializeObject(new Metadata(list.CurrentPage, list.PageSize, list.TotalCount));
        }
    }
}
