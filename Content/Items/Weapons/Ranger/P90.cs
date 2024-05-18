using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
    public class P90 : ModItem
    {
        public override void SetStaticDefaults()
        {     
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public int NotConsumeAmmoChance = 60;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(NotConsumeAmmoChance);
        public override void SetDefaults()
        {
            Item.DefaultToRangedWeapon(ProjectileID.PurificationPowder, AmmoID.Bullet, 4, 10f, true);
            Item.SetWeaponValues(13, 1f);
            Item.SetShopValues((Terraria.Enums.ItemRarityColor)ItemRarityID.LightPurple, Item.sellPrice(0, 1, 2, 0));
            Item.Size = new Vector2(80, 40);      
            Item.DamageType = DamageClass.Ranged;        
            Item.UseSound = SoundID.Item10;
            Item.expert = true;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Item.damage = 14;
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, 0);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= NotConsumeAmmoChance/100f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.MechanicalWheelPiece, 1)
            .AddRecipeGroup(RecipeGroupID.IronBar, 20)
            .AddIngredient(ItemID.SoulofMight, 5)
            .AddIngredient(ItemID.HallowedBar, 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
