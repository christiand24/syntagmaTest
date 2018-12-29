using ninja.model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ninja.Helpers;
using ninja.Models;


namespace ninja.Controllers
{
    /// <summary>
    /// Implementa ABM de Invoice y detalles de la misma
    /// </summary>
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index()
        {
            //throw new Exception("Test"); para probar controlador customizado de excepciones
            var list = GetInvoiceManager().GetAll().ToInvoiceVMList();

            return View(list);
        }

        // GET: Invoice/Details/5
        public ActionResult Details(long id)
        {
            var invoice = GetInvoiceManager().GetById(id).ToInvoiceVM();

            return View(invoice);
        }

        // GET: Invoice/Create
        public ActionResult New()
        {
            return View(new InvoiceVM());
        }

        // POST: Invoice/Create
        [HttpPost]
        public ActionResult New(InvoiceVM invoice)
        {
            if (!GetInvoiceManager().Exists(invoice.Id))
            {
                invoice.InvoiceType = Enum.GetName(typeof(ninja.model.Entity.Invoice.Types), Convert.ToInt32(invoice.InvoiceType));
                GetInvoiceManager().Insert(invoice.ToInvoice());
            }
            else {
                //vista con error 
            }

            return RedirectToAction("Index");
        }

        // GET: Invoice/Edit/5
        public ActionResult Update(long id)
        {
            var invoice = GetInvoiceManager().GetById(id).ToInvoiceVM();
            invoice.InvoiceType = Convert.ToString((int)(ninja.model.Entity.Invoice.Types)Enum.Parse(typeof(ninja.model.Entity.Invoice.Types), invoice.InvoiceType));
            return View(invoice);
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        public ActionResult Update(InvoiceVM invoice)
        {
            if (GetInvoiceManager().Exists(invoice.Id))
            {
                invoice.InvoiceType = Enum.GetName(typeof(ninja.model.Entity.Invoice.Types), Convert.ToInt32(invoice.InvoiceType));
                var details = invoice.Details.ToInvoiceDetailList();
                GetInvoiceManager().UpdateInvoiceType(invoice.Id, invoice.InvoiceType);
            }
            else
            {
                //vista con error 
            }

            return RedirectToAction("Index");
        }

        // POST: Invoice/Delete/5
        public ActionResult Delete(long id)
        {
            GetInvoiceManager().Delete(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Elimina un detalle
        /// </summary>
        /// <param name="idInvoice"></param>
        /// <param name="idDetail"></param>
        /// <returns></returns>
        public ActionResult DeleteDetail(long idInvoice, long idDetail)
        {
            GetInvoiceManager().GetById(idInvoice).DeleteDetail(idDetail);
            return RedirectToAction("Details", new { id = idInvoice});
        }

        /// <summary>
        /// Muestra pantalla para creacion de un detalle
        /// </summary>
        /// <param name="idInvoice"></param>
        /// <returns></returns>
        public ActionResult CreateDetail(long idInvoice)
        {
            var det = new InvoiceDetailVM() { IdInvoice = idInvoice };
            return View("NewDetail", det);
        }

        /// <summary>
        /// Muestra pantalla para edicion de detalle
        /// </summary>
        /// <param name="idInvoice"></param>
        /// <param name="idDetail"></param>
        /// <returns></returns>
        public ActionResult UpdateDetail(long idInvoice, long idDetail)
        {
            var det = GetInvoiceManager().GetById(idInvoice).GetDetail().FirstOrDefault(i => i.Id == idDetail);
            if (det != null)
                return View("UpdateDetail", det.ToInvoiceDetailVM());
            else
                return new ContentResult(); //deberia devolver vista con error
        }

        /// <summary>
        /// Crea un detalle
        /// </summary>
        /// <param name="idInvoice"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateDetail(InvoiceDetailVM detail)
        {
            GetInvoiceManager().GetById(detail.IdInvoice).AddDetail(detail.ToInvoiceDetail());
            return RedirectToAction("Details", new { id = detail.IdInvoice });
        }

        /// <summary>
        /// Actualiza un detalle
        /// </summary>
        /// <param name="idInvoice"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateDetail(InvoiceDetailVM detail)
        {
            //por somplicidad se elimina y se crea uno nuevo en ves de actualizarlo
            GetInvoiceManager().GetById(detail.IdInvoice).DeleteDetail(detail.Id);
            GetInvoiceManager().GetById(detail.IdInvoice).AddDetail(detail.ToInvoiceDetail());

            return RedirectToAction("Details", new { id = detail.IdInvoice });
        }

        private InvoiceManager GetInvoiceManager()
        {
            return new InvoiceManager();
        }

        public ActionResult ShowMessageView(string messageToShow)
        {
            return View("MessageInfo", null, messageToShow);
        }
    }
}
