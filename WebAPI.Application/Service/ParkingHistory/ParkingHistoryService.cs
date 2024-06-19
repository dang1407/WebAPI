using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class ParkingHistoryService : BaseCompanyService<ParkingHistory, ParkingHistoryDTO, ParkingHistoryCreateDTO, ParkingHistoryUpdateDTO>, IParkingHistoryService
    {
        private readonly IParkingHistoryRepository _parkingHistoryRepository;

        public ParkingHistoryService(IParkingHistoryRepository parkingHistoryRepository, IMapper mapper) : base(parkingHistoryRepository, mapper) 
        { 
            

            _parkingHistoryRepository = parkingHistoryRepository;      
        }

        public async Task<ParkingHistoryDTO> EnterVehicleOutGarageAsync(ParkingHistoryCreateDTO parkingHistoryCreateDTO, Guid companyId)
        {
            var parkSlot = Mapper.Map<ParkSlot>(parkingHistoryCreateDTO);
            var parkingHistory = Mapper.Map<ParkingHistory>(parkingHistoryCreateDTO);
            var existVehicle = await FindParkingVehicleAsync(parkingHistory.LicensePlate, companyId);

            if (existVehicle == null)
            {
                throw new NotFoundException("Xe này hiện đang không có ở trong bãi đỗ xe. Vui lòng kiểm tra lại.", 404);
            }

            var result = await _parkingHistoryRepository.EnterVehicleOutGarageAsync(parkingHistory, companyId);
            return MapEntityToDTO(result);
        }


        /// <summary>
        /// Hàm thêm xe vào bãi đỗ xe
        /// </summary>
        /// <param name="parkingHistoryCreateDTO">Thông tin nhập xe vào bãi, bao gồm thông tin vị trí gửi xe để cập nhật trạng thái</param>
        /// <returns></returns>
        /// Created by: nkmdang 18/1/2024
        public async Task<ParkingHistoryDTO> EnterVehicleToGarageAsync( ParkingHistoryCreateDTO parkingHistoryCreateDTO, Guid companyId )
        {
            parkingHistoryCreateDTO.CreatedDate ??= DateTime.Now;
            parkingHistoryCreateDTO.CreatedBy ??= "nkmdang";
            var parkSlot = Mapper.Map<ParkSlot>(parkingHistoryCreateDTO);
            var parkingHistory = Mapper.Map<ParkingHistory>(parkingHistoryCreateDTO);
            if (parkingHistory.GetId() == Guid.Empty)
            {
                parkingHistory.SetId(Guid.NewGuid());
            }

            var existVehicle = await FindParkingVehicleAsync(parkingHistory.LicensePlate, companyId);

            if (existVehicle != null) 
            {
                throw new ConflictException("Xe này hiện đang có ở trong bãi đỗ xe. Vui lòng kiểm tra lại.",409);
            }

            var result = await _parkingHistoryRepository.EnterVehicleToGarageAsync(parkingHistory, companyId);
            return MapEntityToDTO(result);   
        }

        public async Task<ParkingHistoryDTO> FindParkingVehicleAsync(string? licensePlate, Guid companyId)
        {
            var parkingHistory = await _parkingHistoryRepository.FindParkingVehicleAsync(licensePlate, companyId);
            ParkingHistoryDTO result = MapEntityToDTO(parkingHistory);
            return result;
        }

        public async Task<List<ParkingHistoryDTO>> GetParkingHistoryStatistical(string year, int vehicle, Guid companyId)
        {
            var parkingHistory = await _parkingHistoryRepository.GetStatisticalParkingHistoryAsync(year, vehicle, companyId);   
            return parkingHistory.Select(x => MapEntityToDTO(x)).ToList();   
        }
    }
}
