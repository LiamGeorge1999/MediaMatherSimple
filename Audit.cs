namespace MediaMather
{
	public class Audit
	{
		public int Id { get; set; }
		public string Operation { get; set; }
		public double X { get; set; }
		public double Y { get; set; }
		public double? Result { get; set; }
		public DateTime DateTime { get; set; }
	}
}
