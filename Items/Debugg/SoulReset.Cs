using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Items.Fmode;

namespace RemnantOfTheAncientsMod.Items.Debugg
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
				if (player.GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade == true)
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
			player.GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade = n;
			player.GetModPlayer<EyeReaperSoulPlayer>().EyeeReaperUpgrade = n;
			player.GetModPlayer<CorruptReaperSoulPlayer>().CorruptReaperUpgrade = n;
			player.GetModPlayer<SkeletonReaperSoulPlayer>().SkeletonReaperUpgrade = n;
			player.GetModPlayer<BeeReaperSoulPlayer>().BeeReaperUpgrade = n;
			player.GetModPlayer<DeerclopsReaperSoulPlayer>().DeerclopsReaperUpgrade = n;
			player.GetModPlayer<DesertReaperSoulPlayer>().DesertReaperUpgrade = n;
			player.GetModPlayer<FleshReaperSoulPlayer>().FleshReaperUpgrade = n;
			player.GetModPlayer<FrozenReaperSoulPlayer>().FrozenReaperUpgrade = n;
			player.GetModPlayer<QueenReaperSoulPlayer>().QueenReaperUpgrade = n;
			player.GetModPlayer<DestroyerReaperSoulPlayer>().DestroyerReaperUpgrade = n;
			player.GetModPlayer<RetinazorReaperSoulPlayer>().RetinazorReaperUpgrade = n;
			player.GetModPlayer<SpazmatismReaperSoulPlayer>().SpazmatismReaperUpgrade = n;
			player.GetModPlayer<SkeletronPrimeReaperSoulPlayer>().SkeletronPrimeReaperUpgrade = n;
			player.GetModPlayer<PlantReaperSoulPlayer>().PlantReaperUpgrade = n;
			player.GetModPlayer<EmpressReaperSoulPlayer>().EmpressReaperUpgrade = n;
			player.GetModPlayer<InfernalReaperSoulPlayer>().InfernalReaperUpgrade = n;
			player.GetModPlayer<GolemReaperSoulPlayer>().GolemReaperUpgrade = n;
			player.GetModPlayer<DukeReaperSoulPlayer>().DukeReaperUpgrade = n;
			player.GetModPlayer<CultistReaperSoulPlayer>().CultistReaperUpgrade = n;
			player.GetModPlayer<MoonReaperSoulPlayer>().MoonReaperUpgrade = n;

		}

	}
}