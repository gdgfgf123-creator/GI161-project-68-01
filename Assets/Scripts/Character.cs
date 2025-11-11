using UnityEngine;
using UnityEngine.UI;


public abstract class Character : MonoBehaviour
{
    [SerializeField] public Slider HP;
    private int health;
    protected bool alive = true;
    public int Health
    {
        get { return health; }
        set { health = Mathf.Max(0, value); }
    }

    protected Animator anim;
    protected Rigidbody2D rd;

    public void Intialize(int startHealth)
    {
        Health = startHealth;
        Debug.Log($"{name} initialized. Health: {Health}");

        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public virtual void TakeDamage(int damage)
    {
        if (!alive) return;
        Health -= damage;
        Debug.Log($"{name} took {damage} damage. Current Health: {Health}");

        if (Health > 0)
        {
            // เล่นอนิเมชัน Hurt
            if (anim != null) anim.SetTrigger("hurt");
        }
        else
        {
            // เล่นอนิเมชัน Die
            alive = false;
            if (anim != null) anim.SetTrigger("die");
            OnDeath();
        }
    }

    // เวลาจะ Destroy object ให้ delay ก่อน
    protected virtual void  OnDeath()
    {
        
        // ป้องกันเรียกซ้ำ
        this.enabled = false;
        if (HP != null) Destroy(HP.gameObject);
        Destroy(gameObject, 1.5f); // รออนิเมชัน die จบ
    }
}
