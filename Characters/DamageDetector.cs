using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DamageDetector : MonoBehaviour
{
    public static int KillCount = 0;

    CharacterControl control;
    [SerializeField]
    public float hp;

    [SerializeField]
    List<RuntimeAnimatorController> HitReactionList = new List<RuntimeAnimatorController>();

    public LevelLoader levelLoader;
    KillCount KillCountComponent;
    public AudioManager AudioManager;

    private void Awake()
    {
        control = GetComponent<CharacterControl>();
        AudioManager = FindObjectOfType<AudioManager>();
        if(control.aiController == null)
        {
            //Debug.Log("XX");
            //KillCountComponent = GetComponent<KillCount>();
            //Debug.Log(KillCountComponent);
        }

    }

    private void Update()
    {
        if (AttackManager.Instance.CurrentAttacks.Count > 0)
        {
            CheckAttack();
        }
    }

    private void CheckAttack()
    {
        foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
        {
            if (info == null)
            {
                continue;
            }

            if (!info.isRegisterd)
            {
                continue;
            }

            if (info.isFinished)
            {
                continue;
            }

            if (info.CurrentHits >= info.MaxHits)
            {
                continue;
            }

            if (info.Attacker == control)
            {
                continue;
            }

            if (info.MustFaceAttacker)
            {
                Vector3 vec = this.transform.position - info.Attacker.transform.position;
                if (vec.z * info.Attacker.transform.forward.z < 0f)
                {
                    continue;
                }
            }

            if (info.MustCollide)
            {
                if (IsCollided(info))
                {
                    TakeDamage(info);
                }
            }
            else
            {
                float dist = Vector3.SqrMagnitude(this.gameObject.transform.position - info.Attacker.transform.position);
                if (dist <= info.LethalRange)
                {
                    TakeDamage(info);
                }
            }
        }
    }

    private bool IsCollided(AttackInfo info)
    {
        foreach (TriggerDetector trigger in control.GetAllTriggers())
        {
            foreach (Collider collider in trigger.CollidingParts)
            {
                foreach (AttackPartType part in info.AttackParts)
                {
                    if(info.Attacker.GetAttackingPart(part) == collider.gameObject)
                    {
                        control.animationProgress.Attack = info.AttackAbility;
                        control.animationProgress.Attacker = info.Attacker;
                        control.animationProgress.DamagedTrigger = trigger;
                        control.animationProgress.AttackingPart = info.Attacker.GetAttackingPart(part);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public bool IsDead()
    {
        if(hp <= 0f)
        {
            return true;
        }
        return false;
    }

    private void TakeDamage(AttackInfo info)
    {
        if (IsDead())
        {
            return;
        }

        if (info.MustCollide)
        {
            System.Random random = new System.Random();

            int randomPunchSoundIndex = random.Next(0, AudioManager.sounds.Length);

            AudioManager.Play(AudioManager.sounds[randomPunchSoundIndex].name);

            if (info.AttackAbility.UseDeathParticles)
            {
                if(info.AttackAbility.ParticleType.ToString().Contains("VFX"))
                {
                    GameObject vfx = PoolManager.Instance.GetObject(info.AttackAbility.ParticleType);

                    vfx.transform.position = control.animationProgress.AttackingPart.transform.position;

                    vfx.SetActive(true);

                    if (info.Attacker.IsFacingForward())
                    {
                        vfx.transform.rotation = Quaternion.Euler(0f,0f,0f);
                    }
                    else
                    {
                        vfx.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    }
                }                    
            }
        }

        info.CurrentHits++;

        hp -= info.AttackAbility.Damage;

        if (info.Attacker != CharacterManager.Instance.GetPlayableCharacter() && this.gameObject.name.Equals("Ninja")) {
            CharacterManager.Instance.GetPlayableCharacter().GetComponent<DamageHealthBarLink>().healthBar.SetHealth(hp);
        }

        if (this.gameObject.name.Equals("Enemy_Boss"))
        {
            this.gameObject.GetComponent<DamageHealthBarLink>().healthBar.SetHealth(hp);
        }

        AttackManager.Instance.ForceDeregister(control);

        if(IsDead())
        {
            // Turn on Ragdoll
            control.animationProgress.RagdollTriggered = true;
            control.GetComponent<BoxCollider>().enabled = false;
            control.ledgeChecker.GetComponent<BoxCollider>().enabled = false;
            control.RIGID_BODY.useGravity = false;

            if (control.aiController != null)
            {
                KillCount++;
                // set kill count
                CharacterManager.Instance.GetPlayableCharacter().GetComponent<KillCount>().SetText(KillCount);

                control.gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);
                control.aiController.gameObject.SetActive(false);
                control.navMeshObstacle.enabled = false;

                // Drop Health Pack
                GameObject HealthPack = Instantiate(Resources.Load("Trigger_HealthPack", typeof(GameObject)) as GameObject);
                HealthPack.transform.position = new Vector3(control.transform.position.x + 0.5f, control.transform.position.y, control.transform.position.z + 0.5f);
            }
            else
            {
                KillCount = 0;
                CharacterManager.Instance.GetPlayableCharacter().GetComponent<KillCount>().SetText(KillCount);
                // restart level
                levelLoader.Load(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            System.Random random = new System.Random();
            // damage take animation
            control.SkinnedMeshAnimator.runtimeAnimatorController = null;
            control.SkinnedMeshAnimator.runtimeAnimatorController = HitReactionList[random.Next(0, HitReactionList.Count)];
        }            
    }

    public void TakeTotalDamage()
    {
        hp = 0f;
        CharacterManager.Instance.GetPlayableCharacter().GetComponent<DamageHealthBarLink>().healthBar.SetHealth(hp);
        // restart level
        levelLoader.Load(SceneManager.GetActiveScene().name);
    }
}
