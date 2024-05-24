using Microsoft.Extensions.Configuration;
using WebAPI.Application;
using MySqlConnector;
using System.Data;
using System.Data.Common;


namespace WebAPI.Infrastructure
{

    /// <summary>
    /// Unit Of Work (Fake) quản lý connection và transaction
    /// Transaction được sử dụng để đảm bảo các giao dịch không tranh chấp, khi xảy ra lỗi thì các giao dịch sẽ được rollback để không làm thay đổi các dữ liệu ban đầu thành sai
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        private DbConnection _connection = null;
        private DbTransaction _transaction = null;

        private readonly string _connectionString;

        public UnitOfWork(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        /// <summary>
        /// Hàm trả về connection, nếu _connection = null thì tạo connection mới
        /// </summary>
        /// Created by: nkmdang (24/09/2023)
        public DbConnection Connection => _connection ??= new MySqlConnection(_connectionString);

        public DbTransaction? Transaction => _transaction;

        public void BeginTransaction()
        {
            _connection ??= new MySqlConnection(_connectionString);
            if(_connection.State == ConnectionState.Open)
            {
                _transaction = _connection.BeginTransaction();
            } 
            else
            {
                _connection.Open(); 
                _transaction = _connection.BeginTransaction();
            }
        }

        public async Task BeginTransactionAsync()
        {
            _connection ??= new MySqlConnection(_connectionString);
            if (_connection.State == ConnectionState.Open)
            {
                _transaction = await _connection.BeginTransactionAsync();
            }
            else
            {
                _connection.Open();
                _transaction = await _connection.BeginTransactionAsync();
            }
        }

        public void Commit()
        {
            _transaction?.Commit();
            Dispose();
        }

        public async Task CommitAsync()
        {
            if(_transaction != null)
            {
                await _transaction.CommitAsync();   
            }   
            await DisposeAsync();   
        }

        public void Dispose()
        {
            if(_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if(_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }

            if (_connection != null)
            {
                await _connection.DisposeAsync();
                _connection = null;
            }
        }

        public void RollBack()
        {
            if(_transaction != null )
            {
                _transaction.Rollback();    
            }  
            Dispose();
        }

        public async Task RollBackAsync()
        {
            if(_transaction != null)
            {
                await _transaction.RollbackAsync(); 
            }
            await DisposeAsync();   
        }
    }
}
