using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Domain.Entities.Content;

[Table("VideosTags")]
public class VideoTagModel : BaseEntity, IMustHaveTenant
{
    public Guid TenantId { get; set; }

    public Guid VideoId { get; set; }

    public Guid TagId { get; set; }

    public VideoModel? Video { get; set; }

    public TagModel? Tag { get; set; }
}
