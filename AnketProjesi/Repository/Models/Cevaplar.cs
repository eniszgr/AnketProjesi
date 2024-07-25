using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnketProjesi.Repository.Models;

[Table("Cevaplar")]
public partial class Cevaplar
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("AnketID")]
    public int? AnketId { get; set; }

    [Column("SoruID")]
    public int SoruId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Cevap { get; set; } = null!;

    [ForeignKey("AnketId")]
    [InverseProperty("Cevaplars")]
    public virtual Anket? Anket { get; set; }

    [ForeignKey("SoruId")]
    [InverseProperty("Cevaplars")]
    public virtual Sorular Soru { get; set; } = null!;
}
