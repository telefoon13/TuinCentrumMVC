using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuinCentrumMVC.Models
{
    public class PlantProperties
    {
        [Display(ResourceType = typeof(Resources.Teksten), Name = "LablePrijs")]
        [Range(0,100, ErrorMessageResourceType = typeof(Resources.Teksten), ErrorMessageResourceName = "RangePrijs")]
        public decimal VerkoopPrijs { get; set; }
    }
}