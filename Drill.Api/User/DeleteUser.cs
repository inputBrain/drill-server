using System.ComponentModel.DataAnnotations;

namespace Drill.Api.User;

public sealed class DeleteUser
{
    [Required]
    public int UserId { get; set; }
}