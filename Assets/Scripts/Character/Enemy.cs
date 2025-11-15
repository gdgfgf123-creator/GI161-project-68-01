using UnityEngine;

public abstract class Enemy : Character
{
    public int DamgeHit { get; protected set; }

    public abstract void Behavior(); //method signature

}
