using System;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.WindowsReboot.Domain;
using DustInTheWind.WindowsReboot.Ports.ConfigAccess;
using DustInTheWind.WindowsReboot.Ports.UserAccess;
using MediatR;

namespace DustInTheWind.WindowsReboot.Application.ConfigurationArea.SaveConfiguration
{
    internal class SaveConfigurationUseCase : IRequestHandler<SaveConfigurationRequest>
    {
        private readonly IUserInterface userInterface;
        private readonly ExecutionTimer executionTimer;
        private readonly ExecutionPlan executionPlan;
        private readonly IConfigStorage configuration;

        public SaveConfigurationUseCase(IUserInterface userInterface, ExecutionTimer executionTimer, ExecutionPlan executionPlan, IConfigStorage configuration)
        {
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.executionTimer = executionTimer ?? throw new ArgumentNullException(nameof(executionTimer));
            this.executionPlan = executionPlan ?? throw new ArgumentNullException(nameof(executionPlan));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Task Handle(SaveConfigurationRequest request, CancellationToken cancellationToken)
        {
            configuration.ActionTime = executionTimer.ScheduleTime;

            configuration.ActionType = executionPlan.ActionType;
            configuration.ForceClosingPrograms = executionPlan.ForceOption == ForceOption.Yes;

            configuration.Save();

            userInterface.DisplayMessage("The configuration was saved.");

            return Task.CompletedTask;
        }
    }
}