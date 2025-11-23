using UnityEngine;

public class Coin : Item
{
    public override void Use(player player)
    {
        if (player)
        {
            player.AddCoin(ItemValue);
        }
    }
}
