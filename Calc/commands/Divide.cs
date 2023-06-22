namespace Calc.commands
{
    class Divide : Command
    {
        public Divide(ArithmeticUnit arithmeticUnit)
        {
            this.unit = arithmeticUnit;
            OperationCode = "/";
        }

        public override void Do()
        {
            unit.Run('/', operand);
        }

        public override void Undo()
        {
            unit.Run('*', operand);
        }
    }
}
