﻿global using AutoWrapper;
global using AutoWrapper.Wrappers;

global using ElevateOTT.Application;
global using ElevateOTT.Application.Common.Helpers;
global using ElevateOTT.Application.Common.Interfaces.Persistence;
global using ElevateOTT.Application.Common.Interfaces.Services;
global using ElevateOTT.Application.Common.Interfaces.Services.HubServices;
global using ElevateOTT.Application.Common.Models;
global using ElevateOTT.Application.Common.Models.ApplicationOptions;
global using ElevateOTT.Application.Features.AppSettings.Commands.UpdateSettings;
global using ElevateOTT.Application.Features.AppSettings.Queries.GetSettings.GetFileStorageSettings;
global using ElevateOTT.Application.Features.AppSettings.Queries.GetSettings.GetIdentitySettings;
global using ElevateOTT.Application.Features.AppSettings.Queries.GetSettings.GetTokenSettings;
global using ElevateOTT.Application.Features.Dashboard.Queries.GetHeadlines;
global using ElevateOTT.Application.Features.Identity.Account.Commands.ConfirmEmail;
global using ElevateOTT.Application.Features.Identity.Account.Commands.ForgotPassword;
global using ElevateOTT.Application.Features.Identity.Account.Commands.Login;
global using ElevateOTT.Application.Features.Identity.Account.Commands.LoginWith2fa;
global using ElevateOTT.Application.Features.Identity.Account.Commands.LoginWithRecoveryCode;
global using ElevateOTT.Application.Features.Identity.Account.Commands.RefreshToken;
global using ElevateOTT.Application.Features.Identity.Account.Commands.Register;
global using ElevateOTT.Application.Features.Identity.Account.Commands.ResendEmailConfirmation;
global using ElevateOTT.Application.Features.Identity.Account.Commands.ResetPassword;
global using ElevateOTT.Application.Features.Identity.Manage.Commands.ChangeEmail;
global using ElevateOTT.Application.Features.Identity.Manage.Commands.ChangePassword;
global using ElevateOTT.Application.Features.Identity.Manage.Commands.ConfirmEmailChange;
global using ElevateOTT.Application.Features.Identity.Manage.Commands.DeletePersonalData;
global using ElevateOTT.Application.Features.Identity.Manage.Commands.Disable2fa;
global using ElevateOTT.Application.Features.Identity.Manage.Commands.EnableAuthenticator;
global using ElevateOTT.Application.Features.Identity.Manage.Commands.ResetAuthenticator;
global using ElevateOTT.Application.Features.Identity.Manage.Commands.SetPassword;
global using ElevateOTT.Application.Features.Identity.Manage.Commands.UpdateUserAvatar;
global using ElevateOTT.Application.Features.Identity.Manage.Commands.UpdateUserProfile;
global using ElevateOTT.Application.Features.Identity.Manage.Queries.CheckUser2faState;
global using ElevateOTT.Application.Features.Identity.Manage.Queries.DownloadPersonalData;
global using ElevateOTT.Application.Features.Identity.Manage.Queries.GenerateRecoveryCodes;
global using ElevateOTT.Application.Features.Identity.Manage.Queries.Get2faState;
global using ElevateOTT.Application.Features.Identity.Manage.Queries.GetUser;
global using ElevateOTT.Application.Features.Identity.Manage.Queries.GetUserAvatar;
global using ElevateOTT.Application.Features.Identity.Manage.Queries.HasPassword;
global using ElevateOTT.Application.Features.Identity.Manage.Queries.LoadSharedKeyAndQrCodeUri;
global using ElevateOTT.Application.Features.Identity.Permissions.Queries.GetPermissions;
global using ElevateOTT.Application.Features.Identity.Roles.Commands.CreateRole;
global using ElevateOTT.Application.Features.Identity.Roles.Commands.DeleteRole;
global using ElevateOTT.Application.Features.Identity.Roles.Commands.UpdateRole;
global using ElevateOTT.Application.Features.Identity.Roles.Queries.GetRoleForEdit;
global using ElevateOTT.Application.Features.Identity.Roles.Queries.GetRoles;
global using ElevateOTT.Application.Features.Identity.Tenants.Commands.CreateTenantCommand;
global using ElevateOTT.Application.Features.Identity.Users.Commands.CreateUser;
global using ElevateOTT.Application.Features.Identity.Users.Commands.DeleteUser;
global using ElevateOTT.Application.Features.Identity.Users.Commands.GrantOrRevokeUserPermissions;
global using ElevateOTT.Application.Features.Identity.Users.Commands.UpdateUser;
global using ElevateOTT.Application.Features.Identity.Users.Queries.GetUserForEdit;
global using ElevateOTT.Application.Features.Identity.Users.Queries.GetUserPermissions;
global using ElevateOTT.Application.Features.Identity.Users.Queries.GetUsers;
global using ElevateOTT.Application.Features.POC.Applicants.Commands.CreateApplicant;
global using ElevateOTT.Application.Features.POC.Applicants.Commands.DeleteApplicant;
global using ElevateOTT.Application.Features.POC.Applicants.Commands.UpdateApplicant;
global using ElevateOTT.Application.Features.POC.Applicants.Queries.ExportApplicants;
global using ElevateOTT.Application.Features.POC.Applicants.Queries.GetApplicantForEdit;
global using ElevateOTT.Application.Features.POC.Applicants.Queries.GetApplicants;
global using ElevateOTT.Application.Features.POC.Applicants.Queries.GetApplicantsReferences;
global using ElevateOTT.Application.Features.Reports.GetReportForEdit;
global using ElevateOTT.Application.Features.Reports.GetReports;
global using ElevateOTT.BackendResources;
global using ElevateOTT.Domain.Enums;
global using ElevateOTT.Infrastructure;
global using ElevateOTT.Infrastructure.Extensions;
global using ElevateOTT.Infrastructure.Middleware;
global using ElevateOTT.Infrastructure.Persistence;
global using ElevateOTT.WebAPI.Extensions;
global using ElevateOTT.WebAPI.Filters;
global using ElevateOTT.WebAPI.Hubs;
global using ElevateOTT.WebAPI.Managers;
global using ElevateOTT.WebAPI.Middleware;
global using ElevateOTT.WebAPI.Services.HubServices;

global using FluentValidation.AspNetCore;

global using Hangfire;

global using MediatR;

global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Http.Extensions;
global using Microsoft.AspNetCore.Http.Features;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.AspNetCore.SignalR;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;

global using Nancy.Security;

global using Swashbuckle.AspNetCore.SwaggerGen;

global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Security.Claims;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Threading;
global using System.Threading.Tasks;