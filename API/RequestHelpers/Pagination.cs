namespace API.RequestHelpers
{
    public class Pagination<T>
    {
        public Pagination(int pageIndex, int pageSize, int pageCount, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = pageCount;
            Data = data;
        }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public IReadOnlyList<T> Data { get; set; }  
    }
}
