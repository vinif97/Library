using System.Collections;

namespace Library.Application.Helpers.Extensions
{
    public static class ICollectionExtension
    {
        public static IReadOnlyCollection<T> AsReadOnly<T>(this ICollection<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("ICollection source cannot be null");
            }
            return source as IReadOnlyCollection<T> ?? new ReadOnlyCollectionAdapter<T>(source);
        }

        sealed class ReadOnlyCollectionAdapter<T> : IReadOnlyCollection<T>
        {
            readonly ICollection<T> source;
            public ReadOnlyCollectionAdapter(ICollection<T> source) => this.source = source;
            public int Count => source.Count;
            public IEnumerator<T> GetEnumerator() => source.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
