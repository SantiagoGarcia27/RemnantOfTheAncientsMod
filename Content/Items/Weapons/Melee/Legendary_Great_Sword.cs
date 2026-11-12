using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee.Swing;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
    public class Legendary_Great_Sword : ModItem
    {
        public int attackType = 0; // keeps track of which attack it is
        public int comboExpireTimer = 0; // we want the attack pattern to reset if the weapon is not used for certain period of time
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 200;
            Item.DamageType = DamageClass.Melee;
            Item.width = 130;
            Item.height = 160;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 20;
            Item.scale = 1.80f;
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item45;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(gold: 35);
            Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Legendary;
            Item.GetGlobalItem<CustomTooltip>().LegendaryDrop = true;
            Item.shoot = ModContent.ProjectileType<LegendaryGreatSwordSwingProgectile>();
            Item.shootSpeed = 10f;
            Item.noUseGraphic = true;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Item.damage = 400;
            }
        }
        public override void PostReforge()
        {
            base.PostReforge();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 300);
            target.AddBuff(BuffID.OnFire3, 300);
            target.AddBuff(BuffID.Ichor, 300);
            target.AddBuff(BuffID.Venom, 300);
            target.AddBuff(BuffID.Poisoned, 300);

            int x = Main.rand.Next(0, 3);
            int p = Projectile.NewProjectile(Terraria.Entity.GetSource_None(), target.position - new Vector2(x * 16, 50 * 16), new Vector2(0, 5), ProjectileID.BoulderStaffOfEarth, damageDone, 1, player.whoAmI);
            Main.projectile[p].timeLeft = 300;


            Vector2 postition = Main.screenPosition + new Vector2(target.position.X, target.position.Y);
            float ceilingLimit = postition.Y;
            if (ceilingLimit > player.Center.Y - 200f)
            {
                ceilingLimit = player.Center.Y - 200f;
            }
            Vector2 position = target.position - new Vector2(0 * 16, 50 * 16);
            // Loop these functions 3 times.
            for (int i = 0; i < 3; i++)
            {
                position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                position.Y -= 100 * i;
                Vector2 heading = postition - position;

                if (heading.Y < 0f) heading.Y *= -1f;
                if (heading.Y < 20f) heading.Y = 20f;
                
                heading.Normalize();
                //heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(Terraria.Entity.GetSource_None(), position, heading, ProjectileID.BoulderStaffOfEarth, damageDone * 2,hit.Knockback, player.whoAmI, 0f, ceilingLimit);
            }
        }
        public override void MeleeEffects(Player player, Rectangle hitbox) => Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Pixie);
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer, attackType);
            attackType = 0;
            comboExpireTimer = 0;
            return false;
        }
        public override bool MeleePrefix()
        {
            return true; 
        }
        public override void UpdateInventory(Player player)
        {
            if (comboExpireTimer++ >= Utils1.FormatTimeToTick(Second:2)) attackType = 0;
        }
    }
}
