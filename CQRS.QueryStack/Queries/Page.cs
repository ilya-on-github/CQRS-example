using System.Collections.Generic;

namespace CQRS.QueryStack.Queries
{
    public class Page<TItem>
    {
        public Page(int pageNum, int pageSize, IEnumerable<TItem> items, int itemsTotal)
        {
            PageNum = pageNum;
            PageSize = pageSize;
            Items = items;
            ItemsTotal = itemsTotal;
        }

        public int PageNum { get; }
        public int PageSize { get; }
        public IEnumerable<TItem> Items { get; }
        public int ItemsTotal { get; }
    }
}