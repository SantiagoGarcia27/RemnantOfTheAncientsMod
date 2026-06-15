using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.DataSet;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Projectiles.HeldItem;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.Items
{
    public class WeaponRebalanceGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        private bool WeaponConf => ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;
        public override void SetDefaults(Item item)
        {
            bool Calamity = RemnantOfTheAncientsMod.CalamityMod != null;

            if (WeaponConf)
            {
                if (item.type == ItemID.DaedalusStormbow)
                {
                    item.DefaultToRangedWeapon(item.shoot, AmmoID.Arrow, 24, item.shootSpeed);
                    item.SetWeaponValues(35, item.knockBack);
                }
                else if (item.type == ItemID.FetidBaghnakhs)
                {
                    item.damage = 50;
                    item.useTime = 11;
                    item.useAnimation = 11;
                    item.scale = 1.80f;
                }
                else if (item.type == ItemID.HeatRay)
                {
                    item.damage = 200;
                    item.useTime = 58;
                    item.useAnimation = 58;
                }
                else if (item.type == ItemID.NightsEdge)
                {
                    if (RemnantOfTheAncientsMod.CalamityMod == null)
                        item.damage = 45;
                }
                else if (item.type == ItemID.Minishark)
                {
                    item.knockBack = 0.1f;
                }
                else if (item.type == ItemID.DD2BallistraTowerT1Popper) item.damage = 45;
                else if (item.type == ItemID.DD2BallistraTowerT2Popper) item.damage = 85;
                else if (item.type == ItemID.DD2BallistraTowerT3Popper) item.damage = 200;
                else if (item.type == ItemID.DD2FlameburstTowerT1Popper) item.damage = 30;
                else if (item.type == ItemID.DD2FlameburstTowerT2Popper) item.damage = 60;
                else if (item.type == ItemID.DD2FlameburstTowerT3Popper) item.damage = 100;
                else if (item.type == ItemID.DD2ExplosiveTrapT1Popper) item.damage = 40;
                else if (item.type == ItemID.DD2ExplosiveTrapT2Popper) item.damage = 80;
                else if (item.type == ItemID.DD2ExplosiveTrapT3Popper) item.damage = 250;
                else if (item.type == ItemID.DD2LightningAuraT1Popper) item.damage = 10;//30
                else if (item.type == ItemID.DD2LightningAuraT2Popper) item.damage = 16;//50
                else if (item.type == ItemID.DD2LightningAuraT3Popper) item.damage = 50;//150

                else if (item.type == ItemID.PearlwoodBow)
                {
                    item.damage = 30;
                    item.shootSpeed = 10;
                    item.useTime = 8;
                    item.useAnimation = 70;
                }
                else if (item.type == ItemID.FlinxFurCoat)
                {
                    item.defense = 3;
                }
                else if (item.type == ItemID.WeatherPain)
                {
                    item.knockBack = 5;
                }
                else if (item.type == ItemID.BookofSkulls)
                {
                    item.mana = 15;
                    item.damage += 5;
                }
                else if (item.type == ItemID.InfernoFork)
                {
                    item.crit = 76; // da 80 en el juego (valor buscado - 4)
                }
                else if (item.type == ItemID.ClingerStaff)
                {
                    item.mana = 10;
                }
                else if (item.type == ItemID.ToxicFlask)
                {
                    item.useAnimation = 25;
                    item.useTime = 7;
                    item.mana += 10;
                    item.reuseDelay = 40;
                }
                else if (item.type == ItemID.MagicDagger)
                {
                    item.damage -= 3;
                    item.mana = 4;
                }
                else if (item.type == ItemID.ZapinatorGray)
                {
                    item.damage = 25;
                    item.useAnimation = 16;
                    item.useTime = 14;
                }
                else if (item.type == ItemID.ZapinatorOrange)
                {
                    item.damage = 60;
                    item.useAnimation = 16;
                    item.useTime = 14;
                }
                else if (item.type == ItemID.ThrowingKnife || item.type == ItemID.PoisonedKnife || item.type == ItemID.BoneDagger)
                {
                    item.DamageType = DamageClass.Throwing;
                }
                else if (item.type == ItemID.Shuriken || item.type == ItemID.StarAnise)
                {
                    item.DamageType = DamageClass.Throwing;
                }
                else if (item.type == ItemID.SpikyBall)
                {
                    item.DamageType = DamageClass.Throwing;
                }
                else if (item.type == ItemID.Bone)
                {
                    item.DamageType = DamageClass.Throwing;
                }
                else if (item.type == ItemID.RottenEgg)
                {
                    item.DamageType = DamageClass.Throwing;
                }
                else if (item.type == ItemID.Grenade || item.type == ItemID.StickyGrenade || item.type == ItemID.BouncyGrenade || item.type == ItemID.Beenade || item.type == ItemID.PartyGirlGrenade)
                {
                    item.DamageType = DamageClass.Throwing;
                }
                else if (item.type == ItemID.Javelin || item.type == ItemID.BoneJavelin)
                {
                    item.DamageType = DamageClass.Throwing;
                }
                else if (item.type == ItemID.PaperAirplaneA || item.type == ItemID.PaperAirplaneB)
                {
                    item.DamageType = DamageClass.Throwing;
                }
                else if (item.type == ItemID.MolotovCocktail)
                {
                    item.DamageType = DamageClass.Throwing;
                }
                else if (item.type == ItemID.AleThrowingGlove)
                {
                    item.DamageType = DamageClass.Throwing;
                }
                else if (item.type == ItemID.AbigailsFlower)
                {
                    item.damage /= 2;
                }
            }
        }
        float counter;
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.type == ItemID.PearlwoodBow)
            {
                return base.Shoot(item, player, source, position, velocity, ProjectileID.JestersArrow, damage, knockback);
            }
            else if (item.type == ItemID.BabyBirdStaff)
            {
                var a = Projectile.NewProjectile(Projectile.GetSource_None(), player.position, Vector2.One, ProjectileID.BabyBird, item.damage, item.knockBack, Main.myPlayer, 0, 0, 0);
                Main.projectile[a].minionSlots = 0.5f;
                var p = Projectile.NewProjectile(Projectile.GetSource_None(), player.position, Vector2.One, ProjectileID.BabyBird, item.damage / 3, item.knockBack / 2, Main.myPlayer, 0, 0, 0);
                Main.projectile[p].minionSlots = 0.5f;
                Main.projectile[p].alpha = 200;
                return false;
            }
            else if (item.type == ItemID.BookofSkulls)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    float PlayerManaDiscounMultpier = player.statManaMax2 / 20;
                    int Shootproj = ModContent.ProjectileType<BookOfSkullHeldProj>();
                    if (player.altFunctionUse == 2)
                    {
                        item.mana = player.statManaMax2;
                        if (player.ownedProjectileCounts[Shootproj] <= 0)
                        {
                            Projectile.NewProjectile(source, player.position, Vector2.Zero, Shootproj, 0, 0, Main.myPlayer, velocity.X, velocity.Y);
                        }
                        return false;
                    }
                    else
                    {
                        item.mana = 15;
                        return true;
                    }
                }
            }
            else if (item.type == ItemID.AquaScepter)
            {
                if (player.whoAmI == Main.myPlayer)
                {

                    int Shootproj = ModContent.ProjectileType<AquaScepterHeldProj>();
                    if (player.altFunctionUse == 2)
                    {
                        if (player.ownedProjectileCounts[Shootproj] <= 0)
                        {
                            Projectile.NewProjectile(source, player.position, Vector2.Zero, Shootproj, 0, 0, Main.myPlayer, velocity.X, velocity.Y, item.mana);
                        }
                        return false;
                    }
                }
            }
            else if (item.type == ItemID.WandofFrosting || item.type == ItemID.WandofSparking)
            {
                if (player.whoAmI == Main.myPlayer)
                {

                    int Shootproj = ModContent.ProjectileType<WandOfSparkingHeldProj>();
                    if (player.altFunctionUse == 2)
                    {
                        if (player.ownedProjectileCounts[Shootproj] <= 0)
                        {
                            int manacost = player.statManaMax2 / 2;
                            if (player.statMana >= manacost)
                            {
                                player.statMana -= manacost;
                                var p = Projectile.NewProjectile(source, player.position, Vector2.Zero, Shootproj, 0, 0, Main.myPlayer, velocity.X, velocity.Y, item.mana);
                                Main.projectile[p].localAI[0] = item.shoot;
                            }
                        }
                        return false;
                    }
                }
            }
            else if (item.type == ItemID.FrostStaff)
            {
                double ecuacionhonda = Utils1.GenerateWave(3f * 16, player.position.X, counter);
                if (counter % 2 == 0)
                {
                    var p = Projectile.NewProjectile(source, player.position - new Vector2(0, (float)ecuacionhonda - 2 * 16f), velocity, 174, damage / 4, knockback, Main.myPlayer);
                    Main.projectile[p].stepSpeed /= 2;
                }
                Projectile.NewProjectile(source, player.position + new Vector2(0, 2 * 16f), velocity, type, damage, knockback, Main.myPlayer);
                counter++;
                return false;
            }
            else if (item.type == ItemID.FlowerofFire || item.type == ItemID.FlowerofFrost)
            {
                if (player.altFunctionUse != 2)
                {
                    item.useTime = item.type == ItemID.FlowerofFire ? 16 : 12;
                    item.useAnimation = item.type == ItemID.FlowerofFire ? 16 : 12;
                    item.mana = item.type == ItemID.FlowerofFire ? 12 : 11;
                    item.useStyle = ItemUseStyleID.Swing;
                    item.shootSpeed = item.type == ItemID.FlowerofFire ? 7.5f : 9f;
                    item.autoReuse = true;
                }
                else
                {
                    item.useTime = 6;
                    item.useAnimation = 6;
                    item.useStyle = ItemUseStyleID.Shoot;
                    item.shootSpeed = item.type == ItemID.FlowerofFire ? 1f : 2f;
                    item.channel = true;
                    item.mana = 3;
                    item.autoReuse = true;
                    int p = Projectile.NewProjectile(source, position, velocity, ProjectileID.Flames, damage / 2, knockback, player.whoAmI, item.type == ItemID.FlowerofFire ? 2 : 1);
                    Main.projectile[p].DamageType = DamageClass.Magic;
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<FlowerHeldProj>()] <= 0)
                    {
                        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<FlowerHeldProj>(), 0, 0, player.whoAmI, item.type == ItemID.FlowerofFire ? 1 : 2);
                    }
                    return false;
                }
            }
            else if (item.type == ItemID.SharpTears)
            {
                int Shootproj = ModContent.ProjectileType<BloodThornHeldProj>();
                if (player.altFunctionUse == 2)
                {
                    item.mana = 90;
                    if (player.ownedProjectileCounts[Shootproj] <= 0)
                    {
                        Projectile.NewProjectile(source, player.position, Vector2.Zero, Shootproj, 0, 0, Main.myPlayer, velocity.X, velocity.Y);
                    }
                    return false;
                }
                else
                {
                    item.mana = 20;
                    return true;
                }
            }
            else if (item.type == ItemID.CrystalVileShard)
            {
                float numberProjectiles = 2;
                float rotation = MathHelper.ToRadians(10);

                position += Vector2.Normalize(velocity);

                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                    int p = Projectile.NewProjectile(source, position, perturbedSpeed * 6, 491, damage, knockback, player.whoAmI, 0, 0);
                    Main.projectile[p].stepSpeed = 1f;
                    Main.projectile[p].DamageType = DamageClass.Magic;
                }
            }
            else if (item.type == ItemID.NettleBurst)
            {
                float numberProjectiles = 2;
                float rotation = MathHelper.ToRadians(10);

                position += Vector2.Normalize(velocity);
                if (new Random().Next(4) == 1)
                {
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                        int p = Projectile.NewProjectile(source, position, perturbedSpeed * 6, 277, damage, knockback, player.whoAmI, 0, 0);
                        Main.projectile[p].stepSpeed = 1f;
                        Main.projectile[p].friendly = true;
                        Main.projectile[p].hostile = false;
                        Main.projectile[p].DamageType = DamageClass.Magic;
                    }
                }
            }
            else if (item.type == ItemID.ShadowbeamStaff)
            {
                float numberProjectiles = 2;
                float rotation = MathHelper.ToRadians(10);

                position += Vector2.Normalize(velocity);

                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                    int p = Projectile.NewProjectile(source, position, perturbedSpeed * 6, type, damage, knockback, player.whoAmI);
                    Main.projectile[p].stepSpeed = 1f;
                    Main.projectile[p].friendly = true;
                    Main.projectile[p].hostile = false;
                    Main.projectile[p].DamageType = DamageClass.Magic;
                }

            }
            else if (RemnantItemTags.GemStaffs.TryGetValue(item.type, out int value))
            {
                if (player.altFunctionUse == 2)
                {
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<GemStaffHeldProj>()] <= 0 && player.whoAmI == Main.myPlayer)
                    {
                        player.statMana -= item.mana * 3;
                        Projectile.NewProjectile(source, player.position, velocity, ModContent.ProjectileType<GemStaffHeldProj>(), 0, 0, player.whoAmI, type, value);
                    }
                    return false;
                }
            }
            else if (item.type == ItemID.UnholyTrident)
            {
                if (player.altFunctionUse == 2)
                {
                    item.mana = 0;
                    return false;
                }
                else
                {
                    item.mana = 18;
                    item.useStyle = ItemUseStyleID.Shoot;
                }
            }
            else if (item.type == ItemID.BubbleGun)
            {
                if (player.altFunctionUse == 2)
                {
                    item.reuseDelay = 70;
                    int p = Projectile.NewProjectile(source, position, velocity + new Vector2(0, 5), ModContent.ProjectileType<SharknadoBoltClone>(), item.damage * 2, 0, player.whoAmI);
                    Main.projectile[p].friendly = true;
                    Main.projectile[p].hostile = !Main.projectile[p].friendly;
                    return false;
                }
                else
                {
                    item.reuseDelay = 0; 
                }
            }
            else if (item.type == ItemID.PygmyStaff)
            {
                if (Main.rand.NextBool(2))
                {
                    player.AddBuff(BuffID.Pygmies, 10);
                    Projectile.NewProjectile(Projectile.GetSource_None(), position, velocity, ModContent.ProjectileType<PygmyMelee>(), damage, knockback, player.whoAmI);
                    return false;
                }
            }

            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
    }
}
