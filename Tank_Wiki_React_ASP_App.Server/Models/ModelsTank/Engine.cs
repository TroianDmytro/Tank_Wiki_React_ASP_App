﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Tank_Wiki_React_ASP_App.Server.Models.ModelsManyToMany;

namespace Tank_Wiki_React_ASP_App.Server.Models;

public partial class Engine
{
    [Key]
    public int EngineId { get; set; }

    [Required]
    public string Name { get; set; }

    public int Tier { get; set; }

    public int Power { get; set; }

    public double FireChance { get; set; }

    public double Weight { get; set; }

    public long Price { get; set; }

    //[ForeignKey("EngineId")]
    //[InverseProperty("Engines")]
    //public virtual ICollection<Tank> Tanks { get; set; } = new List<Tank>();
    public List<TankEngine> TankEngines { get; set; }
}