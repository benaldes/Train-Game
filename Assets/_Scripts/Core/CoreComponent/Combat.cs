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

    private Movement movement;
    private CollisionSenses collisionSenses;
    private Stats stats;
    private ParticleManager particleManager;
    

    protected override void Awake()
    {
        base.Awake();
        IsDamageImmune = false;
        IsKnockbackImmune = false;
    }

    public override void InitializeCoreComponent()
    {
        base.InitializeCoreComponent();
        movement = core.GetCoreComponent(typeof(Movement)) as Movement;
        collisionSenses = core.GetCoreComponent(typeof(CollisionSenses)) as CollisionSenses;
        particleManager = core.GetCoreComponent(typeof(ParticleManager)) as ParticleManager;
        stats = core.GetCoreComponent(typeof(Stats)) as Stats;
    }

    public override void LogicUpdate()
    {
        CheckKnockback();
    }
    public void Damage(float damage)
    {
        if(IsDamageImmune) return;
        LastDamageTime = Time.time;
        stats.DecreaseHealth(damage);
        if(hitSound == null) Debug.Log("hit sound null");
        SoundManager.Instance.PlaySound(hitSound,transform);
        particleManager.StartParticlesWithRandomRotation(damageParticles);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        if(IsKnockbackImmune) return;
        movement.SetVelocity(strength,angle,direction);
        movement.CanSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if (isKnockbackActive && ((movement.CurrentVelocity.y <= 0.01f && collisionSenses.CheckIfGrounded()) || knockbackStartTime + maxknockbackTime <= Time.time))
        {
            isKnockbackActive = false;
            movement.CanSetVelocity = true;
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
