using UnityEngine;

public class Heal : Item
{
    public override void Use(player player)
    {
        if (player)
        {
            player.Heal(ItemValue);
        }
    }
}
