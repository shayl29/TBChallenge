using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using TenBisChallenge.Models;

namespace TenBisChallenge.ViewModels
{
    public class HomeViewModel
    {
        public List<Company> Companies { get; set; }
        public List<Report> Reports { get; set; }
        public string ChartJson { get; internal set; }
        public FilterInput FilterInput { get; set; }

        public HomeViewModel() : base()
        {
            Init();
        }

        public void Init()
        {
            // Initialize properties in this class
            Companies = new List<Company>();
            Reports = new List<Report>();
            ChartJson = string.Empty;
            FilterInput = new FilterInput();
        }

        public void Publish(Exception ex, string message)
        {
            Publish(ex, message, null);
        }
        public void Publish(Exception ex, string message,
                            NameValueCollection nvc)
        {
            // Update view model properties

            // TODO: Publish exception here	
        }

        protected void PopulateCompanies()
        {
            DBContext db = null;
            try
            {
                db = new DBContext();
                // Get the collection
                Companies = db.Companies.ToList();
            }
            catch (Exception ex)
            {
                Publish(ex, "Error while loading companies.");
            }
        }

        protected void PopulateReports(FilterInput filterInput)
        {
            DBContext db = null;
            try
            {
                db = new DBContext();
                // Get the collection
                Reports = (from bank in db.Banks
                           join card in db.MoneyCards on bank.MoneyCardId equals card.MoneyCardId
                           join user in db.Users on card.UserId equals user.UserId
                           join company in db.Companies on card.CompanyId equals company.CompanyId
                           where (company.CompanyId == filterInput.CompanyId &&
                                 bank.OrderTime >= filterInput.StartDate &&
                                 bank.OrderTime <= filterInput.EndDate)
                           group bank by new { user.UserId, user.UserFullName, card.LastFourDigits } into report
                           select new Report
                           {
                               UserId = report.Key.UserId,
                               UserFullName = report.Key.UserFullName,
                               Pan = report.Key.LastFourDigits,
                               TotalVolume = report.Sum(b => b.TxValue)
                           }).ToList();
            }
            catch (Exception ex)
            {
                Publish(ex, "Error while loading entries.");
            }
        }

        protected void GenerateChart()
        {
            try
            {
                // Get the collection
                Chart _chart = new Chart
                {
                    labels = Reports.Select(report => report.UserFullName).ToArray(),
                    datasets = new List<Datasets>()
                };

                List<Datasets> _dataSet = new List<Datasets>
                {
                    new Datasets()
                    {
                        label = "Total Volume",
                        data = Reports.Select(r => r.TotalVolume).ToArray(),
                        backgroundColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60" },
                        borderColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60" },
                        borderWidth = "1"
                    }
                };

                _chart.datasets = _dataSet;

                ChartJson = JsonConvert.SerializeObject(_chart);
            }
            catch (Exception ex)
            {
                Publish(ex, "Error while building the chart.");
            }
        }

        public void HandleRequest(string name, params object[] reqParams)
        {
            switch (name)
            {
                case "init":
                    PopulateCompanies();
                    break;
                case "filter":
                    PopulateReports(reqParams.First() as FilterInput);
                    GenerateChart();
                    break;
                default:
                    break;
            }
        }
    }
}