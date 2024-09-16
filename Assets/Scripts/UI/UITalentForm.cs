namespace ReGaSLZR
{

    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class UITalentForm : MonoBehaviour
    {

        [SerializeField]
        private Button buttonSave;

        [SerializeField]
        private Button buttonRemove;

        [Space]

        [SerializeField]
        private TMP_Dropdown dropdownTalents;

        [Space]

        [SerializeField]
        private TMP_InputField inputID;

        [SerializeField]
        private TMP_InputField inputName;

        [SerializeField]
        private TMP_InputField inputDescription;

        [SerializeField]
        private TMP_InputField inputCostReductionPercent;

        [SerializeField]
        private TMP_InputField inputCooldownReductionPercent;

        [SerializeField]
        private TMP_InputField inputValueDealtIncreasePercent;

        [SerializeField]
        private TMP_InputField inputRangeIncreasePercent;

        [Space]

        [SerializeField]
        private TMP_InputField inputAdditionalProjectileCount;

        [Space]

        [SerializeField]
        private TMP_Dropdown dropdownSFX;

        [SerializeField]
        private TMP_InputField inputDelaySFX;

        [SerializeField]
        private TMP_Dropdown dropdownVFX;

        [SerializeField]
        private TMP_InputField inputDelayVFX;

        [Space]

        [SerializeField]
        private TMP_Dropdown dropdownFXMode;

        private void Awake()
        {
            ResetUI();
            buttonSave.onClick.AddListener(SaveTalent);
            dropdownTalents.onValueChanged.AddListener(_ => LoadTalent());

            UIBanksChooserSingleton.Instance.RegisterOnLoadBanks(PopulateDropdowns);
        }

        public void RemoveSubscriptions()
        {
            UIBanksChooserSingleton.Instance.UnregisterOnLoadBanks(PopulateDropdowns);
        }

        public void RegisterOnRemove(Action<UITalentForm> onRemove)
        {
            if (onRemove == null)
            {
                return;
            }

            buttonRemove.onClick.AddListener(() => onRemove.Invoke(this));
        }

        public void ResetUI()
        {
            dropdownTalents.Clear();

            inputID.Clear();
            inputName.Clear();
            inputDescription.Clear();

            inputCostReductionPercent.Clear();
            inputCooldownReductionPercent.Clear();
            inputValueDealtIncreasePercent.Clear();
            inputRangeIncreasePercent.Clear();

            inputAdditionalProjectileCount.Clear();

            dropdownSFX.Clear();
            inputDelaySFX.Clear();
            dropdownVFX.Clear();
            inputDelayVFX.Clear();

            dropdownFXMode.Clear();
        }

        public void PopulateDropdowns()
        {
            UIDropdownsPopulatorSingleton.Instance.PopulateDropdownsWithBank(
                UIBanksChooserSingleton.Instance.GetBankTalent(),
                new TMP_Dropdown[] { dropdownTalents });

            UIDropdownsPopulatorSingleton.Instance.PopulateDropdownsWithBank(
                UIBanksChooserSingleton.Instance.GetBankSFX(),
                new TMP_Dropdown[] { dropdownSFX });

            UIDropdownsPopulatorSingleton.Instance.PopulateDropdownsWithBank(
                UIBanksChooserSingleton.Instance.GetBankVFX(),
                new TMP_Dropdown[] { dropdownVFX });
        }

        public Talent GetTalentFromUI()
        {
            var talent = new Talent();

            talent.basicInfo = new BasicInfo(
                inputID.text, inputName.text,
                inputDescription.text);

            talent.costReductionPercent = uint.Parse(
                inputCostReductionPercent.text.ToString());
            talent.cooldownReductionPercent = uint.Parse(
                inputCooldownReductionPercent.text.ToString());

            talent.valueDealtIncreasePercent = uint.Parse(
                inputValueDealtIncreasePercent.text.ToString());
            talent.rangeIncreasePercent = uint.Parse(
                inputRangeIncreasePercent.text.ToString());
            talent.additionalProjectileCount = uint.Parse(
                inputAdditionalProjectileCount.text.ToString());

            var fxSettings = new FXSettings(
                UIBanksChooserSingleton.Instance.GetBankSFX()
                    .GetSelectedItemFromDropdown(dropdownSFX),
                float.Parse(inputDelaySFX.text),
                UIBanksChooserSingleton.Instance.GetBankVFX()
                    .GetSelectedItemFromDropdown(dropdownVFX),
                float.Parse(inputDelayVFX.text));

            talent.fxOnUse = fxSettings;
            talent.fxMode = dropdownFXMode.GetEnumValue<FXMode>();

            return talent;
        }

        private void LoadTalent()
        {
            if (dropdownTalents.HasNoSelectedItemFromBank())
            {
                ResetUI();
                return;
            }

            var talent = UIBanksChooserSingleton.Instance
                .GetBankTalent().GetSelectedItemFromDropdown(dropdownTalents);

            var bankVFX = UIBanksChooserSingleton.Instance.GetBankVFX();
            var bankSFX = UIBanksChooserSingleton.Instance.GetBankSFX();

            inputID.text = talent.basicInfo.id;
            inputName.text = talent.basicInfo.name;
            inputDescription.text = talent.basicInfo.description;

            inputCostReductionPercent.text = talent.costReductionPercent.ToString();
            inputCooldownReductionPercent.text = talent.cooldownReductionPercent.ToString();
            inputValueDealtIncreasePercent.text = talent.valueDealtIncreasePercent.ToString();
            inputRangeIncreasePercent.text = talent.rangeIncreasePercent.ToString();

            inputAdditionalProjectileCount.text = talent.additionalProjectileCount.ToString();

            dropdownSFX.SetSelectedItemIndexFromBank(bankSFX.GetItemId(talent.fxOnUse.SFX));
            inputDelaySFX.text = talent.fxOnUse.delaySFX.ToString();
            dropdownVFX.SetSelectedItemIndexFromBank(bankVFX.GetItemId(talent.fxOnUse.VFX));
            inputDelayVFX.text = talent.fxOnUse.delayVFX.ToString();

            dropdownFXMode.SetToOption(talent.fxMode.ToString());
        }

        private void SaveTalent()
        {
            var talent = GetTalentFromUI();

            if (string.IsNullOrEmpty(talent.basicInfo.id))
            {
                UIPopupMessageSingleton.Instance.ShowMessage(
                    StringConstants.MESSAGE_MISSING_ID);
                return;
            }

            var bank = UIBanksChooserSingleton.Instance.GetBankTalent();
            UIBanksChooserSingleton.Instance.GetBankTalent().SaveItem(talent);

            UIDropdownsPopulatorSingleton.Instance.PopulateDropdownsWithBank(
                bank, new TMP_Dropdown[] { dropdownTalents });

            ResetUI();

            UIPopupMessageSingleton.Instance.ShowMessage(
                StringConstants.MESSAGE_SAVE_SUCCESS);
        }

    }

}