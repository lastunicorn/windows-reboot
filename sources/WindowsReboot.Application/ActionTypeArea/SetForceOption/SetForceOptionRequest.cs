using DustInTheWind.WindowsReboot.Core;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTypeArea.SetForceOption
{
    public class SetForceOptionRequest : IRequest
    {
        public ForceOption ForceOption { get; set; }
    }
}