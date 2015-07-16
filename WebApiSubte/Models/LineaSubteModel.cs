using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApiSubte.Models
{
  public class LineaSubteModel
  {
    [Required]
    [Key]
    [Display(Name = "ID")]
    public string Id { get; set; }
    
    [Required]
    [Display(Name = "Nombre")]
    public string Name { get; set; }

    public Estado EstadoLinea { get; set; }
  }

  public enum Estado
  {
    Normal,
    ConDemora,
    Suspendido
  }
}