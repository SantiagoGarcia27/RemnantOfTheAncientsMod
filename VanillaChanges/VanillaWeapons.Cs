using RemnantOfTheAncientsMod.World;
using RemnantOfTheAncientsMod;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.VanillaChanges
{
    public class VanillaWeapons : GlobalItem
	{
		public override void SetDefaults(Item item)
		{
			bool WeaponConf = ModContent.GetInstance<ConfigClient1>().VanillaWeaponsChangesConf;

			if (WeaponConf) {
				if (item.type == ItemID.DaedalusStormbow)
				{
					item.damage = 35;
					item.useTime = 24;
					item.useAnimation = 24;
				}
				if (item.type == ItemID.FetidBaghnakhs)
				{
					item.damage = 50;
					item.useTime = 11;
					item.useAnimation = 11;
					item.scale = 1.80f;
				}
				if (item.type == ItemID.HeatRay)
				{
					item.damage = 200;
					item.useTime = 58;
					item.useAnimation = 58;
				}
				if (item.type == ItemID.NightsEdge)
                {
                    if (!ModLoader.TryGetMod("CalamityMod", out Mod mod)) item.damage = 45;					
                }
                if (item.type == ItemID.DD2BallistraTowerT1Popper) item.damage = 45;
                if (item.type == ItemID.DD2BallistraTowerT2Popper) item.damage = 85;
                if (item.type == ItemID.DD2BallistraTowerT3Popper) item.damage = 200;
                if (item.type == ItemID.DD2FlameburstTowerT1Popper) item.damage = 30;
                if (item.type == ItemID.DD2FlameburstTowerT2Popper) item.damage = 60;
                if (item.type == ItemID.DD2FlameburstTowerT3Popper) item.damage = 100;
                if (item.type == ItemID.DD2ExplosiveTrapT1Popper) item.damage = 40;
                if (item.type == ItemID.DD2ExplosiveTrapT2Popper) item.damage = 80;
                if (item.type == ItemID.DD2ExplosiveTrapT3Popper) item.damage = 250;
                if (item.type == ItemID.DD2LightningAuraT1Popper) item.damage = 10;//30
                if (item.type == ItemID.DD2LightningAuraT2Popper) item.damage = 16;//50
                if (item.type == ItemID.DD2LightningAuraT3Popper) item.damage = 50;//150
            }
            else
            {

            }
		}
		public override void UpdateInventory(Item item, Player player)
        {
			if (Reaper.ReaperMode)
			{
				FchangesItem.ReaperConsumable(item);
				FchangesItem.ReaperSetDefaut(item);
			}

           // item.GetGlobalItem<FchangesItem>().ReaperSize(item);           
        }


		public override bool InstancePerEntity => true; //InstancePerEntity

		/*public override void OnCraft(Item item, Recipe recipe)
		{
			FchangesItem.ReaperSize(item);
		}*/

	}	
}