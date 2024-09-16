namespace ReGaSLZR
{

    [System.Serializable]
    public class TargetSettings
    {

        public TargetMode mode;
        public TargetStat stat;
        public TargetEffect effect;

        public float range;
        public uint valueDealtToTarget;

        public float delayDealValue;

        public TargetSettings(
            TargetMode mode, TargetStat stat, TargetEffect effect,
            float range, uint valueDealtToTarget, float delayDealValue)
        {
            this.mode = mode;
            this.stat = stat;
            this.effect = effect;
            this.range = range;
            this.valueDealtToTarget = valueDealtToTarget;
            this.delayDealValue = delayDealValue;
        }

    }

}