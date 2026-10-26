using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Pets;
using RemnantOfTheAncientsMod.Content.Projectiles.Pets;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Items.pets.Master
{
    public class GaiaChisel : PetItem
    {
        public override int PetProjectile => ProjectileType<TortugaPetProjectile>();
        public override int PetBuff => BuffType<TortugaPetBuff>();
        public override bool Master => true;
    }
    public class SandolerUrn : PetItem
    {
        public override int PetProjectile => ProjectileType<TortugaPetProjectile>();
        public override int PetBuff => BuffType<TortugaPetBuff>();
        public override bool Master => true;
    }
    public class SnowrunBook : PetItem
    {
        public override int PetProjectile => ProjectileType<TortugaPetProjectile>();
        public override int PetBuff => BuffType<TortugaPetBuff>();
        public override bool Master => true;
    }
    public class BurnyrHammer : PetItem
    {
        public override int PetProjectile => ProjectileType<TortugaPetProjectile>();
        public override int PetBuff => BuffType<TortugaPetBuff>();
        public override bool Master => true;
    }

    public abstract class PetItem : ModItem
    {
        public virtual int PetProjectile { get; set; }
        public virtual int PetBuff { get; set; }
        public virtual bool Master { get; set; } = false;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.shoot = ProjectileType<TortugaPetProjectile>();
            Item.buffType = BuffType<TortugaPetBuff>();
            Item.master = Master;
            Item.sellPrice(gold: 5);
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600);
            }
        }
    }
}