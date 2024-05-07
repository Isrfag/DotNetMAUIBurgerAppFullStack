using BurguerMAUI.Data.Entities;
using BurguerMAUI.Models;
using BurguerMAUI.Pages;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BurguerMAUI.Services
{
    public class DataBaseService : IAsyncDisposable
    {
        private const string DataBaseName = "Burger.db3";
        private static readonly string _dataBasePath = Path.Combine(FileSystem.AppDataDirectory, DataBaseName); //Creando la ruta de la base de datos

        private SQLiteAsyncConnection _connection;

        private SQLiteAsyncConnection Database => 
            _connection ??= new SQLiteAsyncConnection(
                _dataBasePath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache
                );

        private async Task <TResult> ExecuteAsync <TResult>(Func<Task<TResult>> actionOnBb)
        {
            await Database.CreateTableAsync<CartItemEntity>();
            return await actionOnBb.Invoke();
        }

        public async Task<int> AddCartItem (CartItemEntity entity) =>
            // Ejecutamos la base de datos e ensertamos la entidad
             await ExecuteAsync(async () => await Database.InsertAsync(entity));      
        

        public async Task UpdateCartItem (CartItemEntity entity) =>
           await ExecuteAsync(async () => await Database.UpdateAsync (entity));
        

        public async Task DeleteCartItem(CartItemEntity entity) =>
            await ExecuteAsync( async () => await Database.DeleteAsync (entity));
        
        public async Task<CartItemEntity> GetCartItemAysnc (int id) =>
            await ExecuteAsync( async() => await Database.GetAsync<CartItemEntity>(id));


        public async Task<List<CartItemEntity>> GetAllCartItemEntityAsync() =>
            await ExecuteAsync(async () => await Database.Table<CartItemEntity>().ToListAsync());

        public async Task<int> ClearCartAsync() =>
            await ExecuteAsync(async () => await Database.DeleteAllAsync<CartItemEntity>());

        public async ValueTask DisposeAsync() =>
            await _connection.CloseAsync();
        
    }
}
