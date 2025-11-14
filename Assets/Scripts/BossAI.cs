using UnityEngine;

public class Boss2D : Enemy, IShootable
{
    [SerializeField] float atkRange;
    public player player;

    [field: SerializeField] public GameObject Bullet { get; set; }
    [field: SerializeField] public Transform ShootPoint { get; set; }
    public float ReloadTime { get; set; }
    public float WaitTime { get; set; }
    void Start()
    {
        HP.maxValue = 50;
        base.Intialize(50);
        DamgeHit = 30;
        //set atk range and target
        atkRange = 6.0f;
        player = GameObject.FindFirstObjectByType<player>();
        ReloadTime = 1f;
    }
    private void FixedUpdate()
    {
        WaitTime += Time.fixedDeltaTime;
        Behavior();
        HP.value = Health;
    }
    public override void Behavior()
    {
        //find distance between Croccodile and Player
        Vector2 distance = transform.position - player.transform.position;
        if (distance.magnitude <= atkRange)
        {

            Shoot();
        }
    }
    public void Shoot()
    {
        if (WaitTime >= ReloadTime)
        {
            anim.SetTrigger("Shoot");
            var bullet = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
            Banana banana = bullet.GetComponent<Banana>();
            banana.InitWeapon(30, this);
            WaitTime = 0;
        }

    }
}
