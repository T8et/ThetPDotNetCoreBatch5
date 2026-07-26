using DotNetTrainingBatch5.ChartWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTrainingBatch5.ChartWebApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult PieChart()
        {
            ApexPieChartModel chart = new ApexPieChartModel();
            chart.Series = new int[] { 44, 55, 13, 43, 22 };
            chart.Labels = new string[] { "Team A", "Team B", "Team C", "Team D", "Team E" };
            return View(chart);
        }

        public IActionResult MixedChart()
        {
            var chart = new ApexMixedChartModel
            {
                BlogData = new List<int> { 440, 505, 414, 671, 227, 413, 201, 352, 752, 320, 257, 160 },
                SocialData = new List<int> { 23, 42, 35, 27, 43, 22, 17, 31, 22, 22, 12, 16 },
                Labels = new List<string>
                {
                    "01 Jan 2024","02 Jan 2024","03 Jan 2024","04 Jan 2024",
                    "05 Jan 2024","06 Jan 2024","07 Jan 2024","08 Jan 2024",
                    "09 Jan 2024","10 Jan 2024","11 Jan 2024","12 Jan 2024"
                }
            };
            return View(chart);
        }
    }
}
