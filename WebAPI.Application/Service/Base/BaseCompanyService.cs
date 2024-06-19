using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace WebAPI.Application
{
    public class BaseCompanyService<TEntity, TDTO, TCreateDTO, TUpdateDTO> : BaseCompanyReadOnlyService<TEntity, TDTO>, IBaseCompanyService<TDTO, TCreateDTO, TUpdateDTO> where TEntity : IEntity
    {
        protected BaseCompanyService(IBaseCompanyRepository<TEntity> baseCompanyRepository, IMapper mapper) : base(baseCompanyRepository, mapper)
        {

        }


        /// <summary>
        /// Hàm thêm mới một Entity
        /// </summary>
        /// <param name="entity">Instance của Entity</param>
        /// <returns>Thông tin Entity đã thêm thành công</returns>
        /// Created by: nkmdang (20/09/2023)
        public virtual async Task<TDTO> InsertAsync( TCreateDTO createDTO, Guid companyId)
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

            await ValidateCreateBusinessAsync(entity, companyId);

            // Entity có đủ các trường rồi, gán Id ở Backend luôn
            var result = await BaseCompanyRepository.InsertAsync(entity, companyId);
            var resultEmployee = MapEntityToDTO(result);
            return resultEmployee;
        }

        /// <summary>
        /// Hàm thêm mới nhiều Entity
        /// </summary>
        /// <param name="entities">Instances của Entity</param>
        /// <returns>Thông tin các Entity đã thêm thành công</returns>
        /// Created by: nkmdang (20/09/2023)
        public async Task<List<TDTO>> InsertManyAsync(List<TCreateDTO> createDTOs,Guid companyId)
        {
            // Chuyển đổi CreateDTOs thành các Entity
            var listEntities = createDTOs.Select(createDTO => MapCreateDTOToEntity(createDTO)).ToList();
            // Lọc ra các Id
            var listIds = listEntities.Select(entity => entity.GetId()).ToList();
            //Tìm các Entity đã tồn tại
            var existEntities = await BaseCompanyRepository.GetByListIdAsync(listIds, companyId);
            // Lọc ra Id của các Entity đã tồn tại
            var existEntitiesId = existEntities.Select( entity => entity.GetId()).ToList();
            // Lọc ra các Entity mới để gọi hàm Create
            var newEntitiesToCreate = listEntities.Where(entity => !existEntitiesId.Contains(entity.GetId())).ToList();
            var listResultEntities = await BaseCompanyRepository.InsertManyAsync(newEntitiesToCreate, companyId);
            var results = listResultEntities.Select(entity =>  MapEntityToDTO(entity)).ToList(); 
            return results;
        }

        /// <summary>
        /// Hàm sửa thông tin một Entity
        /// </summary>
        /// <param name="entity">Instance của Entity</param>
        /// <returns>Thông tin của Entity sau khi đã thay đổi</returns>
        /// Created by: nkmdang (20/09/2023)
        public virtual async Task<TDTO> UpdateAsync( Guid id, TUpdateDTO updateDTO , Guid companyId)
        {
            var entity = await BaseCompanyRepository.GetByIdAsync(id, companyId);

            
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
            await ValidateUpdateBusinessAsync(newEntity, companyId);

            await BaseCompanyRepository.UpdateAsync(newEntity, companyId);
            var result = MapEntityToDTO(newEntity);
            
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
            await BaseCompanyRepository.GetByIdAsync(id, companyId);

            var result = await BaseCompanyRepository.DeleteAsync(id, companyId);  
            return result;
        }

        /// <summary>
        /// Hàm xóa thông tin nhiều Entity
        /// </summary>
        /// <param name="ids">Danh sách các dịnh danh Entity</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: nkmdang (20/09/2023)
        public async Task<int> DeleteManyAsync(List<Guid> ids, Guid companyId)
        {
            var entities = await BaseCompanyRepository.GetByListIdAsync(ids, companyId);  

            var result = await BaseCompanyRepository.DeleteManyAsync(entities,companyId);    
            return result;
        }

        


        /// <summary>
        /// Hàm Validate nghiệp vụ cho thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entity">Thông tin Entity thêm mới</param>
        /// <returns>Ném ra lỗi nếu đầu vào không thỏa mãn nghiệp vụ</returns>
        /// Created by: nkmdang (19/09/2023)
        public virtual async Task ValidateCreateBusinessAsync(TEntity entity, Guid companyId) 
        {
            var existEntity = await BaseCompanyRepository.GetFilterAsync(1,1,entity.GetId().ToString(), companyId);
            if (existEntity.Count > 0)
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
        public virtual async Task ValidateUpdateBusinessAsync(TEntity entity, Guid companyId)
        {
            var existEntity = await BaseCompanyRepository.GetFilterAsync(0, 2, entity.GetId().ToString(), companyId);

            if(existEntity.Count != 1)
            {
                throw new NotFoundException("Cập nhật tài nguyên không thành công");
            }
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
