using DustInTheWind.WindowsReboot.Core;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTypeArea.ConfigureActionType
{
    public class ConfigureActionTypeRequest : IRequest
    {
        public ActionType ActionType { get; set; }
    }
}