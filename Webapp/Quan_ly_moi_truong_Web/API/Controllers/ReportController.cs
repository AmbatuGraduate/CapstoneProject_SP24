
using Application.Common.Interfaces.Persistence;
using Application.Report.Commands.Create;
using Application.Report.Queries.List;
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

        [HttpGet]
        public async Task<IActionResult> GetReportById(string accessToken, string id)
        {
            var result = await _reportService.GetReportById(accessToken, id);
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Report not found");
            }

            return Ok(result);
        }
    }
}