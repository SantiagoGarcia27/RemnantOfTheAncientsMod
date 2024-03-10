using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.Global;
using System.Collections.Generic;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models
{
    public abstract class SaberBase : ModItem
    {
        public abstract Item ItemBase { get; }
        public abstract float[] DashStrength { get; }
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, Main.itemAnimations[ItemBase.type]);
            ItemID.Sets.AnimatesAsSoul[base.Type] = ItemID.Sets.AnimatesAsSoul[ItemBase.type];
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }  
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AdamantiteSword);
            Item.damage -= 3;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 80;
            Item.useTime -= 3;
            Item.useAnimation -= 3;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack += 5;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.scale = 1;
            Item.GetGlobalItem<CustomTooltip>().Saber = true;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + (2 * 16)) / 16)].HasTile == true)
                {
                    DashPlayer.JumpDash(player, DashStrength[0], DashStrength[1]);
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            List<int> baseRecipe = RecipeUtils.SearchRecipeMaterials(ItemBase.type);
            List<int> baseRecipeTile = RecipeUtils.SearchRecipeTile(ItemBase.type);

            Recipe recipe = CreateRecipe();

            foreach(int item in baseRecipe)
            {
                recipe.AddIngredient(item, RecipeUtils.SearchAmmountRecipe(ItemBase.type, item));
            }
            foreach (int item in baseRecipeTile)
            {
                recipe.AddTile(item);
            }
            recipe.Register();
        }
    }
}