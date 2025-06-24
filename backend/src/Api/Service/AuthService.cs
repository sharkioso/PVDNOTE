using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Api.DTO;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Infrastructure.Data;

public class AuthService
{
    private readonly DBContext context;
    public AuthService(DBContext context)
    {
        this.context = context;
    }

    public async Task<User> RegisterService(UserRegisterDto dto)
    {
        if (await context.Users.AnyAsync(u => u.Login == dto.Login))
            throw new ApplicationException("Email уже зарегистрирован ");

        var user = new User
        {
            Login = dto.Login,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }

    public async Task<User> LoginService(UserLoginDto dto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Login == dto.Login);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            throw new ApplicationException("неверный пароль или email");
        return user;
    }


}