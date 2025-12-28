// Standard .NET namespaces
global using System;
global using System.IO;
global using System.Reflection;
global using AutoMapper;
global using System.Collections.Generic;
global using System.Threading.Tasks;
global using System.Linq;

// Frameworks
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.OpenApi;
global using FluentValidation;

// Domain Specific Namespaces
global using SharedKernel.Models;
global using EShoppingTutorial.Core.Domain;
global using EShoppingTutorial.Core.Domain.Entities;
global using EShoppingTutorial.Core.Domain.ValueObjects;
global using EShoppingTutorialWebAPI.Models.OrderModels;
global using EShoppingTutorial.Core.Application;
global using EShoppingTutorial.Core.Domain.Services;
global using EShoppingTutorial.Core.Domain.Services.Implementations;
global using EShoppingTutorial.Core.Persistence;
global using EShoppingTutorialWebAPI.Filters;
global using EShoppingTutorial.Core.Domain.Enums;



