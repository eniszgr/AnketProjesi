using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnketProjesi.Repository.Models;

[Table("Sorular")]
public partial class Sorular
{
    [Key]
    [Column("SoruID")]
    public int SoruId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Soru { get; set; } = null!;

    [InverseProperty("Soru")]
    public virtual ICollection<Cevaplar> Cevaplars { get; set; } = new List<Cevaplar>();
}
