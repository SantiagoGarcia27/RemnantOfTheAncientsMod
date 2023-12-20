using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Projectiles.Fargos;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Core.Toggler;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;

namespace RemnantOfTheAncientsMod
{
    [ExtendsFromMod("FargowiltasSouls")]
    public class RemnantFargosSoulsPlayer : ModPlayer
    {

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod);
        }

        public bool DaylightEnchantment;

        public bool IronBallistaEnchantment;
        public bool FrostBarrier;
        public static int FrostBarrierCounter;
        public bool DesertMedalion;


        public bool NightTp;
        public static int NightTpCouldown { get; set; }
        public static int NightTpCouldownMax = (int)Utils1.FormatTimeToTick(0, 0, 1, 0);

        public static bool tuxoniteEnchantCrit;
        public static int tuxoniteEnchantCritDuration = 0;
        public static float tuxoniteEnchantCritCounter = 0;


        public override void ResetEffects()
        {
            FrostBarrier = false;
            DesertMedalion = false;
            IronBallistaEnchantment = false;
            NightTp = false;
            DaylightEnchantment = false;
            tuxoniteEnchantCrit = false;
        }

        public override void PostUpdate()
        {
            if (NightTp)
            {
                Player.opacityForAnimation = NightTpCouldown == 0 ? 0.5f : 1;
                if (NightTpCouldown > 0)
                {
                    NightTpCouldown--;
                }
            }
        }
        #region FargosEnchantment

        Projectile IronBallistaProj = null;
        public void ReinforcedIronEnchant(Player player, float SpeedBonus, float DamageReduction)
        {
            if (player.GetToggleValue("ReinforcedIronEnchantmentDamageReductionPlusSlow", checkForMutantPresence: false))
            {
                player.endurance += DamageReduction / 100;
                player.moveSpeed += SpeedBonus / 100;
            }




            if (player.GetToggleValue("ReinforcedIronEnchantmentBallista", checkForMutantPresence: false))
            {
                player.GetModPlayer<RemnantFargosSoulsPlayer>().IronBallistaEnchantment = true;
                int minionCount = (player.ownedProjectileCounts[ModContent.ProjectileType<IronBallistMinion>()]);
                if (minionCount < 1)
                {
                    float damage = 30;

                    damage = player.GetTotalDamage(DamageClass.Summon).ApplyTo(damage);
                    IronBallistaProj = Projectile.NewProjectileDirect(Projectile.GetSource_None(), player.position, new Vector2(0, 2 * 16) * 10, ModContent.ProjectileType<IronBallistMinion>(), (int)damage, 1, Main.myPlayer);
                    IronBallistaProj.minionSlots = 0;
                    IronBallistaProj.timeLeft = 300;
                    IronBallistaProj.originalDamage = (int)damage;
                }
            }
            else
            {
                if (IronBallistaProj != null)
                {
                    IronBallistaProj.Kill();
                    IronBallistaProj = null;
                }
            }
        }
        Projectile Sunflowerproj = null;
        public void DaylightEnchant(Player player)
        {
            RemnantPlayer.DaylightArmorSetBonus = true;
            player.GetModPlayer<RemnantFargosSoulsPlayer>().DaylightEnchantment = true;
            if (Main.dayTime)
            {
                if (player.GetToggleValue("DaylightEnchantmentSunflower", checkForMutantPresence: false))
                {
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<FloatingSunFlowerMinion>()] <= 0)
                        Sunflowerproj = Projectile.NewProjectileDirect(Projectile.GetSource_None(), player.position, Vector2.Zero, ModContent.ProjectileType<FloatingSunFlowerMinion>(), 0, 0, player.whoAmI);
                }
                else
                {
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<FloatingSunFlowerMinion>()] > 0)
                    {
                        foreach (var p in Main.projectile)
                        {
                            if (p != null && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<FloatingSunFlowerMinion>())
                            {
                                p.Kill();
                            }
                        }
                    }
                    if (Sunflowerproj != null)
                    {
                        Sunflowerproj.Kill();
                        Sunflowerproj = null;
                    }
                }
            }
            else
            {
                if (Sunflowerproj != null)
                {
                    Sunflowerproj.Kill();
                    Sunflowerproj = null;
                }
                player.manaCost -= 0.05f;
            }
        }
        public void TuxoniteEnchant(Player player, int CritBonus)
        {
            if (player.GetToggleValue("TuxoniteEnchantmentCritBonus", checkForMutantPresence: false))
            {
                if (tuxoniteEnchantCritCounter == 0)
                {
                    Dust.NewDustDirect(player.Center, 3, 10, DustID.MushroomSpray);
                    player.GetCritChance(DamageClass.Generic) += CritBonus;
                    if (tuxoniteEnchantCritDuration < Utils1.FormatTimeToTick(0, 0, 0, 10))
                    {
                        tuxoniteEnchantCritDuration++;
                    }
                    else
                    {
                        tuxoniteEnchantCritCounter = (int)Main.rand.NextFloat(Utils1.FormatTimeToTick(0, 0, 0, 30));
                        tuxoniteEnchantCritDuration = 0;
                    }
                }
                else
                {
                    tuxoniteEnchantCritCounter--;
                }
            }
        }
        public void NightEnchant(Player player)
        {
            if (!Main.dayTime)
            {
                if (player.GetToggleValue("NightEnchantTeleport", checkForMutantPresence: false))
                {
                    player.opacityForAnimation = NightTpCouldown == 0 ? 0.5f : 1;
                    player.aggro -= 500;
                    player.GetModPlayer<RemnantFargosSoulsPlayer>().NightTp = true;
                }
            }
            else
            {
                player.opacityForAnimation = 1;
                player.GetModPlayer<RemnantFargosSoulsPlayer>().NightTp = false;
            }
        }
        Projectile CactusBoulderproj = null;
        public void DesertEnchant(Player player)
        {
            player.GetModPlayer<RemnantPlayer>().MinionsBuffInflict.Add(BuffID.Electrified);
            player.GetModPlayer<RemnantPlayer>().AllClassBuffInflict.Add(BuffID.Electrified);

            if (player.GetToggleValue("DesertHeraldEnchantmentCactus", checkForMutantPresence: false))
            {
                CactusBoulderproj = player.GetModPlayer<RemnantPlayer>().SpawnProjectileOnMouse(ProjectileID.RollingCactus, CactusBoulderproj);
            }
            else
            {
                if (CactusBoulderproj != null)
                {
                    CactusBoulderproj.Kill();
                    CactusBoulderproj = null;
                }
            }
        }
        public void DesertMedalionEffect(Player player)
        {
            player.GetModPlayer<RemnantPlayer>().MinionsBuffInflict.Add(ModContent.BuffType<Burning_Sand>());
            if (player.GetToggleValue("DesertMedalionExtraProjectile", checkForMutantPresence: false))
            {
                player.GetModPlayer<RemnantFargosSoulsPlayer>().DesertMedalion = true;
            }
            player.buffImmune[ModContent.BuffType<Burning_Sand>()] = true;
        }
        public void FrostBarrierEffect(Player player, bool hideVisual)
        {
            if (player.GetToggleValue("FrostBarrierEffect", checkForMutantPresence: false))
            {
                ModContent.GetInstance<RemnantFargosSoulsPlayer>().FrostBarrier = true;
            }
        }
        public void TyrantMarkEffect(Player player, float MovmentSpeedBonusBonus, float ProjectileSizeBonus, float ProjectileSpeedBonus)
        {
            if (player.GetToggleValue("TyrantMarkEnemyProjectileSize", checkForMutantPresence: false))
            {
                player.GetModPlayer<RemnantPlayer>().EnemyProjectilesScaleBouns -= ProjectileSizeBonus / 100;
            }
            if (player.GetToggleValue("TyrantMarkEnemyProjectileSpeed", checkForMutantPresence: false))
            {
                player.GetModPlayer<RemnantPlayer>().EnemyProjectilesSpeedScaleBouns -= ProjectileSpeedBonus / 100f;
            }
            player.moveSpeed *= 1f + MovmentSpeedBonusBonus / 100;
        }

        Projectile GodZoneproj = null;
        public void SoulOfDiviniy(Player player)
        {
            if (player.GetToggleValue("DivineAura", checkForMutantPresence: false))
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<GodZone>()] <= 0)
                    GodZoneproj = Projectile.NewProjectileDirect(Projectile.GetSource_None(), player.position, Vector2.Zero, ModContent.ProjectileType<GodZone>(), 0, 0, player.whoAmI);
            }
            else
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<GodZone>()] > 0)
                {
                    foreach (var p in Main.projectile)
                    {
                        if (p != null && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<GodZone>())
                        {
                            p.Kill();
                        }
                    }
                }
                if (GodZoneproj != null)
                {
                    GodZoneproj.Kill();
                    GodZoneproj = null;
                }
            }
        }
        #endregion

    }
}