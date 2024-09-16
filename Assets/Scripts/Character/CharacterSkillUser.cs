namespace ReGaSLZR
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CharacterSkillUser : MonoBehaviour
    {

        [SerializeField]
        private GameCharacter character;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Transform projectileSpawnPoint;

        [SerializeField]
        private Transform vfxSpawnPoint;

        [Header("Settings")]

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
            character.ApplyStatChange(
                TargetEffect.Damage, skill.cost.stat, (int)skill.cost.cost);

            StartCoroutine(CoroutineApplyStatChangesFromSkill(
                skill.statChangeSettings, stats));
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

        private IEnumerator CoroutineApplyStatChangesFromSkill(
            StatChangeSettings settings, CharacterStats ownStats)
        {
            if (settings == null)
            {
                yield break;
            }

            yield return new WaitForSeconds(settings.targetSettings.delayDealValue);

            switch (settings.targetSettings.mode)
            {
                case TargetMode.Self:
                    {
                        character.ApplyStatChange(
                            settings.targetSettings.effect,
                            settings.targetSettings.stat, 
                            (int)settings.targetSettings.valueDealtToTarget);
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