using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Debugg
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
        public override bool? UseItem(Player player)
        {
            Main.NewText(player.maxMinions);
            return base.UseItem(player);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			
				
			
		}
	}
}