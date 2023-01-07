using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.VanillaChanges;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod;
using RemnantOfTheAncientsMod.Items.Fmode;

namespace RemnantOfTheAncientsMod.Items.Debugg
{
	public class TestSword : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModLoader.TryGetMod("OpswordsIIDebugMod", out mod);
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("TestSword");
		}
		public override void SetDefaults()
		{
			Item.damage = 1;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
			Item.value = Item.sellPrice(silver: 27);
			Item.rare = ItemRarityID.White;
            Item.accessory = true;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
		}
        public override void UpdateAccessory(Player player, bool hideVisual) {
			Main.NewText(player.GetModPlayer<Player1>().ChaliceOn);

		}
}
}