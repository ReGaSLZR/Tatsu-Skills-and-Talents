namespace ReGaSLZR
{

	[System.Serializable]
	public class Talent
	{

		public BasicInfo basicInfo;

		public uint costReductionPercent;
		public uint cooldownReductionPercent;

		public uint valueDealtIncreasePercent;
		public uint additionalProjectileCount;
		public uint rangeIncreasePercent;

		public FXSettings fxOnUse;
		public FXMode fxMode;

	}

}