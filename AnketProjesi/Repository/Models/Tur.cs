using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnketProjesi.Repository.Models;

[Table("Tur")]
public partial class Tur
{
    [Key]
    [Column("TurID")]
    public int TurId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TurName { get; set; } = null!;

    [InverseProperty("Tur")]
    public virtual ICollection<Anket> Ankets { get; set; } = new List<Anket>();
}
