using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using Terraria.DataStructures;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Bows
{
    public class TrueNigthbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 12;
            Item.height = 38;
            Item.maxStack = 1;
            Item.useTime = 8;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold:5);
            Item.rare = ItemRarityID.Yellow;
            Item.reuseDelay = 14;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.useAmmo = AmmoID.Arrow;
            Item.shootSpeed = 10f;
            Item.autoReuse = false;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return !(player.itemAnimation < Item.useAnimation - 2);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int color = 0;
            if(Main.rand.NextBool(2))
            {
                if(Main.rand.NextBool(2))
                    color = 1;
                int dustID = color == 1? DustID.CursedTorch : DustID.PinkFairy;

                Vector2 newPos = position;
                newPos.Y += Main.rand.Next(10, 80) * (Main.rand.NextBool(2) ? -1 : 1);
                newPos.X -= Main.rand.Next(50, 180) * player.direction;

                for (int i = 0; i < 360; i+= 5)
                {
                    Vector2 dustPos = (newPos + Vector2.One).RotatedBy(MathHelper.ToRadians(i));
                    Vector2 vel = new Vector2(0, 1).RotatedBy(MathHelper.ToRadians(i)) * 10f;
                    Dust.NewDust(dustPos, 10, 10, dustID, vel.X, vel.Y,Scale: 1.5f);
                }
                Projectile.NewProjectile(source,newPos, velocity.RotatedByRandom(MathHelper.ToRadians(10)), ModContent.ProjectileType<TrueNightArrow>(), damage, knockback, player.whoAmI);
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<Nigthbow>()
            .AddIngredient(ItemID.SoulofFright,20)
            .AddIngredient(ItemID.SoulofMight, 20)
            .AddIngredient(ItemID.SoulofSight, 20)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
