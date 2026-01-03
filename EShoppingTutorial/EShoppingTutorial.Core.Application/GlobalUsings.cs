global using System.Reflection;

// Frameworks
global using MediatR;
global using AutoMapper;
global using FluentValidation;
global using Microsoft.Extensions.DependencyInjection;

// Domain Specific Namespaces
global using SharedKernel.Models;
global using EShoppingTutorial.Core.Domain;
global using EShoppingTutorial.Core.Domain.Entities;
global using EShoppingTutorial.Core.Domain.ValueObjects;
global using EShoppingTutorial.Core.Domain.Services;
global using EShoppingTutorial.Core.Domain.Enums;
global using EShoppingTutorial.Core.Application.Orders.Commands.CreateOrder;
global using EShoppingTutorial.Core.Application.Orders.Queries;
global using EShoppingTutorial.Core.Application.Common;
global using EShoppingTutorial.Core.Application.Orders;