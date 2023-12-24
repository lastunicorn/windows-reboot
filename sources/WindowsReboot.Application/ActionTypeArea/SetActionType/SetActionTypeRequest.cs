using DustInTheWind.WindowsReboot.Core;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTypeArea.SetActionType
{
    public class SetActionTypeRequest : IRequest
    {
        public ActionType ActionType { get; set; }
    }
}