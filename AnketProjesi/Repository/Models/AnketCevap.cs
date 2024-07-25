using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnketProjesi.Repository.Models;

[Keyless]
[Table("AnketCevap")]
public partial class AnketCevap
{
    [Column("CevapID")]
    public int CevapId { get; set; }

    [Column("SoruID")]
    public int SoruId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Cevap { get; set; } = null!;

    [ForeignKey("CevapId")]
    public virtual Anket CevapNavigation { get; set; } = null!;

    [ForeignKey("SoruId")]
    public virtual Sorular Soru { get; set; } = null!;
}
