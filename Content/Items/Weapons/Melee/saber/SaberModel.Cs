using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System.Collections.Generic;
using Terraria.Localization;
using System;
using RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels;
using SangarUtilities.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models
{
    public abstract class SaberBase : ModItem
    {
        public abstract Item ItemBase { get; }
        public abstract float[] DashStrength { get; }
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, Main.itemAnimations[ItemBase.type]);
            ItemID.Sets.AnimatesAsSoul[Type] = ItemID.Sets.AnimatesAsSoul[ItemBase.type];
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }
        
        public override LocalizedText Tooltip => IsVanilla() ? base.Tooltip : ToLocalizedText("Tooltip");
        public override LocalizedText DisplayName => IsVanilla() ? base.DisplayName : ToLocalizedText("DisplayName");
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemBase.type);
            float damage = MathUtils.GetValueFromPorcentage(ItemBase.damage, 75f);
            Item.damage = (int)damage;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 80;
            Item.useTime -= 3;
            Item.useAnimation -= 3;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack += 5;
            Item.UseSound = SoundID.Item1;
            if (RemnantOfTheAncientsMod.TerrariaOverhaul != null)
            {
                if (ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig) 
                    Item.shoot = ItemBase.shoot;
            }
            else Item.shoot = ItemBase.shoot;
 
            Item.shootSpeed = ItemBase.shootSpeed;
            Item.autoReuse = true;
            Item.channel = true;
            Item.scale = 1;
            Item.noUseGraphic = false;
            Item.GetGlobalItem<SaberGlobalItem>().DashStrength = new(DashStrength[0], DashStrength[1]);
            Item.GetGlobalItem<SaberGlobalItem>().isSaber = true;
            Item.noMelee = false;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override void AddRecipes()
        {
            List<int> baseRecipe = RecipeUtils.SearchRecipeMaterials(ItemBase.type);
            List<int> baseRecipeTile = RecipeUtils.SearchRecipeTile(ItemBase.type);

            Recipe recipe = CreateRecipe();

            if (baseRecipe != null && baseRecipeTile != null)
            {
                foreach (int item in baseRecipe)
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

        public LocalizedText ToLocalizedText(string modifier)
        {
            if (Item.ModItem.Mod == RemnantOfTheAncientsMod.RemnantOfTheAncients)
            {
                String name = Item.ModItem.Name;
                string line = $"Mods.RemnantOfTheAncientsMod.Items.{name}." + modifier;
                LocalizedText returnText = Language.GetText(line);

                return returnText;
            }
            switch (modifier)
            {
                case "DisplayName":
                return base.DisplayName;
                    case "Tooltip":
                return base.Tooltip;
                    default:
                    return base.Tooltip;
            }
        }
        public bool IsVanilla()
        {
            return Item.type <= 5455;
        }
    }
}