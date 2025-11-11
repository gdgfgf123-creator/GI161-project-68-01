using UnityEngine;

public class Ant : Enemy
{
    [SerializeField] Vector2 velocity; 
    public Transform[] movePoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Intialize(20);
        DamgeHit = 20;
        HP.maxValue = 20;
        velocity = new Vector2(-1.0f, 0.0f);
    }

    public override void Behavior()
    {
        rd.MovePosition(rd.position + velocity * Time.fixedDeltaTime);
        //move left และเกินขอบซ้าย
        if (velocity.x < 0 && rd.position.x <= movePoints[0].position.x)
        {
            Flip();
        }
        //move right และเกินขอบขวา
        if (velocity.x > 0 && rd.position.x >= movePoints[1].position.x)
        {
            Flip();
        }
    }
    public void Flip()
    {
        velocity.x *= -1; //change direction of movement
                          //Flip the image
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void FixedUpdate()
    {
        Behavior();
        HP.value = Health;

    }
    // Update is called once per frame
    void Update()
    {
            
    }
}
