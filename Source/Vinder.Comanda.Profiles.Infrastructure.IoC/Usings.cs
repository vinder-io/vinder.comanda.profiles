global using System.Diagnostics.CodeAnalysis;

global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

global using Vinder.Comanda.Profiles.Domain.Repositories;
global using Vinder.Comanda.Profiles.Domain.Concepts;

global using Vinder.Comanda.Profiles.CrossCutting.Configurations;
global using Vinder.Comanda.Profiles.CrossCutting.Exceptions;

global using Vinder.Comanda.Profiles.Application.Payloads.Customer;
global using Vinder.Comanda.Profiles.Application.Payloads.Owner;

global using Vinder.Comanda.Profiles.Application.Validators.Customer;
global using Vinder.Comanda.Profiles.Application.Validators.Owner;

global using Vinder.Comanda.Profiles.Application.Handlers.Traceability;
global using Vinder.Comanda.Profiles.Infrastructure.Repositories;

global using Vinder.Internal.Essentials.Contracts;
global using Vinder.Internal.Infrastructure.Persistence.Repositories;

global using Vinder.Dispatcher.Extensions;

global using MongoDB.Driver;
global using FluentValidation;
