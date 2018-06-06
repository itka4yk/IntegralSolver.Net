using org.mariuszgromada.math.mxparser;

namespace Extensions
{
    public static class CalculateWithArgumentExtension
    {
        public static double CalculateWith(this Expression func, double x)
        {
            var arg = new Argument($"x={x.ToString().Replace(',','.')}");
            func.addArguments(arg);
            var result = func.calculate();
            func.removeArguments(arg);
            return result;
        }
    }
}
