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
			//DisplayName.SetDefault("SoulReset");
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
				if (player.GetModPlayer<ReaperSoulsPlayer>().AllSoulsAreActive())
				{
					SetSoulValue(player, false);
					Main.NewText("Souls desactivadas");
				}
				else
				{
					SetSoulValue(player, true);
					Main.NewText("Souls activadas");
				}
				Netcode.SyncWorld();
			}
			return true;
		}

		public void SetSoulValue(Player player, bool n)
		{
			for(int i = 0; i < player.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoaded.Length; i++)
			{
				player.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesLoaded[i] = n;
            }
			if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				foreach (KeyValuePair<int, bool> npc in player.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesMaybeLoaded)
				{
					player.GetModPlayer<ReaperSoulsPlayer>().SoulsUpgradesMaybeLoaded[npc.Key] = n;
				}
			}
		}

	}
}