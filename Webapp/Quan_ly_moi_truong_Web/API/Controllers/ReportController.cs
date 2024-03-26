
using Application.Common.Interfaces.Persistence;
using Application.Report.Commands.Create;
using Application.Report.Queries.GetById;
using Application.Report.Queries.List;
using Application.Report.Queries.ListByUser;
using Application.Report.Queries.ListFromDb;
using Application.Report.Queries.ListLateReport;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    [EnableCors("AllowAllHeaders")]
    public class ReportController : ApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        private readonly HttpClient _httpClient;
        private readonly IReportService _reportService;

        public ReportController(IMediator mediator, IMapper mapper, IHttpClientFactory httpClientFactory, IReportService reportService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            _httpClient = httpClientFactory.CreateClient();
            _reportService = reportService;
        }

        // create, send & save report
        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportCommand command)
        {
            var result = await mediator.Send(command);
            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
            }

            return Ok(result);
        }

        // get all emails from google
        [HttpGet]
        public async Task<IActionResult> GetReportFormats(string accessToken)
        {
            var result = await mediator.Send(new ListReportsQuery(accessToken));
            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
            }

            return Ok(result);
        }

        // get report by id
        [HttpGet]
        public async Task<IActionResult> GetReportById(string accessToken, string id)
        {
            var result = await mediator.Send(new GetByIdQuery(accessToken, id));
            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Report not found");
            }

            return Ok(result);
        }

        // get all reports from db
        [HttpGet]
        public IActionResult GetAllReports()
        {
            var result = mediator.Send(new ListFromDbQuery());
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "No reports found");
            }
            return Ok(result);
        }

        // get reports by user
        [HttpGet]
        public async Task<IActionResult> GetReportsByUser(string accessToken, string email)
        {
            var result = await mediator.Send(new ListByEmailQuery(accessToken, email));
            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "No reports found");
            }
            return Ok(result);
        }

        // get all reports that has been late
        [HttpGet]
        public async Task<IActionResult> GetAllLateReports(string accessToken)
        {
            var result = await mediator.Send(new ListLateReportQuery(accessToken));
            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "No reports found");
            }
            return Ok(result);
        }

    }
}