using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ninja.Models
{
    public class InvoiceVM
    {
        public InvoiceVM() {
            Details = new List<InvoiceDetailVM>();
        }

        [Display(Name = "Invoice number")]
        public long Id { get; set; }
        [Display (Name = "Invoice Type")]
        public string InvoiceType { get; set; }
        public IList<InvoiceDetailVM> Details { get; set; }
        [Display(Name = "Total with taxes")]
        public double InvoiceTotalPriceWithTaxes { get; set; }
    }
}