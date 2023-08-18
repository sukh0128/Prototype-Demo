using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Attack()
    {
        anim.SetTrigger("rangedAttack");
    }

    private void RangedAttack()
    {
        cooldownTimer = 0;
        fireballs[FindFireball()].transform.position = firepoint.position;
        fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    public override Enemy Clone()
    {
        return Instantiate(this);
    }
}