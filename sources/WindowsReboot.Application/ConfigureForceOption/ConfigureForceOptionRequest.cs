using DustInTheWind.WindowsReboot.Core;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ConfigureForceOption
{
    public class ConfigureForceOptionRequest : IRequest
    {
        public ForceOption ForceOption { get; set; }
    }
}