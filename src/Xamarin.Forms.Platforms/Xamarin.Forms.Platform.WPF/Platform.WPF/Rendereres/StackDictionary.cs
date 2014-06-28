using System.Collections.Generic;

namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    class StackDictionary<TKey, TValue> : MultiDictionary<TKey, TValue>
    {
        protected override ICollection<TValue> NewCollection(IEnumerable<TValue> collection = null)
        {
            collection = collection ?? new Stack<TValue>();
            return base.NewCollection(collection);
        }
    }
}
