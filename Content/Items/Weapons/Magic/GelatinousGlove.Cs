using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
    public class GelatinousGlove : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.mana = 8;
            Item.rare = ItemRarityID.Master;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shootSpeed = 14f;
            Item.shoot = ProjectileID.SpikedSlimeSpike;
            Item.UseSound = SoundID.Item39;
            Item.value = Item.sellPrice(silver: 50);
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.knockBack = 0.1f;
            Item.ResearchUnlockCount = 1;
            Item.master = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(5);

            position += Vector2.Normalize(velocity) * 1f;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                int p = Projectile.NewProjectile(source, position, perturbedSpeed * 10, type, damage, knockback, player.whoAmI);
                Main.projectile[p].stepSpeed = 10f;
                Main.projectile[p].friendly = true;
                Main.projectile[p].hostile = false;
            }
            return false;
        }
        public override void HoldItem(Player player)
        {
          

            int p = Dust.NewDust(player.GetFrontHandPosition(Player.CompositeArmStretchAmount.None, 0f), 1, 1, DustID.t_Slime, 0, 0, 0, new Color(86, 151, 254, 255), 1.6f);
            Main.dust[p].noGravity = true;
            base.HoldItem(player);
        }
    }
}
