using Ecomerce_Project20260324.Configuration;
using Ecomerce_Project20260324.Data;
using Ecomerce_Project20260324.Models.AuthModels;
using Ecomerce_Project20260324.Models.Bravo;
using Ecomerce_Project20260324.Repository;
using Ecomerce_Project20260324.Repository.AuthRepository;
using Ecomerce_Project20260324.Repository.Banner;
using Ecomerce_Project20260324.Repository.BaseProduct;
using Ecomerce_Project20260324.Repository.CustomerFeedback;
using Ecomerce_Project20260324.Repository.MainCategory;
using Ecomerce_Project20260324.Repository.Material;
using Ecomerce_Project20260324.Repository.ProductColorRepository;
using Ecomerce_Project20260324.Repository.ProductSizeRepository;
using Ecomerce_Project20260324.Repository.ProductVarient;
using Ecomerce_Project20260324.Repository.Status;
using Ecomerce_Project20260324.Repository.ValidationRepository;
//using Ecomerce_Project20260324.Repository.BravoRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Extensions.DependencyInjection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});


//Setting for Identity like Loging 
builder.Services.AddDefaultIdentity<ApiUser>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    options.User.RequireUniqueEmail = true;

}).AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddTokenProvider<DataProtectorTokenProvider<ApiUser>>("PortfolioLoginAPI")
.AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{

    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSetting:Issuser"],
        ValidAudience = builder.Configuration["JwtSetting:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSetting:Key"]))
    };
});


// Add services to the container.

//Commentend for Email Sending Testing

//builder.Services.AddSendGrid(options =>
//{
//    options.ApiKey = builder.Configuration.GetSection("SendGridEmailSettings")
//    .GetValue<string>("APIKey");

//});

//End Here mail sending testing


builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddControllers();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IValidationRepository, ValidationRepository>();
builder.Services.AddScoped<IMainCategoryRepository, MainCategoryRepository>();
builder.Services.AddScoped<IProductColorRepository, ProductColorRepository>();
builder.Services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
builder.Services.AddScoped<IBaseProductRepository, BaseProductRepository>();
builder.Services.AddScoped<IBannerRepository, BannerRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<ICustomerFeedbackRepository, CustomerFeedbackRepository>();
builder.Services.AddScoped<IProductVariantRepository, ProductVariantRepository>();

builder.Services.Configure<BrevoSettings>(
    builder.Configuration.GetSection("BrevoSettings"));

builder.Services.AddScoped<IBrevoEmailService, BrevoEmailService>();
//builder.Services.AddSendGrid(options =>
//{
//    options.ApiKey = builder.Configuration.GetSection("SendGridEmailSettings")
//    .GetValue<string>("APIKey");

//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(); // to access wwwroot folder for images and other static files
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
