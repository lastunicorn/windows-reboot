﻿// Windows Reboot
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
using System.Runtime.Serialization;

namespace DustInTheWind.WindowsReboot.Ports.SystemAccess
{
    /// <summary>
    /// General exception for the WindowsReboot application. It's also used as base class for all the other exceptions.
    /// </summary>
    [Serializable]
    public class WindowsRebootException : ApplicationException
    {
        private const string MESSAGE = "Internal error in WindowsReboot.";

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsRebootException"/> class.
        /// </summary>
        public WindowsRebootException()
            : base(MESSAGE)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsRebootException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public WindowsRebootException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsRebootException"/> class with a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public WindowsRebootException(Exception innerException)
            : base(MESSAGE, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsRebootException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public WindowsRebootException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsRebootException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public WindowsRebootException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}