using Calc.commands;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Calc
{
    public class Calculator : INotifyPropertyChanged
    {
        ArithmeticUnit arithmeticUnit;
        ControlUnit controlUnit;
        private Command _pendingCommand;
        private string _currentNumber;

        public ICommand RemoveCommand { get; private set; }
        public ICommand NumericInputCommand { get; private set; }
        public ICommand AddCommand => new BindingCommand(_ => ExecuteAndSetPendingCommand(new Add(arithmeticUnit)));
        public ICommand SubtractCommand => new BindingCommand(_ => ExecuteAndSetPendingCommand(new Subtract(arithmeticUnit)));
        public ICommand MultiplyCommand => new BindingCommand(_ => ExecuteAndSetPendingCommand(new Multiply(arithmeticUnit)));
        public ICommand DivideCommand => new BindingCommand(_ => ExecuteAndSetPendingCommand(new Divide(arithmeticUnit)));
        public ICommand EqualsCommand => new BindingCommand(_ =>
        {
            ExecutePendingCommand();
        }, _ => CanPerformOperation());
        public ICommand UndoCommand => new BindingCommand(_ => Undo(1));
        public ICommand ClearCommand => new BindingCommand(_ => Clear());

        private Command PendingCommand
        {
            get => _pendingCommand;
            set
            {
                _pendingCommand = value;
                OnPropertyChanged(nameof(PendingOperationSign));
            }
        }

        private bool IsFirstNumber { get; set; }

        public double Register => arithmeticUnit.Register;

        public string PendingOperationSign => PendingCommand?.OperationCode ?? string.Empty;

        public string CurrentNumber
        {
            get => _currentNumber;
            private set
            {
                _currentNumber = value;
                OnPropertyChanged(nameof(CurrentNumber));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public Calculator()
        { 
            arithmeticUnit = new ArithmeticUnit();
            controlUnit = new ControlUnit();
            IsFirstNumber = true;
            
            RemoveCommand = new BindingCommand(
                _ => Remove(),
                _ => CanPerformOperation());

            NumericInputCommand = new BindingCommand(param => AppendToCurrentNumber(param.ToString()));

            arithmeticUnit.RegisterChanged += ArithmeticUnit_RegisterChanged;
        }

        private void ArithmeticUnit_RegisterChanged()
        {
            OnPropertyChanged(nameof(Register));
        }

        private void Remove()
        {
            CurrentNumber = CurrentNumber.Substring(0, CurrentNumber.Length - 1);
        }

        private void Clear()
        {
            arithmeticUnit.Reset();
            PendingCommand = null;
            CurrentNumber = "";

            IsFirstNumber = true;
        }

        private void AppendToCurrentNumber(string numberOrPeriod)
        {
            if (string.IsNullOrEmpty(CurrentNumber) && numberOrPeriod == ".")
            {
                CurrentNumber = "0";
            }
            CurrentNumber += numberOrPeriod;
            Application.Current.MainWindow.Focus();
        }

        private void Undo(int levels)
        {
            controlUnit.UndoCommand(levels);
        }

        private void Run(Command command)
        {
            controlUnit.StoreCommand(command);
            controlUnit.DoCommand(command);
        }

        private void ExecuteAndSetPendingCommand(Command command)
        {
            if (CanPerformOperation())
            {
                ExecutePendingCommand();
            }

            PendingCommand = command;
        }

        private bool CanPerformOperation()
        {
            return !string.IsNullOrEmpty(CurrentNumber);
        }

        private void ExecutePendingCommand()
        {
            if (IsFirstNumber)
            {
                Run(new Add(arithmeticUnit, double.Parse(CurrentNumber)));
                CurrentNumber = string.Empty;
                IsFirstNumber = false;
                return;
            }
            if (PendingCommand == null)
            {
                return;
            }

            PendingCommand.operand = double.Parse(CurrentNumber);
            Run(PendingCommand);

            CurrentNumber = string.Empty;
            PendingCommand = null;
        }

    }
}
