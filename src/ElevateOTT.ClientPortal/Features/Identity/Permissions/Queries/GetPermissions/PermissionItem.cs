namespace ElevateOTT.ClientPortal.Features.Identity.Permissions.Queries.GetPermissions;

public class PermissionItem
{
    #region Public Properties

    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? ParentId { get; set; }
    public bool IsChecked { get; set; }
    public bool HasChildren { get; set; }
    public bool IsRoot { get; set; }

    public List<PermissionItem> Permissions { get; set; }
    public bool IsExpanded { get; set; }

    #endregion Public Properties
}