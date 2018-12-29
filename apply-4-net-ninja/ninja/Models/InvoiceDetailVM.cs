using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ninja.Models
{
    public class InvoiceDetailVM
    {
        public double Taxes { get; set; }
        [Required]
        public long Id { get; set; }
        public long IdInvoice { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        [Display(Name = "Price x Unit")]
        public double UnitPrice { get; set; }
        [Display(Name = "Total")]
        public double TotalPrice { get; set; }
        [Display(Name = "Tota with Taxes")]
        public double TotalPriceWithTaxes { get; set; }
    }
}