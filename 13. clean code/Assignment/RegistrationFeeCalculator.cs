namespace BusinessLayer
{
	class RegistrationFeeCalculator : IRegistrationFeeCalculator
	{
		public int Calculate(ISpeaker speaker)
		{
			return speaker.Experience switch
			{
				<= 1 or null => 500,
				<= 3 => 250,
				<= 5 => 100,
				<= 9 => 50,
				_ => 0
			};
		}
	}
}
