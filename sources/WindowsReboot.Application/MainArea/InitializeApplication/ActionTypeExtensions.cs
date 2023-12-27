// Windows Reboot
// Copyright (C) 2009-2023 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using DustInTheWind.WindowsReboot.Domain;

namespace DustInTheWind.WindowsReboot.Application.MainArea.InitializeApplication
{
    internal static class ActionTypeExtensions
    {
        public static ActionType ToDomain(this Ports.ConfigAccess.ActionType actionType)
        {
            switch (actionType)
            {
                case Ports.ConfigAccess.ActionType.Ring:
                    return ActionType.Ring;

                case Ports.ConfigAccess.ActionType.LockWorkstation:
                    return ActionType.LockWorkstation;

                case Ports.ConfigAccess.ActionType.LogOff:
                    return ActionType.LogOff;

                case Ports.ConfigAccess.ActionType.Sleep:
                    return ActionType.Sleep;

                case Ports.ConfigAccess.ActionType.Hibernate:
                    return ActionType.Hibernate;

                case Ports.ConfigAccess.ActionType.Reboot:
                    return ActionType.Reboot;

                case Ports.ConfigAccess.ActionType.ShutDown:
                    return ActionType.ShutDown;

                case Ports.ConfigAccess.ActionType.PowerOff:
                    return ActionType.PowerOff;

                default:
                    throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null);
            }
        }

        public static Ports.ConfigAccess.ActionType ToConfigModel(this ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.Ring:
                    return Ports.ConfigAccess.ActionType.Ring;

                case ActionType.LockWorkstation:
                    return Ports.ConfigAccess.ActionType.LockWorkstation;

                case ActionType.LogOff:
                    return Ports.ConfigAccess.ActionType.LogOff;

                case ActionType.Sleep:
                    return Ports.ConfigAccess.ActionType.Sleep;

                case ActionType.Hibernate:
                    return Ports.ConfigAccess.ActionType.Hibernate;

                case ActionType.Reboot:
                    return Ports.ConfigAccess.ActionType.Reboot;

                case ActionType.ShutDown:
                    return Ports.ConfigAccess.ActionType.ShutDown;

                case ActionType.PowerOff:
                    return Ports.ConfigAccess.ActionType.PowerOff;

                default:
                    throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null);
            }
        }
    }
}