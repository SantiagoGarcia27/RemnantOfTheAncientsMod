using System;
using Terraria;
using Terraria.ID;
using static FakePlayer_Setup;

public class FakePlayer_Consumables
{
    private readonly Player proxy;
    private readonly NPC npc;
    private readonly FakePlayerEquipmentList inventory;
    private int healTimer;

    public bool IsHealing => healTimer > 0;

    public FakePlayer_Consumables(ref Player proxy, NPC npc, FakePlayerEquipmentList inventory)
    {
        this.proxy = proxy;
        this.npc = npc;
        this.inventory = inventory;
    }

    public void HealingAI()
    {
        if (Main.netMode == NetmodeID.MultiplayerClient || proxy == null || npc == null || inventory?.healPotion?.item is not Item potion)
            return;

        if (healTimer == 0)
        {
            if (proxy.inventory[9].IsAir || proxy.inventory[9].stack <= 0)
                return;

            healTimer = Math.Max(1, potion.useAnimation);
            proxy.selectedItem = 9;
            proxy.itemAnimationMax = healTimer;
            proxy.itemTimeMax = Math.Max(1, potion.useTime);
            npc.netUpdate = true;
        }

        proxy.itemAnimation = healTimer;
        proxy.itemTime = Math.Min(healTimer, proxy.itemTimeMax);
        healTimer--;

        if (healTimer != 0)
            return;

        int healed = Math.Min(potion.healLife, npc.lifeMax - npc.life);
        if (healed > 0)
        {
            npc.life += healed;
            proxy.statLife = npc.life;
            npc.HealEffect(healed);
        }

        inventory.healPotion.stack = Math.Max(0, inventory.healPotion.stack - 1);
        npc.netUpdate = true;
    }
}
