using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soundlinks.Shared.Abstractions.Contracts
{
    /// <summary>
    /// Klasa obsługująca paginację danych
    /// Implementuje <see cref="System.Collections.Generic.List{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.List{T}" />
    public class Pagination<T>: List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }


        /// <summary>
        /// Inicjalizuje nowy obiekt klasy <see cref="Pagination{T}"/>.
        /// </summary>
        /// <param name="items">Lista obiektów</param>
        /// <param name="count">Całkowita liczba obiektów</param>
        /// <param name="pageNumber">Numer aktualnej strony</param>
        /// <param name="pageSize">Liczba obiektów na stronie</param>
        public Pagination(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        /// <summary>
        /// Metoda obsługująca paginację danych
        /// </summary>
        /// <param name="source">Źródło danych</param>
        /// <param name="pageNumber">Numer strony</param>
        /// <param name="pageSize">Liczba obiektów na stronie</param>
        /// <returns>Pagination&lt;T&gt;.</returns>
        public static Pagination<T> ToPaginationList(List<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();

            return new Pagination<T>(items, count, pageNumber, pageSize);
        }
    }
}
