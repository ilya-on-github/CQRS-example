namespace CQRS.QueryStack.Queries
{
    public class PageInfo
    {
        public PageInfo(int pageIndex, int itemsPerPage)
        {
            Page = pageIndex;
            Size = itemsPerPage;
        }

        public int Page { get; }

        public int Size { get; }
    }
}