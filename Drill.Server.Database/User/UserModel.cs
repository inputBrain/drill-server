using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drill.Server.Database.User;

public class UserModel : AbstractModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Email { get; set; }
    
    [StringLength(50)]
    public string FirstName { get; set; }

    [StringLength(50)]
    public string LastName { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    


    public static UserModel CreateModel(
        string? email,
        string firstName,
        string lastName,
        DateTimeOffset createdAt
    )
    {
        return new UserModel
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            CreatedAt = createdAt
        };
    }
}