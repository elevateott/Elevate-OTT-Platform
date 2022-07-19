﻿namespace ElevateOTT.ClientPortal.Pages.Roles;

public partial class RolesLookupDialog : ComponentBase
{
    #region Public Properties

    [Parameter] public List<RoleItem> SelectedUserRoles { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private IRolesClient RolesClient { get; set; }

    private bool SelectAllVisibleRowsCheckBox { get; set; }
    private string SearchString { get; set; }
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
    private MudTable<RoleItem> Table { get; set; }
    private ServerSideValidator ServerSideValidator { get; set; }
    private GetRolesQuery GetRolesQuery { get; set; } = new();
    private RolesResponse RolesResponse { get; set; } = new();

    #endregion Private Properties

    #region Private Methods

    private void AddToSelectedUserRoles(RoleItem item)
    {
        SelectAllVisibleRowsCheckBox = false;
        item.Checked = !item.Checked;
        if (item.Checked)
        {
            var assignedUserRoleItem = new RoleItem
            {
                Id = item.Id,
                Name = item.Name,
                IsStatic = item.IsStatic,
                IsDefault = item.IsDefault,
                Checked = item.Checked,
            };
            SelectedUserRoles.Add(assignedUserRoleItem);
        }
        else
        {
            SelectedUserRoles.RemoveAll(x => x.Id == item.Id);
        }
    }

    private void SelectAllVisibleRows(object @checked)
    {
        if ((bool)@checked)
        {
            RolesResponse?.Roles.Items.ToList().ForEach(s => s.Checked = true);
            if (RolesResponse?.Roles.Items != null)
                SelectedUserRoles.AddRange(RolesResponse?.Roles.Items.Select(ur => new RoleItem
                {
                    Id = ur.Id,
                    Name = ur.Name,
                    Checked = ur.Checked,
                    IsDefault = ur.IsDefault,
                    IsStatic = ur.IsStatic
                }));
        }
        else
        {
            RolesResponse?.Roles.Items.Where(r => r.Checked).ToList().ForEach(s => s.Checked = false);
            RolesResponse?.Roles.Items.ToList().ForEach(r => SelectedUserRoles.RemoveAll(sr => sr.Id == r.Id));
        }
        SelectAllVisibleRowsCheckBox = false;
    }

    private async Task<TableData<RoleItem>> ServerReload(TableState state)
    {
        GetRolesQuery.SearchText = SearchString;
        GetRolesQuery.PageNumber = state.Page + 1;
        GetRolesQuery.RowsPerPage = state.PageSize;
        GetRolesQuery.SortBy = state.SortDirection == SortDirection.None ? string.Empty : $"{state.SortLabel} {state.SortDirection}";

        var httpResponseWrapper = await RolesClient.GetRoles(GetRolesQuery);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<RolesResponse>;
            RolesResponse = successResult.Result;
            if (SelectedUserRoles != null)
                RolesResponse?.Roles.Items.Where(r => SelectedUserRoles.Select(sr => sr.Id).Contains(r.Id)).ToList().ForEach(s => s.Checked = true);
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
            if (exceptionResult is { Status: 401 })
                MudDialog.Cancel();
            return new TableData<RoleItem>();
        }

        return new TableData<RoleItem>() { TotalItems = RolesResponse.Roles.TotalRows, Items = RolesResponse.Roles.Items };
    }

    private void OnSearch(string searchString)
    {
        SearchString = searchString;
        Table.ReloadServerData();
    }

    private void Submit()
    {
        MudDialog.Close(DialogResult.Ok(SelectedUserRoles));
    }

    private void Closed(MudChip chip)
    {
        SelectedUserRoles.RemoveAll(sr => sr.Name.Trim() == chip.Text.Trim());
        Table.ReloadServerData();
    }

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    #endregion Private Methods
}