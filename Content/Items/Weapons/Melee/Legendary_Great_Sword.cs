using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
    public class Legendary_Great_Sword : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Legendary Great Sword");
            //Tooltip.SetDefault("Inflict fire,ichor,venom,hellfire and poison on enemies");

           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Grande épée légendaire");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Inflige le feu aux ennemis");

           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Gran espada legendaria");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije fuego,ichor,ponsoña,fuego infernal y veneno a los enemigos");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 200;
            Item.DamageType = DamageClass.Melee;
            Item.width = 130;
            Item.height = 160;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 20;
            Item.scale = 1.80f;
            Item.UseSound = SoundID.Item45;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(gold: 35);
            Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Legendary;
            Item.GetGlobalItem<CustomTooltip>().LegendaryDrop = true;
            //if (RemnantOfTheAncientsMod.TerrariaOverhaul != null)
            //{
            //    if (ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig) Item.shoot = ModContent.ProjectileType<LegendaryGreatSwordSwingProgectile>();
            //}
            //else Item.shoot = ModContent.ProjectileType<LegendaryGreatSwordSwingProgectile>();
            Item.shootSpeed = 10f;
           // Item.noUseGraphic = true;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Item.damage = 400;
            }
        }
        public override void PostReforge()
        {
            base.PostReforge();
            //  if (Item.prefix == PrefixID.Legendary)// //DisplayName.SetDefault("True Legendary Great Sword");

        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 300);
            target.AddBuff(ModContent.BuffType<Hell_Fire>(), 300);
            target.AddBuff(BuffID.Ichor, 300);
            target.AddBuff(BuffID.Venom, 300);
            target.AddBuff(BuffID.Poisoned, 300);

            //int x = Main.rand.Next(0, 3);
            //int p = Projectile.NewProjectile(Projectile.GetSource_None(), target.position - new Vector2(x*16,50*16), new Vector2(0,5), ProjectileID.BoulderStaffOfEarth, damage, 1, player.whoAmI);
            //Main.projectile[p].timeLeft = 300;


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

                if (heading.Y < 0f)
                {
                    heading.Y *= -1f;
                }

                if (heading.Y < 20f)
                {
                    heading.Y = 20f;
                }

                heading.Normalize();
               // heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(Projectile.GetSource_None(), position, heading, ProjectileID.BoulderStaffOfEarth, damageDone * 2,hit.Knockback, player.whoAmI, 0f, ceilingLimit);
            }
        }
        public override void MeleeEffects(Player player, Rectangle hitbox) => Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Pixie);

    }
}
