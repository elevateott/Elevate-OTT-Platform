using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Domain.Entities.Content;

[Table("LiveStreamsCategories")]
public class LiveStreamCategoryModel : BaseEntity, IMustHaveTenant
{
    public Guid TenantId { get; set; }

    public Guid LiveStreamId { get; set; }

    public Guid CategoryId { get; set; }

    public LiveStreamModel? LiveStream { get; set; }

    public CategoryModel? Category { get; set; }
}
