using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Items.Armor.Desert;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos
{
    public class SandHeraldEnchant : ModItem
	{
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod);
        }
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 26; 
			Item.height = 26;
			Item.maxStack = 1; 
			Item.value = Item.buyPrice(0, 0, 2, 72);  
			Item.rare = 3;
			Item.accessory = true;
		}
        [JITWhenModsEnabled("FargowiltasSouls")]
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.AddEffect<SandHeraldEffect>(base.Item);
           // player.GetModPlayer<RemnantFargosSoulsPlayer>().DesertEnchant(player);
        }

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient<Desert_herald_Helmet>()
            .AddIngredient<Desert_herald_chesplate>()
            .AddIngredient<Desert_herald_Greaves>()
			.AddIngredient(ItemID.ThunderSpear)
            .AddIngredient(ItemID.Cactus,20)
            .AddIngredient<AncientT>()
            .AddTile(TileID.DemonAltar)
			.Register();
		}
	}
}
