using UnityEngine;

public class Rock : Weapon
{
    public Rigidbody2D rb;
    public Vector2 force;
    public override void Move()
    {
        rb.AddForce(force);
    }

    public override void OnHItWith(Character obj)
    {
        if (obj is player)
            obj.TakeDamage(this.damge);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damge = 40;
        force = new Vector2(GetShootDirection() * 90, 400);
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
