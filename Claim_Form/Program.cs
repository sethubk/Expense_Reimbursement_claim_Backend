using Claim_Form.Data;
using Claim_Form.Repositories.Implementations;
using Claim_Form.Repositories.Interface;
using Claim_Form.Services.Implementations;
using Claim_Form.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// ----------------------
// SERVICE REGISTRATIONS
// ----------------------

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IRecentClaimService, RecentClaimService>();
builder.Services.AddScoped<IRecentClaimRepository, RecentClaimRepository>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IInternationalTravelRepository, InternationalTravelRepository>();
builder.Services.AddScoped<IInternationalTravelService, InternationalTravelService>();
builder.Services.AddScoped<IInternationalRepository, InternationalRepository>();
builder.Services.AddScoped<IInternationalServices, InternationalService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IDomesticRepository, DomesticRepository>();
builder.Services.AddScoped<IDomesticService, DomesticService>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["Jwt:Issuer"],
           ValidAudience = builder.Configuration["Jwt:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
       };
   });

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                .AddNegotiate();

// ----------------------
// SWAGGER MUST BE HERE!
// ----------------------
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Claim Form API",
        Version = "v1",
        Description = "API for employee claims with JWT authentication"
    });

    c.EnableAnnotations();
});

// ----------------------
// BUILD APP (AFTER ALL SERVICES)
// ----------------------
var app = builder.Build();


// ----------------------
// MIDDLEWARE PIPELINE
// ----------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();