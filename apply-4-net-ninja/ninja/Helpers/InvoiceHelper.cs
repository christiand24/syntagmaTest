using ninja.model.Entity;
using ninja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ninja.model.Entity.Invoice;

namespace ninja.Helpers
{
    /// <summary>
    /// Se encarga de mapear la clase del modelo de la DB Invoice a la clase del modelo de la vista InvoiceVM y viceversa
    /// Esta clase se puede reemplazar por un modulo de mapeo automatico como automapper o algun desarrollo propio 
    /// usando reflection (se implementa de este modo por simplicidad)
    /// </summary>
    public static class InvoiceHelper
    {
        public static Invoice ToInvoice(this InvoiceVM source)
        {
            var invoice = new Invoice()
            {
                Id = source.Id,
                Type = source.InvoiceType
            };

            if (source.Details.Count > 0)
            {
                source.Details.ToList().ForEach(item =>
                    invoice.AddDetail(item.ToInvoiceDetail())
                );
            }

            return invoice;
        }
        public static List<Invoice> ToInvoiceList(this IList<InvoiceVM> source)
        {
            return source.Select(item => item.ToInvoice()).ToList();
        }

        public static InvoiceVM ToInvoiceVM(this Invoice source)
        {
            var invoice = new InvoiceVM()
            {
                Id = source.Id,
                InvoiceType = source.Type,
                InvoiceTotalPriceWithTaxes = source.CalculateInvoiceTotalPriceWithTaxes()
            };

            if (source.GetDetail().Count > 0)
            {
                source.GetDetail().ToList().ForEach(item =>
                    invoice.Details.Add(item.ToInvoiceDetailVM())
                );
            }

            return invoice;
        }
        public static List<InvoiceVM> ToInvoiceVMList(this IList<Invoice> source)
        {
            return source.Select(item => item.ToInvoiceVM()).ToList();
        }


    }
}