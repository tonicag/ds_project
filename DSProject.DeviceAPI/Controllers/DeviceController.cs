using DSProject.DeviceAPI.Model;
using DSProject.DeviceAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Data;
using UsersAPI.Models.Dto;

namespace DSProject.DeviceAPI.Controllers
{
    [Route("api/device")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        protected readonly AppDbContext _db;
        private ResponseDto _response;

        public DeviceController(AppDbContext db)
        {
            _db = db;
            _response = new ResponseDto();
        }
        [HttpPost]
        public ResponseDto createDevice([FromBody] DeviceDto deviceRequest)
        {
            try
            {
                Device device = new Device()
                {
                    Address = deviceRequest.Address,
                    Description = deviceRequest.Description,
                    MaximumHourlyEnergyConsumption = deviceRequest.MaximumHourlyEnergyConsumption,
                    UserId = deviceRequest.UserId,
                };
                _db.Devices.Add(device);
                _db.SaveChanges();
                _response.IsSuccess = true;
                _response.Result = device;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpGet]
        [Route("{deviceId:Guid}")]
        public ResponseDto getDeviceById(Guid deviceId)
        {
            try
            {
                var entities = _db.Devices.Where(d => d.Id == deviceId);
                _response.Result = entities;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        public ResponseDto getAllDevices([FromQuery] Guid? userId)
        {
            try
            {
                var entities = _db.Devices.Where(d => d.UserId == userId);
                _response.Result = entities;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
        [HttpPut]
        public ResponseDto updateDevice([FromBody] DeviceDto deviceRequest)
        {
            try
            {
                Device device = new Device()
                {
                    Id = deviceRequest.Id,
                    Address = deviceRequest.Address,
                    Description = deviceRequest.Description,
                    MaximumHourlyEnergyConsumption = deviceRequest.MaximumHourlyEnergyConsumption,
                    UserId = deviceRequest.UserId,
                };
                _db.Update(device);
                _db.SaveChanges();
                _response.Result = device;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
        [HttpDelete]
        [Route("{deviceId:Guid}")]
        public ResponseDto deleteDevice(Guid deviceId)
        {
            try
            {
                var entity = _db.Devices.First(d => d.Id == deviceId);
                _db.Devices.Remove(entity);
                _db.SaveChanges();
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
    }
}
