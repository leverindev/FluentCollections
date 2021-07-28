using System.Collections.Generic;

namespace FluentCollections.Tests.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsCollectionsEqual(int[] expected, List<int> list)
        {
            if (expected.Length != list.Count)
            {
                return false;
            }

            int index = 0;
            foreach (var item in list)
            {
                if (item != expected[index])
                {
                    return false;
                }

                index++;
            }

            return true;
        }
    }
}
