using UnityEngine;

public class Weapon : MonoBehaviour
{
   #region Variables

   [SerializeField] protected SO_WeaponData weaponData;
   
   protected Animator baseAnimator;
   protected Animator WeaponAnimator;

   protected PlayerAttackState state;

   protected Core core;

   protected int attackCounter;

   protected float lastAttackTime;
   protected float resetComboTime = 1.5f;
   
   private static readonly int Attack = Animator.StringToHash("Attack");
   private static readonly int AttackCounter = Animator.StringToHash("AttackCounter");

   #endregion

   protected virtual void Awake()
   {
      baseAnimator = transform.Find("Base").GetComponent<Animator>();
      WeaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
      
      gameObject.SetActive(false);
   }
   public void InitializeWeapon(PlayerAttackState state,Core core)
   {
      this.state = state;
      this.core = core;
   }
   public virtual void EnterWeapon()
   {
      gameObject.SetActive(true);
      
      if (attackCounter >= weaponData.amountOfAttacks || Time.time > lastAttackTime + resetComboTime)
      {
         attackCounter = 0;
      }
      
      lastAttackTime = Time.time;
      
      baseAnimator.SetBool(Attack,true);
      WeaponAnimator.SetBool(Attack,true);
      
      baseAnimator.SetInteger(AttackCounter,attackCounter);
      WeaponAnimator.SetInteger(AttackCounter,attackCounter);
      
      
   }
   public virtual void ExitWeapon()
   {
      baseAnimator.SetBool(Attack,false);
      WeaponAnimator.SetBool(Attack,false);

      attackCounter++;
      gameObject.SetActive(false);
   }

   #region Animation Triggers

   public virtual void AnimationFinishTrigger()
   {
      state.AnimationFinishTrigger();
   }

   public virtual void AnimationStartMovementTrigger()
   {
      state.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
   }

   public virtual void AnimationStopMovementTrigger()
   {
      state.SetPlayerVelocity(0f);
   }

   public virtual void AnimationTurnOffFlipTrigger()
   {
      state.SetFlipCheck(false);
   }

   public virtual void AnimationTurnOnFlipTrigger()
   {
      state.SetFlipCheck(true);
   }

   public virtual void AnimationActionTrigger()
   {
      
   }
   
   #endregion
   
    
}
