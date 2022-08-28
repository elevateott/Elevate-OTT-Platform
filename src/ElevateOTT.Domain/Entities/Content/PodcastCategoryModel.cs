using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Domain.Entities.Content;

[Table("PodcastsCategories")]
public class PodcastCategoryModel : IMustHaveTenant
{
    public Guid TenantId { get; set; }

    public Guid PodcastId { get; set; }

    public Guid CategoryId { get; set; }

    public PodcastModel? Podcast { get; set; }

    public CategoryModel? Category { get; set; }
}
