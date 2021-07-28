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
            Assert.True(Extensions.CollectionExtensions.IsCollectionsEqual(expectedResultCollection, resultCollection));
        }
    }
}
