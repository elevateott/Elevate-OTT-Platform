using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Domain.Common.DTOs;

public class TagDto : AuditableDto
{
    public Guid TenantId { get; set; }
    
    public string? Name { get; set; }
}
