using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SnapShop.Infrastructure.Interface;
using SnapShop.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SnapShop.Framework.Authentication;
public class Jwt : IAuthentication
{
    private readonly IUserRespository _userRespository;

    public Jwt(IUserRespository userRespository)
    {
        _userRespository = userRespository;
    }

    public static void AddToService(IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.MustGet("SECRET_KEY")))
            };
        });
    }

    public static void AddToSwagger(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
        Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    }

    public string Login(string username, string password)
    {
        string? hashedPassword = _userRespository.GetPasswordByUsername(username);
        if (hashedPassword is null)
        {
            throw new UnauthorizedAccessException("Incorrect username");
        }
        bool isPasswordCorrect = isPasswordSame(password, hashedPassword);
        if (!isPasswordCorrect)
        {
            throw new UnauthorizedAccessException("Incorrect password");
        }
        return generateJwtToken(username);
    }

    public async Task<string> Register(User user, string password)
    {
        if (_userRespository.ExistsWithUsername(user.Username))
            throw new ArgumentException("Username already exists");
        if (_userRespository.ExistsWithEmail(user.Email))
            throw new ArgumentException("Email already exists");

        user.Password = encryptPassword(password);

        await _userRespository.Create(user);
        return generateJwtToken(user.Username);
    }

    private string generateJwtToken(string username)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.MustGet("SECRET_KEY")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string encryptPassword(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    private bool isPasswordSame(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
