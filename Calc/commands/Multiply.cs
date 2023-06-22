namespace Calc.commands
{
    class Multiply : Command
    {
        public Multiply(ArithmeticUnit arithmeticUnit)
        {
            this.unit = arithmeticUnit;
            OperationCode = "*";
        }

        public Multiply(ArithmeticUnit arithmeticUnit, double operand)
        {
            this.unit = arithmeticUnit;
            this.operand = operand;
            OperationCode = "*";
        }

        public override void Do()
        {
            unit.Run('*', operand);
        }

        public override void Undo()
        {
            unit.Run('/', operand);
        }
    }
}
