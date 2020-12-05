using Xunit;

namespace FluentCollections.Tests
{
    public class ModifiableListTests
    {
        [Fact]
        public void ModifyCollection_Add_Remove_Foreach_ReturnsExpectedCollection()
        {
            // Arrange
            var initialCollection = new[] { 1, 2, 3, 4, 1, 2, 3 };
            var collectionsToAdd = new[] { 2, 4 };
            var collectionsToRemove = new[] { 1, 4, 2 };
            var expectedResultCollection = new[] { 3, 1, 2, 3, 2, 4 };

            var list = new ModifiableList<int>();

            // Act
            // Fill initial collection
            foreach (var item in initialCollection)
            {
                list.Add(item);
            }

            list.Save();

            // Add items inside foreach
            int index = 0;
            foreach (var item in list)
            {
                if (index < collectionsToAdd.Length)
                {
                    list.Add(collectionsToAdd[index]);
                }

                index++;
            }

            list.Save();

            // Remove items inside foreach
            index = 0;
            foreach (var item in list)
            {
                if (index < collectionsToRemove.Length)
                {
                    list.Remove(collectionsToRemove[index]);
                }

                index++;
            }

            list.Save();

            // Assert
            Assert.True(IsCollectionsEqual(expectedResultCollection, list));
        }

        private bool IsCollectionsEqual(int[] expected, ModifiableList<int> list)
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
