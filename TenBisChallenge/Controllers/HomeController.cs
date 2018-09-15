using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TenBisChallenge.Models;
using TenBisChallenge.ViewModels;

namespace TenBisChallenge.Controllers
{
    public class HomeController : Controller
    {
        private DBContext db;
        private HomeViewModel vm;

        public HomeController() : base()
        {
            db = new DBContext();
            vm = new HomeViewModel();
            vm.HandleRequest("init");
        }

        public ActionResult Index()
        {
            return View(vm);
        }

        [HttpGet]
        public ActionResult GetReport(FilterInput FilterInput)
        {
            vm.HandleRequest("filter", FilterInput);

            return View("Index", vm);
        }

        
        private string BarChartData()
        {

            Chart _chart = new Chart();
            _chart.labels = vm.Reports.Select(report => report.UserFullName).ToArray();
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();
            _dataSet.Add(new Datasets()
            {
                label = "Total Volume",
                data = vm.Reports.Select(r => r.TotalVolume).ToArray()
            });
            _chart.datasets = _dataSet;
            return JsonConvert.SerializeObject(_chart);
        }
    }
}