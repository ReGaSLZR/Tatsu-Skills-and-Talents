namespace ReGaSLZR
{

    using UnityEngine;
    using UnityEngine.UI;

    public class GameCharacter : MonoBehaviour
    {

        [SerializeField]
        private CharacterStatsHolder statsHolder;

        [Space]

        [SerializeField]
        private Slider sliderHealth;

        [SerializeField]
        private Slider sliderMana;

        [SerializeField]
        private UICharacterStatChange uiStatChange;

        public void ApplyStatChange(
            TargetEffect effect, TargetStat stat, int valueDealt)
        {
            uiStatChange.PlayStatChange((uint)valueDealt, stat, effect);

            switch (stat)
            {
                case TargetStat.Health:
                    {
                        if (TargetEffect.Damage == effect)
                        {
                            statsHolder.Stats.health = CharacterStats.GetClampedStatValue(
                                (int)statsHolder.Stats.health - valueDealt);
                        }
                        else if (TargetEffect.Heal == effect)
                        {
                            statsHolder.Stats.health = CharacterStats.GetClampedStatValue(
                                (int)statsHolder.Stats.health + valueDealt);
                        }

                        break;
                    }
                case TargetStat.Mana:
                    {
                        if (TargetEffect.Damage == effect)
                        {
                            statsHolder.Stats.mana = CharacterStats.GetClampedStatValue(
                                (int)statsHolder.Stats.mana - valueDealt);
                        }
                        else if (TargetEffect.Heal == effect)
                        {
                            statsHolder.Stats.mana = CharacterStats.GetClampedStatValue(
                                (int)statsHolder.Stats.mana + valueDealt);
                        }

                        break;
                    }
                default:
                    {
                        Debug.LogWarning($"{GetType().Name} cannot apply stat changes " +
                            $"to stat {stat}");
                        break;
                    }
            }

            sliderHealth.value = statsHolder.Stats.health;
            sliderMana.value = statsHolder.Stats.mana;

            if (statsHolder.Stats.health == NumberConstants.STAT_VALUE_MIN)
            {
                Destroy(gameObject);
            }
        }

    }

}