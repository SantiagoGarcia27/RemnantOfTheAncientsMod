using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.Bobbers;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Tools.FishingRods
{
    public class NigthFishingRod : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.CanFishInLava[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.rare = 4;
            Item.fishingPole = 60;
            Item.shootSpeed = 20f;
            Item.shoot = ModContent.ProjectileType<NigthBobber>();
            Item.value = Item.buyPrice(0, 1, 0, 0);
        }
        public override void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
        {
            lineOriginOffset = new Vector2(43, -30);

            if (bobber.ModProjectile is NigthBobber Bobber)
            {
                lineColor = Bobber.FishingLineColor;
            }
        }
        public override void HoldItem(Player player)
        {
            player.accFishingLine = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddRecipeGroup("CorruptCaña")
            .AddIngredient(ItemID.FiberglassFishingPole, 1)
            .AddIngredient(ItemID.MechanicsRod, 1)
            .AddIngredient(ItemID.HotlineFishingHook, 1)
            .AddIngredient(ItemID.GoldenFishingRod, 1)
            .AddTile(TileID.DemonAltar)
            .Register();
        }
    }
}