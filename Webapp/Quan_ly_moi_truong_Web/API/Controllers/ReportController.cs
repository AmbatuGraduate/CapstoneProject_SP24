
using Application.Common.Interfaces.Persistence;
using Application.GoogleAuthentication.Common;
using Application.GoogleAuthentication.Queries.GoogleAccessToken;
using Application.Report.Commands.Create;
using Application.Report.Commands.Response;
using Application.Report.Common;
using Application.Report.Queries.GetById;
using Application.Report.Queries.List;
using Application.Report.Queries.ListByUser;
using Application.Report.Queries.ListFromDb;
using Application.Report.Queries.ListLateReport;
using ErrorOr;
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
        public async Task<IActionResult> CreateReport([FromBody] CreateReportRequest request)
        {
            var clientType = Request.Headers["Client-Type"];

            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }
            var command = new CreateReportCommand
            (
                accessToken,
                request.IssuerEmail,
                request.ReportSubject,
                request.ReportBody,
                request.ExpectedResolutionDate,
                request.ReportImpact
            );
            var result = await mediator.Send(command);
            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
            }

            return Ok(result);
        }

        // get all emails from google
        [HttpGet]
        public async Task<IActionResult> GetReportFormats()
        {
            var clientType = Request.Headers["Client-Type"];

            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }

            var result = await mediator.Send(new ListReportsQuery(accessToken));
            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
            }

            return Ok(result);
        }

        // get report by id
        [HttpGet]
        public async Task<IActionResult> GetReportById(string id)
        {
            var clientType = Request.Headers["Client-Type"];

            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }

            var result = await mediator.Send(new GetByIdQuery(accessToken, id));
            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Report not found");
            }

            return Ok(result);
        }

        // get all reports from db
        [HttpGet]
        public IActionResult GetAllReportsFromDb()
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
        public async Task<IActionResult> GetReportsByUser(string email)
        {
            var clientType = Request.Headers["Client-Type"];

            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }

            var result = await mediator.Send(new ListByEmailQuery(accessToken, email));
            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "No reports found");
            }
            return Ok(result);
        }


        // get all reports that has been late
        [HttpGet]
        public async Task<IActionResult> GetAllLateReports()
        {
            var clientType = Request.Headers["Client-Type"];

            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }

            var result = await mediator.Send(new ListLateReportQuery());
            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: "No reports found");
            }
            return Ok(result);
        }

        // response report
        [HttpPost]
        public async Task<IActionResult> ResponseReport([FromBody] ReponseReportRequest request)
        {
            var clientType = Request.Headers["Client-Type"];

            // declare accesstoken
            string accessToken;
            if (clientType == "Mobile") // mobile client
            {
                var authHeader = Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest("Authorization header is missing");
                }
                accessToken = authHeader.ToString().Replace("Bearer ", "");
            }
            else // web client
            {
                var jwt = Request.Cookies["u_tkn"];
                if (String.IsNullOrEmpty(jwt))
                {
                    return BadRequest("u_tkn cookie is missing");
                }
                System.Diagnostics.Debug.WriteLine("token: " + jwt);
                ErrorOr<GoogleAccessTokenResult> token = await mediator.Send(new GoogleAccessTokenQuery(jwt));
                if (token.IsError)
                {
                    return BadRequest("Invalid token");
                }
                accessToken = token.Value.accessToken;
            }
            var command = new ReponseReportCommand(accessToken, request.ReportID, request.Response, request.Status);
            var result = await mediator.Send(command);
            if (result.IsError)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.FirstError.Description);
            }

            return Ok(result);
        }
    }
}