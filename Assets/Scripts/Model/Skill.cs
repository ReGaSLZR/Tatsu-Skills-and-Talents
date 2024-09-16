using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{

    public BasicInfo basicInfo;
    public SkillCost cost;

    public float coolDownInSeconds;

    //TODO ren use this
    public MobilitySettings mobilitySettings;

    public StatChangeSettings statChangeSettings;

    public FXSettings fxOnUse;
    public string animationState;

    private List<FXSettings> additionalFXSettings 
        = new List<FXSettings>();
    public List<FXSettings> AdditionalFXSettings => additionalFXSettings;

    public void ModifyWithTalents(Talent[] talents)
    {
        foreach (var talent in talents)
        {
            if (talent == null)
            {
                continue;
            }

            cost.cost -= (uint)((int)cost.cost * 
                ((float)talent.costReductionPercent / 
                NumberConstants.WHOLE_NUMBER_PERCENT_DIVISOR));

            coolDownInSeconds -= coolDownInSeconds *
                ((float)talent.cooldownReductionPercent /
                NumberConstants.WHOLE_NUMBER_PERCENT_DIVISOR);

            statChangeSettings.targetSettings.valueDealtToTarget += 
                (uint)((int)statChangeSettings.targetSettings.valueDealtToTarget *
                ((float)talent.valueDealtIncreasePercent /
                NumberConstants.WHOLE_NUMBER_PERCENT_DIVISOR));

            statChangeSettings.projectileSettings.spawnCount += 
                talent.additionalProjectileCount;

            statChangeSettings.targetSettings.range += 
                (uint)((int)statChangeSettings.targetSettings.range *
                ((float)talent.rangeIncreasePercent /
                NumberConstants.WHOLE_NUMBER_PERCENT_DIVISOR));

            switch(talent.fxMode)
            {
                case FXMode.Override:
                    {
                        if (talent.fxOnUse.SFX != null)
                        {
                            fxOnUse.SFX = talent.fxOnUse.SFX;
                            fxOnUse.delaySFX = talent.fxOnUse.delaySFX;
                        }
                        if (talent.fxOnUse.VFX != null)
                        {
                            fxOnUse.VFX = talent.fxOnUse.VFX;
                            fxOnUse.delayVFX = talent.fxOnUse.delayVFX;
                        }

                        break;
                    }
                    case FXMode.Additional:
                    {
                        additionalFXSettings.Add(talent.fxOnUse);
                        break;
                    }
                default:
                    {
                        Debug.Log($"{GetType().Name} Cannot use " +
                            $"talent FX mode {talent.fxMode}");
                        break;
                    }
            }
        }
    }
	
}