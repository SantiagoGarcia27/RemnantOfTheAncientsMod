using Terraria;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class TuxoniteSaber : SaberBase
	{
        public override Item ItemBase => new(ItemType<Tuxonite_Sword>());
        public override float[] DashStrength => [0.75f, 0.75f];

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<TuxoniteBar>(RecipeUtils.SearchAmmountRecipe(ItemID.GoldBroadsword, ItemID.GoldBar))
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}

