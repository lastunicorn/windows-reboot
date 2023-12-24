using System;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ActionTimeArea.SetFixedDate
{
    public class SetFixedDateRequest : IRequest
    {
        public DateTime Date { get; set; }
    }
}