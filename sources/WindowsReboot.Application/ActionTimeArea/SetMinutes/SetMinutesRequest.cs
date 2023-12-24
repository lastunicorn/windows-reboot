using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetMinutes
{
    public class SetMinutesRequest : IRequest
    {
        public int Minutes { get; set; }
    }
}