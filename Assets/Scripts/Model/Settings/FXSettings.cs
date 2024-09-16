namespace ReGaSLZR
{

	using UnityEngine;

	[System.Serializable]
	public class FXSettings
	{

		public AudioClip SFX;
		public float delaySFX;

		public GameObject VFX;
		public float delayVFX;

		public FXSettings(AudioClip SFX, float delaySFX,
			GameObject VFX, float delayVFX)
		{
			this.SFX = SFX;
			this.delaySFX = delaySFX;
			this.VFX = VFX;
			this.delayVFX = delayVFX;
		}

	}

}