﻿namespace BinaryPlate.Application.Common.Interfaces.Services.DemoUserServices;

public interface IDemoIdentitySeeder
{
    #region Public Methods

    Task<Envelope<ApplicationUser>> SeedDemoOfficersUsers();

    #endregion Public Methods
}