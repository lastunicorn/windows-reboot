using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTypeArea.ConfigureWarningOption
{
    public class ConfigureWarningRequest : IRequest
    {
        public bool ActivateWarning { get; set; }
    }
}