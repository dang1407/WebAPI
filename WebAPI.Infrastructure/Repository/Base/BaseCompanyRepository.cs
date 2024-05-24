using Dapper;
using System.Data;
using System.Reflection;
using WebAPI.Application;
using WebAPI.Domain;


namespace WebAPI.Infrastructure
{
    public abstract class BaseCompanyRepository<TEntity> : IBaseCompanyRepository<TEntity> where TEntity : IEntity
    {

        protected readonly IUnitOfWork Uow;
        public virtual string TableName { get; set; } = typeof(TEntity).Name;
        public BaseCompanyRepository(IUnitOfWork uow)
        {
            Uow = uow;

        }

        /// <summary>
        /// Hàm tìm kiếm Entity theo Id
        /// </summary>
        /// <param name="id">Định danh của Entity (Guid)</param>
        /// <returns>Thông tin Entity nếu thành công, null nếu thất bại</returns>
        /// Created by: nkmdang (19/09/2023)
        public async Task<TEntity> FindByIdAsync(Guid id, Guid companyId)
        {
            // Tạo câu truy vấn
            var sql = $"Proc_Read_{TableName}ById";

            // Tạo param 
            var param = new DynamicParameters();
            param.Add($"p_{TableName}Id", id);
            param.Add("p_CompanyId" , companyId);
            var result = await Uow.Connection.QueryFirstOrDefaultAsync<TEntity>(sql, param, commandType: CommandType.StoredProcedure, transaction: Uow.Transaction);
            return result;
        }

        /// <summary>
        /// Hàm lấy ra các bản ghi theo lọc, phân trang
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Created by: nkmdang (19/09/2023)
        public async Task<List<TEntity>> GetFilterAsync(int page, int pageSize, string? property, Guid companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_Page", page, DbType.Int32);
            parameters.Add("p_PageSize", pageSize, DbType.Int32);
            parameters.Add("p_SearchProperty", property, DbType.String);
            parameters.Add("p_CompanyId", companyId, DbType.String);
            // Thực hiện truy vấn
            var result = await Uow.Connection.QueryAsync<TEntity>($"Proc_Read_{TableName}sFilter", parameters, commandType: CommandType.StoredProcedure, transaction: Uow.Transaction);
            return result.ToList();
        }

        /// <summary>
        /// Hàm lấy thông tin Entity theo Id
        /// </summary>
        /// <param name="id">Định danh của Entity (Guid)</param>
        /// <returns>Thông tin Entity nếu thành công, null nếu thất bại</returns>
        /// Created by: nkmdang (19/09/2023)
        public virtual async Task<TEntity> GetByIdAsync(Guid id, Guid companyId)
        {
            var entity = await GetFilterAsync(0, 1, id.ToString(), companyId);
            Console.WriteLine(id.ToString() + " " + companyId.ToString());
            if(entity.Count > 0) 
            {
                return entity[0];
            } 
            else
            {
                throw new NotFoundException("Không tìm thấy tài nguyên", "Không tìm thấy tài nguyên", 404);
            }
        }


        /// <summary>
        /// Hàm lấy thông tin nhiều Entity theo Id
        /// </summary>
        /// <param name="ids">Định danh của các Entity (Guid)</param>
        /// <returns>Thông tin các Entity nếu thành công, null nếu thất bại</returns>
        /// Created by: nkmdang (20/09/2023)
        public async Task<List<TEntity>> GetByListIdAsync(List<Guid> ids, Guid companyId)
        {
            // Tạo câu lệnh SQL (không truyền vào list ids)
            string sql = $"SELECT * FROM {TableName} WHERE {TableName}Id IN @ids";

            //Tạo param
            var param = new DynamicParameters();
            param.Add("ids", ids);

            // Truy vấn
            var entities = await Uow.Connection.QueryAsync<TEntity>(sql, param, transaction: Uow.Transaction);
            return entities.ToList();
        }

