using System.ComponentModel.DataAnnotations;

namespace Authorization.Domain.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Password { get; set; }
}