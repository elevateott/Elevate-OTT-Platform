using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Domain.Entities.Content;

[Table("VideosCollections")]
public class VideoCollectionModel : BaseEntity, IMustHaveTenant
{
    public Guid TenantId { get; set; }

    public Guid VideoId { get; set; }

    public Guid CollectionId { get; set; }

    public VideoModel? Video { get; set; }

    public CollectionModel? Collection { get; set; }
}
