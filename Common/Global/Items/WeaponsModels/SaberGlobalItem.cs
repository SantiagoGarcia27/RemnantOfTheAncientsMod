using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using System;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Content.Projectiles;
using RemnantOfTheAncientsMod.Projectiles.Melee;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee.Swing;

namespace RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels
{
    public class SaberGlobalItem : GlobalItem
    {
        public Vector2 DashStrength = Vector2.Zero;
        public bool isSaber = false;

       /* public override void SetDefaults(Item item)
        {
            bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;
            if (item.type == ItemID.ChlorophyteSaber && WeaponConf)
            {
                isSaber = true;
                DashStrength = new Vector2(1f, 0.75f);
            }
            else if (item?.Name != null && item.ModItem?.Mod?.Name is string modName && modName != "RemnantOfTheAncientsMod" && Utils1.NameHasWord(item.Name, "Saber"))
            {
                for (int j = 0; j <= RemnantOfTheAncientsMod.MaxRarity; j++)
                {
                    if (item.rare == j)
                    {
                        float StrenghtX = j > 10 ? (float)Math.Log(j - Math.Log(j)) : (float)Math.Log(j);
                        float StrenghtY = j > 10 ? 1.7f : (float)Math.Log(j);

                        isSaber = true;
                        DashStrength = new Vector2(StrenghtX, StrenghtY);
                    }
                }
            }
            if (isSaber)
            {
                if (item.shoot == ProjectileID.None)
                {
                    item.shoot = ModContent.ProjectileType<DamageHitbox>();
                    item.shootSpeed = 0f;
                }
            }

            base.SetDefaults(item);
        }*/
        public override void SetDefaults(Item item)
        {
            // 1. Blindaje contra nulos y carga temprana de ContentSamples
            if (item == null) return;

            // 2. Ejecutar base
            base.SetDefaults(item);

            // 3. Evitar procesar ítems "vacíos" o aire que tML usa para inicializar
            if (item.type == ItemID.None || item.IsAir) return;

            var config = ModContent.GetInstance<ConfigServer>();
            if (config == null) return; // Seguridad extra durante la carga

            bool WeaponConf = config.VanillaWeaponsChangesConf;

            if (item.type == ItemID.ChlorophyteSaber && WeaponConf)
            {
                isSaber = true;
                DashStrength = new Vector2(1f, 0.75f);
            }
            // Simplificamos el acceso para que sea 100% seguro contra nulos
            else if (item.ModItem != null && item.ModItem.Mod != null)
            {
                string modName = item.ModItem.Mod.Name;

                if (modName != "RemnantOfTheAncientsMod" && !string.IsNullOrEmpty(item.Name) && Utils1.NameHasWord(item.Name, "Saber"))
                {
                    for (int j = 0; j <= RemnantOfTheAncientsMod.MaxRarity; j++)
                    {
                        if (item.rare == j)
                        {
                            float StrenghtX = j > 10 ? (float)Math.Log(j - Math.Log(j)) : (float)Math.Log(j);
                            float StrenghtY = j > 10 ? 1.7f : (float)Math.Log(j);

                            isSaber = true;
                            DashStrength = new Vector2(StrenghtX, StrenghtY);
                        }
                    }
                }
            }

            // Lógica final de proyectiles
            if (isSaber && item.shoot == ProjectileID.None)
            {
                item.shoot = ModContent.ProjectileType<DamageHitbox>();
                item.shootSpeed = 0f;
            }
        }

