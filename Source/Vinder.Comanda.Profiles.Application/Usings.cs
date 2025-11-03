global using System.Text.Json.Serialization;

global using Vinder.Comanda.Profiles.Domain.Entities;
global using Vinder.Comanda.Profiles.Domain.Concepts;
global using Vinder.Comanda.Profiles.Domain.Errors;
global using Vinder.Comanda.Profiles.Domain.Repositories;
global using Vinder.Comanda.Profiles.Domain.Filtering;

global using Vinder.Comanda.Profiles.Application.Mappers;
global using Vinder.Comanda.Profiles.Application.Payloads;
global using Vinder.Comanda.Profiles.Application.Payloads.Traceability;
global using Vinder.Comanda.Profiles.Application.Payloads.Customer;
global using Vinder.Comanda.Profiles.Application.Payloads.Owner;

global using Vinder.Internal.Essentials.Contracts;
global using Vinder.Internal.Essentials.Patterns;
global using Vinder.Internal.Essentials.Entities;
global using Vinder.Internal.Essentials.Filters;
global using Vinder.Internal.Essentials.Primitives;
global using Vinder.Internal.Essentials.Utilities;
global using Vinder.Internal.Essentials.Extensions;

global using Vinder.Dispatcher.Contracts;
global using FluentValidation;