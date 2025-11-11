using UnityEngine;
using UnityEngine.WSA;

public class Banana : Weapon
{
    [SerializeField]private float speed;

    public override void Move()
    {
        float newX = transform.position.x + speed * Time.fixedDeltaTime;
        float newY = transform.position.y;
        Vector2 newPosition = new Vector2(newX, newY);
        transform.position = newPosition;
    }

    public override void OnHItWith(Character character)
    {
        if (character is Enemy)
             character.TakeDamage(this.damge);
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 4.0f * GetShootDirection();
        damge = 30;
    }
    private void FixedUpdate()
    {
        Move();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
