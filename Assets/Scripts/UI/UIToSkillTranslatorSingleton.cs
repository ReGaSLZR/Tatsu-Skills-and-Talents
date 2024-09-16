namespace ReGaSLZR
{

    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIToSkillTranslatorSingleton : AbstractSingleton<UIToSkillTranslatorSingleton>
    {

        [SerializeField]
        private Button buttonReset;

        [SerializeField]
        private Button buttonSave;

        [SerializeField]
        private Button buttonLoad;

        [Space]

        [SerializeField]
        private TMP_Dropdown dropdownSkills;

        [SerializeField]
        private ScrollRect scroller;

        [Space]

        [SerializeField]
        private TMP_InputField inputID;

        [SerializeField]
        private TMP_InputField inputName;

        [SerializeField]
        private TMP_InputField inputDescription;

        [Space]

        [SerializeField]
        private TMP_InputField inputCost;

        [SerializeField]
        private TMP_Dropdown dropdownStatCost;

        [Space]

        [SerializeField]
        private TMP_InputField inputCooldown;

        [Space]

        [SerializeField]
        private TMP_Dropdown dropdownTargetMode;

        [SerializeField]
        private TMP_Dropdown dropdownStatAffected;

        [SerializeField]
        private TMP_Dropdown dropdownTargetEffect;

        [SerializeField]
        private TMP_InputField inputRange;

        [SerializeField]
        private TMP_InputField inputValueDealt;

        [Space]

        [SerializeField]
        private TMP_Dropdown dropdownProjectile;

        [SerializeField]
        private TMP_InputField inputSpawnCount;

        [SerializeField]
        private TMP_InputField inputSpawnInterval;

        [SerializeField]
        private TMP_Dropdown dropdownProjectileVFX;

        [Space]

        [SerializeField]
        private TMP_Dropdown dropdownSFX;

        [SerializeField]
        private TMP_InputField inputDelayToSFX;

        [SerializeField]
        private TMP_Dropdown dropdownVFX;

        [SerializeField]
        private TMP_InputField inputDelayToVFX;

        [Space]

        [SerializeField]
        private TMP_Dropdown dropdownAnimationState;

        protected override void Awake()
        {
            base.Awake();

            buttonReset.onClick.AddListener(ResetUI);
            buttonSave.onClick.AddListener(SaveSkill);
            buttonLoad.onClick.AddListener(LoadSkill);
            dropdownSkills.onValueChanged.AddListener(_ => LoadSkill());

            ResetUI();
        }

        private void Start()
        {
            scroller.normalizedPosition = new Vector2(0, 1);
        }

        private void LoadSkill()
        {
            if (dropdownSkills.HasNoSelectedItemFromBank())
            {
                ResetUI();
                return;
            }

            var skill = UIBanksChooserSingleton.Instance
                .GetBankSkill().GetSelectedItemFromDropdown(dropdownSkills);

            var bankProjectile = UIBanksChooserSingleton.Instance.GetBankProjectile();
            var bankVFX = UIBanksChooserSingleton.Instance.GetBankVFX();
            var bankSFX = UIBanksChooserSingleton.Instance.GetBankSFX();
            var bankAnimations = UIBanksChooserSingleton.Instance.GetBankAnimationState();

            inputID.text = skill.basicInfo.id;
            inputName.text = skill.basicInfo.name;
            inputDescription.text = skill.basicInfo.description;

            inputCost.text = skill.cost.cost.ToString();
            dropdownStatCost.SetToOption(skill.cost.stat.ToString());

            inputCooldown.text = skill.coolDownInSeconds.ToString();

            dropdownTargetMode.SetToOption(skill.statChangeSettings.targetSettings.mode.ToString());
            dropdownStatAffected.SetToOption(skill.statChangeSettings.targetSettings.stat.ToString());
            dropdownTargetEffect.SetToOption(skill.statChangeSettings.targetSettings.effect.ToString());
            inputRange.text = skill.statChangeSettings.targetSettings.range.ToString();
            inputValueDealt.text = skill.statChangeSettings.targetSettings.valueDealtToTarget.ToString();

            dropdownProjectile.SetSelectedItemIndexFromBank(
                bankProjectile.GetItemId(skill.statChangeSettings.projectileSettings.prefab));
            inputSpawnCount.text = skill.statChangeSettings.projectileSettings.spawnCount.ToString();
            inputSpawnInterval.text = skill.statChangeSettings.projectileSettings.spawnInterval.ToString();
            dropdownProjectileVFX.SetSelectedItemIndexFromBank(
                bankVFX.GetItemId(skill.statChangeSettings.projectileSettings.targetHitVFX));

            dropdownSFX.SetSelectedItemIndexFromBank(
                bankSFX.GetItemId(skill.fxOnUse.SFX));
            inputDelayToSFX.text = skill.fxOnUse.delaySFX.ToString();
            dropdownVFX.SetSelectedItemIndexFromBank(
                bankVFX.GetItemId(skill.fxOnUse.VFX));
            inputDelayToVFX.text = skill.fxOnUse.delayVFX.ToString();

            dropdownAnimationState.SetSelectedItemIndexFromBank(
                bankAnimations.GetItemId(skill.animationState));
        }

        private void SaveSkill()
        {
            var skill = GetSkillFromUIValues();

            if (string.IsNullOrEmpty(skill.basicInfo.id))
            {
                UIPopupMessageSingleton.Instance.ShowMessage(
                    StringConstants.MESSAGE_MISSING_ID);
                return;
            }

            var bank = UIBanksChooserSingleton.Instance.GetBankSkill();
            bank.SaveItem(skill);

            UIDropdownsPopulatorSingleton.Instance.PopulateDropdownsWithBank(
                bank, new TMP_Dropdown[] { dropdownSkills });

            UIPopupMessageSingleton.Instance.ShowMessage(
                StringConstants.MESSAGE_SAVE_SUCCESS);

            ResetUI();
        }

        public void ResetUI()
        {
            dropdownSkills.Clear();

            inputID.Clear();
            inputName.Clear();
            inputDescription.Clear();

            inputCost.Clear();
            dropdownStatCost.Clear();

            inputCooldown.Clear();

            dropdownTargetMode.Clear();
            dropdownStatAffected.Clear();
            dropdownTargetEffect.Clear();
            inputRange.Clear();
            inputValueDealt.Clear();

            dropdownProjectile.Clear();
            inputSpawnCount.Clear();
            inputSpawnInterval.Clear();
            dropdownProjectileVFX.Clear();

            dropdownSFX.Clear();
            inputDelayToSFX.Clear();
            dropdownProjectileVFX.Clear();
            inputDelayToVFX.Clear();

            dropdownAnimationState.Clear();
        }

        public Skill GetSkillFromUIValues()
        {
            var skill = new Skill();

            skill.basicInfo = new BasicInfo(
                inputID.text,
                inputName.text,
                inputDescription.text);

            skill.cost = new SkillCost(
                dropdownStatCost.GetEnumValue<TargetStat>(),
                uint.Parse(inputCost.text));

            skill.coolDownInSeconds = float.Parse(inputCooldown.text);

            var targetSettings = new TargetSettings(
                dropdownTargetMode.GetEnumValue<TargetMode>(),
                dropdownStatAffected.GetEnumValue<TargetStat>(),
                dropdownTargetEffect.GetEnumValue<TargetEffect>(),
                float.Parse(inputRange.text),
                uint.Parse(inputValueDealt.text));

            var projectileSettings = new ProjectileSettings(
                UIBanksChooserSingleton.Instance.GetBankProjectile()
                    .GetSelectedItemFromDropdown(dropdownProjectile),
                uint.Parse(inputSpawnCount.text),
                float.Parse(inputSpawnInterval.text),
                UIBanksChooserSingleton.Instance.GetBankVFX()
                    .GetSelectedItemFromDropdown(dropdownProjectileVFX));

            skill.statChangeSettings = new StatChangeSettings(
                targetSettings, projectileSettings);

            skill.fxOnUse = new FXSettings(
                UIBanksChooserSingleton.Instance.GetBankSFX()
                    .GetSelectedItemFromDropdown(dropdownSFX),
                float.Parse(inputDelayToSFX.text),
                UIBanksChooserSingleton.Instance.GetBankVFX()
                    .GetSelectedItemFromDropdown(dropdownVFX),
                float.Parse(inputDelayToVFX.text));

            skill.animationState = UIBanksChooserSingleton.Instance
                .GetBankAnimationState().GetSelectedItemFromDropdown(dropdownAnimationState);

            return skill;
        }

    }

}