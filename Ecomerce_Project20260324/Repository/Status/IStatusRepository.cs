using Ecomerce_Project20260324.DTOs.StatusDto.Request;
using Ecomerce_Project20260324.DTOs.StatusDto.Response;

namespace Ecomerce_Project20260324.Repository.Status
{
    public interface IStatusRepository
    {
        Task<Response_Status> GetAllStatus();
        Task<Response_Status> GetStatusById(int statusId);
        Task<Response_Status> AddStatus(Request_Status resquest_Status);
        Task<Response_Status> UpdateStatus(int statusId, Request_Status request_Status);
        Task<Response_Status> DeleteStatus(int statusId);
    }
}
