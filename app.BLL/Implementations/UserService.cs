using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using app.BLL.DTO;
using app.BLL.Exceptions;
using app.BLL.Services;
using app.DAL.Model;
using app.DAL.Repositories;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;


namespace app.BLL.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitofWork unitofWork;
        public IMapper mapper;
        public readonly IValidator<UserDTO> validator;
        private readonly IConfiguration configuration;
        public UserService(IUnitofWork unitofWork, IMapper mapper, IValidator<UserDTO> validator, IConfiguration configuration)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
            this.validator = validator;
            this.configuration = configuration;
        }   
        public async Task<ApiResponse> GetUserById(int id)
        {
            var getUser = await unitofWork.userRepository.GetAsync(id);
            if(getUser==null)
            {
                throw new NotFoundException($"User of this {id} could not be found");
            }
            return new ApiResponse(200, "User Displayed successfully", getUser);
        }
        public async Task<ApiResponse> GetUsers()
        {
            var getUsers = await unitofWork.userRepository.GetAllAsync();
            if(getUsers==null)
            {
                throw new NotFoundException($"Users could not be found");
            }
            return new ApiResponse(200, "Users displayed successfully", getUsers);
        }
        public async Task<ApiResponse> AddUser(UserDTO user) 
        {
            try
            {
                var validate_user =  validator.Validate(user);
                GeneratePasswordHash(user.Password, out byte[] passwordhash, out byte[] passwordsalt);
                if (validate_user.IsValid)
                {
                    var addUser = new User()
                    {
                        Email = user.Email,
                        PasswordSalt = passwordsalt,
                        PasswordHash = passwordhash,
                        UserName = user.UserName,
                        VerificationToken = CreateToken(),
                        RoleID = user.RoleId
                    };

                    await unitofWork.userRepository.Post(addUser);
                    await unitofWork.Save();
                    return new ApiResponse(200, "new user added successsfully", addUser);
                }
                else
                {
                    throw new BadRequestException(validate_user.ToString());
                }
            }
            catch(Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }

           
        } 
        public async Task<ApiResponse> UpdateUser(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }
        public async Task<ApiResponse> PatchUpdateUser(int id, JsonPatchDocument patch)
        {
            try
            {
                var updated_user = await unitofWork.userRepository.UpdateUserPatch(id, patch);
                return new ApiResponse(200, "User Updated Successfully", updated_user);
            }
            catch(Exception ex)
            {
                throw new BadRequestException(ex.Message);  
            }

        }
        public async Task<ApiResponse> DeleteUser(int id)
        {
            var userId = await unitofWork.userRepository.Delete(id);
            if(userId== null)
            {
                throw new NotFoundException($"User of this {id} could not be found");
            }
            return new ApiResponse(200, "User deleted successfully", userId);
        }
        
        public async Task<ApiResponse> LoginUser(UserLoginDTO userLogin)
        {
                var search_user = await unitofWork.FindByUserEmail(userLogin.Email);
                if (search_user == null)
                {
                    throw new NotFoundException($"User of this email {userLogin.Email} could not be found");
                }
                List<Claim> Claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, search_user.UserName),
                new Claim(ClaimTypes.Role, search_user.RoleName)
                };

                if (GetPasswordHash(userLogin.Password, search_user.PasswordHash, search_user.PasswordSalt))
                {
                    var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT").GetSection("SecretKey").Value));
                    var SigningCredentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha512);
                    var token = new JwtSecurityToken(
                        issuer: configuration.GetSection("JWT").GetSection("ValidIssuer").Value,
                        audience: configuration.GetSection("JWT").GetSection("ValidAudience").Value,
                        claims: Claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: SigningCredentials
                        );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return new ApiResponse(200, "JWT Token generated successfully", tokenString);

                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            
            
        }
        
        private static void GeneratePasswordHash(string Password, out byte[] passwordhash, out byte[] passwordsalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
            }
        }
        private string CreateToken()
        {
            var token = Guid.NewGuid().ToString();
            return token;
        }
        private bool GetPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using(var hmac = new HMACSHA512(PasswordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
                return computeHash.SequenceEqual(PasswordHash);
            }
            
        }
    }   
}
