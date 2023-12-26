using DustInTheWind.WindowsReboot.Domain;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTypeArea.SetForceOption
{
    public class SetForceOptionRequest : IRequest
    {
        public ForceOption ForceOption { get; set; }
    }
}