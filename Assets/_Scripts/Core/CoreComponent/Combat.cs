using UnityEngine;

public class Combat : CoreComponent,Idamageble,IKnockbackable
{
    [SerializeField] private GameObject damageParticles;
    [SerializeField] private float maxknockbackTime = 0.2f;

    [SerializeField] private AudioClip hitSound;

    public bool IsDamageImmune { get; private set; }
    public bool IsKnockbackImmune { get; private set; }
    
    public bool isKnockbackActive;
    
    private float knockbackStartTime;
    
    public float LastDamageTime { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        IsDamageImmune = false;
        IsKnockbackImmune = false;
    }

    public override void LogicUpdate()
    {
        CheckKnockback();
    }
    public void Damage(float damage)
    {
        if(IsDamageImmune) return;
        LastDamageTime = Time.time;
        core.Stats.DecreaseHealth(damage);
        if(hitSound == null) Debug.Log("hit sound null");
        SoundManager.Instance.PlaySound(hitSound,transform);
        core.ParticleManager.StartParticlesWithRandomRotation(damageParticles);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        if(IsKnockbackImmune) return;
        core.Movement.SetVelocity(strength,angle,direction);
        core.Movement.CanSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if (isKnockbackActive && ((core.Movement.CurrentVelocity.y <= 0.01f && core.CollisionSenses.CheckIfGrounded()) || knockbackStartTime + maxknockbackTime <= Time.time))
        {
            isKnockbackActive = false;
            core.Movement.CanSetVelocity = true;
        }
    }

    public void SetDamageImmune(bool state)
    {
        IsDamageImmune = state;
    }
    public void SetKnockbackImmune(bool state)
    {
        IsKnockbackImmune = state;

    }
}
