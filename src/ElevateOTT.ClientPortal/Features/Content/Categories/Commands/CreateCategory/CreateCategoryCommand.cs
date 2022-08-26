using System.ComponentModel;
using ElevateOTT.ClientPortal.Annotations;
using System.Runtime.CompilerServices;

namespace ElevateOTT.ClientPortal.Features.Content.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string _title = string.Empty;

    public string Title
    {
        get
        {
            return _title;
        }

        set
        {
            if (value != _title)
            {
                _title = value;
                OnPropertyChanged();
            }
        }
    }
    
    public string? Description { get; set; }

    public int Position { get; set; } = 1;

    public string? ImageUrl { get; set; }

    public string? SeoTitle { get; set; }

    public string? SeoDescription { get; set; }

    public string? Slug { get; set; }

    public bool IsImageAdded { get; set; }

    public IFormFile? ImageFile { get; set; }


    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
