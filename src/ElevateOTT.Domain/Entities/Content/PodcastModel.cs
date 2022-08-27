using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Domain.Entities.Content;

[Table("Podcasts")]
public class PodcastModel : BaseAsset, IMustHaveTenant
{
    public Guid TenantId { get; set; }

    #region foreign keys

    [ForeignKey(nameof(AuthorModel))]
    public Guid? AuthorId { get; set; }

    #endregion

    #region Navigational Properties
    public AuthorModel? Author { get; set; }
    public ICollection<PodcastCollectionModel>? PodcastsCollections { get; set; }
    public ICollection<PodcastCategoryModel>? PodcastsCategoriess { get; set; }

    #endregion
}
