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
using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace DustInTheWind.WorkerEngine.Setup.Autofac
{
    internal class WorkerProvider : IWorkerProvider
    {
        private readonly IComponentContext context;

        public WorkerProvider(IComponentContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<IWorker> GetNewWorkers()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsAssignableTo<IWorker>())
                .Select(x => context.IsRegistered(x)
                    ? (IWorker)context.Resolve(x)
                    : null)
                .Where(x => x != null);
        }
    }
}