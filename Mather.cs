namespace MediaMather
{
	public static class Mather
	{
		//
		public static double? Add(Operands operands)
		{
				return operands.X + operands.Y;
		}
		public static double? Sub(Operands operands)
		{
				return operands.X - operands.Y;
		}
		public static double? Mult(Operands operands)
		{
				return operands.X * operands.Y;
		}
		public static double? Div(Operands operands)
		{
				return operands.X / operands.Y;
		}
	}
}
