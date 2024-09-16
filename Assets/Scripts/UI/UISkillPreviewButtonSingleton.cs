namespace ReGaSLZR
{

    using System;
    using System.Collections;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class UISkillPreviewButtonSingleton : AbstractSingleton<UISkillPreviewButtonSingleton>
    {

        [SerializeField]
        private Button button;

        [SerializeField]
        private TextMeshProUGUI textInfo;

        [Space]

        [SerializeField]
        private Color colorTextCooldown;

        [SerializeField]
        private Color colorTextNormal;

        private const float checkInterval = 0.1f;
        private WaitForSeconds cooldownCheckInterval;

        protected override void Awake()
        {
            base.Awake();

            cooldownCheckInterval = new WaitForSeconds(checkInterval);
        }

        public void RegisterOnClickListener(Action onClick)
        {
            if (onClick == null)
            {
                return;
            }

            button.onClick.AddListener(onClick.Invoke);
        }

        public void SetTextInfo(string info)
        {
            textInfo.text = info;
        }

        public void SetEnabled(bool isEnabled)
        {
            button.interactable = isEnabled;
        }

        public void ApplyCooldown(float cooldown)
        {
            StartCoroutine(CoroutineApplyCooldown(cooldown));
        }

        private IEnumerator CoroutineApplyCooldown(float cooldown)
        {
            var cachedText = textInfo.text;
            var timeStart = Time.time;
            var timeEnd = timeStart + cooldown;

            SetTextInfo(cooldown.ToString());
            SetEnabled(false);
            textInfo.color = colorTextCooldown;

            while (Time.time < timeEnd)
            {
                yield return cooldownCheckInterval;
                SetTextInfo((cooldown - (Time.time - timeStart)).ToString("F1"));
            }

            textInfo.color = colorTextNormal;
            SetTextInfo(cachedText);
            SetEnabled(true);
        }

    }

}