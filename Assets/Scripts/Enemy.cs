using UnityEngine;

public abstract class Enemy : Character
{
    public int DamgeHit { get; protected set; }

    public abstract void Behavior(); //method signature


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
