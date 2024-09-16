namespace ReGaSLZR
{

    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIPopupMessageSingleton : AbstractSingleton<UIPopupMessageSingleton>
    {

        [SerializeField]
        private GameObject rootUI;

        [SerializeField]
        private TextMeshProUGUI textMessage;

        [SerializeField]
        private Button buttonClose;

        protected override void Awake()
        {
            base.Awake();

            buttonClose.onClick.AddListener(() => SetEnabled(false));
        }

        private void Start()
        {
            SetEnabled(false);
        }

        private void SetEnabled(bool isEnabled)
        {
            rootUI.SetActive(isEnabled);
        }

        public void ShowMessage(string message)
        {
            textMessage.text = message;
            SetEnabled(true);
        }

    }

}