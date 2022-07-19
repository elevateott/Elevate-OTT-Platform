﻿global using Azure.Storage.Blobs;
global using Azure.Storage.Blobs.Models;

global using ElevateOTT.Application.Common.Extensions;
global using ElevateOTT.Application.Common.Extensions.Identity;
global using ElevateOTT.Application.Common.Helpers.Validators;
global using ElevateOTT.Application.Common.Interfaces.Persistence;
global using ElevateOTT.Application.Common.Interfaces.Services;
global using ElevateOTT.Application.Common.Interfaces.Services.DemoUserServices;
global using ElevateOTT.Application.Common.Interfaces.Services.HubServices;
global using ElevateOTT.Application.Common.Interfaces.Services.StorageServices;
global using ElevateOTT.Application.Common.Interfaces.UseCases.Identity;
global using ElevateOTT.Application.Common.Interfaces.UseCases.POC;
global using ElevateOTT.Application.Common.Interfaces.UseCases.Reports;
global using ElevateOTT.Application.Common.Interfaces.UseCases.Settings;
global using ElevateOTT.Application.Common.Models;
global using ElevateOTT.Application.Common.Models.ApplicationOptions;
global using ElevateOTT.Application.Common.Models.ApplicationOptions.ApplicationIdentityOptions;
global using ElevateOTT.Application.Features.POC.Applicants.Queries.ExportApplicants;
global using ElevateOTT.Application.Features.POC.Applicants.Queries.GetApplicants;
global using ElevateOTT.Application.Services;
global using ElevateOTT.Application.Services.Demo;
global using ElevateOTT.Application.UseCases.Identity;
global using ElevateOTT.Application.UseCases.POC;
global using ElevateOTT.Application.UseCases.Reports;
global using ElevateOTT.Application.UseCases.Settings;
global using ElevateOTT.BackendResources;
global using ElevateOTT.Domain.Common.Interfaces;
global using ElevateOTT.Domain.Entities;
global using ElevateOTT.Domain.Entities.Identity;
global using ElevateOTT.Domain.Entities.POC;
global using ElevateOTT.Domain.Entities.Settings;
global using ElevateOTT.Domain.Entities.Settings.IdentitySettings;
global using ElevateOTT.Domain.Enums;
global using ElevateOTT.Infrastructure.Extensions;
global using ElevateOTT.Infrastructure.Identity.Extensions;
global using ElevateOTT.Infrastructure.Identity.Settings;
global using ElevateOTT.Infrastructure.Identity.Validators;
global using ElevateOTT.Infrastructure.Persistence;
global using ElevateOTT.Infrastructure.Services;
global using ElevateOTT.Infrastructure.Services.IdentityServices;
global using ElevateOTT.Infrastructure.Services.StorageServices;

global using DocumentFormat.OpenXml;
global using DocumentFormat.OpenXml.Packaging;
global using DocumentFormat.OpenXml.Spreadsheet;

global using Hangfire;
global using Hangfire.SqlServer;

global using IronPdf;

global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Localization;
global using Microsoft.AspNetCore.Mvc.ApplicationParts;
global using Microsoft.AspNetCore.Mvc.Controllers;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.EntityFrameworkCore.Metadata.Internal;
global using Microsoft.EntityFrameworkCore.Query;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Localization;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;

global using Stubble.Core.Builders;

global using System;
global using System.Collections.Generic;
global using System.Collections.ObjectModel;
global using System.ComponentModel.DataAnnotations;
global using System.IdentityModel.Tokens.Jwt;
global using System.IO;
global using System.Linq;
global using System.Linq.Expressions;
global using System.Net.Mime;
global using System.Reflection;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;
global using System.Threading;
global using System.Threading.Tasks;