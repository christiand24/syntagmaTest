using ninja.model.Entity;
using ninja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ninja.Helpers
{
    /// <summary>
    /// Se encarga de mapear la clase del modelo de la DB InvoiceDetail a la clase del modelo de la vista InvoiceDetailVM y viceversa
    /// Esta clase se puede reemplazar por un modulo de mapeo automatico como automapper o algun desarrollo propio 
    /// usando reflection (se implementa de este modo por simplicidad)
    /// </summary>
    public static class InvoiceDetailHelper
    {
        public static InvoiceDetail ToInvoiceDetail(this InvoiceDetailVM source)
        {
            return new InvoiceDetail()
            {
                Amount = source.Amount,
                Description = source.Description,
                Id = source.Id,
                UnitPrice = source.UnitPrice,
                InvoiceId = source.IdInvoice
                
            };
        }

        public static List<InvoiceDetail> ToInvoiceDetailList(this IList<InvoiceDetailVM> source)
        {
            return source.Select(item => item.ToInvoiceDetail()).ToList();
        }

        public static InvoiceDetailVM ToInvoiceDetailVM(this InvoiceDetail source)
        {
            return new InvoiceDetailVM()
            {
                Amount = source.Amount,
                Description = source.Description,
                Id = source.Id,
                UnitPrice = source.UnitPrice,
                Taxes = source.Taxes,
                TotalPrice = source.TotalPrice,
                TotalPriceWithTaxes = source.TotalPriceWithTaxes,
                IdInvoice = source.InvoiceId
                
            };
        }

        public static List<InvoiceDetailVM> ToInvoiceDetailVMList(this IList<InvoiceDetail> source)
        {
            return source.Select(item => item.ToInvoiceDetailVM()).ToList();
        }
    }
}