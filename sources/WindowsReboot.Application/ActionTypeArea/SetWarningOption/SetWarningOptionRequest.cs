using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTypeArea.SetWarningOption
{
    public class SetWarningOptionRequest : IRequest
    {
        public bool ActivateWarning { get; set; }
    }
}