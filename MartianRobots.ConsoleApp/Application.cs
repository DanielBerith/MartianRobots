using MartianRobots.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.ConsoleApp
{
    public class Application
    {
        private readonly IInputParser inputParser;
        private readonly IRobotSimulationService simulationService;
        private readonly IOutputFormatter outputFormatter;

        public Application(
            IInputParser inputParser,
            IRobotSimulationService simulationService,
            IOutputFormatter outputFormatter)
        {
            this.inputParser = inputParser;
            this.simulationService = simulationService;
            this.outputFormatter = outputFormatter;
        }

        public void Run()
        {
            try
            {
                // Read all input
                var input = ReadAllInput();

                // Parse, simulate, and output
                var simulationInput = inputParser.Parse(input);
                var results = simulationService.RunSimulation(simulationInput);
                var output = outputFormatter.Format(results);

                Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                Environment.Exit(1);
            }
        }

        private string ReadAllInput()
        {
            var lines = new List<string>();
            string line;

            while ((line = Console.ReadLine()) != null)
            {
                lines.Add(line);
            }

            return string.Join(Environment.NewLine, lines);
        }
    }
}
