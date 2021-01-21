using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine.Models;

namespace Mine.Services
{
    public class MockDataStore : IDataStore<ItemModel>
    {
        readonly List<ItemModel> items;

        public MockDataStore()
        {
            items = new List<ItemModel>()
            {
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Banana", Description="Let go Potassium.", Value = 2 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Apple", Description="Keep the Doctor away.", Value = 4 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Orange", Description="Juice.", Value = 6  },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Coconut", Description="Welcome to the island.", Value = 3 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Strawberry", Description="On a summer evening.", Value = 8 },
            };
        }

        public async Task<bool> AddItemAsync(ItemModel item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ItemModel item)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ItemModel> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ItemModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}