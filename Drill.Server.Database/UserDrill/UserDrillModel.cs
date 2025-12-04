using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Drill.Server.Database.Drill;
using Drill.Server.Database.User;

namespace Drill.Server.Database.UserDrill;

public class UserDrillModel : AbstractModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserModel User { get; set; }

    public int DrillId { get; set; }

    [ForeignKey(nameof(DrillId))]
    public DrillModel Drill { get; set; }

    public DateTimeOffset StartedAt { get; set; }

    public DateTimeOffset? StoppedAt { get; set; }

    

    public static UserDrillModel StartDrill(int userId, int drillId, DateTimeOffset startedAt)
    {
        return new UserDrillModel
        {
            UserId = userId,
            DrillId = drillId,
            StartedAt = startedAt
        };
    }
    

    public void StopDrill(UserDrillModel model, DateTimeOffset stoppedAt)
    {
        model.StoppedAt = stoppedAt;
    }
}