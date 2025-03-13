using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApiMySQL.Models;

public class TaskItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(60)]
    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("description", TypeName = "TEXT")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [DefaultValue(0)]
    [Column("state")]
    public int State { get; set; } 
}