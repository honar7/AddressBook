using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Contact
{
    public long Id { get; set; }

    [Required]
    [StringLength(128, ErrorMessage = "FirstName is long too long")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(128, ErrorMessage = "LastName is long too long")]
    public string LastName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    [Required] public string Address { get; set; }
    [Required] public string MobilePhone { get; set; }
    public string? HomePhone { get; set; }
    public string? WorkPhone { get; set; }
}