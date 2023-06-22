namespace Calc.commands
{
    class Subtract : Command
    {
        public Subtract(ArithmeticUnit arithmeticUnit)
        {
            this.unit = arithmeticUnit;
            OperationCode = "-";
        }

        public override void Do()
        {
            unit.Run('-', operand);
        }

        public override void Undo()
        {
            unit.Run('+', operand);
        }
    }
}
