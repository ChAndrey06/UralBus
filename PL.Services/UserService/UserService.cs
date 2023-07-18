using System.Security.Claims;
using AutoMapper;
using BLL.Interfaces;
using Common.Delta;
using Common.Enums;
using Common.Helpers;
using Newtonsoft.Json;
using PL.Entities.PersonIdentity;
using PL.Entities.User;
using PL.Mappings.MapperFactory;
using PL.Services.Admin.Models;
using PL.Services.Interfaces.UserService;
using AuthCode = BLL.Entities.AuthCode.AuthCode;
using Environment = System.Environment;

namespace PL.Services.UserService;

public class UserService : IUserService
{
    private readonly IRepository<BLL.Entities.User.User, Guid> _userRepository;
    private readonly IRepository<BLL.Entities.AuthCode.AuthCode, Guid> _authCodeRepository;
    private readonly IRepository<BLL.Entities.PersonIdentity.ClientPersonIdentity, Guid> _clientPersonIdentityRepository;

    private readonly IMapper _mapper;

    public UserService(
        IRepository<BLL.Entities.User.User, Guid> userRepository, 
        IRepository<AuthCode, Guid> authCodeRepository,
        IRepository<BLL.Entities.PersonIdentity.ClientPersonIdentity, Guid> clientPersonIdentityRepository
    )
    {
        _userRepository = userRepository;
        _authCodeRepository = authCodeRepository;
        _clientPersonIdentityRepository = clientPersonIdentityRepository;
        _mapper = MapperFactory.GetMapper();
    }

    /// <summary>
    /// Проверка пароля по мейлу
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns>bool</returns>
    public async Task<bool> CheckPasswordAsync(string email, string password)
    {
        var users = await _userRepository.GetAsync(filter: u => u.Email == email);

        if (users.TotalCount == 0)
        {
            return false;
        }

        var user = users.Items?.First();

        var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, user?.PasswordHash);

