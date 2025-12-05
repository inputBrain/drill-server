using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drill.Server.Database.Drill;

public class DrillModel : AbstractModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public float PricePerMinute { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }


    public static DrillModel CreateModel(string title, float pricePerMinute, DateTimeOffset createdAt)
    {
        return new DrillModel
        {
            Title = title,
            PricePerMinute = pricePerMinute,
            CreatedAt = createdAt
        };
    }

    public bool IsSame(string title, float pricePerMinute)
    {
        return Title == title && Math.Abs(PricePerMinute - pricePerMinute) < 0.001f;
    }
}