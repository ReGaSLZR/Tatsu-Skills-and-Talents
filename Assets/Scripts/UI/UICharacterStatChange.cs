namespace ReGaSLZR
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.Playables;

    public class UICharacterStatChange : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI textStat;

        [SerializeField]
        private PlayableDirector playableDirector;

        [Space]

        [SerializeField]
        private Color colorTextPositive;

        [SerializeField]
        private Color colorTextNegative;

        public void PlayStatChange(uint valueDealt, 
            TargetStat stat, TargetEffect effect)
        {
            textStat.text = string.Concat(
                effect == TargetEffect.Damage ? "-" : "+",
                valueDealt,
                " ",
                stat.ToString());

            textStat.color = effect == TargetEffect.Damage 
                ? colorTextNegative : colorTextPositive;

            playableDirector.Stop();
            playableDirector.Play();
        }

    }

}