namespace ReGaSLZR
{

    using UnityEngine;

    [System.Serializable]
    public class CharacterStats
    {

        public uint health = NumberConstants.STAT_VALUE_MAX;
        public uint mana = NumberConstants.STAT_VALUE_MAX;

        public float walkSpeed = 5f;

        public static uint GetClampedStatValue(int statValue)
        {
            return (uint)Mathf.Clamp(statValue,
                NumberConstants.STAT_VALUE_MIN, NumberConstants.STAT_VALUE_MAX);
        }

    }

}