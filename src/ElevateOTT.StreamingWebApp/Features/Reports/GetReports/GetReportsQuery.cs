﻿namespace ElevateOTT.StreamingWebApp.Features.Reports.GetReports;

public class GetReportsQuery : FilterableQuery
{
    #region Public Properties

    public ReportStatus? SelectedReportStatus { get; set; }

    #endregion Public Properties
}