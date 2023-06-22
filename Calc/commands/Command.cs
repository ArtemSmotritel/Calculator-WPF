namespace Calc.commands
{
    abstract class Command
    {
        public double operand;
        protected ArithmeticUnit unit;
        public string OperationCode { get; protected set; }

        public abstract void Do();
        public abstract void Undo();

    }
}
