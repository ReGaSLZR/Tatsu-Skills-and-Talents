namespace ReGaSLZR
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class CharacterSkillUser : MonoBehaviour
    {

        [Header("UI Elements")]

        [SerializeField]
        private Slider sliderHealth;

        [SerializeField]
        private Slider sliderMana;

        [Header("General Elements")]

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Transform projectileSpawnPoint;

        [SerializeField]
        private Transform vfxSpawnPoint;

        [Space]

        [SerializeField]
        private float delayOnVFXKill;

        [SerializeField]
        private float delayOnProjectileKill;

        public bool CanUseSkill(Skill skill, CharacterStats stats)
        {
            switch (skill.cost.stat)
            {
                case TargetStat.Mana:
                    {
                        return stats.mana >= skill.cost.cost;
                    }
                case TargetStat.Health:
                    {
                        return stats.health >= skill.cost.cost;
                    }
                default:
                    {
                        Debug.LogWarning($"{GetType().Name} " +
                            $"cannot apply cost to stat {skill.cost.stat}");
                        return false;
                    }
            }
        }

        public void UseSkill(Skill skill, CharacterStats stats)
        {
            ApplySkillCostToSelf(skill.cost, stats);
            ApplyStatChangesFromSkill(skill.statChangeSettings, stats);
            ApplyFXSettingsFromSkill(skill.fxOnUse, skill.AdditionalFXSettings);

            if (!StringConstants.DROPDOWN_UNSET.Equals(
                skill.animationState))
            {
                animator.Play(skill.animationState);
            }
        }

        private void ApplyFXSettingsFromSkill(
            FXSettings fxOnUse, List<FXSettings> additionalFXs)
        {
            ApplyFX(fxOnUse);

            foreach (var fx in additionalFXs)
            {
                if (fx == null)
                {
                    continue;
                }
                ApplyFX(fx);
            }
        }

        private void ApplyFX(FXSettings fx)
        {
            AudioManagerSingleton.Instance.Play(
                fx.SFX, fx.delaySFX, true);
            StartCoroutine(CoroutinePlayVFX(fx));
        }

        private void ApplyStatChangesFromSkill(
            StatChangeSettings settings, CharacterStats ownStats)
        {
            if (settings == null)
            {
                return;
            }

            switch (settings.targetSettings.mode)
            {
                case TargetMode.Self:
                    {
                        ApplyStatChangeToSelf(settings, ownStats);
                        break;
                    }
                case TargetMode.Single:
                    {
                        ApplyStatChangeToSingleEnemy(settings);
                        break;
                    }
                case TargetMode.Multi:
                    {
                        ApplyStateChangeToMultiEnemies(settings);
                        break;
                    }
                default:
                    {
                        Debug.LogWarning($"{GetType().Name} cannot apply " +
                            $"stat changes to mode {settings.targetSettings.mode}");
                        break;
                    }
            }
        }

        private void ApplyStatChangeToSingleEnemy(StatChangeSettings settings)
        {
            var hasHit = Physics.Raycast(transform.position,
                spriteRenderer.flipX ? Vector3.left : Vector3.right,
                out RaycastHit hit,
                settings.targetSettings.range);

            Debug.Log($"{GetType().Name}.Single-Hit target " +
                $"has hit? {hasHit}", gameObject);

            if (hasHit)
            {
                ApplyStateChangeToTargetHit(hit, settings);
            }
        }

        private void ApplyStateChangeToMultiEnemies(StatChangeSettings settings)
        {
            var rays = Physics.SphereCastAll(
                transform.position, settings.targetSettings.range, Vector3.one);

            Debug.Log($"{GetType().Name}.Multi-Hit target " +
                $"detected count {rays.Length}", gameObject);

            foreach (var ray in rays)
            {
                ApplyStateChangeToTargetHit(ray, settings);
            }
        }

        private void ApplyStateChangeToTargetHit(
            RaycastHit hitTarget, StatChangeSettings settings)
        {
            if (hitTarget.collider.CompareTag(Tag.Enemy.ToString()) &&
                hitTarget.collider.gameObject.TryGetComponent(
                    out GameCharacter character))
            {
                Debug.Log($"{GetType().Name}.Applying Stat change to " +
                    $"{hitTarget.collider.gameObject.name}", hitTarget.collider.gameObject);

                character.ApplyStatChange(
                    settings.targetSettings.effect, settings.targetSettings.stat,
                    (int)settings.targetSettings.valueDealtToTarget);
            }
        }

        private void ApplyStatChangeToSelf(
            StatChangeSettings settings, CharacterStats ownStats)
        {
            var valueDealt = (int)settings.targetSettings.valueDealtToTarget;

            switch (settings.targetSettings.stat)
            {
                case TargetStat.Health:
                    {
                        if (TargetEffect.Damage == settings.targetSettings.effect)
                        {
                            ownStats.health = CharacterStats.GetClampedStatValue(
                                (int)ownStats.health - valueDealt);
                        }
                        else if (TargetEffect.Heal == settings.targetSettings.effect)
                        {
                            ownStats.health = CharacterStats.GetClampedStatValue(
                                (int)ownStats.health + valueDealt);
                        }

                        break;
                    }
                case TargetStat.Mana:
                    {
                        if (TargetEffect.Damage == settings.targetSettings.effect)
                        {
                            ownStats.mana = CharacterStats.GetClampedStatValue(
                                (int)ownStats.mana - valueDealt);
                        }
                        else if (TargetEffect.Heal == settings.targetSettings.effect)
                        {
                            ownStats.mana = CharacterStats.GetClampedStatValue(
                                (int)ownStats.mana + valueDealt);
                        }

                        break;
                    }
                default:
                    {
                        Debug.LogWarning($"{GetType().Name} cannot apply stat changes " +
                            $"to stat {settings.targetSettings.stat}");
                        break;
                    }
            }

            sliderHealth.value = ownStats.health;
            sliderMana.value = ownStats.mana;
        }

        private void ApplySkillCostToSelf(
            SkillCost skillCost, CharacterStats stats)
        {
            if (skillCost == null)
            {
                return;
            }

            switch (skillCost.stat)
            {
                case TargetStat.Mana:
                    {
                        stats.mana = CharacterStats.GetClampedStatValue(
                            (int)stats.mana - (int)skillCost.cost);
                        break;
                    }
                case TargetStat.Health:
                    {
                        stats.health = CharacterStats.GetClampedStatValue(
                            (int)stats.health - (int)skillCost.cost);
                        break;
                    }
                default:
                    {
                        Debug.LogWarning($"{GetType().Name} cannot apply " +
                            $"cost to stat {skillCost.stat}");
                        break;
                    }
            }

            sliderHealth.value = stats.health;
            sliderMana.value = stats.mana;
        }

        private IEnumerator CoroutinePlayVFX(FXSettings fXSettings)
        {
            if (fXSettings == null || fXSettings.VFX == null)
            {
                yield break;
            }

            yield return new WaitForSeconds(fXSettings.delayVFX);
            var vfx = Instantiate(fXSettings.VFX, vfxSpawnPoint);

            yield return new WaitForSeconds
                (delayOnVFXKill);
            Debug.Log($"{GetType().Name}.Now Destroying VFX...");
            Destroy(vfx);
        }

    }

}