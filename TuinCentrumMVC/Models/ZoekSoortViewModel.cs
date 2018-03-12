using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuinCentrumMVC.Models
{
    public class ZoekSoortViewModel
    {
        [Display(Name = "Begin soortnaam:")]
        [Required(ErrorMessage = "Verplicht")]
        public string beginNaam { get; set; }

        public List<Soort> Soorten { get; set; }
    }
}