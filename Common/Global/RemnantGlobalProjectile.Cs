using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.World;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global
{
    public class RemnantGlobalProjectile : GlobalProjectile
    {
        public bool Spear = false;
        public List<int> SpearsList = new List<int>();
        public int[] lanceId = new int[]
        {
            ProjectileID.Spear,
            ProjectileID.Trident,
            ProjectileID.DarkLance,
            ProjectileID.TheRottedFork,
            ProjectileID.ThunderSpear,
            ProjectileID.Swordfish,
            ProjectileID.CobaltNaginata,
            ProjectileID.PalladiumPike,
            ProjectileID.MythrilHalberd,
            ProjectileID.OrichalcumHalberd,
            ProjectileID.AdamantiteGlaive,
            ProjectileID.TitaniumTrident,
            ProjectileID.ChlorophytePartisan,
            ProjectileID.MushroomSpear,
            ProjectileID.ObsidianSwordfish,
            ProjectileID.NorthPoleWeapon,
            ProjectileID.JoustingLance,
            ProjectileID.HallowJoustingLance,
            ProjectileID.ShadowJoustingLance
        };
        public override bool InstancePerEntity => true;
        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.type == ProjectileID.BabySlime)
            {
                projectile.minionSlots = 0.5f;
            }
            if(projectile.type == ProjectileID.PiercingStarlight)
            {
                projectile.DamageType = DamageClass.Melee;
                projectile.friendly = true; 
            }
            SpearPostProces(projectile);
            
        }
        public void SpearPostProces(Projectile projectile)
        {
            AddVanilaSpear();
            AddModedSpearGeneric(projectile);

            if (SpearsList.Contains(projectile.type))
            {
                projectile.GetGlobalProjectile<RemnantGlobalProjectile>().Spear = true;
            }
            if (projectile.GetGlobalProjectile<RemnantGlobalProjectile>().Spear && Reaper.ReaperMode)
            {
                projectile.timeLeft *= 45;
            }
        }
        public void AddVanilaSpear()
        {
            AddSecureToList(SpearsList, ProjectileID.Spear);
            AddSecureToList(SpearsList, ProjectileID.Trident);
            AddSecureToList(SpearsList, ProjectileID.DarkLance);
            AddSecureToList(SpearsList, ProjectileID.TheRottedFork);
            AddSecureToList(SpearsList, ProjectileID.ThunderSpear);
            AddSecureToList(SpearsList, ProjectileID.Swordfish);
            AddSecureToList(SpearsList, ProjectileID.CobaltNaginata);
            AddSecureToList(SpearsList, ProjectileID.PalladiumPike);
            AddSecureToList(SpearsList, ProjectileID.MythrilHalberd);
            AddSecureToList(SpearsList, ProjectileID.OrichalcumHalberd);
            AddSecureToList(SpearsList, ProjectileID.AdamantiteGlaive);
            AddSecureToList(SpearsList, ProjectileID.TitaniumTrident);
            AddSecureToList(SpearsList, ProjectileID.ChlorophytePartisan);
            AddSecureToList(SpearsList, ProjectileID.MushroomSpear);
            AddSecureToList(SpearsList, ProjectileID.ObsidianSwordfish);
            AddSecureToList(SpearsList, ProjectileID.NorthPoleWeapon);
            AddSecureToList(SpearsList, ProjectileID.JoustingLance);
            AddSecureToList(SpearsList, ProjectileID.HallowJoustingLance);
            AddSecureToList(SpearsList, ProjectileID.ShadowJoustingLance);
        }

        public void AddModedSpearGeneric(Projectile projectile)
        {
            string[] LanceNames = new string[]
            {
                "Spear",
                "Lance",
                "Trient",
                "Partisan",
                "Halberd",
                "Pike",
                "Halberd",
                "Naginata"
            };

            if (projectile.DamageType == DamageClass.Melee)
            {
                for (int i = 0; i < LanceNames.Length; i++)
                {
                    if (Utils1.NameHasWord(projectile.Name, LanceNames[i]))
                    {
                        AddSecureToList(SpearsList, projectile.type);
                    }
                }
            }
        }
        public void AddSecureToList(List<int> list, int Content)
        {
            if (!list.Contains(Content)) list.Add(Content);
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (Reaper.ReaperMode && projectile.CountsAsClass(DamageClass.Melee) && projectile.friendly)
            {
                projectile.width *= (int)1.5f;
                projectile.height *= (int)1.5f;
                projectile.scale *= 2.5f;
            }
        }
    }
}