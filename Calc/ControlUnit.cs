using Calc.commands;
using System.Collections.Generic;

namespace Calc
{
    class ControlUnit
    {
        private List<Command> commands = new List<Command>();
        private int currentCommandIndex = 0;

        public void StoreCommand(Command command)
        {
            if (currentCommandIndex < commands.Count)
            {
                commands[currentCommandIndex] = command;
            }
            else
            {
                commands.Add(command);
            }
        }

        public void DoCommand(Command command)
        {
            commands[currentCommandIndex].Do();
            currentCommandIndex++;
        }

        public void UndoCommand(int levels)
        {
            for (int i = 0; i < levels; i++)
            {
                if (currentCommandIndex - 1 <= commands.Count - 1 && currentCommandIndex > 0)
                {
                    commands[--currentCommandIndex].Undo();
                }
            }
        }

    }
}