        return isPasswordCorrect;
    }
    
    /// <summary>
    /// Получение пользователя по мейлу
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<User> GetUserByEmailAsync(string username)
    {
        var getWrapper = await _userRepository.GetAsync(filter: u => u.Email == username, includeProperties: new[] { "Identities" });
        var user = getWrapper?.Items?.FirstOrDefault();
        return _mapper.Map<User>(user);
    }

    /// <summary>
    /// Получить клеймы пользователя по его объекту (PL.Entities.User.User)
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public IEnumerable<Claim> GetUserClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.PhoneNumber ?? string.Empty),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new (ClaimTypes.Authentication,"Cookie"),
            new(ClaimTypes.UserData,JsonConvert.SerializeObject(user, new JsonSerializerSettings
{               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }))
        };

        claims.AddRange(user.Roles.ConvertFlagsToString().Split(", ").Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }

    /// <summary>
    /// Получение пользователя (PL.Entities.User.User) по номеру телефона
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns>User</returns>
    public async Task<User> GetUserByPhoneNumberAsync(string phoneNumber)
    {
        var getWrapper = await _userRepository.GetAsync(filter: u => u.PhoneNumber == phoneNumber, includeProperties: new[] { "Identities" });
        var user = getWrapper?.Items?.FirstOrDefault();

        return _mapper.Map<User>(user);
    }

    /// <summary>
    /// Сохранение кода авторизации в БД
    /// </summary>
    /// <param name="username"></param>
    /// <param name="authCode"></param>
    /// <param name="type"></param>
    public async Task SaveAuthCodeAsync(string username, string authCode, AuthCodeType type)
    {
        var user = await GetUserByEmailAsync(username);

        var code = new AuthCode()
        {
            UserId = user.Id,
            Code = authCode,
            Type = type.ToString(),
            //TODO: add hashing for code
        };

        await _authCodeRepository.AddAsync(code);
    }

    public async Task<ValidateCodeResult> VerifyCodeAsync(string phone, string authCode, AuthCodeType type)
    {
        var user = await GetUserByPhoneNumberAsync(phone);

        var codes = await _authCodeRepository.GetAsync(filter: c =>
            c.UserId == user.Id &&
            c.Code == authCode &&
            c.Type == type.ToString()
            //,
           // orderBy: o => o.OrderByDescending(c => c.CreatedAt)
            );

        if (codes.TotalCount == 0)
        {
            return ValidateCodeResult.Invalid;
        }

        var code = codes.Items?.First();

        if (DateTime.UtcNow > code.ExpiresAt.AddHours(3))
        {
            return ValidateCodeResult.ExpiredByTime;
        }

        if (code.Used)
        {
            return ValidateCodeResult.AlreadyUsed;
        }

        await _authCodeRepository.PatchAsync(code.Id, new Delta<AuthCode>()
        {
            {nameof(code.Used),true}
        });

        return ValidateCodeResult.Valid;
    }

    public async Task<bool> CheckUserExistsAsync(string username, string phoneNumber)
    {
        var user = await _userRepository.GetAsync(filter: u =>
            u.Email == username || u.PhoneNumber == phoneNumber
        );

        return user.TotalCount > 0;
    }

    public async Task<bool> CheckUserExistsByPhoneAsync(string phone)
    {
        var user = await _userRepository.GetAsync(filter: u => u.PhoneNumber == phone);

        return user.TotalCount > 0;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        var userEntity = _mapper.Map<BLL.Entities.User.User>(user);

        userEntity = await _userRepository.AddAsync(userEntity);

        return _mapper.Map<User>(userEntity);
    }


    public async Task<string> CreateCodeAsync(Guid userId, AuthCodeType type)
    {
        var code = string.Empty;

        if (Environment.GetEnvironmentVariable("USE_MOCK_CODES") == "true")
        {
            code = "1234";
        }
        else
        {
             code = new Random().Next(1, 10000).ToString("0000");
        }

        await _authCodeRepository.AddAsync(new AuthCode
        {
            UserId = userId,
            Code = code,
            Type = type.ToString()
        });

        return code;
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        var getWrapper = await _userRepository.GetAsync(filter: u => u.Id == id, includeProperties: new[] { "Identities" });
        var user = getWrapper?.Items?.FirstOrDefault();

        return _mapper.Map<User>(user);
    }
    
    public async Task DeleteUserByIdAsync(Guid id)
    {
        await _userRepository.DeleteAsync(id, true);
    }

    public async Task<User> GetClientByIdAsync(Guid id)
    {
        var user = await GetUserByIdAsync(id);
        var clientIdentity =
            (await _clientPersonIdentityRepository.GetAsync(filter: r =>
                r.Discriminator == PersonIdentityType.Client.ToString() && r.UserId == id)).Items?.First();
        
        user.ClientIdentity = _mapper.Map<ClientPersonIdentity?>(clientIdentity);
        
        return user;
    }
    
    public async Task UpdateClientAsync(Guid clientId, User userModel)
    {
        var client = await GetClientByIdAsync(clientId);
        
        await _userRepository.PatchAsync(client.Id, new Delta<BLL.Entities.User.User>()
        {
            { "BirthDate", userModel.BirthDate },
            { "FirstName", userModel.FirstName },
            { "LastName", userModel.LastName },
            { "Sex", userModel.Sex },
            { "Patronymic", userModel.Patronymic },
            { "Email", userModel.Email },
            { "PhoneNumber", userModel.PhoneNumber },
        });
        
        await _clientPersonIdentityRepository.PatchAsync(client.ClientIdentity.Id, new Delta<BLL.Entities.PersonIdentity.ClientPersonIdentity>()
        {
            { "IdentificationDocumentType", userModel.DocumentType.ToString() },
            { "IdentificationDocumentNumber", userModel.DocumentNumber },
            { nameof(userModel.SendAdvertisements), userModel.SendAdvertisements }
        });
    }
}