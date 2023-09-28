using EmployeeMgmnt.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prometheus;

namespace EmployeeMgmnt.Controllers
{
    [Route("api/[controller]")]
    //[Route("metrics")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class EmployeeController : ControllerBase
    {
        //Metrics to track how many times this method has been executed
        //Counter counter = Metrics.CreateCounter("MyCounter", "Number Of Clicks");

        private readonly Counter requestsCounter;
        public readonly IConfiguration config;
        public readonly EmployeeContext context;

        public EmployeeController(IConfiguration _config, EmployeeContext _context)
        {
            config = _config;
            context = _context;
            requestsCounter = Metrics.CreateCounter("api_my_counter" , "Total number of API requests");

        }



        //[HttpPost("CreateEmp")]
        //public IActionResult CreateEmployee()
        //{

        //     context.SaveChangesAsync();
        //    return Ok("success");
        //}





        private static readonly Histogram RequestDuration = Metrics.CreateHistogram(
    "http_request_duration_seconds",
    "Duration of HTTP requests in seconds",
    new HistogramConfiguration
    {
        Buckets = Histogram.LinearBuckets(start: 0.1, width: 0.2, count: 5) // Configure buckets as needed
    }
    );





        [HttpPost("InsertEmp")]
        public async Task<ActionResult<Employee>> InsertTableRow(Employee employee)
        {
            
            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();
            requestsCounter.Inc();
            return Ok("success");
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetAllData()
        {
            requestsCounter.Inc();
            //var datlist = from x in context.Employees
            //                     select x;
            var datlist = await(from x in context.Employees
                                select x).ToListAsync();
            //requestsCounter.Inc();
            //return Ok(datlist);
            return Ok("Hi mahi");
        }








    }
}
