using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.MainArea.CloseApplication
{
    internal class CloseApplicationUseCase : IRequestHandler<CloseApplicationRequest, CloseApplicationResponse>
    {
        public Task<CloseApplicationResponse> Handle(CloseApplicationRequest request, CancellationToken cancellationToken)
        {
            CloseApplicationResponse response = new CloseApplicationResponse
            {
            };

            return Task.FromResult(response);
        }
    }
}