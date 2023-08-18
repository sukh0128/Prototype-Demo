using UnityEngine;

public class MeleeEnemy : Enemy
{
    
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Attack()
    {
        anim.SetTrigger("meleeAttack");
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }

    public override Enemy Clone()
    {
        return Instantiate(this);
    }
}