using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Contact
{
    public int ContactId { get; set; }

    [Required]
    [StringLength(128, ErrorMessage = "Name is long too long")]
    public string Name { get; set; }

    [Required] public string Email { get; set; }
    [Required] public string Address { get; set; }

    [Required] public string MobilePhone { get; set; }

    public string? HomePhone { get; set; }

    public string? WorkPhone { get; set; }
}