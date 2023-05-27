using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObjectGraphs.Models;
using ObjectGraphs;

namespace WebCorso.Controllers
{
    public class GraficoController : Controller
    {

        Repository grafico = new Repository();
          
        public ActionResult Index()
        {
            
            Repository repositoryBll = new Repository();
            var graficoPieno = grafico.ImpostaRecord();
            var datatbl = Repository.SaveGrandRecords(graficoPieno);
            
              
                return View(graficoPieno);
        }
    }
}