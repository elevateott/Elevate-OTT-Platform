using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevateOTT.Domain.Entities;

[Table("Customers")]
public class CustomerModel : BaseEntity, IMustHaveTenant
{
    public Guid TenantId { get; set; }

    public string ChargebeeId { get; set; } = string.Empty;
    public string ChargebeeApiKey { get; set; } = string.Empty;
    public string ChargebeeWebhookKey { get; set; } = string.Empty;
    public string ChargebeeWebhookPassword { get; set; } = string.Empty;
    public string ChargebeeWebhookEncodedAuth { get; set; } = string.Empty;


    [MaxLength(50, ErrorMessage = "Maximum length for the OTT Channel Name is 100 characters.")]
    public string OttChannelName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "First Name is required.")]
    [MaxLength(50, ErrorMessage = "Maximum length for the First Name is 50 characters.")]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50, ErrorMessage = "Maximum length for the Last Name is 50 characters.")]
    public string LastName { get; set; } = string.Empty;

    [Phone]
    public string Phone { get; set; } = string.Empty;

    public string Company { get; set; } = string.Empty;

    public string Locale { get; set; } = string.Empty;

    public string AddressLine1 { get; set; } = string.Empty;

    public string AddressLine2 { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string StateCode { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string CountryCode { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;

    // public string ValidationStatus { get; set; } = string.Empty;

    public bool Deleted { get; set; }
}
