

using Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;

namespace Application.Report.Commands.Delete
{
    public class DeleteReportHandler : IRequestHandler<DeleteReportCommand, ErrorOr<Unit>>
    {
        private readonly IReportService _reportService;
        
        // constructor
        public DeleteReportHandler(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
        {
            await _reportService.DeleteReport(request.ReportID);
            return Unit.Value;
        }
    }
}
