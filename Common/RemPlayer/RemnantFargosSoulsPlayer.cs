using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Gemstone;
using RemnantOfTheAncientsMod.Content.Projectiles.Multiclass;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.RemPlayer
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
        public int FrostBarrierCounter;
        public bool DesertMedalion;

        public bool StoneBand;
        public int OrbitalSaphireCount;


        public bool NightTp;
        public int NightTpCouldown { get; set; }
        public int NightTpCouldownMax { get; set; }

        public bool tuxoniteEnchantCrit;
        public int tuxoniteEnchantCritDuration = 0;
        public float tuxoniteEnchantCritCounter = 0;

        public float ProjectileSpeedBonus = 0;
        public float ProjectileSizeBonus = 0;

        public override void ResetEffects()
        {
            FrostBarrier = false;
            DesertMedalion = false;
            StoneBand = false;
            IronBallistaEnchantment = false;
            NightTp = false;
            DaylightEnchantment = false;
            tuxoniteEnchantCrit = false;
            OrbitalSaphireCount = 0;
        }


        private float highestY;
        private bool wasOnGround;
        private bool initialized;

        public float LastFallHeight { get; private set; }

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

            if (!Player.GetToggleValue<DivineAuraEffect>() && Player.ownedProjectileCounts[ModContent.ProjectileType<GodZone>()] > 0)
            {
                Projectile proj = Utils1.SearchFirstProjectile(ModContent.ProjectileType<GodZone>(),false,false);
                if (proj != null && proj.owner == Player.whoAmI && proj.type == ModContent.ProjectileType<GodZone>()) proj.Kill();     
            }



            bool onGround = Player.velocity.Y == 0f && Collision.SolidCollision(new Vector2(Player.position.X, Player.Bottom.Y), Player.width, 2, true);

            if (!initialized)
            {
                initialized = true;
                wasOnGround = onGround;
                highestY = Player.Bottom.Y;
                return;
            }
            if (!onGround)
            {
                if (wasOnGround) highestY = Player.Bottom.Y;
                highestY = MathHelper.Min(highestY, Player.Bottom.Y);
            }

            if (onGround && !wasOnGround)
            {
                float fallPixels = Player.Bottom.Y - highestY;
                LastFallHeight = MathHelper.Max(0f, fallPixels / 16f);

                if (StoneBand && LastFallHeight > 15)
                {
                    int numProj = 5;

                    int offset = 3.ToCoordinatePosition();
                    int projID = ProjectileID.DD2ExplosiveTrapT2Explosion;

                    int damageBase = 10;

                    for (int i = -numProj; i <= numProj; i++)
                    {
                        Vector2 pos = new(Player.Center.X + (i * 2).ToCoordinatePosition(), (Player.Bottom.Y) - offset);

                        int posX = (int)pos.X;
                        int posY = (int)pos.Y;

                        StatModifier genericDamage = Player.GetDamage(DamageClass.Generic);
                        float bonus = (Player.GetDamage(DamageClass.Generic).Additive - 1f);

                        int damage = damageBase + (int)(damageBase * bonus); 

                        int index = Projectile.NewProjectile(Player.GetSource_TileInteraction(posX, posY), pos, Vector2.Zero, projID, damage, 1, Player.whoAmI, ai0: Player.whoAmI);   
                    }
                }
            }

            if (Player.ownedProjectileCounts[ModContent.ProjectileType<GemstoneCrusherProj_Sapphire>()] < OrbitalSaphireCount)
            {
                int damageBase = 20;

                StatModifier genericDamage = Player.GetDamage(DamageClass.Generic);
                float bonus = (Player.GetDamage(DamageClass.Generic).Additive - 1f);

                int damage = damageBase + (int)(damageBase * bonus);


                int index = Projectile.NewProjectile(Player.GetSource_None(), Player.Center + new Vector2(16 * Player.direction, 0), Vector2.Zero, ModContent.ProjectileType<GemstoneCrusherProj_Sapphire>(), damage, 2, Player.whoAmI);
                Main.projectile[index].friendly = true;
                Main.projectile[index].hostile = false;
                Main.projectile[index].timeLeft = Utils1.FormatTimeToTick(Second: 3);
            }

            wasOnGround = onGround;
        }
    }
}