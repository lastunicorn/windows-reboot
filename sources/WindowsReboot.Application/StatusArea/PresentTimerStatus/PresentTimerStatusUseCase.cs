using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.StatusArea.PresentTimerStatus
{
    internal class PresentTimerStatusUseCase : IRequestHandler<PresentTimerStatusRequest, PresentTimerStatusResponse>
    {
        public Task<PresentTimerStatusResponse> Handle(PresentTimerStatusRequest request, CancellationToken cancellationToken)
        {
            PresentTimerStatusResponse response = new PresentTimerStatusResponse
            {
            };

            return Task.FromResult(response);
        }
    }
}