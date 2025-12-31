// Standard .NET namespaces
global using System.Net;
global using System.Text.Json;

// Frameworks
global using MediatR;
global using AutoMapper;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using FluentValidation;
global using Microsoft.AspNetCore.Mvc.Filters;

// Domain Specific Namespaces
global using SharedKernel.Models;
global using SharedKernel.Exceptions;
global using EShoppingTutorial.Core.Domain;
global using EShoppingTutorial.Core.Domain.Entities;
global using EShoppingTutorial.Core.Domain.ValueObjects;
global using EShoppingTutorial.Core.Application;
global using EShoppingTutorial.Core.Persistence;
global using EShoppingTutorial.Core.Domain.Enums;
global using EShoppingTutorial.Core.Domain.Services;
global using EShoppingTutorial.Infrastructure.ExternalServices;
global using EShoppingTutorialWebAPI.Models.OrderModels;
global using EShoppingTutorialWebAPI.Filters;