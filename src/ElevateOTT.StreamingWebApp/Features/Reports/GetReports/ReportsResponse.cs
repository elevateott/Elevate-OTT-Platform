﻿namespace ElevateOTT.StreamingWebApp.Features.Reports.GetReports;

public class ReportsResponse
{
    #region Public Properties

    public PagedList<ReportItem> Reports { get; set; }

    #endregion Public Properties
}