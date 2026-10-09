using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Bows;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Rep
{
    public class WoodenRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.WoodenBow);
    }
    public class RichMahoganyRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.RichMahoganyBow);
    }
    public class PearlwoodRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.PearlwoodBow);
    }
    public class PalmWoodRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.PalmWoodBow);
    }
    public class EbonwoodRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.EbonwoodBow);
    }
    public class BorealWoodRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.BorealWoodBow);
    }
    public class AshWoodRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.AshWoodBow);
    }
    public class ShadewoodRepeater : RepeaterModel
    {
        public override Item Base => new(ItemID.ShadewoodBow);
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
            Item.damage = Base.damage - 2;
            Item.knockBack = Base.knockBack;
            Item.useTime = Base.useTime + 6;
            Item.useAnimation = Base.useAnimation;

            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = Base.shootSpeed + 4;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.value = Base.value;
            Item.rare = Base.rare;
            Item.DamageType = DamageClass.Ranged;
            Item.useAmmo = AmmoID.Arrow;
            Item.autoReuse = true;
  
            Item.Size = new Vector2(12, 38);
            Item.UseSound = SoundID.Item5;
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