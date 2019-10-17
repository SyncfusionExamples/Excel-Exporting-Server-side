using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Syncfusion.EJ2.Base;
using Syncfusion.XlsIO;
using TestSample.Models;

namespace TestSample.Controllers
{

    public class HomeController : Controller
    {

        public IActionResult Index()
        {

            order = new List<OrdersDetails>();
            if (order.Count == 0)
                BindDataSource();
            ViewBag.dataSource = order.ToArray();
            return View();
        }
        public static List<OrdersDetails> order = new List<OrdersDetails>();
        public class ExportModel // Created the model class to Deserialize the GridModel
        {
            public List<Syncfusion.EJ2.Grids.GridColumns> Columns { get; set; }
            public DataManagerRequest Queries { get; set; }
        }

        public ActionResult ExcelExport(string GridModel)
        {
            ExportModel exportModel = new ExportModel();
            exportModel = (ExportModel)JsonConvert.DeserializeObject(GridModel, typeof(ExportModel));  // Deserialized the GridModel
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];
                IEnumerable DataSource = order;

                //Import the data to worksheet
                IList<OrdersDetails> reports = DataSource.AsQueryable().Cast<OrdersDetails>().ToList();
                worksheet.ImportData(reports, 2, 1, true);
                MemoryStream stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                //Download the Excel file in the browser
                FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");

                fileStreamResult.FileDownloadName = "Output.xlsx";

                return fileStreamResult;
            }
        }


        public void BindDataSource()
        {
            int code = 10000;
            for (int i = 1; i < 10; i++)
            {
                order.Add(new OrdersDetails(code + 1, null,  2.3 * i,  "Berlin"));
                order.Add(new OrdersDetails(code + 2, "ANATR", 3.3 * i, "Madrid"));
                order.Add(new OrdersDetails(code + 3, "ANTON", 4.3 * i, "Cholchester"));
                order.Add(new OrdersDetails(code + 4, "BLONP", 5.3 * i, "Marseille"));
                order.Add(new OrdersDetails(code + 5, "BOLID", 6.3 * i, "Tsawassen"));
                code += 5;
            }
        }
    }
    public class OrdersDetails
    {
        public OrdersDetails()
        {

        }
        public OrdersDetails(long OrderId, string CustomerId, double Freight, string ShipCity)
        {
            this.OrderID = OrderId;
            this.CustomerID = CustomerId;
            this.Freight = Freight;
            this.ShipCity = ShipCity;
        }
        public long OrderID { get; set; }
        public string CustomerID { get; set; }
        public double Freight { get; set; }
        public string ShipCity { get; set; }
    }
}
