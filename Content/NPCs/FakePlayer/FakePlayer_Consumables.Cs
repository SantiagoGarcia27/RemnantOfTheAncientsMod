
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.ID;
using static FakePlayer_Setup;

public class FakePlayer_Consumables
{
    Player proxy;
    NPC npc;
    FakePlayerEquipmentList inventory;
    int healTimmer = 0;
    int healTimmerMax = Utils1.FormatTimeToTick(Second:0.1f);

    bool isHealing = false;
    public FakePlayer_Consumables(ref Player proxy,NPC npc, FakePlayerEquipmentList inventory)
    {
        this.proxy = proxy;
        this.npc = npc;
        this.inventory = inventory;
    }

    public void HealingAI()
    {
        if (proxy == null || npc == null || inventory == null) return;

        if(inventory.healPotion.item.type != ItemID.None)
        {
            npc.velocity.X = npc.Center.X - Main.player[npc.target].Center.X;
            npc.velocity.Normalize();

            int lifeLosed = proxy.statLifeMax2 - proxy.statLife;
            if (lifeLosed > inventory.healPotion.item.healLife)
            {
                Item potion = proxy.inventory[9];
                proxy.selectedItem = 9;

                proxy.itemAnimationMax = inventory.healPotion.item.useAnimation;     
                proxy.itemTimeMax = inventory.healPotion.item.useTime;
                //proxy.itemAnimation = inventory.healPotion.item.useAnimation;
                if (healTimmer++ >= healTimmerMax)
                {
                    isHealing = false;
                    if (proxy.itemTime <= 0)
                        proxy.itemTime = potion.useTime;

                    if (proxy.itemAnimation <= 0)
                        proxy.itemAnimation = potion.useAnimation;

                    healTimmer = 0;
                }
                
                proxy.ItemCheck();
                if (!isHealing && proxy.itemAnimation == 0)
                {
                    isHealing = true;
                    proxy.inventory[9].stack--;
                    proxy.statLife += potion.healLife;
                    proxy.HealEffect(potion.healLife, broadcast: true);
                }
                npc.life = proxy.statLife;

            }
        }
    }

   
}