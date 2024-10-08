using abs_backend.Models;
using abs_backend.Repositories;
using BCrypt.Net;
using System.ComponentModel.DataAnnotations;

namespace abs_backend.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        //private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository/*, IEmailService emailService */)
        {
            _userRepository = userRepository;
            //_emailService = emailService;
        }

        public async Task<User> CreateAsync(RegisterModel model)
        {
            if (await _userRepository.UsernameExistsAsync(model.Username))
                throw new ValidationException("Username already exists");

            if (await _userRepository.EmailExistsAsync(model.Email))
                throw new ValidationException("Email already exists");

            var passwordHash = HashPassword(model.Password);
            //var verificationToken = GenerateVerificationToken();
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = passwordHash,
                IsEmailVerified = true,
                EmailVerificationToken = null,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                PreferredContactMethod = model.PreferredContactMethod
            };

            var createdUser = await _userRepository.CreateAsync(user);
            //await _emailService.SendVerificationEmailAsync(model.Email, verificationToken);
            return createdUser;
        }

        public async Task<User> UpdateAsync(UserUpdateModel model)
        {
            var user = await _userRepository.GetByIdAsync(model.Id);
            if (user == null) throw new Exception("User not found");

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.PreferredContactMethod = model.PreferredContactMethod;

            return await _userRepository.UpdateAsync(user);
        }

        //public async Task<bool> VerifyEmailAsync(string token)
        //{
        //    var user = await _userRepository.GetByVerificationTokenAsync(token);
        //    if (user == null) return false;

        //    user.IsEmailVerified = true;
        //    user.EmailVerificationToken = null;
        //    await _userRepository.UpdateAsync(user);
        //    return true;
        //}

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
                return null;

            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        //private string GenerateVerificationToken()
        //{
        //    return Guid.NewGuid().ToString();
        //}
    }
}
