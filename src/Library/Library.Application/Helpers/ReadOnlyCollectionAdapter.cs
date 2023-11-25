using System.Collections;

namespace Library.Application.Helpers
{
    sealed class ReadOnlyCollectionAdapter<T> : IReadOnlyCollection<T>
    {
        readonly ICollection<T> source;
        public ReadOnlyCollectionAdapter(ICollection<T> source) => this.source = source;
        public int Count => source.Count;
        public IEnumerator<T> GetEnumerator() => source.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
