using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls;
using System.Collections.Generic;

namespace RemnantOfTheAncientsMod.Content.Items.Debugg
{
	public class SoulReset : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModLoader.TryGetMod("OpswordsIIDebugMod", out mod);
		}
		public override void SetStaticDefaults()
		{
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 4));//5,4
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
			ItemID.Sets.ItemIconPulse[Item.type] = true;
			ItemID.Sets.ItemNoGravity[Item.type] = true;
			DisplayName.SetDefault("SoulReset");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.Pink;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item60;

		}
		public override bool? UseItem(Player player)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				if (player.GetModPlayer<ReaperPlayer>().AllSoulsAreActive())
				{
					efects(player, false);
					Main.NewText("Souls desactivadas");
				}
				else
				{
					efects(player, true);
					Main.NewText("Souls activadas");
				}
				Netcode.SyncWorld();
			}
			return true;
		}

		public void efects(Player player, bool n)
		{
			List<bool> a = player.GetModPlayer<ReaperPlayer>().SoulsUpgrades;
            for (int i = 0; i < a.Count; i++)
			{
				player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[i] = n;
            }

			//Main.NewText(player.GetModPlayer<EyeReaperSoulPlayer>().EyeeReaperUpgrade);
		}

	}
}