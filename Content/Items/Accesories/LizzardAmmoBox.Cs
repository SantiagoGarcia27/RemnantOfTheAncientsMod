using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class LizzardAmmoBox : ModItem
    {
        int pierceBonus = 2;
        int pierceChance = 40;
        int CritBonus = 10;
        int ReinforcedIronBulletBonus = 50;
        int CorruptBulletBonus = 20;
        int TombBulletBonus = 5;
        int ExplodingBulletBonus = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(pierceChance, pierceBonus, CritBonus, ReinforcedIronBulletBonus, TombBulletBonus, CorruptBulletBonus, ExplodingBulletBonus);
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 4, 10, 0);
            Item.sellPrice(0, 4, 10, 50);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<TitaniumAmmoBox>()
            .AddIngredient(ItemID.LihzahrdBrick, 10)
            .AddIngredient(ItemID.ShroomiteBar, 6)
            .AddIngredient(ItemID.EyeoftheGolem, 1)
            .AddTile(TileID.LihzahrdFurnace)
            .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            player.GetCritChance(DamageClass.Ranged) += CritBonus;
            player.GetModPlayer<StatPlayer>().SetProjectilePenetration(DamageClass.Ranged, pierceBonus);
            player.GetModPlayer<StatPlayer>().SetRangerExtraProjectile(AmmoID.Bullet, ModContent.ProjectileType<ReinforcedBulletP>(), ReinforcedIronBulletBonus);
            player.GetModPlayer<StatPlayer>().SetRangerExtraProjectile(AmmoID.Bullet, ModContent.ProjectileType<Tomb_BulletP>(), TombBulletBonus);

            if(!WorldGen.crimson)
                player.GetModPlayer<StatPlayer>().SetRangerExtraProjectile(AmmoID.Bullet, ProjectileID.CursedBullet, CorruptBulletBonus);
            else
                player.GetModPlayer<StatPlayer>().SetRangerExtraProjectile(AmmoID.Bullet, ProjectileID.IchorBullet, CorruptBulletBonus);

            player.GetModPlayer<StatPlayer>().SetRangerExtraProjectile(AmmoID.Bullet, ProjectileID.ExplosiveBullet, ExplodingBulletBonus);
            player.GetModPlayer<StatPlayer>().SetProjectilePenetrationChance(DamageClass.Ranged, pierceChance);
        }
    }
}