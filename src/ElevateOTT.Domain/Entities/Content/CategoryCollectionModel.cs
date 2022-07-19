namespace ElevateOTT.Domain.Entities.Content;

[Table("CategoriesCollections")]
public class CategoryCollectionModel : EntityBase
{
    public Guid CategoryId { get; set; }

    public Guid CollectionId { get; set; }

    public CategoryModel? Category { get; set; }

    public CollectionModel? Collection { get; set; }
}
