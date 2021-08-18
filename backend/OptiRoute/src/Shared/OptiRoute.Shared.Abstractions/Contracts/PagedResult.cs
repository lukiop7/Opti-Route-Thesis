using System;
using System.Collections.Generic;

namespace OptiRoute.Shared.Abstractions.Contracts
{
    /// <summary>
    /// Klasa reprezentująca zwracaną odpowiedź z procesu paginacji danych
    /// </summary>
    /// <typeparam name="TEntity">Typ obiektu</typeparam>
    public class PagedResult<TEntity>
    {
        public IReadOnlyList<TEntity> Results { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int PagesCount
        {
            get
            {
                return PageSize != 0 ? Convert.ToInt32(Math.Ceiling(Convert.ToDouble(TotalCount) / Convert.ToDouble(PageSize))) : 0;
            }
        }


        /// <summary>
        /// Tworzy i zwraca nowy obiekt zawierający dane odpowiedzi procesu paginacji.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>PagedResult&lt;T&gt;.</returns>
        public PagedResult<T> CreateNew<T>(IReadOnlyList<T> list)
        {
            return new PagedResult<T>
            {
                Results = list,
                Page = Page,
                PageSize = PageSize,
                TotalCount = TotalCount
            };
        }
    }
}
