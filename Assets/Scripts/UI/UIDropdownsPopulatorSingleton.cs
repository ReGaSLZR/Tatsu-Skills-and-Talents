namespace ReGaSLZR
{

    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class UIDropdownsPopulatorSingleton : AbstractSingleton<UIDropdownsPopulatorSingleton>
    {

        [Header("UI elements")]

        [SerializeField]
        private TMP_Dropdown[] dropdownsSkill;

        [SerializeField]
        private TMP_Dropdown[] dropdownsTalent;

        [Space]

        [SerializeField]
        private TMP_Dropdown[] dropdownsAnimationState;

        [SerializeField]
        private TMP_Dropdown[] dropdownsProjectile;

        [SerializeField]
        private TMP_Dropdown[] dropdownsSFX;

        [SerializeField]
        private TMP_Dropdown[] dropdownsVFX;

        private void Start()
        {
            UIBanksChooserSingleton.Instance.RegisterOnLoadBanks(OnLoadBanks);
        }

        private void OnLoadBanks()
        {
            PopulateDropdownsWithBank(UIBanksChooserSingleton.Instance.GetBankSkill(), dropdownsSkill);
            PopulateDropdownsWithBank(UIBanksChooserSingleton.Instance.GetBankTalent(), dropdownsTalent);

            PopulateDropdownsWithBank(UIBanksChooserSingleton.Instance.GetBankAnimationState(), dropdownsAnimationState);
            PopulateDropdownsWithBank(UIBanksChooserSingleton.Instance.GetBankProjectile(), dropdownsProjectile);
            PopulateDropdownsWithBank(UIBanksChooserSingleton.Instance.GetBankSFX(), dropdownsSFX);
            PopulateDropdownsWithBank(UIBanksChooserSingleton.Instance.GetBankVFX(), dropdownsVFX);
        }

        public void PopulateDropdownsWithBank<T>(AbstractBank<T> bank, TMP_Dropdown[] dropdowns) where T : class
        {
            var optionData = new List<TMP_Dropdown.OptionData>();
            optionData.Add(new TMP_Dropdown.OptionData(StringConstants.DROPDOWN_UNSET));

            if (bank == null || bank == default)
            {
                Debug.LogWarning($"{GetType().Name}.Empty or wrong bank");
            }
            else
            {
                //Debug.LogWarning($"{GetType().Name}.Item count on bank: {bank.Items.Count}");
                for (int x = 0; x < bank.Items.Count; x++)
                {
                    var item = bank.Items[x];

                    if (item == null)
                    {
                        continue;
                    }

                    var optionName = string.Concat(
                        x, StringConstants.DROPDOWN_BANK_ITEM_CONCATINATOR, bank.GetItemId(item));
                    optionData.Add(new TMP_Dropdown.OptionData(optionName));
                }
            }

            foreach (var dropdown in dropdowns)
            {
                if (dropdown == null)
                {
                    continue;
                }

                dropdown.ClearOptions();
                dropdown.options = optionData;
            }
        }

    }

}