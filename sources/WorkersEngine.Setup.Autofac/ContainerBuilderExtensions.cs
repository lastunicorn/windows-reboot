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
using System.Reflection;
using Autofac;

namespace DustInTheWind.WorkersEngine.Setup.Autofac
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterWorkers(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<WorkersContainer>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<WorkerProvider>().As<IWorkerProvider>().SingleInstance();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            RegisterWorkersInternal(containerBuilder, assemblies);
        }

        public static void RegisterWorkers(this ContainerBuilder containerBuilder, params Assembly[] assemblies)
        {
            containerBuilder.RegisterType<WorkersContainer>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<WorkerProvider>().As<IWorkerProvider>().SingleInstance();

            if (assemblies?.Length > 0)
                RegisterWorkersInternal(containerBuilder, assemblies);
        }

        private static void RegisterWorkersInternal(ContainerBuilder containerBuilder, Assembly[] assemblies)
        {
            IEnumerable<Type> workerTypes = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && x.IsAssignableTo<IWorker>());

            foreach (Type workerType in workerTypes)
                containerBuilder.RegisterType(workerType).AsSelf().As<IWorker>().SingleInstance();
        }
    }
}