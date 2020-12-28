using System.Collections.Generic;
using Xunit;

namespace FluentCollections.Tests
{
    public class PriorityQueueTests
    {
        [Fact]
        public void FillAndReadQueue_ReturnsExpectedCollection()
        {
            // Arrange
            var sourceCollection = new[] { 1, 5, 6, 3, 7, 4, 8 };
            var expectedResultCollection = new[] { 1, 3, 4, 5, 6, 7, 8 };

            var queue = new PriorityQueue<int>();

            // Act
            // Fill queue
            foreach (var item in sourceCollection)
            {
                queue.Add(item);
            }

            var resultCollection = new List<int>();
            while (queue.Count > 0)
            {
                resultCollection.Add(queue.Dequeue());
            }

            // Assert
            Assert.True(IsCollectionsEqual(expectedResultCollection, resultCollection));
        }

        private static bool IsCollectionsEqual(int[] expected, List<int> list)
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
