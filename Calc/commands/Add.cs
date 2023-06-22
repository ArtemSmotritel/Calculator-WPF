namespace Calc.commands
{
    class Add : Command
    {
        public Add(ArithmeticUnit arithmeticUnit)
        {
            unit = arithmeticUnit;
            OperationCode = "+";
        }

        public Add(ArithmeticUnit arithmeticUnit, double operand)
        {
            unit = arithmeticUnit;
            this.operand = operand;
            OperationCode = "+";
        }

        public override void Do()
        {
            unit.Run('+', operand);
        }

        public override void Undo()
        {
            unit.Run('-', operand);
        }
    }
}
