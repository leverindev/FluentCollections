using System.Linq;
using FluentCollections.Tests.Extensions;
using Xunit;

namespace FluentCollections.Tests
{
    public class RichQueueTests
    {
        [Fact]
        public void EmptyQueue_ZeroCountDefaultFirstValue()
        {
            // Arrange
            var queue = new RichQueue<int>();

            // Act
            var count = queue.Count;
            var first = queue.First;

            // Assert
            Assert.Equal(0, count);
            Assert.Equal(0, first);
        }

        [Fact]
        public void EnqueueCollection_ValidCountAndFirst()
        {
            // Arrange
            var queue = new RichQueue<int>();
            var collection = new [] { 1, 2, 3 };

            // Act
            queue.Enqueue(collection);

            // Assert
            Assert.Equal(collection.Length, queue.Count);
            Assert.Equal(collection[0], queue.First);
        }

        [Fact]
        public void DequeueCollection_ValidCountAndFirst()
        {
            // Arrange
            var queue = new RichQueue<int>();
            var collection = new[] { 1, 2, 3 };

            // Act
            queue.Enqueue(collection);
            var first = queue.Dequeue();
            var second = queue.Dequeue();

            // Assert
            Assert.Equal(collection[0], first);
            Assert.Equal(collection[1], second);
            Assert.Equal(collection[2], queue.First);
        }

        [Fact]
        public void DequeueEvenValues_ReturnsExpectedCollection()
        {
            // Arrange
            var queue = new RichQueue<int>();
            var sourceCollection = new[] { 1, 2, 3, 4, 5, 6, 7 };
            var expectedCollection = new[] { 2, 4, 6 };

            // Act
            queue.Enqueue(sourceCollection);
            var result = queue.Remove(x => x % 2 == 0).ToList();

            // Assert
            Assert.True(CollectionExtensions.IsCollectionsEqual(expectedCollection, result));
        }

        [Fact]
        public void DequeueEvenValues_ReturnsEmptyCollection()
        {
            // Arrange
            var queue = new RichQueue<int>();
            var sourceCollection = new[] { 1, 3, 5, 7 };

            // Act
            queue.Enqueue(sourceCollection);
            var result = queue.Remove(x => x % 2 == 0).ToList();

            // Assert
            Assert.Empty(result);
        }
    }
}
