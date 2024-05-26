using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Bows;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Rep
{
    public class WoodenRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.WoodenBow);
    }
    public class CopperRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.CopperBow);
    }
    public class TinRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.TinBow);
    }
    public class IronRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.IronBow);
    }
    public class LeadRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.LeadBow);
    }
    public class TungstenRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.TungstenBow);
    }
    public class SilverRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.SilverBow);
    }
    public class GoldRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.GoldBow);
    }
    public class PlatinumRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.PlatinumBow);
    }
    public class TuxoniteRepeater : RepeaterModel
    {
        public override Item Base => new Item(ModContent.ItemType<TuxoniteBow>());
    }
    public abstract class RepeaterModel : ModItem
    {
        public abstract Item Base { get; }
        public override void SetDefaults()
        {
            Item.DefaultToRangedWeapon(ProjectileID.WoodenArrowFriendly, AmmoID.Arrow, Base.useTime + 6, 10f, true);
            Item.SetWeaponValues(Base.damage - 2, Base.knockBack);
            Item.SetShopValues((Terraria.Enums.ItemRarityColor)Base.rare, Base.value);
            Item.Size = new Vector2(12, 38);
            Item.DamageType = DamageClass.Ranged;
            Item.UseSound = SoundID.Item12;
            Item.channel = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = RecipeUtils.SearchRecipe(Base.type);
            CreateRecipe()
            .AddIngredient(recipe.requiredItem[0].type, recipe.requiredItem[0].stack)
            .AddTile(recipe.requiredTile[0])
            .Register();
        }
    }
}