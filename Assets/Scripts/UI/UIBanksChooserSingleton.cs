using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIBanksChooserSingleton : AbstractSingleton<UIBanksChooserSingleton>
{

    [SerializeField]
    private UIBankChooserRow rowBankSkill;

    [SerializeField]
    private UIBankChooserRow rowBankTalent;

    [SerializeField]
    private UIBankChooserRow rowBankAnimationState;

    [SerializeField]
    private UIBankChooserRow rowBankProjectile;

    [SerializeField]
    private UIBankChooserRow rowBankSFX;

    [SerializeField]
    private UIBankChooserRow rowBankVFX;

    [Space]

    [SerializeField]
    private Button buttonOpen;

    [SerializeField]
    private Button buttonClose;

    [SerializeField]
    private Button buttonLoadBanks;

    [Space]

    [SerializeField]
    private GameObject rootUI;

    //TODO ren
    //private SOAnimationStateBank cachedBankAnimation;
    //private SOProjectileBank cachedBankProjectile;
    //private SOSkillBank cachedBankSkill;
    //private SOSoundFXBank cachedBankSFX;
    //private SOTalentBank cachedBankTalent;
    //private SOVisualFXBank cachedBankVFX;

    protected override void Awake()
    {
        base.Awake();

        buttonClose.onClick.AddListener(() => rootUI.SetActive(false));
        buttonOpen.onClick.AddListener(() => rootUI.SetActive(true));
        buttonLoadBanks.onClick.AddListener(buttonClose.onClick.Invoke);
    }

    private IEnumerator Start()
    {
        rowBankSkill.ConfigureDefaultSettings();
        rowBankTalent.ConfigureDefaultSettings();

        rowBankAnimationState.ConfigureDefaultSettings();
        rowBankProjectile.ConfigureDefaultSettings();
        rowBankSFX.ConfigureDefaultSettings();
        rowBankVFX.ConfigureDefaultSettings();

        yield return new WaitForEndOfFrame();

        buttonLoadBanks.onClick.Invoke();
    }

    public void RegisterOnLoadBanks(Action onLoadBanks)
    {
        if (onLoadBanks == null)
        {
            return;
        }

        buttonLoadBanks.onClick.AddListener(onLoadBanks.Invoke);
    }

    public void UnregisterOnLoadBanks(Action onLoadBanks)
    {
        if (onLoadBanks == null)
        {
            return;
        }

        buttonLoadBanks.onClick.RemoveListener(onLoadBanks.Invoke);
    }

    private T GetScriptableBank<T>(string path) where T : ScriptableObject
    {
        //Debug.Log($"{GetType().Name}.full path is {path}");

        if (string.IsNullOrEmpty(path))
        {
            Debug.LogWarning($"{GetType().Name}.Empty path for bank type: {typeof(T).Name}");
            return default;
        }

        var simpleFilePath = path.Split(StringConstants.RESOURCES_CONCATINATOR)[1]
            .Replace(StringConstants.SCRIPTABLEOBJECT_EXTENSION, string.Empty);

        //TODO ren: cache results later
        var bank = Resources.Load<T>(simpleFilePath);
        //Debug.Log($"{GetType().Name}.bank retrieved is {bank} for simple path {simpleFilePath}");

        if (bank == null)
        {
            Debug.LogWarning($"{GetType().Name}.cannot parse bank type {typeof(T).Name} from path '{path}'");
            return default;
        }

        return bank;
    }

    public AnimationStateBank GetBankAnimationState() => 
        GetScriptableBank<AnimationStateBank>(rowBankAnimationState.GetPathChosenBank());
    public ProjectileBank GetBankProjectile() =>
        GetScriptableBank<ProjectileBank>(rowBankProjectile.GetPathChosenBank());
    public SkillBank GetBankSkill() => 
        GetScriptableBank<SkillBank>(rowBankSkill.GetPathChosenBank());
    public SoundFXBank GetBankSFX() => 
        GetScriptableBank<SoundFXBank>(rowBankSFX.GetPathChosenBank());
    public TalentBank GetBankTalent() => 
        GetScriptableBank<TalentBank>(rowBankTalent.GetPathChosenBank());
    public VisualFXBank GetBankVFX() => 
        GetScriptableBank<VisualFXBank>(rowBankVFX.GetPathChosenBank());

}