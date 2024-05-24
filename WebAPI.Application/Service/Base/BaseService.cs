using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace WebAPI.Application
{
    public class BaseService<TEntity, TDTO, TCreateDTO, TUpdateDTO> : BaseReadOnlyService<TEntity, TDTO>, IBaseService<TDTO, TCreateDTO, TUpdateDTO> where TEntity : IEntity
    {
        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {

        }


        /// <summary>
        /// Hàm thêm mới một Entity
        /// </summary>
        /// <param name="entity">Instance của Entity</param>
        /// <returns>Thông tin Entity đã thêm thành công</returns>
        /// Created by: nkmdang (20/09/2023)
        public async Task<TDTO> InsertAsync(TCreateDTO createDTO)
        {
            var entity = MapCreateDTOToEntity(createDTO);
            if(entity.GetId() == Guid.Empty)
            {
                entity.SetId(Guid.NewGuid());   
            }

            // Map các trường của Base Enity
            if(entity is BaseEntity baseEntity)
            {
                baseEntity.CreatedDate ??= DateTime.Now;
                baseEntity.CreatedBy ??= "NKMDANG";
                baseEntity.ModifiedDate ??= DateTime.Now;
                baseEntity.ModifiedBy ??= "NKMDANG";
            }

            await ValidateCreateBusinessAsync(entity);

            // Entity có đủ các trường rồi, gán Id ở Backend luôn
            var result = await BaseRepository.InsertAsync(entity);
            var resultEmployee = MapEntityToDTO(result);
            return resultEmployee;
        }

        /// <summary>
        /// Hàm thêm mới nhiều Entity
        /// </summary>
        /// <param name="entities">Instances của Entity</param>
        /// <returns>Thông tin các Entity đã thêm thành công</returns>
        /// Created by: nkmdang (20/09/2023)
        public async Task<List<TDTO>> InsertManyAsync(List<TCreateDTO> createDTOs)
        {
            // Chuyển đổi CreateDTOs thành các Entity
            var listEntities = createDTOs.Select(createDTO => MapCreateDTOToEntity(createDTO)).ToList();
            // Lọc ra các Id
            var listIds = listEntities.Select(entity => entity.GetId()).ToList();
            //Tìm các Entity đã tồn tại
            var existEntities = await BaseRepository.GetByListIdAsync(listIds);
            // Lọc ra Id của các Entity đã tồn tại
            var existEntitiesId = existEntities.Select( entity => entity.GetId()).ToList();
            // Lọc ra các Entity mới để gọi hàm Create
            var newEntitiesToCreate = listEntities.Where(entity => !existEntitiesId.Contains(entity.GetId())).ToList();
            var listResultEntities = await BaseRepository.InsertManyAsync(newEntitiesToCreate);
            var results = listResultEntities.Select(entity =>  MapEntityToDTO(entity)).ToList(); 
            return results;
        }

        /// <summary>
        /// Hàm sửa thông tin một Entity
        /// </summary>
        /// <param name="entity">Instance của Entity</param>
        /// <returns>Thông tin của Entity sau khi đã thay đổi</returns>
        /// Created by: nkmdang (20/09/2023)
        public async Task<TDTO> UpdateAsync( Guid id, TUpdateDTO updateDTO)
        {
            var entity = await BaseRepository.GetByIdAsync(id);

            
            // Map các trường của Base Enity
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.ModifiedDate ??= DateTime.Now;
                baseEntity.ModifiedBy ??= "NKMDANG";
            }
            var newEntity = MapUpdateDTOToEntity(updateDTO, entity);
            Console.WriteLine(entity.GetId());
            Console.WriteLine(newEntity.GetId());
            if (newEntity.GetId() == Guid.Empty)
            {
                newEntity.SetId(id);
            }
            await ValidateUpdateBusinessAsync(newEntity);

            await BaseRepository.UpdateAsync(newEntity);
            var result = MapEntityToDTO(newEntity);
            
            return result;
        }

        /// <summary>
        /// Hàm xóa thông tin một Entity
        /// </summary>
        /// <param name="id">Định danh Entity</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: nkmdang (20/09/2023)
        public async Task<int> DeleteAsync(Guid id)
        {
            await BaseRepository.GetByIdAsync(id);

            var result = await BaseRepository.DeleteAsync(id);  
            return result;
        }

        /// <summary>
        /// Hàm xóa thông tin nhiều Entity
        /// </summary>
        /// <param name="ids">Danh sách các dịnh danh Entity</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: nkmdang (20/09/2023)
        public async Task<int> DeleteManyAsync(List<Guid> ids)
        {
            var entities = await BaseRepository.GetByListIdAsync(ids);  

            var result = await BaseRepository.DeleteManyAsync(entities);    
            return result;
        }

        


        /// <summary>
        /// Hàm Validate nghiệp vụ cho thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entity">Thông tin Entity thêm mới</param>
        /// <returns>Ném ra lỗi nếu đầu vào không thỏa mãn nghiệp vụ</returns>
        /// Created by: nkmdang (19/09/2023)
        public virtual async Task ValidateCreateBusinessAsync(TEntity entity) 
        {
            var existEntity = await BaseRepository.FindByIdAsync(entity.GetId());
            if (existEntity != null)
            {
                throw new ConflictException("Tài nguyên đã tồn tại");
            }
        }

        /// <summary>
        /// Hàm Validate nghiệp vụ cho sửa đổi 1 bản ghi
        /// </summary>
        /// <param name="entity">Thông tin Entity thêm mới</param>
        /// <returns>Ném ra lỗi nếu đầu vào không thỏa mãn nghiệp vụ</returns>
        /// Created by: nkmdang (19/09/2023)
        public virtual async Task ValidateUpdateBusinessAsync(TEntity entity)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Hàm chuyển đổi từ CreateDTO sang Entity
        /// </summary>
        /// <param name="createDTO">Instance của TCreateDTO</param>
        /// <returns>Entity</returns>
        /// Created by: nkmdang (19/09/2023)
        protected TEntity MapCreateDTOToEntity(TCreateDTO createDTO)
        {
            var entity = Mapper.Map<TEntity>(createDTO);
            if(entity.GetId() == Guid.Empty)
            {
                entity.SetId(Guid.NewGuid());   
            }
            return entity;
        }

        /// Hàm chuyển đổi từ UpdateDTO sa
        /// <summary>ng Entity
        /// </summary>
        /// <param name="updateDTO">Instance của TUpdateDTO</param>
        /// <returns>Entity</returns>
        /// Created by: nkmdang (19/09/2023)
        protected TEntity MapUpdateDTOToEntity(TUpdateDTO updateDTO, TEntity entity)
        {
            var resultEntity = Mapper.Map(updateDTO, entity);
            return resultEntity;
        }

        protected TEntity MapDTOToEntity(TDTO dto)
        {
            var entity = Mapper.Map<TEntity>(dto);
            return entity;
        }

        


        
        
    }
}
