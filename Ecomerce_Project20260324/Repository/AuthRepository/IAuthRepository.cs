using Ecomerce_Project20260324.DTOs.ApiDTOs.Request;
using Ecomerce_Project20260324.DTOs.ApiDTOs.Response;

namespace Ecomerce_Project20260324.Repository.AuthRepository
{
    public interface IAuthRepository
    {
        Task<Response_ApiUserRegisterDto> Register(Request_ApiUserRegisterDto userDto);
        Task<Response_ApiUserRegisterDto> RegisterAdmin(Request_ApiUserRegisterDto userDto, int secretKey);
        Task<Response_LoginDto> LoginDto(Request_LoginDto request_LoginDto);
        Task<Response_LoginDto> VarifyAndGenerateToken(Request_TokenDto request_TokenDto);      
        Task<bool> LogoutDeleteRefreshToken(string userId);

    }
}
