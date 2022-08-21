using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevateOTT.ClientPortal.Models.DTOs
{
    public class AuthorForAutoCompleteDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}