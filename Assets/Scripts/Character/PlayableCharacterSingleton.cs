using UnityEngine;

public class PlayableCharacterSingleton : AbstractSingleton<PlayableCharacterSingleton>
{

    //[SerializeField] //marked readonly for Inspector debugging //TODO ren improve this
    //private Skill testSkill;

    //[SerializeField] //marked readonly for Inspector debugging //TODO ren improve this
    //private Talent[] testTalents;

    [SerializeField]
    private CharacterStatsHolder statsHolder;
    public CharacterStatsHolder StatsHolder => statsHolder;

    [SerializeField]
    private CharacterSkillUser skillUser;
    public CharacterSkillUser SkillUser => skillUser;

    protected override void Awake()
    {
        base.Awake();

        UISkillPreviewButtonSingleton.Instance.RegisterOnClickListener(OnUsePreviewSkill);
    }

    private void OnUsePreviewSkill()
    {
        Debug.Log($"{GetType().Name}.OnUsePreviewSkill()");

        var talents = UIToTalentTranslatorSingleton.Instance.GetTalentsFromUIValue();
        var skill = UIToSkillTranslatorSingleton.Instance.GetSkillFromUIValues();
        skill.ModifyWithTalents(talents);

        if (!skillUser.CanUseSkill(skill, statsHolder.Stats))
        {
            return;
        }

        UISkillPreviewButtonSingleton.Instance.ApplyCooldown(skill.coolDownInSeconds);
        skillUser.UseSkill(skill, statsHolder.Stats);
    }

}