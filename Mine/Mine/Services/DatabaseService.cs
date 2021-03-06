﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mine.Models;

namespace Mine.Services
{
    class DatabaseService : IDataStore<ItemModel>
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public DatabaseService()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ItemModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ItemModel)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }
        /// <summary>
        /// Insert method at add items to databased
        /// </summary>
        /// <param name="item">Item being add to databse</param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(ItemModel item)
        {
            if (item == null)
                return false;

            var result = await Database.InsertAsync(item);

            if (result == 0)
                return false;

            return true;
        }
        /// <summary>
        /// Method used to update values in database
        /// </summary>
        /// <param name="item">Item being updated</param>
        /// <returns>Bool value</returns>
        public async Task<bool> UpdateAsync(ItemModel item)
        {
            if (item == null)
                return false;

            var result = await Database.UpdateAsync(item);
            if (result == 0)
                return false;

            return true;
        }
        /// <summary>
        /// Method use to delete an item from the database
        /// </summary>
        /// <param name="id">ID of the item</param>
        /// <returns>Bool value</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var data = await ReadAsync(id);
            if (data == null)
                return false;

            var result = await Database.DeleteAsync(data);
            if (result == 0)
                return false;

            return true;
        }
        /// <summary>
        /// Method used to read from the Database
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns>Item</returns>
        public Task<ItemModel> ReadAsync(string id)
        {
            if (id == null)
                return null;

            var result = Database.Table<ItemModel>().FirstOrDefaultAsync(m => m.Id.Equals(id));

            return result;
        }
        /// <summary>
        /// Index Implemenation of Items
        /// </summary>
        /// <param name="forceRefresh">Bool value</param>
        /// <returns>List of Items</returns>
        public async Task<IEnumerable<ItemModel>> IndexAsync(bool forceRefresh = false)
        {
            var result = await Database.Table<ItemModel>().ToListAsync();
            return result;
        }
    }
}
