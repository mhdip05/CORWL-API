using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Net;

namespace NMS_API_N.Controllers
{
    public class ReportController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReportController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("PrintReport")]
        public IActionResult PrintReport()
        {
#nullable disable
            //string mimtype = "";
            //int extension = 1;
            //var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\Report.rdlc";

            var reportPath = $"{_webHostEnvironment.ContentRootPath}Reports\\Report1.rdlc";
            Stream reportDefinition;
            using var fs = new FileStream(reportPath, FileMode.Open);
            reportDefinition = fs;
            var report = new LocalReport();
            report.LoadReportDefinition(reportDefinition);

            report.SetParameters(new[] { new ReportParameter("rp1", "RDLC Sample Report ") });
            byte[] pdf = report.Render("PDF");
            fs.Dispose();
            return File(pdf, "application/pdf", "hello world" + ".pdf");

            //var parameters = new Dictionary<string, string>();
            //parameters.Add("rp1", "welcome to this report");
            //var localReport = new LocalReport(path);
            //var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            //return File(result.MainStream, "application/pdf");
        }

        [HttpGet("PrintQuestPdf")]
        public void PrintQuestPdf()
        {
            Document.Create(document =>
             {
                 document.Page(page =>
                 {
                     page.Header()
                         .Background(Colors.Blue.Lighten2)
                         .Height(1, Unit.Inch);
                 });

                 document.Page(page =>
                 {
                     page.Size(PageSizes.A5.Landscape());
                 });

             }).ShowInPreviewer();



            // return Ok();
        }
    }
}
