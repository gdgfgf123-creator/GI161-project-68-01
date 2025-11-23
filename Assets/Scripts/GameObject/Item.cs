using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [field: SerializeField]protected int ItemValue { get; set; }
    public abstract void Use(player player);
    public void PickUp(player player)
    {
        Use(player);
        Destroy(this.gameObject);
    }

}
