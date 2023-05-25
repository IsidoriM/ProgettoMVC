using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using OCM.DatiGraph;

namespace WebCorso.Controllers
{
    public class GraficoController : Controller
    {

        Graph grafico = new Graph(); 
        // GET: Grafico
        public ActionResult Index()
        {
            List<GrandRecord> graficoPieno = new List<GrandRecord>();
            graficoPieno = grafico.ImpostaRecord();
            return View(graficoPieno);
        }
    }
}