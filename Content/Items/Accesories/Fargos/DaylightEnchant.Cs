using RemnantOfTheAncientsMod.Common;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Daylight;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos
{
    public class DaylightEnchant : ModItem
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
			Item.value = Item.buyPrice(0, 1, 10, 72);  
			Item.rare = 3;
			Item.accessory = true;
			Item.defense = 0;
		}
		public List<int> Grass = new List<int>()
		{
			380
        };
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.GetModPlayer<RemnantFargosSoulsPlayer>().DaylightEnchant(player);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utils1.FindAndReplaceTooltip(tooltips, "&0", Utils1.TooltipHotkeyString(FargosKeybindSystem.ToggNightTp),"Tooltip1");  
            base.ModifyTooltips(tooltips);
        }

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient<DaylightHat>()
            .AddIngredient<Daylight_chesplate>()
            .AddIngredient<Daylight_Legging>()
            .AddIngredient<BrightDaisy>()
            .AddIngredient<SunflowerStaff>()
            .AddTile(TileID.DemonAltar)
			.Register();
		}
	}
}
