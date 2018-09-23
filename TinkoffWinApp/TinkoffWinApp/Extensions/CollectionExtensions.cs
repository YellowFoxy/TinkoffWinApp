using System.Collections;

namespace TinkoffWinApp.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsEmpty(this ICollection collection)
        {
            return collection == null || collection.Count == 0;
        }
    }
}
