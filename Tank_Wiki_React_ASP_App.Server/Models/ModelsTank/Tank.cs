﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Tank_Wiki_React_ASP_App.Server.Models.ModelsManyToMany;

namespace Tank_Wiki_React_ASP_App.Server.Models;

[Index("NationId", Name = "IX_Tanks_NationId")]
[Index("TypeId", Name = "IX_Tanks_TypeId")]
public partial class Tank
{
    [Key]
    public int TankId { get; set; }

    [Required]
    public string Name { get; set; }

    public int Tier { get; set; }

    public int HitPoints { get; set; }

    public bool Status { get; set; }

    public long Price { get; set; }

    public string Description { get; set; }

    public int TypeId { get; set; }

    public int ArmorId { get; set; }

    [Required]
    public List<string> Crew { get; set; }

    public int NationId { get; set; }

    [InverseProperty("Tank")]
    public virtual Armor Armor { get; set; }

    [ForeignKey("NationId")]
    [InverseProperty("Tanks")]
    public virtual Nation Nation { get; set; }

    [ForeignKey("TypeId")]
    [InverseProperty("Tanks")]
    public virtual TankType TankType { get; set; }
    // Связь с картинками через промежуточную таблицу
    public List<TankPicture> TankPictures { get; set; } = new List<TankPicture>();
    public List<TankTurret> TankTurrets { get; set; } = [];//Башні
    public List<TankEngine> TankEngines { get; set; } = []; // Двигун
                                                            //public int EngineId { get; set; } // Двигун
    public List<TankSuspension> TankSuspensions { get; set; } = []; // Підвіска
                                                                    //public int SuspensionID { get; set; } // Підвіска
    public List<TankRadio> TankRadios { get; set; } = [];// Радіо
                                                         //public int RadioId { get; set; } // Радіо
}