        public static void DashEffect(Player player, int type)
        {
            Item item = ContentSamples.ItemsByType[type];
            bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;

            if (type == ModContent.ItemType<CorruptedSaber>())
            {
                int proj = Projectile.NewProjectile(Entity.GetSource_None(), player.MountedCenter + new Vector2(DistanceUtils.ToCoordenatePosition(8) * player.direction, 0), new Vector2(player.direction, 0f) * new Vector2(0.5f, 0.5f), ProjectileID.LightsBane, item.damage + 10, item.knockBack, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, 3.4f);
                Main.projectile[proj].ai[0] = Main.rand.NextFloat(3.5f, 4.6f);
            }
            else if (type == ModContent.ItemType<HallowedSaber>())
            {
                if (!player.HasBuff<HolyCouldownDebuff>())
                {
                    player.AddBuff(BuffID.ShadowDodge, (int)Utils1.FormatTimeToTick(0, 0, 0, 3));
                }
            }
            else if (type == ModContent.ItemType<GrassSaber>())
            {
                Vector2 Velocity = item.shootSpeed * 0.1f * (player.position - Main.MouseWorld);
                Vector2 position = player.position + new Vector2(3 * 16, 0) * player.direction;
                var p = Projectile.NewProjectile(Entity.GetSource_None(), position, Velocity, ModContent.ProjectileType<BladeOfGrassLeaftClone>(), (int)(item.damage * 0.25f), item.knockBack, Main.myPlayer, -1f * player.direction, 0, 0);
                Main.projectile[p].scale = 2;
            }
            else if (type == ModContent.ItemType<EnchantedSaber>())
            {
                if (player.controlUseTile && Main.mouseRight)
                {
                    var p = Projectile.NewProjectile(Entity.GetSource_None(), player.Center, new Vector2(0, 0), ModContent.ProjectileType<DamageHitbox>(), item.damage * 2, 2, Main.myPlayer, 2, 1);
                    Main.projectile[p].width = item.width * 2;
                    Main.projectile[p].height = item.height * 2;
                    Main.projectile[p].scale = item.scale;
                    Main.projectile[p].timeLeft = 100;

                    for (int i = 0; i < RemnantOfTheAncientsMod.ParticleMeter(50); i++)
                    {
                        Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.EnchantedNightcrawler, 0f, 0f, 100, default, 2f);
                        dust.noGravity = true;
                    }
                }
            }
            else if (type == ModContent.ItemType<NightSaber>())
            {
                float adjustedItemScale = player.GetAdjustedItemScale(item); // Get the melee scale of the player and item.
                Projectile.NewProjectile(Entity.GetSource_None(), player.MountedCenter, new Vector2(player.direction, 0f), ProjectileID.NightsEdge, item.damage + 10, item.knockBack, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale + 0.7f);
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.

                var p = Projectile.NewProjectile(Entity.GetSource_None(), player.position, new Vector2(0, 0), ModContent.ProjectileType<DamageHitbox>(), item.damage, 2f, Main.myPlayer, 3, 1);
                Main.projectile[p].width = item.width * 2;
                Main.projectile[p].height = item.height * 2;
                Main.projectile[p].scale = item.scale;
                Main.projectile[p].timeLeft = 100;
            }
            else if (type == ModContent.ItemType<FireSaber>())
            {
                var p = Projectile.NewProjectile(Entity.GetSource_None(), player.Center, new Vector2(0, 0), ModContent.ProjectileType<DamageHitbox>(), item.damage * 2, 2, Main.myPlayer, 1, 1);
                Main.projectile[p].width = item.width * 2;
                Main.projectile[p].height = item.height * 2;
                Main.projectile[p].scale = item.scale;
                Main.projectile[p].timeLeft = 100;

                for (int i = 0; i < 50; i++)
                {
                    Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Torch, 0f, 0f, 100, default, 2f);
                    dust.noGravity = true;
                }
            }
            else if (type == ModContent.ItemType<SpikeSaber>())
            {
                bool PlayerTouchFlour = Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + 2 * 16) / 16)].HasTile;
                int projectileCount = 32;
                float projectileDistance = 10f;

                for (int i = 0; i < projectileCount; i++)
                {
                    float angle = MathHelper.ToRadians(360f / projectileCount * i);
                    Vector2 velocity = angle.ToRotationVector2() * projectileDistance;
                    Vector2 position = player.Center + velocity;

                    var p = Projectile.NewProjectile(Entity.GetSource_None(), position, velocity, ModContent.ProjectileType<InfernalSpike_f>(), item.damage, 2f, Main.myPlayer);
                    Main.projectile[p].timeLeft = 100;
                }
            }
            else if (item.type == ItemID.ChlorophyteSaber && WeaponConf)
            {
                bool PlayerTouchFlour = Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + 2 * 16) / 16)].HasTile;
                int projectileCount = 32;
                float projectileDistance = 10f;

                for (int i = 0; i < projectileCount; i++)
                {
                    float angle = MathHelper.ToRadians(360f / projectileCount * i);
                    Vector2 velocity = angle.ToRotationVector2() * projectileDistance;
                    Vector2 position = player.Center + velocity;

                    var p = Projectile.NewProjectile(Entity.GetSource_None(), position, velocity, item.shoot, item.damage, 2f, Main.myPlayer);
                    Main.projectile[p].timeLeft = 100;
                }
            }
        }
        public override void MeleeEffects(Item item, Player player, Rectangle hitbox)
        {
            if (item.type == ModContent.ItemType<CorruptedSaber>())
            {
                int choice = (int)Math.Pow(RemnantOfTheAncientsMod.ParticleMeter(3, true), 2);
                if (choice > 0)
                {
                    if (Main.rand.NextBool(choice))
                    {
                        Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Demonite);
                    }
                }
            }
            else if (item.type == ModContent.ItemType<GrassSaber>())
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Grass);
            }
            else if (item.type == ModContent.ItemType<NightSaber>())
            {
                if (Main.rand.NextBool(3))
                    Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Demonite);
                if (Main.rand.NextBool(3))
                    Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Shadowflame);
                if (Main.rand.NextBool(7))
                    Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.WaterCandle, 0f, 0f, 1, Color.MediumPurple);
                if (Main.rand.NextBool(2))
                    Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Water_Corruption);
                if (Main.rand.NextBool(3))
                    Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Water_Cavern);
            }
            else if (item.type == ModContent.ItemType<FireSaber>())
            {
                for (int i = 0; i < 30; i++)
                {
                    Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Pixie);
                }
            }

            base.MeleeEffects(item, player, hitbox);
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.shoot == ModContent.ProjectileType<DamageHitbox>())
            {
                damage = 0;
                //velocity.Y -= 50;
            }

            if (item.type == ModContent.ItemType<CorruptedSaber>())
            {
                int proj = Projectile.NewProjectile(source, position + new Vector2(DistanceUtils.ToCoordenatePosition(8) * player.direction, 0), velocity * new Vector2(0.5f, 0.5f), type, damage, knockback, player.whoAmI, 2.4f);
                Main.projectile[proj].ai[0] = Main.rand.NextFloat(1.5f, 1.6f);
                //return false;
            }
            else if (item.type == ModContent.ItemType<HallowedSaber>())
            {
                float adjustedItemScale = player.GetAdjustedItemScale(item);
                int p = Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.
            }
            else if (item.type == ModContent.ItemType<GrassSaber>())
            {
                Projectile.NewProjectile(Entity.GetSource_None(), position, velocity, ModContent.ProjectileType<BladeOfGrassLeaftClone>(), (int)(item.damage * 0.25f), item.knockBack, Main.myPlayer, -0.1f * player.direction, 0, 0);
                //return false;
            }
            else if (item.type == ModContent.ItemType<EnchantedSaber>())
            {
                if (RemnantOfTheAncientsMod.TerrariaOverhaul != null && !ModContent.GetInstance<ConfigServer>().OverhaulMeleeManaCostConfig)
                    Projectile.NewProjectile(source, position, velocity, ProjectileID.EnchantedBeam, damage, knockback);
            }
            else if (item.type == ModContent.ItemType<NightSaber>())
            {
                float adjustedItemScale = player.GetAdjustedItemScale(item); // Get the melee scale of the player and item.
                Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), ProjectileID.NightsEdge, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale + Main.rand.NextFloat(0.4f, 1f));
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.
            }
            /*if (isSaber)
            {
                if (item.shoot != ModContent.ProjectileType<DamageHitbox>())
                    item.noUseGraphic = true;
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SaberSwingProgectile>(), damage, knockback, Main.myPlayer, 0);
                SaberSwingProgectile.SetID(item);

                return true;
            }*/
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (item.type == ModContent.ItemType<GrassSaber>())
            {
                target.AddBuff(BuffID.Poisoned, 80);
            }
            else if (item.type == ModContent.ItemType<FireSaber>())
            {
                target.AddBuff(BuffID.OnFire, 80);
                if (RemnantOfTheAncientsMod.ParticleMeter(4) != 0)
                {
                    Projectile.NewProjectile(Entity.GetSource_None(), target.position, new Vector2(0f, 0f), ProjectileID.Volcano, damageDone / 10, 0);
                }
            }
            else if (item.type == ModContent.ItemType<CrimsonSaber>())
            {
                target.AddBuff(BuffID.BloodButcherer, 540);

                Vector2 postion = target.position + new Vector2(target.width, player.height);
                Projectile.NewProjectile(Entity.GetSource_None(), postion, target.DirectionTo(player.Center), ProjectileID.BloodButcherer, 0, 0, player.whoAmI, 1, target.whoAmI);
            }

            base.OnHitNPC(item, player, target, hit, damageDone);
        }

        public override void PostDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            string Texture = null;

            if (item.type == ModContent.ItemType<CorruptedSaber>())
                Texture = "RemnantOfTheAncientsMod/Content/Items/Weapons/Melee/saber/Corrupted_saber_Glow";


            if (Texture != null)
            {
                item.glowMask = RemnantOfTheAncientsMod.AddGlowMask(Texture);
                Texture2D texture = ModContent.Request<Texture2D>(Texture, AssetRequestMode.ImmediateLoad).Value;
                spriteBatch.Draw
                (
                    texture,
                    new Vector2
                    (
                        item.position.X - Main.screenPosition.X + item.width * 0.5f,
                        item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }
            base.PostDrawInWorld(item, spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (isSaber)
            {
                if (player.altFunctionUse == 2)
                {
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<SaberDashProj>()] < 1)
                    {
                        if (Main.tile[(int)(player.Center.X / 16), (int)((player.Center.Y + 2 * 16) / 16)].HasTile == true)
                        {
                            Projectile.NewProjectile(Entity.GetSource_None(), player.Center, Vector2.Zero, ModContent.ProjectileType<SaberDashProj>(), item.damage * 2, 2, Main.myPlayer, DashStrength.X, DashStrength.Y, item.type);
                        }
                    }
                    return false;
                }
                else
                {
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<SaberDashProj>()] >= 1)
                        return false;
                }
            }
            return base.CanUseItem(item, player);
        }
        public override bool InstancePerEntity => true;
    }
}
