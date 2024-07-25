using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnketProjesi.Repository.Models;

[Table("Anket")]
public partial class Anket
{
    [Key]
    [Column("AnketID")]
    public int AnketId { get; set; }

    [Column("TurID")]
    public int TurId { get; set; }

    [Column("TipID")]
    public int TipId { get; set; }

    [ForeignKey("TipId")]
    [InverseProperty("Ankets")]
    public virtual Tip Tip { get; set; } = null!;

    [ForeignKey("TurId")]
    [InverseProperty("Ankets")]
    public virtual Tur Tur { get; set; } = null!;
}
