using System;

namespace Calc
{
    class ArithmeticUnit
    {
        private double _register;
        public double Register
        { 
            get => _register;
            private set
            { 
                _register = value;
                RegisterChanged?.Invoke();
            }
        }
        public event Action RegisterChanged;

        public ArithmeticUnit()
        {
            Register = 0;
        }

        public void Reset()
        {
            Register = 0;
        }

        public void Run(char operationCode, double value)
        {
            switch (operationCode)
            {
                case '+':
                    Register += value;
                    return;
                case '-':
                    Register -= value;
                    return;
                case '*':
                    Register *= value;
                    return;
                case '/':
                    Register /= value;
                    return;

                default:
                    return;
            }
        }
    }
}
