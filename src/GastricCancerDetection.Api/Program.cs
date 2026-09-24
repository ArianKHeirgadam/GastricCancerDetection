using GastricCancerDetection.Application.Interfaces;
using GastricCancerDetection.Infrastructure.Persistence;
using GastricCancerDetection.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder=WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GastricCancerDbContext>(o=>o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),sql=>sql.EnableRetryOnFailure(5)));
builder.Services.AddScoped<IAnalysisService,AnalysisService>();
builder.Services.AddScoped<IGenomeAnalysisService,GenomeAnalysisService>();
builder.Services.AddScoped<IVariantAnalysisService,VariantAnalysisService>();
builder.Services.AddScoped<ICNVAnalysisService,CNVAnalysisService>();
builder.Services.AddScoped<ICfDNADetectabilityService,CfDNADetectabilityService>();
builder.Services.AddScoped<ICandidateScoringService,CandidateScoringService>();
builder.Services.AddScoped<IAnalysisRunService,AnalysisRunService>();
builder.Services.AddScoped<IReportService,ReportService>();
var jwtKey=builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is missing.");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o=>o.TokenValidationParameters=new TokenValidationParameters{
 ValidateIssuerSigningKey=true,IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
 ValidateIssuer=false,ValidateAudience=false,ValidateLifetime=true,ClockSkew=TimeSpan.FromMinutes(1)
});
builder.Services.AddAuthorization();
var app=builder.Build();
if(app.Environment.IsDevelopment()){app.UseSwagger();app.UseSwaggerUI();}
app.UseHttpsRedirection();
app.UseAuthentication();app.UseAuthorization();
app.MapControllers();
app.Run();