        /// <summary>
        /// Hàm thêm mới một Entity
        /// </summary>
        /// <param name="entity">Instance của Entity</param>
        /// <returns>Thông tin Entity đã thêm thành công</returns>
        /// Created by: nkmdang (20/09/2023)
        public virtual async Task<TEntity> InsertAsync(TEntity entity, Guid companyId)
        {
            var param = new DynamicParameters();
            Type type = entity.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach(var property in properties)
            {
                param.Add("p_" + property.Name, property.GetValue(entity));
            }
            param.Add("p_CompanyId", companyId);
            // Thực thi truy vấn
            var result = await Uow.Connection.QuerySingleOrDefaultAsync<TEntity>($"Proc_Create_{TableName}", param, commandType: CommandType.StoredProcedure, transaction: Uow.Transaction);

            return entity;
        }

        /// <summary>
        /// Hàm thêm mới nhiều Entity
        /// </summary>
        /// <param name="entities">Instances của Entity</param>
        /// <returns>Thông tin các Entity đã thêm thành công</returns>
        /// Created by: nkmdang (20/09/2023)
        public virtual async Task<List<TEntity>> InsertManyAsync(List<TEntity> entities, Guid companyId)
        {
            var firstEntity = entities.FirstOrDefault();
            if (firstEntity != null)
            {
                string sql = TEntitySQL.InsertSQL(firstEntity);
                var result = new List<TEntity>();
                foreach (var entity in entities)
                {
                    if(entity.GetId() == Guid.Empty)
                    {
                        entity.SetId(Guid.NewGuid());   
                    }   
                    var addSuccessEntity = await Uow.Connection.QueryAsync<TEntity>(sql, entity, transaction: Uow.Transaction);
                    if (addSuccessEntity != null)
                    {
                        result.Add(entity);
                    }
                }
                return result;
            }
            else return [];
        }


        /// <summary>
        /// Hàm sửa thông tin một Entity
        /// </summary>
        /// <param name="entity">Instance của Entity</param>
        /// <returns>Thông tin của Entity sau khi đã thay đổi</returns>
        /// Created by: nkmdang (20/09/2023)
        public virtual async Task<TEntity> UpdateAsync(TEntity entity, Guid companyId)
        {
            var param = new DynamicParameters();
            Type type = entity.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                param.Add("p_" + property.Name, property.GetValue(entity));
            }
            param.Add("p_CompanyId", companyId);
            //// Thực thi truy vấn
            var result = await Uow.Connection.QuerySingleOrDefaultAsync<TEntity>($"Proc_Update_{TableName}", param, commandType: CommandType.StoredProcedure, transaction: Uow.Transaction);
            return result;
        }

        /// <summary>
        /// Hàm xóa thông tin một Entity
        /// </summary>
        /// <param name="id">Định danh Entity</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: nkmdang (20/09/2023)
        public async Task<int> DeleteAsync(Guid id, Guid companyId)
        {
            // Tạo câu lệnh SQL
            string sql = $"Proc_Delete_{TableName}";
            var param = new DynamicParameters();
            param.Add($"p_{TableName}Id", id);
            param.Add($"p_CompanyId", companyId);
            var result = await Uow.Connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure, transaction: Uow.Transaction);
            return result;
        }

        /// <summary>
        /// Hàm xóa thông tin nhiều Entity
        /// </summary>
        /// <param name="ids">Danh sách các dịnh danh Entity</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: nkmdang (20/09/2023)
        public async Task<int> DeleteManyAsync(List<TEntity> entities, Guid companyId)
        {
            // Tạo câu lệnh SQL
            string sql = TEntitySQL.DeleteByListIdSQL(TableName);

            var param = new DynamicParameters();
            var ids = entities.Select(entity => entity.GetId()).ToList();
            param.Add("ids", ids);
            var result = await Uow.Connection.ExecuteAsync(sql, param, transaction: Uow.Transaction); 
            return result;
        }

        /// <summary>
        /// Hàm lấy ra số bản ghi thỏa mãn tiêu chí đang xem
        /// </summary>
        /// <param name="property"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> GetNumberRecordsAsync(string? property, Guid companyId)
        {
            string sql = $"Proc_Read_Number{TableName}s";
            var param = new DynamicParameters();    
            param.Add("p_SearchProperty", property);
            param.Add("p_CompanyId", companyId);
            var result = await Uow.Connection.QuerySingleAsync<int>(sql, param, commandType: CommandType.StoredProcedure, transaction: Uow.Transaction);
            return result;
        }
    }
}
