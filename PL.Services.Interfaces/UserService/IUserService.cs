using System.Security.Claims;
using Common.Enums;
using PL.Entities.PersonIdentity;
using PL.Entities.User;

namespace PL.Services.Interfaces.UserService;

public interface IUserService
{
    /// <summary>
    /// Проверка пароля по мейлу
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns>bool</returns>
    Task<bool> CheckPasswordAsync(string username, string password);
    /// <summary>
    /// Получение пользователя по мейлу
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    Task<User> GetUserByEmailAsync(string username);
    /// <summary>
    /// Получить клеймы пользователя по его объекту (PL.Entities.User.User)
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    IEnumerable<Claim> GetUserClaims(User user);
    
    /// <summary>
    /// Получение пользователя (PL.Entities.User.User) по номеру телефона
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns>User</returns>
    Task<User> GetUserByPhoneNumberAsync(string phoneNumber);
    
    /// <summary>
    /// Сохранение кода авторизации в БД
    /// </summary>
    /// <param name="username"></param>
    /// <param name="authCode"></param>
    /// <param name="type"></param>
    Task SaveAuthCodeAsync(string username, string authCode, AuthCodeType type);
    
    /// <summary>
    /// Проверка кода авторизации
    /// </summary>
    /// <param name="phone"></param>
    /// <param name="authCode"></param>
    /// <returns></returns>
    Task<ValidateCodeResult> VerifyCodeAsync(string phone, string authCode, AuthCodeType type);
    
    /// <summary>
    /// Проверка существования пользователя по мейлу и номеру телефона
    /// </summary>
    /// <param name="username"></param>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    Task<bool> CheckUserExistsAsync(string username, string phoneNumber);
    Task<bool> CheckUserExistsByPhoneAsync(string phone);

    Task<string> CreateCodeAsync(Guid userId, AuthCodeType type);
    
    /// <summary>
    /// Создание пользователя
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<User> CreateUserAsync(User user);

    Task<User> GetUserByIdAsync(Guid id);
    Task<User> GetClientByIdAsync(Guid id);

    Task DeleteUserByIdAsync(Guid id);

    Task UpdateClientAsync(Guid clientId, User userModel);
}