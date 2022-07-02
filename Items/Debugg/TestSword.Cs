using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.VanillaChanges;
using static Terraria.ModLoader.ModContent;
using opswordsII;

namespace opswordsII.Items.Debugg
{
	public class TestSword : ModItem
	{
		/*public override bool Autoload(ref string name)
		{
		return ModLoader.GetMod("OpswordsIIDebugMod") != null;
		}*/
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
			Player1 p = player.GetModPlayer<Player1>();
			//world1 w = world.GetModWorld<world1>();
			//world1.Fmode = false;

            if (Utils1.IsAnyBossAlive()) {
                Item.damage = 272727;
                Item.rare = 12;
				Main.NewText("Boss");
            }
           /* else {
                Item.rare = ItemRarityID.Orange; 
				Item.damage = 1;
				Main.NewText("No Boss");
            }*/

    }
}
}