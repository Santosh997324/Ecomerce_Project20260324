using AutoMapper;
using Ecomerce_Project20260324.Data;
using Ecomerce_Project20260324.DTOs.StatusDto.Request;
using Ecomerce_Project20260324.DTOs.StatusDto.Response;
using Ecomerce_Project20260324.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce_Project20260324.Repository.Status
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public StatusRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response_Status> AddStatus(Request_Status request_Status)
        {

            var status = await _db.Statuses
                .FirstOrDefaultAsync(m => m.Status_Name == request_Status.Status_Name);

            if (status != null)
            {
                return new Response_Status()
                {
                    isSuccess = false,
                    Message = "Status with the same name already exists."
                };
            }
            var statusData = _mapper.Map<Ecomerce_Project20260324.Models.Master.Status>(request_Status);
            await _db.Statuses.AddAsync(statusData);
            await _db.SaveChangesAsync();

            return new Response_Status()
            {
                isSuccess = true,
                Message = "Status added successfully.",
                status = new List<Models.Master.Status>()
                    {
                         statusData
                    }
            };

        }
        public async Task<Response_Status> DeleteStatus(int statusId)
        {
            var Status = await _db.Statuses.FirstOrDefaultAsync(m => m.Status_Id == statusId);
            if (Status == null)
            {
                return new Response_Status()
                {
                    isSuccess = false,
                    Message = "Status not found."
                };
            }
            _db.Remove(statusId);
            await _db.SaveChangesAsync();

            return new Response_Status()
            {
                isSuccess = true,
                Message = "Status deleted successfully.",
                status = new List<Models.Master.Status>()
                {
                    Status
                }
            };
        }

        public async Task<Response_Status> GetAllStatus()
        {

            var status = await _db.Statuses.ToListAsync();

            return new Response_Status()
            {
                isSuccess = true,
                Message = "Status retrieved successfully.",
                status = status
            };

            //throw new NotImplementedException();
        }

        public async Task<Response_Status> GetStatusById(int statusId)
        {
            var status = await _db.Statuses.FirstOrDefaultAsync(m => m.Status_Id == statusId);
            if (status == null)
            {
                return new Response_Status()
                {
                    isSuccess = false,
                    Message = "Status not found."
                };
            }
            return new Response_Status()
            {
                isSuccess = true,
                Message = "Status retrieved successfully.",
                status = new List<Models.Master.Status>()
                {
                    status
                }
            };

            // throw new NotImplementedException();
        }

        public async Task<Response_Status> UpdateStatus(int statusId, Request_Status request_Status)
        {
            var status = await _db.Statuses.FirstOrDefaultAsync(m => m.Status_Id == statusId);

            if (status == null)
            {
                return new Response_Status()
                {
                    isSuccess = false,
                    Message = "Status not found."
                };
            }

            status.Status_Name = request_Status.Status_Name;

            _db.Update(status);
            await _db.SaveChangesAsync();

            return new Response_Status()
            {
                isSuccess = true,
                Message = "Status updated successfully.",
                status = new List<Models.Master.Status>()
                {
                    status
                }
            };

            // throw new NotImplementedException();
        }
    }
}
