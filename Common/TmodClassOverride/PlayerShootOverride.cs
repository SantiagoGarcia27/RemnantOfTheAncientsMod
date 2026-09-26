using Microsoft.Xna.Framework;
using MonoMod.RuntimeDetour;
using PlayerProxyLib.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Achievements;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace RemnantOfTheAncientsMod.Common.TmodClassOverride
{
    public class PlayerShootOverride : ModSystem
    {
        private static Hook? _itemCheckShootHook;

        public override void OnModLoad()
        {
            MethodInfo method = typeof(Player).GetMethod(
                "ItemCheck_Shoot",
                BindingFlags.Instance | BindingFlags.NonPublic
            )!;

            _itemCheckShootHook = new Hook(
                method,
                ItemCheckShootDetour
            );
        }

        public override void OnModUnload()
        {
            _itemCheckShootHook?.Dispose();
            _itemCheckShootHook = null;
        }

        private static float GetMeleeSpeed(Player player, DamageClass damageClass) => player.GetAttackSpeed(DamageClass.Melee);
        private static float GetInverseMeleeSpeed(Player player, DamageClass damageClass) => 1f / player.GetTotalAttackSpeed(DamageClass.Melee);

        private static IEntitySource GetProjectileSource_Item_WithPotentialAmmo(Player player, Item item, int ammoItemId)
        {
            return player.GetSource_ItemUse_WithPotentialAmmo(item, ammoItemId);
        }
        private delegate void OrigItemCheckShoot(Player self, int i, Item item, int weaponDamage);

        private static void ItemCheckShootDetour(OrigItemCheckShoot orig, Player self, int i, Item item, int weaponDamage)
        {
            if (self.IsProxyPlayer())
            {
                Vector2 aimWorld = FakeMain.MouseWorld(self);
                CustomItemCheckShoot(self, item, weaponDamage, aimWorld);
                return;
            }

            orig(self, i, item, weaponDamage);
        }

        private static void CustomItemCheckShoot(Player player, Item sItem, int weaponDamage,Vector2 aimWorld)
        {
            float meleeSpeed = GetMeleeSpeed(player, DamageClass.Melee);
            float inverseMeleeSpeed = GetInverseMeleeSpeed(player, DamageClass.Melee);
            Vector2 Directions = new(player.direction, player.gravDir);

            if (!CombinedHooks.CanShoot(player, sItem)) return;
            
            int projToShoot = sItem.shoot;
            float speed = sItem.shootSpeed;
            int damage = sItem.damage;

            if (sItem.DamageType == DamageClass.Melee && !ProjectileID.Sets.NoMeleeSpeedVelocityScaling[projToShoot])
            {
                speed /= inverseMeleeSpeed;
            }
            if (sItem.CountsAsClass(DamageClass.Throwing) && speed < 16f)
            {
                speed *= player.ThrownVelocity;
                if (speed > 16f) speed = 16f;
            }

            bool canShoot = false;
            int Damage = weaponDamage;
            float KnockBack = sItem.knockBack;
            int usedAmmoItemId = 0;

            if (sItem.useAmmo > 0)
            {
               canShoot = player.PickAmmo(sItem, out projToShoot, out speed, out Damage, out KnockBack, out usedAmmoItemId, ItemID.Sets.gunProj[sItem.type]);
            }
            else
            {
                canShoot = true;
            }
            if (ItemID.Sets.gunProj[sItem.type])
            {
                KnockBack = sItem.knockBack;
                Damage = weaponDamage;
                speed = sItem.shootSpeed;
            }
            if (sItem.IsACoin)
            {
                canShoot = false;
            }
            if (sItem.type == ItemID.SniperRifle && projToShoot == 14)
            {
                projToShoot = 242;
            }
            if (sItem.type == ItemID.VenusMagnum && projToShoot == 14)
            {
                projToShoot = 242;
            }
            if (sItem.type == ItemID.Uzi && projToShoot == 14)
            {
                projToShoot = 242;
            }
            if (sItem.type == ItemID.NebulaBlaze)
            {
                if (Main.rand.Next(100) < 20)
                {
                    projToShoot++;
                    Damage *= 3;
                }
                else
                {
                    speed -= 1f;
                }
            }
            if (sItem.type == ItemID.ChristmasTreeSword)
            {
                Damage = (int)(Damage * 1f);
            }
            if (sItem.type == ItemID.Meowmere)
            {
                Damage = (int)(Damage * 1.25f);
            }
            if (sItem.type == ItemID.IceSickle)
            {
                Damage = (int)(Damage * 0.67);
            }
            if (sItem.type == ItemID.ChlorophyteSaber)
            {
                Damage = (int)(Damage * 0.7);
            }
            if (!canShoot)
            {
                return;
            }
            KnockBack = player.GetWeaponKnockback(sItem, KnockBack);
            IEntitySource projectileSource_Item_WithPotentialAmmo = GetProjectileSource_Item_WithPotentialAmmo(player, sItem, usedAmmoItemId);
            if (projToShoot == 228)
            {
                KnockBack = 0f;
            }
            if (projToShoot == 1 && sItem.type == ItemID.MoltenFury)
            {
                projToShoot = 2;
            }
            if (sItem.type == ItemID.Marrow)
            {
                projToShoot = 117;
            }
            if (sItem.type == ItemID.IceBow)
            {
                projToShoot = 120;
            }
            if (sItem.type == ItemID.ElectrosphereLauncher)
            {
                projToShoot = 442;
            }
            if (sItem.type == ItemID.FireworksLauncher)
            {
                projToShoot = 167;
            }
            if (sItem.type == ItemID.PulseBow)
            {
                projToShoot = 357;
            }
            if (sItem.type == ItemID.PewMaticHorn)
            {
                projToShoot = 968;
            }
            if (sItem.fishingPole > 0 && player.overrideFishingBobber > -1)
            {
                projToShoot = player.overrideFishingBobber;
            }
            player.ApplyItemTime(sItem);
            Vector2 pointPoisition = player.RotatedRelativePoint(player.MountedCenter);
            bool flag = true;
            _ = sItem.type;
            if (!sItem.ChangePlayerDirectionOnShoot)
            {
                flag = false;
            }
            Vector2 unitX = Vector2.UnitX;
            double radians = player.fullRotation;
            Vector2 val = default;
            Vector2 val2 = unitX.RotatedBy(radians, val);
            Vector2 vector = aimWorld - pointPoisition;
            Vector2 v = player.itemRotation.ToRotationVector2() * player.direction;
            if (sItem.type == ItemID.BookStaff && !player.ItemAnimationJustStarted)
            {
                vector = (v.ToRotation() + player.fullRotation).ToRotationVector2();
            }
            if (vector != Vector2.Zero)
            {
                vector.Normalize();
            }
            float num = Vector2.Dot(val2, vector);
            if (flag)
            {
                if (num > 0f)
                {
                    player.ChangeDir(1);
                }
                else
                {
                    player.ChangeDir(-1);
                }
            }
            if (sItem.type == ItemID.Javelin || sItem.type == ItemID.BoneJavelin || sItem.type == ItemID.DayBreak)
            {
                pointPoisition.Y = player.position.Y + (player.height / 3);
            }
            if (sItem.type == ItemID.PewMaticHorn)
            {
                pointPoisition.Y = player.position.Y + (player.height / 3);
            }
            if (sItem.type == ItemID.MagicDagger)
            {
                pointPoisition.X += Main.rand.Next(-3, 4) * 3.5f;
                pointPoisition.Y += Main.rand.Next(-3, 4) * 3.5f;
            }
            if (sItem.type == ItemID.Flairon)
            {
                Vector2 vector2 = vector;
                if (vector2 != Vector2.Zero)
                {
                    vector2.Normalize();
                }
                pointPoisition += vector2;
            }
            if (sItem.type == ItemID.DD2SquireBetsySword)
            {
                Vector2 val3 = pointPoisition;
                Vector2 spinningpoint = vector.SafeNormalize(Vector2.Zero);
                double radians2 = player.direction * (-(float)Math.PI / 2f);
                val = new Vector2();
                pointPoisition = val3 + spinningpoint.RotatedBy(radians2, val) * 24f;
            }
            if (projToShoot == 9)
            {
                pointPoisition = new(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -player.direction + (aimWorld.X + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                KnockBack = 0f;
                Damage = (int)(Damage * 1.5f);
            }
            if (sItem.type == ItemID.Blowgun || sItem.type == ItemID.Blowpipe)
            {
                pointPoisition.X += 6 * player.direction;
                pointPoisition.Y -= 6f * player.gravDir;
            }
            if (sItem.type == ItemID.DartPistol)
            {
                pointPoisition.X -= 4 * player.direction;
                pointPoisition.Y -= 2f * player.gravDir;
            }
            float num2 = aimWorld.X - pointPoisition.X;
            float num3 = aimWorld.Y - pointPoisition.Y;
            if (sItem.type == ItemID.BookStaff && !player.ItemAnimationJustStarted)
            {
                Vector2 val4 = vector;
                num2 = val4.X;
                num3 = val4.Y;
            }
            /*if (player.gravDir == -1f)
            {
                num3 = Main.screenPosition.Y + Main.screenHeight - aimWorld.Y - pointPoisition.Y;
            }*/
            float num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
            float num5 = num4;
            if ((float.IsNaN(num2) && float.IsNaN(num3)) || (num2 == 0f && num3 == 0f))
            {
                num2 = player.direction;
                num3 = 0f;
                num4 = speed;
            }
            else
            {
                num4 = speed / num4;
            }
            if (sItem.type == ItemID.ChainGun || sItem.type == ItemID.Gatligator)
            {
                num2 += Main.rand.Next(-50, 51) * 0.03f / num4;
                num3 += Main.rand.Next(-50, 51) * 0.03f / num4;
            }
            num2 *= num4;
            num3 *= num4;
            if (projToShoot == 250)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (Main.projectile[j].active && Main.projectile[j].owner == player.whoAmI && (Main.projectile[j].type == ProjectileID.RainbowFront || Main.projectile[j].type == ProjectileID.RainbowBack))
                    {
                        Main.projectile[j].Kill();
                    }
                }
            }
            if (projToShoot == 12 && Collision.CanHitLine(player.Center, 0, 0, pointPoisition + new Vector2(num2, num3) * 4f, 0, 0))
            {
                pointPoisition += new Vector2(num2, num3) * 3f;
            }
            if (projToShoot == 728 && !Collision.CanHitLine(player.Center, 0, 0, pointPoisition + new Vector2(num2, num3) * 2f, 0, 0))
            {
                Vector2 vector4 = new Vector2(num2, num3) * 0.25f;
                pointPoisition = player.Center - vector4;
            }
            if (projToShoot == 85)
            {
                Vector2 val5 = pointPoisition;
                Vector2 spinningpoint2 = new Vector2(0f, -6f * player.direction * Directions.Y);
                double radians3 = vector.ToRotation();
                val = default(Vector2);
                pointPoisition = val5 + Utils.RotatedBy(spinningpoint2, radians3, val);
                if (Collision.CanHitLine(pointPoisition, 0, 0, pointPoisition + new Vector2(num2, num3) * 5f, 0, 0))
                {
                    pointPoisition += new Vector2(num2, num3) * 4f;
                }
            }
            if (projToShoot == 802 || projToShoot == 842)
            {
                Vector2 v2 = default(Vector2);
                v2 = new(num2, num3);
                float num6 = (float)Math.PI / 4f;
                Vector2 spinningpoint3 = v2.SafeNormalize(Vector2.Zero);
                double radians4 = num6 * (Main.rand.NextFloat() - 0.5f);
                val = default(Vector2);
                Vector2 val6 = spinningpoint3.RotatedBy(radians4, val) * (v2.Length() - Main.rand.NextFloatDirection() * 0.7f);
                num2 = val6.X;
                num3 = val6.Y;
            }
            if (projToShoot == 17)
            {
                pointPoisition = aimWorld;
                if (player.gravDir == -1f)
                {
                    pointPoisition.Y = Main.screenPosition.Y + Main.screenHeight - aimWorld.Y;
                }
                player.LimitPointToPlayerReachableArea(ref pointPoisition);
            }
            Vector2 velocity = default(Vector2);
            velocity = new(num2, num3);
            CombinedHooks.ModifyShootStats(player, sItem, ref pointPoisition, ref velocity, ref projToShoot, ref Damage, ref KnockBack);
            num2 = velocity.X;
            num3 = velocity.Y;
            if (sItem.useStyle == ItemUseStyleID.Shoot)
            {
                if (sItem.type == ItemID.DaedalusStormbow)
                {
                    Vector2 vector6 = default(Vector2);
                    vector6 = new(num2, num3)
                    {
                        X = aimWorld.X - pointPoisition.X,
                        Y = aimWorld.Y - pointPoisition.Y - 1000f
                    };
                    player.itemRotation = (float)Math.Atan2(vector6.Y * player.direction, vector6.X * player.direction);
                    NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
                    NetMessage.SendData(MessageID.ShotAnimationAndSound, -1, -1, null, player.whoAmI);
                }
                else if (sItem.type == ItemID.BloodRainBow)
                {
                    Vector2 vector7 = default(Vector2);
                    vector7 = new(num2, num3)
                    {
                        X = aimWorld.X - pointPoisition.X,
                        Y = aimWorld.Y - pointPoisition.Y - 1000f
                    };
                    player.itemRotation = (float)Math.Atan2(vector7.Y * player.direction, vector7.X * player.direction);
                    NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
                    NetMessage.SendData(MessageID.ShotAnimationAndSound, -1, -1, null, player.whoAmI);
                }
                else if (sItem.type == ItemID.SpiritFlame)
                {
                    player.itemRotation = 0f;
                    NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
                    NetMessage.SendData(MessageID.ShotAnimationAndSound, -1, -1, null, player.whoAmI);
                }
                else
                {
                    player.itemRotation = (float)Math.Atan2(num3 * player.direction, num2 * player.direction) - player.fullRotation;
                    NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
                    NetMessage.SendData(MessageID.ShotAnimationAndSound, -1, -1, null, player.whoAmI);
                }
            }
            if (sItem.useStyle == ItemUseStyleID.Rapier)
            {
                player.itemRotation = (float)Math.Atan2(num3 * player.direction, num2 * player.direction) - player.fullRotation;
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
                NetMessage.SendData(MessageID.ShotAnimationAndSound, -1, -1, null, player.whoAmI);
            }
            if (!CombinedHooks.Shoot(player, sItem, (EntitySource_ItemUse_WithAmmo)projectileSource_Item_WithPotentialAmmo, pointPoisition, velocity, projToShoot, Damage, KnockBack))
            {
                return;
            }
            if (projToShoot == 76)
            {
                projToShoot += Main.rand.Next(3);
                float num7 = Main.screenHeight / Main.GameViewMatrix.Zoom.Y;
                num5 /= num7 / 2f;
                if (num5 > 1f)
                {
                    num5 = 1f;
                }
                float num8 = num2 + Main.rand.Next(-40, 41) * 0.01f;
                float num9 = num3 + Main.rand.Next(-40, 41) * 0.01f;
                num8 *= num5 + 0.25f;
                num9 *= num5 + 0.25f;
                int num10 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num8, num9, projToShoot, Damage, KnockBack, player.whoAmI);
                Main.projectile[num10].ai[1] = 1f;
                num5 = num5 * 2f - 1f;
                if (num5 < -1f)
                {
                    num5 = -1f;
                }
                if (num5 > 1f)
                {
                    num5 = 1f;
                }
                num5 = (float)Math.Round(num5 * Player.musicNotes);
                num5 /= Player.musicNotes;
                Main.projectile[num10].ai[0] = num5;
                NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, num10);
            }
            else if (sItem.type == ItemID.DaedalusStormbow)
            {
                int num11 = 3;
                if (ProjectileID.Sets.FiresFewerFromDaedalusStormbow[projToShoot])
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        num11--;
                    }
                }
                else if (Main.rand.Next(3) == 0)
                {
                    num11++;
                }
                for (int k = 0; k < num11; k++)
                {
                    pointPoisition = new(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -player.direction + (aimWorld.X + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                    pointPoisition.X = (pointPoisition.X * 10f + player.Center.X) / 11f + Main.rand.Next(-100, 101);
                    pointPoisition.Y -= 150 * k;
                    num2 = aimWorld.X - pointPoisition.X;
                    num3 = aimWorld.Y - pointPoisition.Y;
                    if (num3 < 0f)
                    {
                        num3 *= -1f;
                    }
                    if (num3 < 20f)
                    {
                        num3 = 20f;
                    }
                    num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
                    num4 = speed / num4;
                    num2 *= num4;
                    num3 *= num4;
                    float num12 = num2 + Main.rand.Next(-40, 41) * 0.03f;
                    float speedY = num3 + Main.rand.Next(-40, 41) * 0.03f;
                    num12 *= Main.rand.Next(75, 150) * 0.01f;
                    pointPoisition.X += Main.rand.Next(-50, 51);
                    int num13 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num12, speedY, projToShoot, Damage, KnockBack, player.whoAmI);
                    Main.projectile[num13].noDropItem = true;
                }
            }
            else if (sItem.type == ItemID.BloodRainBow)
            {
                int num14 = Main.rand.Next(1, 3);
                if (Main.rand.Next(3) == 0)
                {
                    num14++;
                }
                for (int l = 0; l < num14; l++)
                {
                    pointPoisition = new(player.position.X + player.width * 0.5f + Main.rand.Next(61) * -player.direction + (aimWorld.X + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                    pointPoisition.X = (pointPoisition.X * 10f + player.Center.X) / 11f + Main.rand.Next(-30, 31);
                    pointPoisition.Y -= 150f * Main.rand.NextFloat();
                    num2 = aimWorld.X - pointPoisition.X;
                    num3 = aimWorld.Y - pointPoisition.Y;
                    if (num3 < 0f) num3 *= -1f;
                    
                    if (num3 < 20f) num3 = 20f;
                    
                    num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
                    num4 = speed / num4;
                    num2 *= num4;
                    num3 *= num4;
                    float num15 = num2 + Main.rand.Next(-20, 21) * 0.03f;
                    float speedY2 = num3 + Main.rand.Next(-40, 41) * 0.03f;
                    num15 *= Main.rand.Next(55, 80) * 0.01f;
                    pointPoisition.X += Main.rand.Next(-50, 51);
                    int num16 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num15, speedY2, projToShoot, Damage, KnockBack, player.whoAmI);
                    Main.projectile[num16].noDropItem = true;
                }
            }
            else if (sItem.type == ItemID.Minishark || sItem.type == ItemID.Megashark)
            {
                float speedX = num2 + Main.rand.Next(-40, 41) * 0.01f;
                float speedY3 = num3 + Main.rand.Next(-40, 41) * 0.01f;
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, speedX, speedY3, projToShoot, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.SnowballCannon)
            {
                float speedX2 = num2 + Main.rand.Next(-40, 41) * 0.02f;
                float speedY4 = num3 + Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, speedX2, speedY4, projToShoot, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.NailGun)
            {
                float speedX3 = num2 + Main.rand.Next(-40, 41) * 0.02f;
                float speedY5 = num3 + Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, speedX3, speedY5, projToShoot, Damage, KnockBack, player.whoAmI);
            }
            else if (ProjectileID.Sets.IsAGolfBall[projToShoot])
            {
                Vector2 vector8 = aimWorld;
                Vector2 vector9 = vector8 - player.Center;
                bool flag2 = false;
                if (vector9.Length() < 100f)
                {
                    flag2 = player.TryPlacingAGolfBallNearANearbyTee(vector8);
                }
                if (!flag2)
                {
                    if (vector9.Length() > 100f || !Collision.CanHit(player.Center, 1, 1, vector8, 1, 1))
                    {
                        Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot, Damage, KnockBack, player.whoAmI);
                    }
                    else
                    {
                        Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, vector8.X, vector8.Y, 0f, 0f, projToShoot, Damage, KnockBack, player.whoAmI);
                    }
                }
            }
            else if (sItem.type == ItemID.ShadowFlameHexDoll)
            {
                bool flag3 = false;
                if (player.itemAnimation <= player.itemTimeMax)
                {
                    flag3 = true;
                }
                Vector2 vector10 = default(Vector2);
                vector10 = new(num2, num3);
                vector10.Normalize();
                vector10 *= 4f;
                if (!flag3)
                {
                    Vector2 vector11 = default(Vector2);
                    vector11 = new(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                    vector11.Normalize();
                    vector10 += vector11;
                }
                vector10.Normalize();
                vector10 *= sItem.shootSpeed;
                float num17 = Main.rand.Next(10, 80) * 0.001f;
                if (Main.rand.Next(2) == 0)
                {
                    num17 *= -1f;
                }
                float num18 = Main.rand.Next(10, 80) * 0.001f;
                if (Main.rand.Next(2) == 0)
                {
                    num18 *= -1f;
                }
                if (flag3)
                {
                    num18 = (num17 = 0f);
                }
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, vector10.X, vector10.Y, projToShoot, Damage, KnockBack, player.whoAmI, num18, num17);
            }
            else if (sItem.type == ItemID.HellwingBow)
            {
                Vector2 vector12 = default(Vector2);
                vector12 = new(num2, num3);
                float num19 = vector12.Length();
                vector12.X += Main.rand.Next(-100, 101) * 0.01f * num19 * 0.15f;
                vector12.Y += Main.rand.Next(-100, 101) * 0.01f * num19 * 0.15f;
                float num20 = num2 + Main.rand.Next(-40, 41) * 0.03f;
                float num21 = num3 + Main.rand.Next(-40, 41) * 0.03f;
                vector12.Normalize();
                vector12 *= num19;
                num20 *= Main.rand.Next(50, 150) * 0.01f;
                num21 *= Main.rand.Next(50, 150) * 0.01f;
                Vector2 vector13 = default(Vector2);
                vector13 = new(num20, num21);
                vector13.X += Main.rand.Next(-100, 101) * 0.025f;
                vector13.Y += Main.rand.Next(-100, 101) * 0.025f;
                vector13.Normalize();
                vector13 *= num19;
                num20 = vector13.X;
                num21 = vector13.Y;
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num20, num21, projToShoot, Damage, KnockBack, player.whoAmI, vector12.X, vector12.Y);
            }
            else if (sItem.type == ItemID.Xenopopper)
            {
                Vector2 vector14 = Vector2.Normalize(new Vector2(num2, num3)) * 40f * sItem.scale;
                if (Collision.CanHit(pointPoisition, 0, 0, pointPoisition + vector14, 0, 0))
                {
                    pointPoisition += vector14;
                }
                float ai = Utils.ToRotation(new Vector2(num2, num3));
                float num22 = (float)Math.PI * 2f / 3f;
                int num23 = Main.rand.Next(4, 5);
                if (Main.rand.Next(4) == 0)
                {
                    num23++;
                }
                for (int m = 0; m < num23; m++)
                {
                    float num24 = (float)Main.rand.NextDouble() * 0.2f + 0.05f;
                    Vector2 spinningpoint4 = new Vector2(num2, num3);
                    double radians5 = num22 * (float)Main.rand.NextDouble() - num22 / 2f;
                    val = default(Vector2);
                    Vector2 vector15 = Utils.RotatedBy(spinningpoint4, radians5, val) * num24;
                    int num25 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, vector15.X, vector15.Y, ProjectileID.Xenopopper, Damage, KnockBack, player.whoAmI, ai);
                    Main.projectile[num25].localAI[0] = projToShoot;
                    Main.projectile[num25].localAI[1] = speed;
                }
            }
            else if (sItem.type == ItemID.Gatligator)
            {
                float num26 = num2 + Main.rand.Next(-40, 41) * 0.05f;
                float num27 = num3 + Main.rand.Next(-40, 41) * 0.05f;
                if (Main.rand.Next(3) == 0)
                {
                    num26 *= 1f + Main.rand.Next(-30, 31) * 0.02f;
                    num27 *= 1f + Main.rand.Next(-30, 31) * 0.02f;
                }
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num26, num27, projToShoot, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.PewMaticHorn)
            {
                float speedX4 = num2 + Main.rand.Next(-15, 16) * 0.075f;
                float speedY6 = num3 + Main.rand.Next(-15, 16) * 0.075f;
                int num28 = Main.rand.Next(Main.projFrames[sItem.shoot]);
                int damage2 = Damage;
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, speedX4, speedY6, projToShoot, damage2, KnockBack, player.whoAmI, 0f, num28);
            }
            else if (sItem.type == ItemID.Razorpine)
            {
                int num29 = 2 + Main.rand.Next(3);
                for (int n = 0; n < num29; n++)
                {
                    float num30 = num2;
                    float num31 = num3;
                    float num32 = 0.025f * n;
                    num30 += Main.rand.Next(-35, 36) * num32;
                    num31 += Main.rand.Next(-35, 36) * num32;
                    num4 = (float)Math.Sqrt(num30 * num30 + num31 * num31);
                    num4 = speed / num4;
                    num30 *= num4;
                    num31 *= num4;
                    float x = pointPoisition.X + num2 * (num29 - n) * 1.75f;
                    float y = pointPoisition.Y + num3 * (num29 - n) * 1.75f;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, x, y, num30, num31, projToShoot, Damage, KnockBack, player.whoAmI, Main.rand.Next(0, 10 * (n + 1)));
                }
            }
            else if (sItem.type == ItemID.BlizzardStaff)
            {
                int num33 = 2;
                for (int num34 = 0; num34 < num33; num34++)
                {
                    pointPoisition = new(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -player.direction + (aimWorld.X + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                    pointPoisition.X = (pointPoisition.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
                    pointPoisition.Y -= 100 * num34;
                    num2 = aimWorld.X - pointPoisition.X;
                    num3 = aimWorld.Y - pointPoisition.Y;
                    /*if (player.gravDir == -1f)
                    {
                        num3 = Main.screenPosition.Y + (float)Main.screenHeight - (float)aimWorld.Y - pointPoisition.Y;
                    }*/
                    if (num3 < 0f)
                    {
                        num3 *= -1f;
                    }
                    if (num3 < 20f)
                    {
                        num3 = 20f;
                    }
                    num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
                    num4 = speed / num4;
                    num2 *= num4;
                    num3 *= num4;
                    float speedX5 = num2 + Main.rand.Next(-40, 41) * 0.02f;
                    float speedY7 = num3 + Main.rand.Next(-40, 41) * 0.02f;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, speedX5, speedY7, projToShoot, Damage, KnockBack, player.whoAmI, 0f, Main.rand.Next(5));
                }
            }
            else if (sItem.type == ItemID.MeteorStaff)
            {
                int num35 = 1;
                for (int num36 = 0; num36 < num35; num36++)
                {
                    pointPoisition = new(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -player.direction + (aimWorld.X + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                    pointPoisition.X = (pointPoisition.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
                    pointPoisition.Y -= 100 * num36;
                    num2 = aimWorld.X - pointPoisition.X + Main.rand.Next(-40, 41) * 0.03f;
                    num3 = aimWorld.Y - pointPoisition.Y;
                    if (player.gravDir == -1f)
                    {
                        num3 = Main.screenPosition.Y + Main.screenHeight - aimWorld.Y - pointPoisition.Y;
                    }
                    if (num3 < 0f)
                    {
                        num3 *= -1f;
                    }
                    if (num3 < 20f)
                    {
                        num3 = 20f;
                    }
                    num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
                    num4 = speed / num4;
                    num2 *= num4;
                    num3 *= num4;
                    float num37 = num2;
                    float num38 = num3 + Main.rand.Next(-40, 41) * 0.02f;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num37 * 0.75f, num38 * 0.75f, projToShoot + Main.rand.Next(3), Damage, KnockBack, player.whoAmI, 0f, 0.5f + (float)Main.rand.NextDouble() * 0.3f);
                }
            }
            else if (sItem.type == ItemID.LunarFlareBook)
            {
                int num39 = 3;
                for (int num40 = 0; num40 < num39; num40++)
                {
                    pointPoisition = new(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -player.direction + (aimWorld.X + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                    pointPoisition.X = (pointPoisition.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
                    pointPoisition.Y -= 100 * num40;
                    num2 = aimWorld.X - pointPoisition.X;
                    num3 = aimWorld.Y - pointPoisition.Y;
                    float ai2 = num3 + pointPoisition.Y;
                    if (num3 < 0f)
                    {
                        num3 *= -1f;
                    }
                    if (num3 < 20f)
                    {
                        num3 = 20f;
                    }
                    num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
                    num4 = speed / num4;
                    num2 *= num4;
                    num3 *= num4;
                    Vector2 vector16 = new Vector2(num2, num3) / 2f;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, vector16.X, vector16.Y, projToShoot, Damage, KnockBack, player.whoAmI, 0f, ai2);
                }
            }
            else if (sItem.type == ItemID.PrincessWeapon)
            {
                Vector2 farthestSpawnPositionOnLine = player.GetFarthestSpawnPositionOnLine(pointPoisition, num2, num3);
                Vector2 zero = Vector2.Zero;
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, farthestSpawnPositionOnLine, zero, projToShoot, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.StarWrath)
            {
                Vector2 mousePos = aimWorld;
                float num41 = mousePos.Y;
                if (num41 > player.Center.Y - 200f)
                {
                    num41 = player.Center.Y - 200f;
                }
                for (int num42 = 0; num42 < 3; num42++)
                {
                    pointPoisition = player.Center + new Vector2(-Main.rand.Next(0, 401) * player.direction, -600f);
                    pointPoisition.Y -= 100 * num42;
                    Vector2 vector18 = mousePos - pointPoisition;
                    if (vector18.Y < 0f)
                    {
                        vector18.Y *= -1f;
                    }
                    if (vector18.Y < 20f)
                    {
                        vector18.Y = 20f;
                    }
                    vector18.Normalize();
                    vector18 *= speed;
                    num2 = vector18.X;
                    num3 = vector18.Y;
                    float speedX6 = num2;
                    float speedY8 = num3 + Main.rand.Next(-40, 41) * 0.02f;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, speedX6, speedY8, projToShoot, Damage, KnockBack, player.whoAmI, 0f, num41);
                }
            }
            else if (sItem.type == ItemID.Tsunami)
            {
                float num43 = (float)Math.PI / 10f;
                int num44 = 5;
                Vector2 vector19 = default(Vector2);
                vector19 = new(num2, num3);
                vector19.Normalize();
                vector19 *= 40f;
                bool flag4 = Collision.CanHit(pointPoisition, 0, 0, pointPoisition + vector19, 0, 0);
                for (int num45 = 0; num45 < num44; num45++)
                {
                    float num46 = num45 - (num44 - 1f) / 2f;
                    Vector2 spinningpoint5 = vector19;
                    double radians6 = num43 * num46;
                    val = default(Vector2);
                    Vector2 vector20 = spinningpoint5.RotatedBy(radians6, val);
                    if (!flag4)
                    {
                        vector20 -= vector19;
                    }
                    int num47 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X + vector20.X, pointPoisition.Y + vector20.Y, num2, num3, projToShoot, Damage, KnockBack, player.whoAmI);
                    Main.projectile[num47].noDropItem = true;
                }
            }
            else if (sItem.type == ItemID.ChainGun)
            {
                float speedX7 = num2 + Main.rand.Next(-40, 41) * 0.03f;
                float speedY9 = num3 + Main.rand.Next(-40, 41) * 0.03f;
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, speedX7, speedY9, projToShoot, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.SDMG)
            {
                float speedX8 = num2 + Main.rand.Next(-40, 41) * 0.005f;
                float speedY10 = num3 + Main.rand.Next(-40, 41) * 0.005f;
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, speedX8, speedY10, projToShoot, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.CrystalStorm)
            {
                float num48 = num2;
                float num49 = num3;
                num48 += Main.rand.Next(-40, 41) * 0.04f;
                num49 += Main.rand.Next(-40, 41) * 0.04f;
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num48, num49, projToShoot, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.Uzi)
            {
                float num50 = num2;
                float num51 = num3;
                num50 += Main.rand.Next(-30, 31) * 0.03f;
                num51 += Main.rand.Next(-30, 31) * 0.03f;
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num50, num51, projToShoot, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.MysticCoilSnake)
            {
                float num52 = 2.6666667f;
                _ = player.Bottom;
                _ = (int)player.Bottom.X / 16;
                int num53 = 4;
                float num54 = Math.Abs(aimWorld.X + Main.screenPosition.X - player.position.X) / 16f;
                if (player.direction < 0)
                {
                    num54 += 1f;
                }
                num53 = (int)num54;
                if (num53 > 15)
                {
                    num53 = 15;
                }
                Point point = player.Center.ToTileCoordinates();
                int maxDistance = 31;
                for (int num55 = num53; num55 >= 0; num55--)
                {
                    if (Collision.CanHitLine(player.Center, 1, 1, player.Center + new Vector2(16 * num55 * player.direction, 0f), 1, 1) && WorldUtils.Find(new Point(point.X + player.direction * num55, point.Y), Searches.Chain(new Searches.Down(maxDistance), new Terraria.WorldBuilding.Conditions.MysticSnake()), out var result))
                    {
                        int num56 = result.Y;
                        while (Main.tile[result.X, num56 - 1].HasTile)
                        {
                            num56--;
                            if (Main.tile[result.X, num56 - 1] == null || num56 < 10 || result.Y - num56 > 7)
                            {
                                num56 = -1;
                                break;
                            }
                        }
                        if (num56 >= 10)
                        {
                            result.Y = num56;
                            for (int num57 = 0; num57 < 1000; num57++)
                            {
                                Projectile projectile = Main.projectile[num57];
                                if (projectile.active && projectile.owner == player.whoAmI && projectile.type == projToShoot)
                                {
                                    if (projectile.ai[1] == 2f)
                                    {
                                        projectile.timeLeft = 4;
                                    }
                                    else
                                    {
                                        projectile.Kill();
                                    }
                                }
                            }
                            Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, result.X * 16 + 8, result.Y * 16 + 8 - 16, 0f, 0f - num52, projToShoot, Damage, KnockBack, player.whoAmI, result.Y * 16 + 8 - 16);
                            break;
                        }
                    }
                }
            }
            else if (sItem.type == ItemID.FairyQueenMagicItem)
            {
                Vector2 vector21 = Main.rand.NextVector2Circular(1f, 1f) + Main.rand.NextVector2CircularEdge(3f, 3f);
                if (vector21.Y > 0f)
                {
                    vector21.Y *= -1f;
                }
                float num58 = player.itemAnimation / (float)player.itemAnimationMax * 0.66f + player.miscCounterNormalized;
                pointPoisition = player.MountedCenter + new Vector2(player.direction * 15, player.gravDir * 3f);
                Point point2 = pointPoisition.ToTileCoordinates();
                Tile tile = Main.tile[point2.X, point2.Y];
                if (tile != null && tile.HasTile && Main.tileSolid[tile.TileType] && !Main.tileSolidTop[tile.TileType] && !TileID.Sets.Platforms[tile.TileType])
                {
                    pointPoisition = player.MountedCenter;
                }
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, vector21.X, vector21.Y, projToShoot, Damage, KnockBack, player.whoAmI, -1f, num58 % 1f);
            }
            else if (sItem.type == ItemID.FairyQueenRangedItem)
            {
                float num59 = (float)Math.PI / 10f;
                int num60 = 5;
                Vector2 vector22 = default(Vector2);
                vector22 = new(num2, num3);
                vector22.Normalize();
                vector22 *= 40f;
                bool num61 = Collision.CanHit(pointPoisition, 0, 0, pointPoisition + vector22, 0, 0);
                int num62 = (player.itemAnimationMax - player.itemAnimation) / 2;
                int num63 = num62;
                if (player.direction == 1)
                {
                    num63 = 4 - num62;
                }
                float num64 = num63 - (num60 - 1f) / 2f;
                Vector2 spinningpoint6 = vector22;
                double radians7 = num59 * num64;
                val = default(Vector2);
                Vector2 vector23 = spinningpoint6.RotatedBy(radians7, val);
                if (!num61)
                {
                    vector23 -= vector22;
                }
                Vector2 mouseWorld = aimWorld;
                Vector2 origin = pointPoisition + vector23;
                Vector2 vector24 = origin.DirectionTo(mouseWorld).SafeNormalize(-Vector2.UnitY);
                Vector2 value2 = player.Center.DirectionTo(player.Center + new Vector2(num2, num3)).SafeNormalize(-Vector2.UnitY);
                float lerpValue = Utils.GetLerpValue(100f, 40f, mouseWorld.Distance(player.Center), clamped: true);
                if (lerpValue > 0f)
                {
                    vector24 = Vector2.Lerp(vector24, value2, lerpValue).SafeNormalize(Utils.SafeNormalize(new Vector2(num2, num3), -Vector2.UnitY));
                }
                Vector2 v3 = vector24 * speed;
                if (num62 == 2)
                {
                    projToShoot = 932;
                    Damage *= 2;
                }
                if (projToShoot == 932)
                {
                    float ai3 = player.miscCounterNormalized * 12f % 1f;
                    v3 = v3.SafeNormalize(Vector2.Zero) * (speed * 2f);
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, origin, v3, projToShoot, Damage, KnockBack, player.whoAmI, 0f, ai3);
                }
                else
                {
                    int num65 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, origin, v3, projToShoot, Damage, KnockBack, player.whoAmI);
                    Main.projectile[num65].noDropItem = true;
                }
            }
            else if (sItem.type == ItemID.Shotgun)
            {
                int num66 = Main.rand.Next(4, 6);
                for (int num67 = 0; num67 < num66; num67++)
                {
                    float num68 = num2;
                    float num69 = num3;
                    num68 += Main.rand.Next(-40, 41) * 0.05f;
                    num69 += Main.rand.Next(-40, 41) * 0.05f;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num68, num69, projToShoot, Damage, KnockBack, player.whoAmI);
                }
            }
            else if (sItem.type == ItemID.QuadBarrelShotgun)
            {
                float num70 = (float)Math.PI / 2f;
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot, Damage, KnockBack, player.whoAmI);
                Vector2 v4 = default(Vector2);
                for (int num71 = 0; num71 < 7; num71++)
                {
                    v4 = new(num2, num3);
                    float num72 = v4.Length();
                    Vector2 val7 = v4;
                    Vector2 spinningpoint7 = v4.SafeNormalize(Vector2.Zero);
                    double radians8 = num70 * Main.rand.NextFloat();
                    val = default(Vector2);
                    v4 = val7 + spinningpoint7.RotatedBy(radians8, val) * Main.rand.NextFloatDirection() * 5f;
                    v4 = v4.SafeNormalize(Vector2.Zero) * num72;
                    float x2 = v4.X;
                    float y2 = v4.Y;
                    x2 += Main.rand.Next(-40, 41) * 0.05f;
                    y2 += Main.rand.Next(-40, 41) * 0.05f;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, x2, y2, projToShoot, Damage, KnockBack, player.whoAmI);
                }
            }
            else if (sItem.type == ItemID.SharpTears)
            {
                Vector2 pointPoisition2 = aimWorld;
                player.LimitPointToPlayerReachableArea(ref pointPoisition2);
                Vector2 vector25 = pointPoisition2 + Main.rand.NextVector2Circular(8f, 8f);
                Vector2 vector26 = FindSharpTearsSpot(player,vector25).ToWorldCoordinates(Main.rand.Next(17), Main.rand.Next(17));
                Vector2 vector27 = (vector25 - vector26).SafeNormalize(-Vector2.UnitY) * 16f;
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, vector26.X, vector26.Y, vector27.X, vector27.Y, projToShoot, Damage, KnockBack, player.whoAmI, 0f, Main.rand.NextFloat() * 0.5f + 0.6f);
            }
            else if (sItem.type == ItemID.SparkleGuitar)
            {
                Vector2 vector28 = aimWorld;
                List<NPC> validTargets;
                bool sparkleGuitarTarget = GetSparkleGuitarTarget(player,out validTargets);
                if (sparkleGuitarTarget)
                {
                    NPC nPC = validTargets[Main.rand.Next(validTargets.Count)];
                    vector28 = nPC.Center + nPC.velocity * 20f;
                }
                Vector2 vector29 = vector28 - player.Center;
                if (!sparkleGuitarTarget)
                {
                    vector28 += Main.rand.NextVector2Circular(24f, 24f);
                    if (vector29.Length() > 700f)
                    {
                        vector29 *= 700f / vector29.Length();
                        vector28 = player.Center + vector29;
                    }
                }
                Vector2 vector30 = Main.rand.NextVector2CircularEdge(1f, 1f);
                if (vector30.Y > 0f)
                {
                    vector30 *= -1f;
                }
                if (Math.Abs(vector30.Y) < 0.5f)
                {
                    vector30.Y = (0f - Main.rand.NextFloat()) * 0.5f - 0.5f;
                }
                vector30 *= vector29.Length() * 2f;
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, vector30.X, vector30.Y, projToShoot, Damage, KnockBack, player.whoAmI, vector28.X, vector28.Y);
            }
            else if (sItem.type == ItemID.FirstFractal)
            {
                Vector2 vector31 = aimWorld;
                List<NPC> validTargets2;
                bool sparkleGuitarTarget2 = GetSparkleGuitarTarget(player,out validTargets2);
                if (sparkleGuitarTarget2)
                {
                    NPC nPC2 = validTargets2[Main.rand.Next(validTargets2.Count)];
                    vector31 = nPC2.Center + nPC2.velocity * 20f;
                }
                Vector2 vector32 = vector31 - player.Center;
                Vector2 vector33 = Main.rand.NextVector2CircularEdge(1f, 1f);
                float num73 = 1f;
                int num74 = 1;
                Vector2 vector35 = default(Vector2);
                for (int num75 = 0; num75 < num74; num75++)
                {
                    if (!sparkleGuitarTarget2)
                    {
                        vector31 += Main.rand.NextVector2Circular(24f, 24f);
                        if (vector32.Length() > 700f)
                        {
                            vector32 *= 700f / vector32.Length();
                            vector31 = player.Center + vector32;
                        }
                        float num76 = Utils.GetLerpValue(0f, 6f, velocity.Length(), clamped: true) * 0.8f;
                        vector33 *= 1f - num76;
                        vector33 += velocity * num76;
                        vector33 = vector33.SafeNormalize(Vector2.UnitX);
                    }
                    float num77 = 60f;
                    float num78 = Main.rand.NextFloatDirection() * (float)Math.PI * (1f / num77) * 0.5f * num73;
                    float num79 = num77 / 2f;
                    float num80 = 12f + Main.rand.NextFloat() * 2f;
                    Vector2 vector34 = vector33 * num80;
                    vector35 = new(0f, 0f);
                    Vector2 vector36 = vector34;
                    for (int num81 = 0; num81 < num79; num81++)
                    {
                        vector35 += vector36;
                        Vector2 spinningpoint8 = vector36;
                        double radians9 = num78;
                        val = default(Vector2);
                        vector36 = spinningpoint8.RotatedBy(radians9, val);
                    }
                    Vector2 vector37 = -vector35;
                    Vector2 vector38 = vector31 + vector37;
                    float lerpValue2 = Utils.GetLerpValue(player.itemAnimationMax, 0f, player.itemAnimation, clamped: true);
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, vector38, vector34, projToShoot, Damage, KnockBack, player.whoAmI, num78, lerpValue2);
                }
            }
            else if (sItem.type == ItemID.StormTigerStaff)
            {
                int minionProjectileId = projToShoot;
                float knockBack = KnockBack;
                val = default(Vector2);
                Vector2 offsetFromCursor = val;
                val = default(Vector2);
                player.SpawnMinionOnCursor(projectileSource_Item_WithPotentialAmmo, player.whoAmI, minionProjectileId, damage, knockBack, offsetFromCursor, val);
            }
            else if (sItem.type == ItemID.FlinxStaff)
            {
                int minionProjectileId2 = projToShoot;
                float knockBack2 = KnockBack;
                val = default(Vector2);
                Vector2 offsetFromCursor2 = val;
                val = default(Vector2);
                player.SpawnMinionOnCursor(projectileSource_Item_WithPotentialAmmo, player.whoAmI, minionProjectileId2, damage, knockBack2, offsetFromCursor2, val);
            }
            else if (sItem.type == ItemID.AbigailsFlower)
            {
                int minionProjectileId3 = projToShoot;
                float knockBack3 = KnockBack;
                val = default(Vector2);
                Vector2 offsetFromCursor3 = val;
                val = default(Vector2);
                player.SpawnMinionOnCursor(projectileSource_Item_WithPotentialAmmo, player.whoAmI, minionProjectileId3, damage, knockBack3, offsetFromCursor3, val);
            }
            else if (sItem.type == ItemID.VenomStaff)
            {
                int num82 = 4;
                if (Main.rand.Next(3) == 0)
                {
                    num82++;
                }
                if (Main.rand.Next(4) == 0)
                {
                    num82++;
                }
                if (Main.rand.Next(5) == 0)
                {
                    num82++;
                }
                for (int num83 = 0; num83 < num82; num83++)
                {
                    float num84 = num2;
                    float num85 = num3;
                    float num86 = 0.05f * num83;
                    num84 += Main.rand.Next(-35, 36) * num86;
                    num85 += Main.rand.Next(-35, 36) * num86;
                    num4 = (float)Math.Sqrt(num84 * num84 + num85 * num85);
                    num4 = speed / num4;
                    num84 *= num4;
                    num85 *= num4;
                    float x3 = pointPoisition.X;
                    float y3 = pointPoisition.Y;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, x3, y3, num84, num85, projToShoot, Damage, KnockBack, player.whoAmI);
                }
            }
            else if (sItem.type == ItemID.PoisonStaff)
            {
                int num87 = 3;
                if (Main.rand.Next(3) == 0)
                {
                    num87++;
                }
                for (int num88 = 0; num88 < num87; num88++)
                {
                    float num89 = num2;
                    float num90 = num3;
                    float num91 = 0.05f * num88;
                    num89 += Main.rand.Next(-35, 36) * num91;
                    num90 += Main.rand.Next(-35, 36) * num91;
                    num4 = (float)Math.Sqrt(num89 * num89 + num90 * num90);
                    num4 = speed / num4;
                    num89 *= num4;
                    num90 *= num4;
                    float x4 = pointPoisition.X;
                    float y4 = pointPoisition.Y;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, x4, y4, num89, num90, projToShoot, Damage, KnockBack, player.whoAmI);
                }
            }
            else if (sItem.type == ItemID.Stynger)
            {
                float num92 = num2;
                float num93 = num3;
                num92 += Main.rand.Next(-40, 41) * 0.01f;
                num93 += Main.rand.Next(-40, 41) * 0.01f;
                pointPoisition.X += Main.rand.Next(-40, 41) * 0.05f;
                pointPoisition.Y += Main.rand.Next(-45, 36) * 0.05f;
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num92, num93, projToShoot, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.Boomstick)
            {
                int num94 = Main.rand.Next(3, 5);
                for (int num95 = 0; num95 < num94; num95++)
                {
                    float num96 = num2;
                    float num97 = num3;
                    num96 += Main.rand.Next(-35, 36) * 0.04f;
                    num97 += Main.rand.Next(-35, 36) * 0.04f;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num96, num97, projToShoot, Damage, KnockBack, player.whoAmI);
                }
            }
            else if (sItem.type == ItemID.VampireKnives)
            {
                int num98 = 4;
                if (Main.rand.Next(2) == 0)
                {
                    num98++;
                }
                if (Main.rand.Next(4) == 0)
                {
                    num98++;
                }
                if (Main.rand.Next(8) == 0)
                {
                    num98++;
                }
                if (Main.rand.Next(16) == 0)
                {
                    num98++;
                }
                for (int num99 = 0; num99 < num98; num99++)
                {
                    float num100 = num2;
                    float num101 = num3;
                    float num102 = 0.05f * num99;
                    num100 += Main.rand.Next(-35, 36) * num102;
                    num101 += Main.rand.Next(-35, 36) * num102;
                    num4 = (float)Math.Sqrt(num100 * num100 + num101 * num101);
                    num4 = speed / num4;
                    num100 *= num4;
                    num101 *= num4;
                    float x5 = pointPoisition.X;
                    float y5 = pointPoisition.Y;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, x5, y5, num100, num101, projToShoot, Damage, KnockBack, player.whoAmI);
                }
            }
            else if (sItem.type == ItemID.StaffoftheFrostHydra || sItem.type == ItemID.QueenSpiderStaff || sItem.type == ItemID.RainbowCrystalStaff || sItem.type == ItemID.MoonlordTurretStaff || sItem.type == ItemID.HoundiusShootius)
            {
                bool num103 = sItem.type == ItemID.RainbowCrystalStaff || sItem.type == ItemID.MoonlordTurretStaff;
                int num104 = (int)(aimWorld.X + Main.screenPosition.X) / 16;
                int num105 = (int)(aimWorld.Y + Main.screenPosition.Y) / 16;
                if (player.gravDir == -1f)
                {
                    num105 = (int)(Main.screenPosition.Y + Main.screenHeight - aimWorld.Y) / 16;
                }
                if (!num103)
                {
                    for (; num105 < Main.maxTilesY - 10 && Main.tile[num104, num105] != null && !WorldGen.SolidTile2(num104, num105) && Main.tile[num104 - 1, num105] != null && !WorldGen.SolidTile2(num104 - 1, num105) && Main.tile[num104 + 1, num105] != null && !WorldGen.SolidTile2(num104 + 1, num105); num105++)
                    {
                    }
                    num105--;
                }
                int num106 = 0;
                switch (sItem.type)
                {
                    case ItemID.StaffoftheFrostHydra:
                        num106 = 60;
                        break;
                    case ItemID.HoundiusShootius:
                        num106 = 90;
                        break;
                }
                int num107 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, aimWorld.X, num105 * 16 - 24, 0f, 15f, projToShoot, Damage, KnockBack, player.whoAmI, num106);
                Main.projectile[num107].originalDamage = damage;
                player.UpdateMaxTurrets();
            }
            else if (sItem.type == ItemID.NimbusRod || sItem.type == ItemID.CrimsonRod)
            {
                int num108 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot, Damage, KnockBack, player.whoAmI);
                Main.projectile[num108].ai[0] = aimWorld.X;
                Main.projectile[num108].ai[1] = aimWorld.Y;
            }
            else if (sItem.type == ItemID.ChlorophyteShotbow)
            {
                int num109 = 2;
                if (Main.rand.Next(3) == 0)
                {
                    num109++;
                }
                for (int num110 = 0; num110 < num109; num110++)
                {
                    float num111 = num2;
                    float num112 = num3;
                    if (num110 > 0)
                    {
                        num111 += Main.rand.Next(-35, 36) * 0.04f;
                        num112 += Main.rand.Next(-35, 36) * 0.04f;
                    }
                    if (num110 > 1)
                    {
                        num111 += Main.rand.Next(-35, 36) * 0.04f;
                        num112 += Main.rand.Next(-35, 36) * 0.04f;
                    }
                    if (num110 > 2)
                    {
                        num111 += Main.rand.Next(-35, 36) * 0.04f;
                        num112 += Main.rand.Next(-35, 36) * 0.04f;
                    }
                    int num113 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num111, num112, projToShoot, Damage, KnockBack, player.whoAmI);
                    Main.projectile[num113].noDropItem = true;
                }
            }
            else if (sItem.type == ItemID.BeeGun)
            {
                int num114 = Main.rand.Next(1, 4);
                if (Main.rand.Next(6) == 0)
                {
                    num114++;
                }
                if (Main.rand.Next(6) == 0)
                {
                    num114++;
                }
                if (player.strongBees && Main.rand.Next(3) == 0)
                {
                    num114++;
                }
                for (int num115 = 0; num115 < num114; num115++)
                {
                    float num116 = num2;
                    float num117 = num3;
                    num116 += Main.rand.Next(-35, 36) * 0.02f;
                    num117 += Main.rand.Next(-35, 36) * 0.02f;
                    int num118 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num116, num117, player.beeType(), player.beeDamage(Damage), player.beeKB(KnockBack), player.whoAmI);
                    Main.projectile[num118].DamageType = DamageClass.Magic;
                }
            }
            else if (sItem.type == ItemID.WaspGun)
            {
                int num119 = Main.rand.Next(2, 5);
                for (int num120 = 0; num120 < num119; num120++)
                {
                    float num121 = num2;
                    float num122 = num3;
                    num121 += Main.rand.Next(-35, 36) * 0.02f;
                    num122 += Main.rand.Next(-35, 36) * 0.02f;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num121, num122, projToShoot, Damage, KnockBack, player.whoAmI);
                }
            }
            else if (sItem.type == ItemID.BatScepter)
            {
                int num123 = Main.rand.Next(2, 4);
                for (int num124 = 0; num124 < num123; num124++)
                {
                    float num125 = num2;
                    float num126 = num3;
                    num125 += Main.rand.Next(-35, 36) * 0.05f;
                    num126 += Main.rand.Next(-35, 36) * 0.05f;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num125, num126, projToShoot, Damage, KnockBack, player.whoAmI);
                }
            }
            else if (sItem.type == ItemID.TacticalShotgun)
            {
                for (int num127 = 0; num127 < 6; num127++)
                {
                    float num128 = num2;
                    float num129 = num3;
                    num128 += Main.rand.Next(-40, 41) * 0.05f;
                    num129 += Main.rand.Next(-40, 41) * 0.05f;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num128, num129, projToShoot, Damage, KnockBack, player.whoAmI);
                }
            }
            else if (sItem.type == ItemID.PiranhaGun)
            {
                for (int num130 = 0; num130 < 3; num130++)
                {
                    float num131 = num2;
                    float num132 = num3;
                    num131 += Main.rand.Next(-40, 41) * 0.05f;
                    num132 += Main.rand.Next(-40, 41) * 0.05f;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num131, num132, projToShoot, Damage, KnockBack, player.whoAmI);
                }
            }
            else if (sItem.type == ItemID.ReleaseDoves)
            {
                for (int num133 = 0; num133 < 3; num133++)
                {
                    float num134 = num2;
                    float num135 = num3;
                    num134 += Main.rand.Next(-20, 21) * 0.1f;
                    num135 += Main.rand.Next(-20, 21) * 0.1f;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num134, num135, projToShoot, Damage, KnockBack, player.whoAmI);
                }
            }
            else if (sItem.type == ItemID.BubbleGun)
            {
                for (int num136 = 0; num136 < 3; num136++)
                {
                    float num137 = num2;
                    float num138 = num3;
                    num137 += Main.rand.Next(-40, 41) * 0.1f;
                    num138 += Main.rand.Next(-40, 41) * 0.1f;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num137, num138, projToShoot, Damage, KnockBack, player.whoAmI);
                }
            }
            else if (sItem.type == ItemID.Toxikarp)
            {
                Vector2 vector39 = default(Vector2);
                vector39 = new(num2, num3);
                vector39.X += Main.rand.Next(-30, 31) * 0.04f;
                vector39.Y += Main.rand.Next(-30, 31) * 0.03f;
                vector39.Normalize();
                vector39 *= Main.rand.Next(70, 91) * 0.1f;
                vector39.X += Main.rand.Next(-30, 31) * 0.04f;
                vector39.Y += Main.rand.Next(-30, 31) * 0.03f;
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, vector39.X, vector39.Y, projToShoot, Damage, KnockBack, player.whoAmI, Main.rand.Next(20));
            }
            else if (sItem.type == ItemID.ClockworkAssaultRifle)
            {
                float num139 = num2;
                float num140 = num3;
                if (player.itemAnimation < 5)
                {
                    num139 += Main.rand.Next(-40, 41) * 0.01f;
                    num140 += Main.rand.Next(-40, 41) * 0.01f;
                    num139 *= 1.1f;
                    num140 *= 1.1f;
                }
                else if (player.itemAnimation < 10)
                {
                    num139 += Main.rand.Next(-20, 21) * 0.01f;
                    num140 += Main.rand.Next(-20, 21) * 0.01f;
                    num139 *= 1.05f;
                    num140 *= 1.05f;
                }
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num139, num140, projToShoot, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.PygmyStaff)
            {
                projToShoot = Main.rand.Next(191, 195);
                int minionProjectileId4 = projToShoot;
                float knockBack4 = KnockBack;
                val = default(Vector2);
                Vector2 offsetFromCursor4 = val;
                val = default(Vector2);
                int num141 = player.SpawnMinionOnCursor(projectileSource_Item_WithPotentialAmmo, player.whoAmI, minionProjectileId4, damage, knockBack4, offsetFromCursor4, val);
                Main.projectile[num141].localAI[0] = 30f;
            }
            else if (sItem.type == ItemID.RavenStaff)
            {
                int minionProjectileId5 = projToShoot;
                float knockBack5 = KnockBack;
                val = default(Vector2);
                Vector2 offsetFromCursor5 = val;
                val = default(Vector2);
                player.SpawnMinionOnCursor(projectileSource_Item_WithPotentialAmmo, player.whoAmI, minionProjectileId5, damage, knockBack5, offsetFromCursor5, val);
            }
            else if (sItem.type == ItemID.HornetStaff || sItem.type == ItemID.ImpStaff)
            {
                int minionProjectileId6 = projToShoot;
                float knockBack6 = KnockBack;
                val = default(Vector2);
                Vector2 offsetFromCursor6 = val;
                val = default(Vector2);
                player.SpawnMinionOnCursor(projectileSource_Item_WithPotentialAmmo, player.whoAmI, minionProjectileId6, damage, knockBack6, offsetFromCursor6, val);
            }
            else if (sItem.type == ItemID.OpticStaff)
            {
                num2 = 0f;
                num3 = 0f;
                Vector2 spinningpoint9 = default(Vector2);
                spinningpoint9 = new(num2, num3);
                Vector2 spinningpoint10 = spinningpoint9;
                val = default(Vector2);
                spinningpoint9 = spinningpoint10.RotatedBy(1.5707963705062866, val);
                player.SpawnMinionOnCursor(projectileSource_Item_WithPotentialAmmo, player.whoAmI, projToShoot, damage, KnockBack, spinningpoint9, spinningpoint9);
                Vector2 spinningpoint11 = spinningpoint9;
                val = default(Vector2);
                spinningpoint9 = spinningpoint11.RotatedBy(-3.1415927410125732, val);
                player.SpawnMinionOnCursor(projectileSource_Item_WithPotentialAmmo, player.whoAmI, projToShoot + 1, damage, KnockBack, spinningpoint9, spinningpoint9);
            }
            else if (sItem.type == ItemID.SpiderStaff)
            {
                int minionProjectileId7 = projToShoot + player.nextCycledSpiderMinionType;
                float knockBack7 = KnockBack;
                val = default(Vector2);
                Vector2 offsetFromCursor7 = val;
                val = default(Vector2);
                player.SpawnMinionOnCursor(projectileSource_Item_WithPotentialAmmo, player.whoAmI, minionProjectileId7, damage, knockBack7, offsetFromCursor7, val);
                player.nextCycledSpiderMinionType++;
                player.nextCycledSpiderMinionType %= 3;
            }
            else if (sItem.type == ItemID.PirateStaff)
            {
                int minionProjectileId8 = projToShoot + Main.rand.Next(3);
                float knockBack8 = KnockBack;
                val = default(Vector2);
                Vector2 offsetFromCursor8 = val;
                val = default(Vector2);
                player.SpawnMinionOnCursor(projectileSource_Item_WithPotentialAmmo, player.whoAmI, minionProjectileId8, damage, knockBack8, offsetFromCursor8, val);
            }
            else if (sItem.type == ItemID.TempestStaff)
            {
                int minionProjectileId9 = projToShoot;
                float knockBack9 = KnockBack;
                val = default(Vector2);
                Vector2 offsetFromCursor9 = val;
                val = default(Vector2);
                player.SpawnMinionOnCursor(projectileSource_Item_WithPotentialAmmo, player.whoAmI, minionProjectileId9, damage, knockBack9, offsetFromCursor9, val);
            }
            else if (sItem.type == ItemID.XenoStaff || sItem.type == ItemID.DeadlySphereStaff || sItem.type == ItemID.StardustCellStaff || sItem.type == ItemID.VampireFrogStaff || sItem.type == ItemID.BabyBirdStaff)
            {
                int minionProjectileId10 = projToShoot;
                float knockBack10 = KnockBack;
                val = default(Vector2);
                Vector2 offsetFromCursor10 = val;
                val = default(Vector2);
                player.SpawnMinionOnCursor(projectileSource_Item_WithPotentialAmmo, player.whoAmI, minionProjectileId10, damage, knockBack10, offsetFromCursor10, val);
            }
            else if (sItem.type == ItemID.StardustDragonStaff)
            {
                int num142 = -1;
                int num143 = -1;
                for (int num144 = 0; num144 < 1000; num144++)
                {
                    if (Main.projectile[num144].active && Main.projectile[num144].owner == player.whoAmI)
                    {
                        if (num142 == -1 && Main.projectile[num144].type == ProjectileID.StardustDragon1)
                        {
                            num142 = num144;
                        }
                        if (num143 == -1 && Main.projectile[num144].type == ProjectileID.StardustDragon4)
                        {
                            num143 = num144;
                        }
                        if (num142 != -1 && num143 != -1)
                        {
                            break;
                        }
                    }
                }
                if (num142 == -1 && num143 == -1)
                {
                    num2 = 0f;
                    num3 = 0f;
                    pointPoisition = aimWorld;
                    int num145 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot, Damage, KnockBack, player.whoAmI);
                    int num146 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot + 1, Damage, KnockBack, player.whoAmI, num145);
                    int num147 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot + 2, Damage, KnockBack, player.whoAmI, num146);
                    int num148 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot + 3, Damage, KnockBack, player.whoAmI, num147);
                    Main.projectile[num146].localAI[1] = num147;
                    Main.projectile[num147].localAI[1] = num148;
                    Main.projectile[num145].originalDamage = damage;
                    Main.projectile[num146].originalDamage = damage;
                    Main.projectile[num147].originalDamage = damage;
                    Main.projectile[num148].originalDamage = damage;
                }
                else if (num142 != -1 && num143 != -1)
                {
                    int num149 = (int)Main.projectile[num143].ai[0];
                    int num150 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot + 1, Damage, KnockBack, player.whoAmI, num149);
                    int num151 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot + 2, Damage, KnockBack, player.whoAmI, num150);
                    Main.projectile[num150].localAI[1] = num151;
                    Main.projectile[num150].netUpdate = true;
                    Main.projectile[num150].ai[1] = 1f;
                    Main.projectile[num151].localAI[1] = num143;
                    Main.projectile[num151].netUpdate = true;
                    Main.projectile[num151].ai[1] = 1f;
                    Main.projectile[num143].ai[0] = num151;
                    Main.projectile[num143].netUpdate = true;
                    Main.projectile[num143].ai[1] = 1f;
                    Main.projectile[num150].originalDamage = damage;
                    Main.projectile[num151].originalDamage = damage;
                    Main.projectile[num143].originalDamage = damage;
                }
            }
            else if (sItem.type == ItemID.SlimeStaff || sItem.type == ItemID.Smolstar || sItem.type == ItemID.SanguineStaff || sItem.type == ItemID.EmpressBlade)
            {
                int minionProjectileId11 = projToShoot;
                float knockBack11 = KnockBack;
                val = default(Vector2);
                Vector2 offsetFromCursor11 = val;
                val = default(Vector2);
                player.SpawnMinionOnCursor(projectileSource_Item_WithPotentialAmmo, player.whoAmI, minionProjectileId11, damage, knockBack11, offsetFromCursor11, val);
            }
            else if (sItem.shoot > ProjectileID.None && (Main.projPet[sItem.shoot] || sItem.shoot == ProjectileID.BlueFairy || sItem.shoot == ProjectileID.ShadowOrb || sItem.shoot == ProjectileID.CrimsonHeart || sItem.shoot == ProjectileID.SuspiciousTentacle) && sItem.DamageType != DamageClass.Summon)
            {
                for (int num152 = 0; num152 < 1000; num152++)
                {
                    Projectile projectile2 = Main.projectile[num152];
                    if (projectile2.active && projectile2.owner == player.whoAmI)
                    {
                        if (sItem.shoot == ProjectileID.BlueFairy && (projectile2.type == ProjectileID.BlueFairy || projectile2.type == ProjectileID.PinkFairy || projectile2.type == ProjectileID.GreenFairy))
                        {
                            projectile2.Kill();
                        }
                        else if (sItem.type == ItemID.ResplendentDessert && (projectile2.type == ProjectileID.KingSlimePet || projectile2.type == ProjectileID.QueenSlimePet))
                        {
                            projectile2.Kill();
                        }
                        else if (sItem.shoot == projectile2.type)
                        {
                            projectile2.Kill();
                        }
                    }
                }
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot, 0, 0f, player.whoAmI);
            }
            else if (sItem.type == ItemID.SoulDrain)
            {
                pointPoisition = player.GetFarthestSpawnPositionOnLine(pointPoisition, num2, num3);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, 0f, 0f, projToShoot, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.ClingerStaff)
            {
                Vector2 pointPoisition3 = new Vector2
                {
                    X = aimWorld.X,
                    Y = aimWorld.Y
                };
                player.LimitPointToPlayerReachableArea(ref pointPoisition3);
                while (Collision.CanHitLine(player.position, player.width, player.height, pointPoisition, 1, 1))
                {
                    pointPoisition.X += num2;
                    pointPoisition.Y += num3;
                    val = pointPoisition - pointPoisition3;
                    if (val.Length() < 20f + Math.Abs(num2) + Math.Abs(num3))
                    {
                        pointPoisition = pointPoisition3;
                        break;
                    }
                }
                bool flag5 = false;
                int num153 = (int)pointPoisition.Y / 16;
                int num154 = (int)pointPoisition.X / 16;
                int num155;
                for (num155 = num153; num153 < Main.maxTilesY - 10 && num153 - num155 < 30 && !WorldGen.SolidTile(num154, num153) && !TileID.Sets.Platforms[Main.tile[num154, num153].TileType]; num153++)
                {
                }
                if (!WorldGen.SolidTile(num154, num153) && !TileID.Sets.Platforms[Main.tile[num154, num153].TileType])
                {
                    flag5 = true;
                }
                float num156 = num153 * 16;
                num153 = num155;
                while (num153 > 10 && num155 - num153 < 30 && !WorldGen.SolidTile(num154, num153))
                {
                    num153--;
                }
                float num157 = num153 * 16 + 16;
                float num158 = num156 - num157;
                int num159 = 15;
                if (num158 > 16 * num159)
                {
                    num158 = 16 * num159;
                }
                num157 = num156 - num158;
                pointPoisition.X = (int)(pointPoisition.X / 16f) * 16;
                if (!flag5)
                {
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, 0f, 0f, projToShoot, Damage, KnockBack, player.whoAmI, num157, num158);
                }
            }
            else if (sItem.type == ItemID.PortalGun)
            {
                int num160 = ((player.altFunctionUse == 2) ? 1 : 0);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot, Damage, KnockBack, player.whoAmI, 0f, num160);
            }
            else if (sItem.type == ItemID.SolarEruption)
            {
                float ai4 = (Main.rand.NextFloat() - 0.5f) * ((float)Math.PI / 4f);
                Vector2 vector40 = default(Vector2);
                vector40 = new(num2, num3);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, vector40.X, vector40.Y, projToShoot, Damage, KnockBack, player.whoAmI, 0f, ai4);
            }
            else if (sItem.type == ItemID.Zenith)
            {
                int num161 = (player.itemAnimationMax - player.itemAnimation) / player.itemTime;
                Vector2 vector41 = default(Vector2);
                vector41 = new(num2, num3);
                int num162 = FinalFractalHelper.GetRandomProfileIndex();
                if (num161 == 0)
                {
                    num162 = 4956;
                }
                Vector2 pointPoisition4 = aimWorld;
                player.LimitPointToPlayerReachableArea(ref pointPoisition4);
                Vector2 vector42 = pointPoisition4 - player.MountedCenter;
                if (num161 == 1 || num161 == 2)
                {
                    int npcTargetIndex;
                    bool zenithTarget = GetZenithTarget(player,pointPoisition4, 400f, out npcTargetIndex);
                    if (zenithTarget)
                    {
                        vector42 = Main.npc[npcTargetIndex].Center - player.MountedCenter;
                    }
                    bool flag6 = num161 == 2;
                    if (num161 == 1 && !zenithTarget)
                    {
                        flag6 = true;
                    }
                    if (flag6)
                    {
                        vector42 += Main.rand.NextVector2Circular(150f, 150f);
                    }
                }
                vector41 = vector42 / 2f;
                float ai5 = Main.rand.Next(-100, 101);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition, vector41, projToShoot, Damage, KnockBack, player.whoAmI, ai5, num162);
            }
            else if (sItem.type == ItemID.MonkStaffT2)
            {
                float ai6 = Main.rand.NextFloat() * speed * 0.75f * player.direction;
                val = new(num2, num3);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition, val, projToShoot, Damage, KnockBack, player.whoAmI, ai6);
            }
            else if (sItem.type == ItemID.MonkStaffT3)
            {
                bool num163 = player.altFunctionUse == 2;
                Vector2 vector43 = default(Vector2);
                vector43 = new(num2, num3);
                if (num163)
                {
                    vector43 *= 1.5f;
                    float ai7 = (0.3f + 0.7f * Main.rand.NextFloat()) * speed * 1.75f * player.direction;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition, vector43, ProjectileID.MonkStaffT3_Alt, (int)(Damage * 0.5f), KnockBack + 4f, player.whoAmI, ai7);
                }
                else
                {
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition, vector43, projToShoot, Damage, KnockBack, player.whoAmI);
                }
            }
            else if (sItem.type == ItemID.DD2BetsyBow)
            {
                Vector2 vector44 = default(Vector2);
                vector44 = new(num2, num3);
                projToShoot = 710;
                vector44 *= 0.8f;
                Vector2 vector45 = vector44.SafeNormalize(-Vector2.UnitY);
                float num164 = (float)Math.PI / 180f * -player.direction;
                for (float num165 = -2.5f; num165 < 3f; num165 += 1f)
                {
                    Vector2 val8 = pointPoisition;
                    Vector2 spinningpoint12 = vector44 + vector45 * num165 * 0.5f;
                    double radians10 = num165 * num164;
                    val = default(Vector2);
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, val8, spinningpoint12.RotatedBy(radians10, val), projToShoot, Damage, KnockBack, player.whoAmI);
                }
            }
            else if (sItem.type == ItemID.ApprenticeStaffT3)
            {
                Vector2 vector46 = Vector2.Normalize(new Vector2(num2, num3)) * 40f * sItem.scale;
                if (Collision.CanHit(pointPoisition, 0, 0, pointPoisition + vector46, 0, 0))
                {
                    pointPoisition += vector46;
                }
                Vector2 vector47 = default(Vector2);
                vector47 = new(num2, num3);
                vector47 *= 0.8f;
                Vector2 vector48 = vector47.SafeNormalize(-Vector2.UnitY);
                float num166 = (float)Math.PI / 180f * -player.direction;
                for (int num167 = 0; num167 <= 2; num167++)
                {
                    Vector2 val9 = pointPoisition;
                    Vector2 spinningpoint13 = vector47 + vector48 * num167 * 1f;
                    double radians11 = num167 * num166;
                    val = default(Vector2);
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, val9, spinningpoint13.RotatedBy(radians11, val), projToShoot, Damage, KnockBack, player.whoAmI);
                }
            }
            else if (sItem.type == ItemID.NebulaBlaze)
            {
                float num168 = (Main.rand.NextFloat() - 0.5f) * ((float)Math.PI / 4f) * 0.7f;
                for (int num169 = 0; num169 < 10; num169++)
                {
                    Vector2 position = pointPoisition;
                    Vector2 val10 = pointPoisition;
                    Vector2 spinningpoint14 = new Vector2(num2, num3);
                    double radians12 = num168;
                    val = default(Vector2);
                    if (Collision.CanHit(position, 0, 0, val10 + Utils.RotatedBy(spinningpoint14, radians12, val) * 100f, 0, 0))
                    {
                        break;
                    }
                    num168 = (Main.rand.NextFloat() - 0.5f) * ((float)Math.PI / 4f) * 0.7f;
                }
                Vector2 spinningpoint15 = new Vector2(num2, num3);
                double radians13 = num168;
                val = default(Vector2);
                Vector2 vector49 = Utils.RotatedBy(spinningpoint15, radians13, val) * (0.95f + Main.rand.NextFloat() * 0.3f);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, vector49.X, vector49.Y, projToShoot, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.SpiritFlame)
            {
                float num170 = Main.rand.NextFloat() * ((float)Math.PI * 2f);
                for (int num171 = 0; num171 < 10; num171++)
                {
                    Vector2 position2 = pointPoisition;
                    Vector2 val11 = pointPoisition;
                    Vector2 spinningpoint16 = new Vector2(num2, num3);
                    double radians14 = num170;
                    val = default(Vector2);
                    if (Collision.CanHit(position2, 0, 0, val11 + Utils.RotatedBy(spinningpoint16, radians14, val) * 100f, 0, 0))
                    {
                        break;
                    }
                    num170 = Main.rand.NextFloat() * ((float)Math.PI * 2f);
                }
                Vector2 spinningpoint17 = new Vector2(num2, num3);
                double radians15 = num170;
                val = default(Vector2);
                Vector2 vector50 = Utils.RotatedBy(spinningpoint17, radians15, val) * (0.95f + Main.rand.NextFloat() * 0.3f);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition + vector50 * 30f, Vector2.Zero, projToShoot, Damage, KnockBack, player.whoAmI, -2f);
            }
            else if (sItem.type == ItemID.SkyFracture)
            {
                float f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
                float value3 = 20f;
                float value4 = 60f;
                Vector2 vector51 = pointPoisition + f.ToRotationVector2() * MathHelper.Lerp(value3, value4, Main.rand.NextFloat());
                for (int num172 = 0; num172 < 50; num172++)
                {
                    vector51 = pointPoisition + f.ToRotationVector2() * MathHelper.Lerp(value3, value4, Main.rand.NextFloat());
                    if (Collision.CanHit(pointPoisition, 0, 0, vector51 + (vector51 - pointPoisition).SafeNormalize(Vector2.UnitX) * 8f, 0, 0))
                    {
                        break;
                    }
                    f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
                }
                Vector2 v5 = aimWorld - vector51;
                Vector2 vector52 = Utils.SafeNormalize(new Vector2(num2, num3), Vector2.UnitY) * speed;
                v5 = v5.SafeNormalize(vector52) * speed;
                v5 = Vector2.Lerp(v5, vector52, 0.25f);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, vector51, v5, projToShoot, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.OnyxBlaster)
            {
                Vector2 vector53 = default(Vector2);
                vector53 = new(num2, num3);
                float num173 = (float)Math.PI / 4f;
                for (int num174 = 0; num174 < 2; num174++)
                {
                    Vector2 val12 = pointPoisition;
                    Vector2 val13 = vector53;
                    Vector2 spinningpoint18 = vector53.SafeNormalize(Vector2.Zero);
                    double radians16 = num173 * (Main.rand.NextFloat() * 0.5f + 0.5f);
                    val = default(Vector2);
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, val12, val13 + spinningpoint18.RotatedBy(radians16, val) * Main.rand.NextFloatDirection() * 2f, projToShoot, Damage, KnockBack, player.whoAmI);
                    Vector2 val14 = pointPoisition;
                    Vector2 val15 = vector53;
                    Vector2 spinningpoint19 = vector53.SafeNormalize(Vector2.Zero);
                    double radians17 = (0f - num173) * (Main.rand.NextFloat() * 0.5f + 0.5f);
                    val = default(Vector2);
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, val14, val15 + spinningpoint19.RotatedBy(radians17, val) * Main.rand.NextFloatDirection() * 2f, projToShoot, Damage, KnockBack, player.whoAmI);
                }
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition, vector53.SafeNormalize(Vector2.UnitX * player.direction) * (speed * 1.3f), ProjectileID.BlackBolt, Damage * 2, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.Gladius || sItem.type == ItemID.Ruler)
            {
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition, new Vector2(num2, num3), projToShoot, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.LightsBane)
            {
                Vector2 spinningpoint20 = Utils.SafeNormalize(new Vector2(player.direction, player.gravDir * 4f), Vector2.UnitY);
                double radians18 = (float)Math.PI * 2f * Main.rand.NextFloatDirection() * 0.05f;
                val = default(Vector2);
                Vector2 vector54 = spinningpoint20.RotatedBy(radians18, val);
                Vector2 searchCenter = player.MountedCenter + new Vector2(70f, -40f) * Directions + vector54 * -10f;
                if (GetZenithTarget(player,searchCenter, 50f, out var npcTargetIndex2))
                {
                    NPC nPC3 = Main.npc[npcTargetIndex2];
                    searchCenter = nPC3.Center + Main.rand.NextVector2Circular(nPC3.width / 2, nPC3.height / 2);
                }
                else
                {
                    searchCenter += Main.rand.NextVector2Circular(20f, 20f);
                }
                float ai8 = 1f;
                if (Main.rand.Next(100) < player.GetCritChance(DamageClass.Melee))
                {
                    ai8 = 2f;
                    Damage *= 2;
                }
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, searchCenter, vector54 * 0.001f, projToShoot, (int)(Damage * 0.5), KnockBack, player.whoAmI, ai8);
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            }
            else if (sItem.type == ItemID.NightsEdge)
            {
                float adjustedItemScale = player.GetAdjustedItemScale(sItem);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, player.MountedCenter, new Vector2(player.direction, 0f), projToShoot, Damage, KnockBack, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, player.MountedCenter, new Vector2(num2, num3), projToShoot, Damage, KnockBack, player.whoAmI, player.direction * player.gravDir * 0.1f, 30f, adjustedItemScale);
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            }
            else if (sItem.type == ItemID.Excalibur)
            {
                float adjustedItemScale2 = player.GetAdjustedItemScale(sItem);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, player.MountedCenter, new Vector2(player.direction, 0f), projToShoot, Damage, KnockBack, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale2);
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            }
            else if (sItem.type == ItemID.TheHorsemansBlade)
            {
                float adjustedItemScale3 = player.GetAdjustedItemScale(sItem);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, player.MountedCenter, new Vector2(player.direction, 0f), projToShoot, Damage, KnockBack, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale3);
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            }
            else if (sItem.type == ItemID.TrueNightsEdge)
            {
                float adjustedItemScale4 = player.GetAdjustedItemScale(sItem);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, player.MountedCenter, new Vector2(player.direction, 0f), ProjectileID.NightsEdge, Damage, KnockBack, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale4);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, player.MountedCenter, new Vector2(num2, num3), projToShoot, Damage / 2, KnockBack, player.whoAmI, player.direction * player.gravDir, 32f, adjustedItemScale4);
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            }
            else if (sItem.type == ItemID.TrueExcalibur)
            {
                float adjustedItemScale5 = player.GetAdjustedItemScale(sItem);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, player.MountedCenter, new Vector2(player.direction, 0f), projToShoot, Damage, KnockBack, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale5);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, player.MountedCenter, new Vector2(player.direction, 0f), ProjectileID.Excalibur, 0, KnockBack, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale5);
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            }
            else if (sItem.type == ItemID.TerraBlade)
            {
                float adjustedItemScale6 = player.GetAdjustedItemScale(sItem);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, player.MountedCenter, new Vector2(player.direction, 0f), ProjectileID.TerraBlade2, Damage, KnockBack, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale6);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, player.MountedCenter, new Vector2(num2, num3) * 5f, projToShoot, Damage, KnockBack, player.whoAmI, player.direction * player.gravDir, 18f, adjustedItemScale6);
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            }
            else if (sItem.type == ItemID.BladeofGrass)
            {
                Vector2 vector55 = player.MountedCenter + new Vector2(70f, -40f) * Directions;
                int npcTargetIndex3;
                bool zenithTarget2 = GetZenithTarget(player, vector55, 150f, out npcTargetIndex3);
                if (zenithTarget2)
                {
                    NPC nPC4 = Main.npc[npcTargetIndex3];
                    vector55 = Main.rand.NextVector2FromRectangle(nPC4.Hitbox);
                }
                else
                {
                    vector55 += Main.rand.NextVector2Circular(20f, 20f);
                }
                Vector2 vector56 = player.Center + new Vector2(Main.rand.NextFloatDirection() * player.width / 2f, player.height / 2) * Directions;
                Vector2 v6 = vector55 - vector56;
                float num175 = ((float)Math.PI + (float)Math.PI * 2f * Main.rand.NextFloat() * 1.5f) * (-player.direction * player.gravDir);
                int num176 = 60;
                float num177 = num175 / num176;
                float num178 = 16f;
                float num179 = v6.Length();
                if (Math.Abs(num177) >= 0.17f)
                {
                    num177 *= 0.7f;
                }
                _ = player.direction;
                _ = player.gravDir;
                Vector2 vector57 = Vector2.UnitX * num178;
                Vector2 v7 = vector57;
                int num180 = 0;
                while (v7.Length() < num179 && num180 < num176)
                {
                    num180++;
                    v7 += vector57;
                    Vector2 spinningpoint21 = vector57;
                    double radians19 = num177;
                    val = default(Vector2);
                    vector57 = spinningpoint21.RotatedBy(radians19, val);
                }
                float num181 = v7.ToRotation();
                Vector2 spinningpoint22 = v6.SafeNormalize(Vector2.UnitY);
                double radians20 = 0f - num181 - num177;
                val = default(Vector2);
                Vector2 spinningpoint23 = spinningpoint22.RotatedBy(radians20, val) * num178;
                if (num180 == num176)
                {
                    spinningpoint23 = new Vector2(player.direction, 0f) * num178;
                }
                if (!zenithTarget2)
                {
                    vector56.Y -= player.gravDir * 24f;
                    Vector2 spinningpoint24 = spinningpoint23;
                    double radians21 = player.direction * player.gravDir * ((float)Math.PI * 2f) * 0.14f;
                    val = default(Vector2);
                    spinningpoint23 = spinningpoint24.RotatedBy(radians21, val);
                }
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, vector56, spinningpoint23, projToShoot, (int)(Damage * 0.25), KnockBack, player.whoAmI, num177, num180);
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            }
            else if (sItem.type == ItemID.VortexBeater)
            {
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, ProjectileID.VortexBeater, Damage, KnockBack, player.whoAmI, 5 * Main.rand.Next(0, 20));
            }
            else if (sItem.type == ItemID.Celeb2)
            {
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, ProjectileID.Celeb2Weapon, Damage, KnockBack, player.whoAmI, 5 * Main.rand.Next(0, 20));
            }
            else if (sItem.type == ItemID.Phantasm)
            {
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, ProjectileID.Phantasm, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.JimsDrone)
            {
                for (int num182 = 0; num182 < 1000; num182++)
                {
                    Projectile projectile3 = Main.projectile[num182];
                    if (projectile3.type == projToShoot && projectile3.owner == player.whoAmI)
                    {
                        projectile3.Kill();
                    }
                }
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.DD2PhoenixBow)
            {
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, ProjectileID.DD2PhoenixBow, Damage, KnockBack, player.whoAmI);
            }
            else if (sItem.type == ItemID.FireworksLauncher)
            {
                for (int num183 = 0; num183 < 2; num183++)
                {
                    float num184 = num2;
                    float num185 = num3;
                    num184 += Main.rand.Next(-40, 41) * 0.05f;
                    num185 += Main.rand.Next(-40, 41) * 0.05f;
                    Vector2 val16 = pointPoisition;
                    Vector2 spinningpoint25 = new Vector2(num184, num185);
                    double radians22 = -(float)Math.PI / 2f * player.direction;
                    val = default(Vector2);
                    Vector2 vector58 = val16 + Vector2.Normalize(Utils.RotatedBy(spinningpoint25, radians22, val)) * 6f;
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, vector58.X, vector58.Y, num184, num185, 167 + Main.rand.Next(4), Damage, KnockBack, player.whoAmI, 0f, 1f);
                }
            }
            else if (sItem.type == ItemID.PainterPaintballGun)
            {
                float num186 = num2;
                float num187 = num3;
                num186 += Main.rand.Next(-1, 2) * 0.5f;
                num187 += Main.rand.Next(-1, 2) * 0.5f;
                if (Collision.CanHitLine(player.Center, 0, 0, pointPoisition + new Vector2(num186, num187) * 2f, 0, 0))
                {
                    pointPoisition += new Vector2(num186, num187);
                }
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y - player.gravDir * 4f, num186, num187, projToShoot, Damage, KnockBack, player.whoAmI, 0f, Main.rand.Next(12) / 6f);
            }
            else if (sItem.type == ItemID.BookStaff)
            {
                if (player.altFunctionUse == 2)
                {
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, player.Bottom.Y - 100f, player.direction * speed, 0f, ProjectileID.DD2ApprenticeStorm, (int)(Damage * 1.75f), KnockBack, player.whoAmI);
                }
                else
                {
                    Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot, Damage, KnockBack, player.whoAmI);
                }
            }
            else if (sItem.type == ItemID.DD2FlameburstTowerT1Popper || sItem.type == ItemID.DD2FlameburstTowerT2Popper || sItem.type == ItemID.DD2FlameburstTowerT3Popper || sItem.type == ItemID.DD2BallistraTowerT1Popper || sItem.type == ItemID.DD2BallistraTowerT2Popper || sItem.type == ItemID.DD2BallistraTowerT3Popper || sItem.type == ItemID.DD2LightningAuraT1Popper || sItem.type == ItemID.DD2LightningAuraT2Popper || sItem.type == ItemID.DD2LightningAuraT3Popper || sItem.type == ItemID.DD2ExplosiveTrapT1Popper || sItem.type == ItemID.DD2ExplosiveTrapT2Popper || sItem.type == ItemID.DD2ExplosiveTrapT3Popper)
            {
                PayDD2CrystalsBeforeUse(player, sItem);
                player.FindSentryRestingSpot(sItem.shoot, out var worldX, out var worldY, out var pushYUp);
                int num188 = 0;
                int num189 = 0;
                int num190 = 0;
                switch (sItem.type)
                {
                    case ItemID.DD2BallistraTowerT1Popper:
                    case ItemID.DD2BallistraTowerT2Popper:
                    case ItemID.DD2BallistraTowerT3Popper:
                        num188 = 1;
                        num189 = Projectile.GetBallistraShotDelay(player);
                        break;
                    case ItemID.DD2ExplosiveTrapT1Popper:
                    case ItemID.DD2ExplosiveTrapT2Popper:
                    case ItemID.DD2ExplosiveTrapT3Popper:
                        num190 = Projectile.GetExplosiveTrapCooldown(player);
                        break;
                    case ItemID.DD2FlameburstTowerT1Popper:
                        num188 = 1;
                        num189 = 80;
                        break;
                    case ItemID.DD2FlameburstTowerT2Popper:
                        num188 = 1;
                        num189 = 70;
                        break;
                    case ItemID.DD2FlameburstTowerT3Popper:
                        num188 = 1;
                        num189 = 60;
                        break;
                }
                int num191 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, worldX, worldY - pushYUp, 0f, 0f, projToShoot, Damage, KnockBack, player.whoAmI, num188, num189);
                Main.projectile[num191].originalDamage = damage;
                Main.projectile[num191].localAI[0] = num190;
                player.UpdateMaxTurrets();
            }
            else if (sItem.type == ItemID.Starfury)
            {
                Vector2 vector59 = new(num2, num3);
                new Vector2(100f, 0f);
                Vector2 mouseWorld2 = aimWorld;
                Vector2 vec = mouseWorld2;
                Vector2 vector60 = (pointPoisition - mouseWorld2).SafeNormalize(new Vector2(0f, -1f));
                while (vec.Y > pointPoisition.Y && WorldGen.SolidTile(vec.ToTileCoordinates()))
                {
                    vec += vector60 * 16f;
                }
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition, vector59, projToShoot, Damage, KnockBack, player.whoAmI, 0f, vec.Y);
            }
            else if (sItem.type == ItemID.PiercingStarlight)
            {
                float adjustedItemScale7 = player.GetAdjustedItemScale(sItem);
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot, Damage, KnockBack, player.whoAmI, 0f, adjustedItemScale7);
            }
            else if (sItem.type == ItemID.ElfMelter)
            {
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot, Damage, KnockBack, player.whoAmI, 1f);
            }
            else if (sItem.type == ItemID.Clentaminator2)
            {
                Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot, Damage, KnockBack, player.whoAmI, 0f, 1f);
            }
            else
            {
                int num192 = Projectile.NewProjectile(projectileSource_Item_WithPotentialAmmo, pointPoisition.X, pointPoisition.Y, num2, num3, projToShoot, Damage, KnockBack, player.whoAmI);
                if (sItem.type == ItemID.FrostStaff)
                {
                    Main.projectile[num192].DamageType = DamageClass.Magic;
                }
                if (sItem.type == ItemID.IceBlade || sItem.type == ItemID.Frostbrand)
                {
                    Main.projectile[num192].DamageType = DamageClass.Melee;
                }
                if (projToShoot == 80)
                {
                    Main.projectile[num192].ai[0] = Player.tileTargetX;
                    Main.projectile[num192].ai[1] = Player.tileTargetY;
                }
                if (sItem.type == ItemID.ProximityMineLauncher)
                {
                    DestroyOldestProximityMinesOverMinesCap(player,20);
                }
                if (projToShoot == 442)
                {
                    Main.projectile[num192].ai[0] = Player.tileTargetX;
                    Main.projectile[num192].ai[1] = Player.tileTargetY;
                }
                if (projToShoot == 826)
                {
                    Main.projectile[num192].ai[1] = Main.rand.Next(3);
                }
                if (sItem.type == ItemID.Snowball)
                {
                    Main.projectile[num192].ai[1] = 1f;
                }
                if (Main.projectile[num192].aiStyle == ProjAIStyleID.Yoyo)
                {
                    AchievementsHelper.HandleSpecialEvent(player, 7);
                }
                if (Main.projectile[num192].aiStyle == ProjAIStyleID.Kite && Main.IsItAHappyWindyDay)
                {
                    AchievementsHelper.HandleSpecialEvent(player, 17);
                }
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI);
            }
        }

        private static List<Projectile> _oldestProjCheckList = new List<Projectile>();
        private static void DestroyOldestProximityMinesOverMinesCap(Player player, int minesCap)
        {
            _oldestProjCheckList.Clear();
            for (int i = 0; i < 1000; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && projectile.owner == player.whoAmI)
                {
                    switch (projectile.type)
                    {
                        case ProjectileID.ProximityMineI:
                        case ProjectileID.ProximityMineII:
                        case ProjectileID.ProximityMineIII:
                        case ProjectileID.ProximityMineIV:
                        case ProjectileID.ClusterMineI:
                        case ProjectileID.ClusterMineII:
                        case ProjectileID.WetMine:
                        case ProjectileID.LavaMine:
                        case ProjectileID.HoneyMine:
                        case ProjectileID.MiniNukeMineI:
                        case ProjectileID.MiniNukeMineII:
                        case ProjectileID.DryMine:
                            _oldestProjCheckList.Add(projectile);
                            break;
                    }
                }
            }
            while (_oldestProjCheckList.Count > minesCap)
            {
                Projectile projectile2 = _oldestProjCheckList[0];
                for (int j = 1; j < _oldestProjCheckList.Count; j++)
                {
                    if (_oldestProjCheckList[j].timeLeft < projectile2.timeLeft)
                    {
                        projectile2 = _oldestProjCheckList[j];
                    }
                }
                projectile2.Kill();
                _oldestProjCheckList.Remove(projectile2);
            }
            _oldestProjCheckList.Clear();
        }


        private static bool GetZenithTarget(Player player, Vector2 searchCenter, float maxDistance, out int npcTargetIndex)
        {

            npcTargetIndex = 0;
            int? num = null;
            float num2 = maxDistance;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.CanBeChasedBy(player))
                {
                    float num3 = searchCenter.Distance(nPC.Center);
                    if (!(num2 <= num3))
                    {
                        num = i;
                        num2 = num3;
                    }
                }
            }
            if (!num.HasValue)
            {
                return false;
            }
            npcTargetIndex = num.Value;
            return true;
        }

        private static bool GetSparkleGuitarTarget(Player player, out List<NPC> validTargets)
        {
            validTargets = [];
            Rectangle value = Utils.CenteredRectangle(player.Center, new Vector2(1000f, 800f));
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.CanBeChasedBy(player))
                {
                    Rectangle hitbox = nPC.Hitbox;
                    if (hitbox.Intersects(value))
                    {
                        validTargets.Add(nPC);
                    }
                }
            }
            if (validTargets.Count == 0) return false;
            return true;
        }

        private static Point FindSharpTearsSpot(Player player,Vector2 targetSpot)
        {
            Vector2 center = player.Center;
            Vector2 endPoint = targetSpot;

            int samplesToTake = 3;
            float samplingWidth = 4f;

            Collision.AimingLaserScan(center, endPoint, samplingWidth, samplesToTake, out var vectorTowardsTarget, out var samples);

            float num = float.PositiveInfinity;

            for (int i = 0; i < samples.Length; i++)
            {
                if (samples[i] < num)
                {
                    num = samples[i];
                }
            }

            targetSpot = center + vectorTowardsTarget.SafeNormalize(Vector2.Zero) * num;
            Point point = targetSpot.ToTileCoordinates();

            Rectangle value = new(point.X, point.Y, 1, 1);
            value.Inflate(6, 16);

            Rectangle value2 = new(0, 0, Main.maxTilesX, Main.maxTilesY);
            value2.Inflate(-40, -40);

            value = Rectangle.Intersect(value, value2);
            List<Point> list = [];
            List<Point> list2 = [];
            for (int j = value.Left; j <= value.Right; j++)
            {
                for (int k = value.Top; k <= value.Bottom; k++)
                {
                    if (!WorldGen.SolidTile2(j, k))
                    {
                        continue;
                    }
                    Vector2 value3 = new((j * 16 + 8), k * 16 + 8);
                    if (!(Vector2.Distance(targetSpot, value3) > 200f))
                    {
                        if (FindSharpTearsOpening(j, k, j > point.X, j < point.X, k > point.Y, k < point.Y))
                        {
                            list.Add(new Point(j, k));
                        }
                        else
                        {
                            list2.Add(new Point(j, k));
                        }
                    }
                }
            }
            if (list.Count == 0 && list2.Count == 0)
            {
                list.Add((player.Center.ToTileCoordinates().ToVector2() + Main.rand.NextVector2Square(-2f, 2f)).ToPoint());
            }
            List<Point> list3 = list;
            if (list3.Count == 0)
            {
                list3 = list2;
            }
            int index = Main.rand.Next(list3.Count);
            return list3[index];
        }

        private static bool FindSharpTearsOpening(int x, int y, bool acceptLeft, bool acceptRight, bool acceptUp, bool acceptDown)
        {
            if (acceptLeft && !WorldGen.SolidTile(x - 1, y)) return true;
            
            if (acceptRight && !WorldGen.SolidTile(x + 1, y)) return true;
            
            if (acceptUp && !WorldGen.SolidTile(x, y - 1)) return true;
            
            if (acceptDown && !WorldGen.SolidTile(x, y + 1)) return true;
            
            return false;
        }

        private static void PayDD2CrystalsBeforeUse(Player player,Item item)
        {
            int requiredDD2CrystalsToUse = GetRequiredDD2CrystalsToUse(item);
            for (int i = 0; i < requiredDD2CrystalsToUse; i++)
                player.ConsumeItem(ItemID.DD2EnergyCrystal, reverseOrder: true);
        }

        private static int GetRequiredDD2CrystalsToUse(Item item)
        {
            switch (item.type)
            {
                case ItemID.DD2FlameburstTowerT1Popper:
                case ItemID.DD2FlameburstTowerT2Popper:
                case ItemID.DD2FlameburstTowerT3Popper:
                    return 10;
                case ItemID.DD2BallistraTowerT1Popper:
                case ItemID.DD2BallistraTowerT2Popper:
                case ItemID.DD2BallistraTowerT3Popper:
                    return 10;
                case ItemID.DD2ExplosiveTrapT1Popper:
                case ItemID.DD2ExplosiveTrapT2Popper:
                case ItemID.DD2ExplosiveTrapT3Popper:
                    return 10;
                case ItemID.DD2LightningAuraT1Popper:
                case ItemID.DD2LightningAuraT2Popper:
                case ItemID.DD2LightningAuraT3Popper:
                    return 10;
                default:
                    return 0;
            }
        }
    }
}