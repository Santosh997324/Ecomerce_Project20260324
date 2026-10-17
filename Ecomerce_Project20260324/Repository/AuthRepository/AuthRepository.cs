using AutoMapper;
using Ecomerce_Project20260324.Contants;
using Ecomerce_Project20260324.Data;
using Ecomerce_Project20260324.DTOs.ApiDTOs.Request;
using Ecomerce_Project20260324.DTOs.ApiDTOs.Response;
using Ecomerce_Project20260324.Models.AuthModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Ecomerce_Project20260324.Repository.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;

        public AuthRepository(UserManager<ApiUser> userManager, IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _dbContext = dbContext;

        }

        public async Task<Response_LoginDto> LoginDto(Request_LoginDto request_LoginDto)
        {
            //throw new NotImplementedException();

            bool isValidUser = false;

            var user = await _userManager.FindByEmailAsync(request_LoginDto.Email);

            if (user == null)
            {
                return new Response_LoginDto()
                {
                    Result = false,
                    error = new List<string>() { "Invalid Email or Password" }
                };
            }

            if (user.EmailConfirmed == false)
            {
                return new Response_LoginDto()
                {
                    Result = false,
                    error = new List<string>() { "Please Confirm Your Email Before Login" }
                };
            }

            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, request_LoginDto.Password);

            if ((!isPasswordValid))
            {
                return new Response_LoginDto()
                {
                    Result = false,
                    error = new List<string>() { "Invalid Email or Password" }
                };
            }
            var token = await GenerateToken(user);
            var refreshToken = await CreateRefreshToken(user, token);

            return new Response_LoginDto()
            {
                Result = true,
                UserId = user.Id,
                Token = token,
                RefreshToken = refreshToken
            };
        }

        //Private Function
        private async Task<string> GenerateToken(ApiUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSetting:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(roles => new Claim(ClaimTypes.Role, roles));

            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("uid", user.Id),}.Union(userClaims).Union(roleClaims);

            //Generate Token
            var token = new JwtSecurityToken(
     issuer: _configuration["JwtSetting:Issuser"],
     audience: _configuration["JwtSetting:Audience"],
     claims: claims,
     expires: DateTime.Now.AddMinutes(
         Convert.ToInt32(_configuration["JwtSetting:DurationInMinutes"])
     ), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private async Task<string> CreateRefreshToken(ApiUser user, string token)
        {
            var existinRefreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == user.Id);

            if (existinRefreshToken != null)
            {
                _dbContext.RefreshTokens.Remove(existinRefreshToken);
                await _dbContext.SaveChangesAsync();
            }

            var jwtSecuiryTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecuiryTokenHandler.ReadJwtToken(token);
            var refreshToken = new RefreshToken()
            {
                UserId = user.Id,
                Token = RandomStringGeneration(23),
                JwtId = tokenContent.Id,
                AddedDate = DateTime.Now,
                ExpiryDate = DateTime.UtcNow.AddMinutes(110)
            };
            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();

            return refreshToken.Token;
        }

        private string RandomStringGeneration(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public async Task<Response_ApiUserRegisterDto> Register(Request_ApiUserRegisterDto userDto)
        {
            //throw new NotImplementedException();
            var user = new ApiUser()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
            };

            user.UserName = userDto.Email;
            user.EmailConfirmed = false; // Set to true if you want to skip email confirmation for testing purposes
            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Customer);

                return new Response_ApiUserRegisterDto()
                {
                    IsSuccess = true,
                    apiUser = user
                };
            }

            List<string> error = new List<string>();
            foreach (var err in result.Errors)
            {
                error.Add(err.Description.ToString());
            }

            return new Response_ApiUserRegisterDto()
            {
                IsSuccess = false,
                Message = error
            };
        }

        public async Task<Response_ApiUserRegisterDto> RegisterAdmin(Request_ApiUserRegisterDto userDto, int secretKey)
        {
            if (secretKey != 12345)
            {
                return new Response_ApiUserRegisterDto()
                {
                    IsSuccess = false,
                    Message = new List<string>()
                    {
                        "Wrong Secret Key "
                    }
                };
            }
            var user = new ApiUser()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
            };

            user.UserName = userDto.Email;
            user.EmailConfirmed = true; // Set to true if you want to skip email confirmation for testing purposes
            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Adminstrator);

                return new Response_ApiUserRegisterDto()
                {
                    IsSuccess = true,
                    apiUser = user
                };
            }

            List<string> error = new List<string>();
            foreach (var err in result.Errors)
            {
                error.Add(err.Description.ToString());
            }

            return new Response_ApiUserRegisterDto()
            {
                IsSuccess = false,
                Message = error
            };
        }

        public async Task<Response_LoginDto> VarifyAndGenerateToken(Request_TokenDto request_TokenDto)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request_TokenDto.Token);

            var tokenIssuer = tokenContent.Issuer;

            if (tokenIssuer != _configuration["JwtSetting:Issuser"])
            {
                return new Response_LoginDto()
                {
                    Result = false,
                    error = new List<string>()
                {
                    "Token Parameter are wrong!"
                }
                };
            }

            var tokenAudience = tokenContent.Audiences.ToList();

            if (!tokenAudience.Contains(_configuration["JwtSetting:Audience"]))
            {
                return new Response_LoginDto()
                {
                    Result = false,
                    error = new List<string>()
                {
                    "Token Parameter are wrong!"
                }
                };
            }

            var userName = tokenContent.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null || user.Id != request_TokenDto.UsrerId)
            {
                return new Response_LoginDto()
                {
                    Result = false,
                    error = new List<string>()
                {
                    "Token Parameter are wrong!"
                }
                };
            }


            var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == request_TokenDto.RefreshToken);

            if(refreshToken == null)
            {
                return new Response_LoginDto()
                {
                    Result = false,
                    error = new List<string>()
                {
                    "Refresh Tokens are wrong!"
                }
                };
            }

            if(refreshToken.JwtId != tokenContent.Id)
            {
                return new Response_LoginDto()
                {
                    Result = false,
                    error = new List<string>()
                {
                    "Refresh Tokens are wrong!"
                }
                };
            }


            if(refreshToken.ExpiryDate < DateTime.UtcNow)
            {
                return new Response_LoginDto()
                {
                    Result = false,
                    error = new List<string>()
                {
                    "Refresh Tokens Expired!"
                }
                };
            }

            var newToken = await GenerateToken(user);

            var newRefreshToken = await CreateRefreshToken(user, newToken);

            return new Response_LoginDto()
            {
                Result = true,
                Token = newToken,
                RefreshToken = newRefreshToken,
                UserId = user.Id,
            };
        }

        public async Task<bool> LogoutDeleteRefreshToken(string userId)
        {
            var refreshToken = await _dbContext.RefreshTokens
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (refreshToken == null)
            {
                return false;
            }

            _dbContext.RefreshTokens.Remove(refreshToken);

            await _dbContext.SaveChangesAsync(); // ✅ IMPORTANT

            return true;
        }


        //////////////------------------------------------------------------Not Implemented------------------////////////////////
    }
}

