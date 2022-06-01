using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using opswordsII;

namespace opswordsII.Items.Debugg
{
	public class TestSword : ModItem
	{
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
			Item.useStyle = 1;
			Item.knockBack = 8;
			Item.value = Item.sellPrice(silver: 27);
			Item.rare = 0;
            Item.accessory = true;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
		}
        public override void UpdateAccessory(Player player, bool hideVisual) {
			Player1 p = player.GetModPlayer<Player1>();
            if (p.hasInfernal_core == true) {
                Item.damage = 272727;
                Item.rare = 12;
            }
            if (p.hasInfernal_core == false) {
                Item.rare = 3; 
            }

    }
}
}