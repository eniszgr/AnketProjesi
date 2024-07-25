using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AnketProjesi.Repository.Models;

[Table("Tip")]
public partial class Tip
{
    [Key]
    [Column("TipID")]
    public int TipId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TipName { get; set; } = null!;

    [InverseProperty("Tip")]
    public virtual ICollection<Anket> Ankets { get; set; } = new List<Anket>();
}
