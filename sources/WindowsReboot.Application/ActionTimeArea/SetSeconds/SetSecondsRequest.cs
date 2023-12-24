using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetSeconds
{
    public class SetSecondsRequest : IRequest
    {
        public int Seconds { get; set; }
    }
}