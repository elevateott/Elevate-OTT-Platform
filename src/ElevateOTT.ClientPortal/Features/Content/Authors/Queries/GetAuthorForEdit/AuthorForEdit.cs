using System.ComponentModel;
using System.Runtime.CompilerServices;
using ElevateOTT.ClientPortal.Annotations;

namespace ElevateOTT.ClientPortal.Features.Content.Authors.Queries.GetAuthorForEdit;

public class AuthorForEdit : INotifyPropertyChanged
{
    #region Public Properties

    public event PropertyChangedEventHandler? PropertyChanged;
    public Guid Id { get; set; }

    private string _name = string.Empty;

    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            if (value != _name)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    public string Bio { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string SeoTitle { get; set; } = string.Empty;
    public string SeoDescription { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public bool IsImageAdded { get; set; }

    #endregion Public Properties


    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
