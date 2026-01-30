global using System.Diagnostics.CodeAnalysis;
global using System.Text.Json;
global using System.Web;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authorization;

global using Vinder.Comanda.Profiles.WebApi.Extensions;
global using Vinder.Comanda.Profiles.WebApi.Constants;
global using Vinder.Comanda.Profiles.Domain.Errors;

global using Vinder.Comanda.Profiles.Application.Payloads.Traceability;
global using Vinder.Comanda.Profiles.Application.Payloads.Customer;
global using Vinder.Comanda.Profiles.Application.Payloads.Owner;
global using Vinder.Comanda.Profiles.Application.Payloads;

global using Vinder.Comanda.Profiles.Infrastructure.IoC.Extensions;
global using Vinder.Comanda.Profiles.CrossCutting.Configurations;

global using Vinder.Dispatcher.Contracts;
global using Vinder.IdentityProvider.Sdk.Extensions;

global using Scalar.AspNetCore;
global using Serilog;
global using FluentValidation.AspNetCore;