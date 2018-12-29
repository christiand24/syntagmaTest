using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity.Validation;
using System.Text;

namespace ninja.Filters
{
    /// <summary>
    /// Filtro para manejar las excepciones de todos los controladores y sus respectivas acciones (configurado en el global ajax)
    /// </summary>
    public class ExceptionCatchActionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {

            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new
                {
                    action = "ShowMessageView",
                    controller = "Invoice",
                    messageToShow = @"Lo sentimos hubo un problema procesando la petición requerida, intentelo nuevamente mas tarde y si 
                        el problema persiste, contacte al adminitrador." 
                })
            );


            //loguear error a archivo o db con log4net

            filterContext.ExceptionHandled = true;
        }
    }
}