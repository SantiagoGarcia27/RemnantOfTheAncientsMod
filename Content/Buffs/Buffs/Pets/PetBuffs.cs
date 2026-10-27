using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs.Pets
{
    public class YtBuff : PetBuff
    {
        public override int PetType => ModContent.ProjectileType<YtPet>();
    }
    public class YoutubeMoradoBuff : PetBuff
    {
        public override int PetType => ModContent.ProjectileType<TwitchPet>();
    }
    public class TortugaPetBuff : PetBuff
    {
        public override int PetType => ModContent.ProjectileType<TortugaPetProjectile>();
    }
    public class DakoVanityBuff : PetBuff
    {
        public override int PetType => ModContent.ProjectileType<DakoPetProjectile>();
    }
    public class CrusherPetBuff : PetBuff
    {
        public override int PetType => ModContent.ProjectileType<CrusherPetProjectile>();
    }
    public class DesertPetBuff : PetBuff
    {
        public override int PetType => ModContent.ProjectileType<DesertPetProjectile>();
    }
    public class FrozenPetBuff : PetBuff
    {
        public override int PetType => ModContent.ProjectileType<FrozenPetProjectile>();
    }
    public class TyrantPetBuff : PetBuff
    {
        public override int PetType => ModContent.ProjectileType<TyrantPetProjectile>();

        public override void Update(Player player, ref int buffIndex)
        {
            bool unused = false;
            player.buffTime[buffIndex] = 18000;

            player.BuffHandle_SpawnPetIfNeededAndSetTime(
                buffIndex,
                ref unused,
                ModContent.ProjectileType<TyrantPetProjectile>()
            );
        }
    }

    public abstract class PetBuff : ModBuff
    {
        public virtual int PetType { get; set; }
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.lightPet[Type] = false;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;

            int projType = PetType;

            if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0)
            {
                Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
            }
        }
    }
}