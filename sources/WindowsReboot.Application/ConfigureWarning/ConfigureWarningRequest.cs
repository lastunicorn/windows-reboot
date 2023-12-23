using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActivateWarning
{
    public class ConfigureWarningRequest : IRequest
    {
        public bool ActivateWarning { get; set; }
    }
}