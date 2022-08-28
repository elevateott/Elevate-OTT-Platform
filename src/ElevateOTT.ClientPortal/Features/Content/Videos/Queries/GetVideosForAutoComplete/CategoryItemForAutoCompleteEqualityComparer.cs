using System.Diagnostics.CodeAnalysis;
using ElevateOTT.ClientPortal.Features.Content.Categories.Queries.GetCategoriesForAutoComplete;

namespace ElevateOTT.ClientPortal.Features.Content.Videos.Queries.GetVideosForAutoComplete;

public class CategoryItemForAutoCompleteEqualityComparer : IEqualityComparer<CategoryItemForAutoComplete>
{
    public bool Equals(CategoryItemForAutoComplete? x, CategoryItemForAutoComplete? y)
    {
        return x != null && y != null && x.Id.Equals(y.Id);
    }

    public int GetHashCode([DisallowNull] CategoryItemForAutoComplete obj)
    {
        return obj.Id.GetHashCode();
    }
}
