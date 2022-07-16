﻿global using BinaryPlate.BlazorPlate.Consumers;
global using BinaryPlate.BlazorPlate.Enums;
global using BinaryPlate.BlazorPlate.Extensions;
global using BinaryPlate.BlazorPlate.Features.AppSettings.Commands.UpdateSettings;
global using BinaryPlate.BlazorPlate.Features.AppSettings.Queries.GetSettings.GetFileStorageSettings;
global using BinaryPlate.BlazorPlate.Features.AppSettings.Queries.GetSettings.GetIdentitySettings;
global using BinaryPlate.BlazorPlate.Features.AppSettings.Queries.GetSettings.GetTokenSettings;
global using BinaryPlate.BlazorPlate.Features.Dashboard.Queries.GetHeadlines;
global using BinaryPlate.BlazorPlate.Features.Identity.Account.Commands.ConfirmEmail;
global using BinaryPlate.BlazorPlate.Features.Identity.Account.Commands.ForgotPassword;
global using BinaryPlate.BlazorPlate.Features.Identity.Account.Commands.Login;
global using BinaryPlate.BlazorPlate.Features.Identity.Account.Commands.LoginWith2fa;
global using BinaryPlate.BlazorPlate.Features.Identity.Account.Commands.LoginWithRecoveryCode;
global using BinaryPlate.BlazorPlate.Features.Identity.Account.Commands.RefreshToken;
global using BinaryPlate.BlazorPlate.Features.Identity.Account.Commands.Register;
global using BinaryPlate.BlazorPlate.Features.Identity.Account.Commands.ResendEmailConfirmation;
global using BinaryPlate.BlazorPlate.Features.Identity.Account.Commands.ResetPassword;
global using BinaryPlate.BlazorPlate.Features.Identity.Manage.Commands.ChangeEmail;
global using BinaryPlate.BlazorPlate.Features.Identity.Manage.Commands.ChangePassword;
global using BinaryPlate.BlazorPlate.Features.Identity.Manage.Commands.ConfirmEmailChange;
global using BinaryPlate.BlazorPlate.Features.Identity.Manage.Commands.DeletePersonalData;
global using BinaryPlate.BlazorPlate.Features.Identity.Manage.Commands.EnableAuthenticator;
global using BinaryPlate.BlazorPlate.Features.Identity.Manage.Commands.ResetAuthenticator;
global using BinaryPlate.BlazorPlate.Features.Identity.Manage.Commands.UpdateUserAvatar;
global using BinaryPlate.BlazorPlate.Features.Identity.Manage.Commands.UpdateUserProfile;
global using BinaryPlate.BlazorPlate.Features.Identity.Manage.Queries.CheckUser2faState;
global using BinaryPlate.BlazorPlate.Features.Identity.Manage.Queries.DownloadPersonalData;
global using BinaryPlate.BlazorPlate.Features.Identity.Manage.Queries.GenerateRecoveryCodes;
global using BinaryPlate.BlazorPlate.Features.Identity.Manage.Queries.Get2faState;
global using BinaryPlate.BlazorPlate.Features.Identity.Manage.Queries.GetUser;
global using BinaryPlate.BlazorPlate.Features.Identity.Manage.Queries.GetUserAvatar;
global using BinaryPlate.BlazorPlate.Features.Identity.Manage.Queries.LoadSharedKeyAndQrCodeUri;
global using BinaryPlate.BlazorPlate.Features.Identity.Permissions.Queries.GetPermissions;
global using BinaryPlate.BlazorPlate.Features.Identity.Roles.Commands.CreateRole;
global using BinaryPlate.BlazorPlate.Features.Identity.Roles.Commands.UpdateRole;
global using BinaryPlate.BlazorPlate.Features.Identity.Roles.Queries.GetRoleForEdit;
global using BinaryPlate.BlazorPlate.Features.Identity.Roles.Queries.GetRoles;
global using BinaryPlate.BlazorPlate.Features.Identity.Tenants.Commands.CreateTenantCommand;
global using BinaryPlate.BlazorPlate.Features.Identity.Users.Commands.CreateUser;
global using BinaryPlate.BlazorPlate.Features.Identity.Users.Commands.GrantOrRevokeUserPermissions;
global using BinaryPlate.BlazorPlate.Features.Identity.Users.Commands.UpdateUser;
global using BinaryPlate.BlazorPlate.Features.Identity.Users.Queries.GetUserForEdit;
global using BinaryPlate.BlazorPlate.Features.Identity.Users.Queries.GetUserPermissions;
global using BinaryPlate.BlazorPlate.Features.Identity.Users.Queries.GetUsers;
global using BinaryPlate.BlazorPlate.Features.POC.Applicants.Commands.CreateApplicant;
global using BinaryPlate.BlazorPlate.Features.POC.Applicants.Commands.UpdateApplicant;
global using BinaryPlate.BlazorPlate.Features.POC.Applicants.Queries.ExportApplicants;
global using BinaryPlate.BlazorPlate.Features.POC.Applicants.Queries.GetApplicantForEdit;
global using BinaryPlate.BlazorPlate.Features.POC.Applicants.Queries.GetApplicants;
global using BinaryPlate.BlazorPlate.Features.POC.Applicants.Queries.GetApplicantsReferences;
global using BinaryPlate.BlazorPlate.Features.Reports.GetReportForEdit;
global using BinaryPlate.BlazorPlate.Features.Reports.GetReports;
global using BinaryPlate.BlazorPlate.Helpers;
global using BinaryPlate.BlazorPlate.Interfaces;
global using BinaryPlate.BlazorPlate.Interfaces.Consumers;
global using BinaryPlate.BlazorPlate.Interfaces.Providers;
global using BinaryPlate.BlazorPlate.Interfaces.Services;
global using BinaryPlate.BlazorPlate.Models;
global using BinaryPlate.BlazorPlate.Models.Reporting;
global using BinaryPlate.BlazorPlate.Models.Settings;
global using BinaryPlate.BlazorPlate.Models.Settings.IdentitySettings;
global using BinaryPlate.BlazorPlate.Pages.Roles;
global using BinaryPlate.BlazorPlate.Providers;
global using BinaryPlate.BlazorPlate.Services;
global using BinaryPlate.BlazorPlate.Shared;
global using BinaryPlate.FrontendResources;

global using Blazored.LocalStorage;

global using FluentValidation;

global using Microsoft.AspNetCore.Components;
global using Microsoft.AspNetCore.Components.Authorization;
global using Microsoft.AspNetCore.Components.Forms;
global using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.SignalR.Client;
global using Microsoft.AspNetCore.WebUtilities;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Localization;
global using Microsoft.JSInterop;

global using MudBlazor;
global using MudBlazor.Services;

global using System;
global using System.Collections.Generic;
global using System.Globalization;
global using System.IO;
global using System.Linq;
global using System.Net;
global using System.Net.Http;
global using System.Net.Http.Headers;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text;
global using System.Text.Json;
global using System.Text.RegularExpressions;
global using System.Threading;
global using System.Threading.Tasks;

global using Toolbelt.Blazor;
global using Toolbelt.Blazor.Extensions.DependencyInjection;

global using Severity = MudBlazor.Severity;