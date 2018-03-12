using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuinCentrumMVC.Models
{
    public class PlantProperties
    {

        [Range(0,100)]
        public decimal VerkoopPrijs { get; set; }
    }
}