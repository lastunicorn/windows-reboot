using MediatR;

namespace DustInTheWind.WindowsReboot.Application.MainArea.CloseApplication
{
    public class CloseApplicationRequest : IRequest<CloseApplicationResponse>
    {
        public bool Force { get; set; }
    }
}