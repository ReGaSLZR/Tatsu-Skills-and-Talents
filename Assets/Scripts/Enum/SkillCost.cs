namespace ReGaSLZR
{

    [System.Serializable]
    public class SkillCost
    {

        public TargetStat stat;
        public uint cost;

        public SkillCost(TargetStat stat, uint cost)
        {
            this.stat = stat;
            this.cost = cost;
        }

    }

}