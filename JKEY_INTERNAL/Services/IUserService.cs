using Azure.Core;
using JKEY_INTERNAL.Models;
using JKEY_INTERNAL.Models.CustomModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Collections;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JKEY_INTERNAL.Services
{
    public interface IUserService
    {
        ServiceResponse RegisterUser(User registerModel);
        Task<ServiceResponse> Login(User user, List<UserRole> userRoles);
        Task<ServiceResponse> LoginSystem(LoginModel loginModel);
    }

    public class UserService : IUserService //impliment
    {
        #region Feild
        private readonly JkeyInternalContext _context;
        private IConfiguration _configuration;
        #endregion

        #region Contructor

        public UserService(
            JkeyInternalContext context,      
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        #endregion

        /// <summary>
        /// Đăng ký thông tin người dùng mới
        /// </summary>
        /// <param name="registerModel">thông tin người dùng</param>
        /// <returns>true + message nếu thành công/ false+ message nếu thất bại</returns>
        public ServiceResponse RegisterUser(User registerModel)
        {
            try
            {
                //Nếu tham số null
                if (registerModel == null)
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Data = "Data register not null"
                    };
                }

                //Nếu pass không trùng với Confirm 
                if (registerModel.Password != registerModel.ConfirmPassWord)
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Data = "Confirm password not match to password"
                    };
                }


                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    FullName = registerModel.FullName,
                    UserName = registerModel.UserName,
                    Password = registerModel.Password,
                    Gender = registerModel.Gender,
                    Active = registerModel.Active,
                    Email = registerModel.Email,
                    Phone = registerModel.Phone,
                    DateOfBirth = registerModel.DateOfBirth,
                    Address = registerModel.Address,
                    DateCreated = DateTime.Now,
                    UserCreated = registerModel.UserCreated,
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();

                return new ServiceResponse
                {
                    Success = true,
                    Data = "Register success"
                };
            }
            catch (Exception ex)
            {

                return new ServiceResponse
                {
                    StatusCode=StatusCodes.Status500InternalServerError,
                    Success = false,
                    Data = ex
                };
            }
            //Thực hiện thêm mới                     
            //var result = await _userManager.CreateAsync(identityUser, registerModel.PassWord);//

            //Thành công 
            //if (result.Succeeded)
            //    {                    
            //        // trả về dữ liệu cho client
            //        return new ServiceResponse
            //        {
            //            Success = true,
            //            Data = Resource.RegisterSuccess
            //        };
            //    }
            //    else
            //    {// thất bại
            //        return new ServiceResponse
            //        {
            //            Success = false,

            //            Data = "Register failed"

            //        };
            //    }
            //}
            //else
            //{
            //    //nếu tên người dùng đã tồn tại
            //    return new ServiceResponse
            //    {
            //        Success = false,

            //        Data = "Username already existed"

            //    };
            //}

        }
       
        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="loginModel">Thông tin đăng nhập</param>
        /// <returns>true + message nếu thành công/ false+ message nếu thất bại</returns>
        public async Task<ServiceResponse> Login(User user, List<UserRole> userRoles)
        {
            try
            {                
                if (user == null)
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Data = "Login Failed"
                    };

                }
                else if (userRoles.Count == 0 || userRoles == null)
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Data = "User authorize"
                    };
                }
                else
                {
                    // thêm thông tin payload token
                    var authClaims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),//ID của JWT

                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),// thời điểm phát hành token tính theo UNIX time

                        new Claim(ClaimTypes.Name, user.UserName),//Tên đăng nhập
                                                                    //
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),//Id người dùng
                    };

                    // thêm role
                    
                    //for (int i = 0; i < userRoles.Count; i++)
                    //{
                    //    authClaims.Add(new Claim(ClaimTypes.Role, userRoles[i].ToString()));
                    //}
                    DateTime now = DateTime.Now;

                    TimeSpan expireTimeSpanInsideToken = now.AddDays(7) - now;

                    TimeSpan expireTimeSpanToValidateToken = now.AddDays(7) - now;

                    var key = new SymmetricSecurityKey(Encoding.Default.GetBytes("Y2F0Y2hlciUyMHdvbmclMjBsb3ZlJTIwLm5ldA=="));
                    var securityKey1 = new SymmetricSecurityKey(Encoding.Default.GetBytes("ProEMLh5e_qnzdNU"));
                    var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);// kiểu mã hóa
                    //var signingCredentials = new SigningCredentials(
                    //  key,
                    //  SecurityAlgorithms.HmacSha512);

                    var encryptingCredentials = new EncryptingCredentials(
                            securityKey1,
                            SecurityAlgorithms.Aes128KW,
                            SecurityAlgorithms.Aes128CbcHmacSha256);

                    //gọi hàm tạo token
                    var token = CreateToken(authClaims);

                    // convert sang chuỗi
                    var access_token = new JwtSecurityTokenHandler().WriteToken(token);
                    //var tokenString = new JwtSecurityTokenHandler().CreateJwtSecurityToken(
                    //    issuer: "JITS",
                    //    audience: "WEBSERVICE",
                    //    subject: new ClaimsIdentity(authClaims),
                    //    notBefore: DateTime.Now,
                    //    expires: DateTime.Now.Add(expireTimeSpanInsideToken),
                    //    issuedAt: DateTime.Now,
                    //    signingCredentials: signingCredentials,
                    //    encryptingCredentials: encryptingCredentials);


                    //var access_token = new JwtSecurityTokenHandler().WriteToken(tokenString);



                    using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
                    {
                        var randomBytes = new byte[64];
                        rngCryptoServiceProvider.GetBytes(randomBytes);

                        RefreshTokenModel refreshTokenModel = new RefreshTokenModel
                        {
                            RefreshToken = Convert.ToBase64String(randomBytes),
                            RefreshToken_ExpiryDate = DateTime.Now.AddDays(7),
                            CreatedByIp = "",
                            RefreshToken_ExpiryDateTimeSpan = TimeSpan.FromDays(7),
                            Token = access_token,
                            Token_ExpiryDate = DateTime.Now.Add(expireTimeSpanToValidateToken),
                            Token_ExpiryDateTimeSpan = expireTimeSpanToValidateToken,
                            LastUsedDate = DateTime.Now
                        };

                        string keycache = "key1";

                        var check=Cache.GetString(keycache, _configuration["ModuleCache"]);
                        Cache.DelString(keycache, _configuration["ModuleCache"]);

                        Cache.SetString(keycache, JsonConvert.SerializeObject(refreshTokenModel), _configuration["ModuleCache"]);

                       
                        return new ServiceResponse
                        {
                            Data = refreshTokenModel,
                            Success = true,
                        };
                    }
                    //Trả về dữ liệu cho client
                    //return new ServiceResponse
                    //{
                    //    Data = new
                    //    {
                    //        AccessToken = access_token,
                    //        Expiration = token.ValidTo,
                    //        Rol = userRoles,
                    //        UserName = user.UserName,
                    //    },
                    //    Success = true,
                    //};

                }
            }
            catch (Exception ex)
            {

                return new ServiceResponse
                {
                    Data = ex,
                    Success = false,
                };
            }
        }


        public async Task<ServiceResponse> LoginSystem(LoginModel jBodyRequest)
        {
            try
            {
                if (jBodyRequest == null)
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Data = "Login Failed"
                    };
                }

                if (string.IsNullOrEmpty(jBodyRequest.UserName) || string.IsNullOrEmpty(jBodyRequest.PassWord))
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Data = "Login Failed"
                    };
                }
                var user = _context.Users.Where(x => x.UserName == jBodyRequest.UserName && x.Password == jBodyRequest.PassWord).FirstOrDefault();

                if (user == null)
                {

                    return new ServiceResponse
                    {
                        Success = false,
                        Data = "Login Failed"
                    };

                }
                var token = await GenerateToken(user.Id.ToString());
                if (token.Success)
                {
                    return new ServiceResponse
                    {
                        Success = true,
                        Data = token
                    };
                }
                else
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Data = "Login Failed"
                    };
                }

                
            }
            catch (Exception ex)
            {

                return new ServiceResponse
                {
                    Success = false,
                    Data = ex
                };
            }
        }

        public async Task<TokenResponse> GenerateToken(string claim)
        {
            try
            {
                string userId = string.Empty;
              
                //string[] splitClaim = claim.Split('|');
                if (claim.Length>0)
                {
                    userId = claim;
                    
                }

                RefreshTokenModel refreshTokenModel = await GenerateRefreshToken(userId);

                TokenResponse jresponse = new TokenResponse
                {       Success=true,
                        AccessToken = refreshTokenModel.Token,
                        TokenType= "Bearer",
                        RefreshToken = refreshTokenModel.RefreshToken,
                        ExpiresIn= (int)refreshTokenModel.Token_ExpiryDateTimeSpan.TotalSeconds,
                        RefreshTokenExpiresIn =(int)refreshTokenModel.RefreshToken_ExpiryDateTimeSpan.TotalSeconds,
                };
               
                return jresponse;
            }
            catch (Exception ex)
            {
                //LogProcess.LogError(ex, Assembly.GetExecutingAssembly().ToString(), MethodBase.GetCurrentMethod().Name);
                return new TokenResponse
                {
                    Success = false,
                 
                };
            }
        }

        public async Task<RefreshTokenModel> GenerateRefreshToken(string claim)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.Default.GetBytes("Y2F0Y2hlciUyMHdvbmclMjBsb3ZlJTIwLm5ldA=="));
            var securityKey1 = new SymmetricSecurityKey(Encoding.Default.GetBytes("ProEMLh5e_qnzdNU"));

            var claims = claim;


            DateTime now = DateTime.Now;
            TimeSpan expireTimeSpanInsideToken = now.AddDays(7) - now;
            TimeSpan expireTimeSpanToValidateToken = now.AddDays(7) - now;

            var signingCredentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha512);

            var encryptingCredentials = new EncryptingCredentials(
                    securityKey1,
                    SecurityAlgorithms.Aes128KW,
                    SecurityAlgorithms.Aes128CbcHmacSha256);

            var jwtSecurityToken = handler.CreateJwtSecurityToken(
                "JITS",
                "WEBSERVICE",
                new ClaimsIdentity(claims),
                DateTime.Now,
                DateTime.Now.Add(expireTimeSpanInsideToken),
                DateTime.Now,
                signingCredentials,
                encryptingCredentials);

            var access_token = handler.WriteToken(jwtSecurityToken);

            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);

                RefreshTokenModel refreshTokenModel = new RefreshTokenModel
                {
                    RefreshToken = Convert.ToBase64String(randomBytes),
                    RefreshToken_ExpiryDate = DateTime.Now.AddDays(7),                    
                    RefreshToken_ExpiryDateTimeSpan = TimeSpan.FromDays(7),
                    Token = access_token,
                    Token_ExpiryDate = DateTime.Now.Add(expireTimeSpanToValidateToken),
                    Token_ExpiryDateTimeSpan = expireTimeSpanToValidateToken,
                    LastUsedDate = DateTime.Now
                };

                var keyCache = claim;
                Cache.SetString(keyCache, JsonConvert.SerializeObject(refreshTokenModel), _configuration["ModuleCache"]);
                var valueCache = Cache.GetString(keyCache, _configuration["ModuleCache"]);
                //Cache.DelString(keyCache, "cache_Jkey");

                if (!string.IsNullOrEmpty(valueCache))
                {
                    var vl = valueCache.Split(',');

                    for (int i = 0; i < vl.Length; i++)
                    {
                        if (vl[i].Contains("Token_ExpiryDateTimeSpan"))
                        {
                            var e = "Token_ExpiryDateTimeSpan".Length;
                            var result = vl[i];
                            var a = result.Split("\"");
                            var exp = int.Parse(a[4]);
                            //var vl1 = result.Substring(24, a);
                           // var vl2 = vl1[2].StartWith(":");
                        }
                    }
                }

                return  refreshTokenModel;
            }
        }

        //public bool ValidateTokenWhenHaveRequestFromClient(TransactionInfo trans, out List<string> listReason)
        //{
        //    return ValidateTokenWhenHaveRequestFromClient(trans.TokenInfo.UserId + "|" + trans.TokenInfo.ServiceId + "|" + trans.TokenInfo.FirstLogin, trans.IpAddress, trans.TokenInfo.AccessToken, trans.ApiTransId, out listReason);
        //}

        public bool ValidateTokenWhenHaveRequestFromClient(string claim, string token, out List<string> listReason)
        {
            listReason = new List<string>();          
            string userId = string.Empty;
            if (claim.Length > 0)
            {
                userId = claim;
            }          
            

            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }

            bool TokenValid = false;
            string key = userId;
           

            string valueCache = Cache.GetString(key, _configuration["ModuleCache"]);

            if (!string.IsNullOrEmpty(valueCache))
            {
                listReason.Add(valueCache);
                TimeSpan expireTimeSpanToValidateToken = GetTokenExpire(userId) - DateTime.Now;

                RefreshTokenModel refreshTokenModel = JsonConvert.DeserializeObject<RefreshTokenModel>(valueCache);
                if (refreshTokenModel.Token_ExpiryDate > DateTime.Now &&
                    refreshTokenModel.Token.Equals(token) 
                    )
                {
                    refreshTokenModel.LastUsedDate = DateTime.Now;
                    refreshTokenModel.Token_ExpiryDate = DateTime.Now.Add(expireTimeSpanToValidateToken);
                    refreshTokenModel.Token_ExpiryDateTimeSpan = expireTimeSpanToValidateToken;
                    Cache.SetString(key, JsonConvert.SerializeObject(refreshTokenModel), _configuration["ModuleCache"]);
                    TokenValid = true;
                }
                else
                {
                    listReason.Add($"Token can be expired or Token not match or IP not match!");
                }
            }
            else
            {
                listReason.Add($"Can not found {key} in cache!");
               // LogProcess.LogInformation(Information: "{LogName}, {Message}", propertyValues: new object[] { "ValidateTokenWhenHaveRequestFromClient", $"{claim} and {ipAddress} not exist in system or invalid!" }, ApiGwTranId: RequestId);
            }
            return TokenValid;
        }




        private DateTime GetTokenExpire(string userId)
        {
            string defaultKeyName = "DEFAULT_TOKENEXPIREDATE";
            string keyName = "_TOKENEXPIREDATE";

           string  dataFromCache = Cache.GetString(userId, "Cache_Jkey");

            if (string.IsNullOrEmpty(dataFromCache))
            {
                var vl=dataFromCache.Split(',');
            }

            //if (string.IsNullOrEmpty(dataFromCache))
            //{
            //    var strExpireTime= int.Parse(strExpireTime.Substring(0, strExpireTime.Length - 1));
            //}

            //string strExpireTime = listSysVariableTable.Where(item => item.Name.Equals(userId + keyName)).Select(s => s.Value).FirstOrDefault();

            //if (string.IsNullOrEmpty(strExpireTime))
            //{
            //    strExpireTime = listSysVariableTable.Where(item => item.Name.Equals(defaultKeyName)).Select(s => s.Value).FirstOrDefault();
            //}

            //int param01 = int.Parse(strExpireTime.Substring(0, strExpireTime.Length - 1));
            //string param02 = strExpireTime.Substring(strExpireTime.Length - 1, 1);

            //DateTime finalDateTime = DateTime.Now;
            //switch (param02)
            //{
            //    case "s":
            //        finalDateTime = DateTime.Now.AddSeconds(param01);
            //        break;
            //    case "m":
            //        finalDateTime = DateTime.Now.AddMinutes(param01);
            //        break;
            //    case "h":
            //        finalDateTime = DateTime.Now.AddHours(param01);
            //        break;
            //    case "d":
            //        finalDateTime = DateTime.Now.AddDays(param01);
            //        break;
            //    case "M":
            //        finalDateTime = DateTime.Now.AddMonths(param01);
            //        break;
            //    case "y":
            //        finalDateTime = DateTime.Now.AddYears(param01);
            //        break;
            //}

            return DateTime.Now;
        }




        /// <summary>
        /// Sinh token
        /// </summary>
        /// <param name="authClaims"></param>
        /// <returns></returns>
        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);// kiểu mã hóa

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: authClaims,
                expires: DateTime.UtcNow.AddDays(7),// xét thời gian tồn tại 
                signingCredentials: signIn);//chữ ký
            return token;
        }





    }
}
