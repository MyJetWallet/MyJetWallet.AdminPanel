using System;
using System.Collections.Generic;

namespace Backoffice.Datatypes
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedList(List<T> items, int totalCount, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalCount = totalCount;
            PageSize = pageSize;
            TotalPages = (int) Math.Ceiling(totalCount / (double) pageSize);

            AddRange(items);
        }

        private PaginatedList()
        {

        }

        public static PaginatedList<T> Empty => new PaginatedList<T>();
    }
}