using System.Text;
using Encurtador.API.Configurations;
using Encurtador.API.Data;
using Encurtador.API.Data.Repositories;
using Encurtador.API.Data.Repositories.Interfaces;
using Encurtador.API.Models;
using Encurtador.API.Services;
using Encurtador.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using Prometheus;
using Serilog;
using Serilog.Settings.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ApiConfiguration(builder.Configuration);
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseApiConfiguration();

app.Run();

