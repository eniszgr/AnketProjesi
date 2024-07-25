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

    [MaxLength(50)]
    public byte[] Soru { get; set; } = null!;
}
