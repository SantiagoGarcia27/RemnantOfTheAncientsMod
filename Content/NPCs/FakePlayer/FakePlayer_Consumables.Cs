using CalamityMod.Items.Accessories;
using Terraria;
using Terraria.ID;
using static FakePlayer_Setup;

public class FakePlayer_Consumables
{
    Player proxy;
    NPC npc;
    FakePlayerEquipmentList inventory;
    public FakePlayer_Consumables(Player proxy, NPC npc, FakePlayerEquipmentList inventory)
    {
        this.proxy = proxy;
        this.npc = npc;
        this.inventory = inventory;
    }

    public void AI()
    {
        if (proxy == null || npc == null || inventory == null) return;

        if(inventory.healPotion.item.type != ItemID.None)
        {
            if(proxy.statLifeMax2 - proxy.statLife < inventory.healPotion.item.healLife)
            {
                proxy.selectedItem = 9;
                proxy.ItemCheck();
                npc.life = proxy.statLife;
                
            }
        }
    }

   
}