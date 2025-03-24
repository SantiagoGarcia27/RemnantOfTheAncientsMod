using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class TitaniumAmmoBox : ModItem
    {
        int pierceBonus = 2;
        int pierceChance = 20;
        int ReinforcedIronBulletBonus = 40;
        int CorruptBulletBonus = 10;
        int TombBulletBonus = 2;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(pierceChance, pierceBonus, ReinforcedIronBulletBonus, TombBulletBonus, CorruptBulletBonus);
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 2, 10, 0);
            Item.sellPrice(0, 2, 10, 50);
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<NecroAmmoBox>()
            .AddRecipeGroup("anyHardmodeTier3Bar", 5)
            .AddIngredient(ItemID.CursedFlame, 6)
            .AddTile(TileID.MythrilAnvil)
            .Register();

            CreateRecipe()
            .AddIngredient<NecroAmmoBox>()
            .AddRecipeGroup("anyHardmodeTier3Bar", 5)
            .AddIngredient(ItemID.Ichor, 6)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<StatPlayer>().AddProjectilePenetration(DamageClass.Ranged, pierceBonus);
            player.GetModPlayer<StatPlayer>().SetRangerExtraProjectile(AmmoID.Bullet, ModContent.ProjectileType<ReinforcedBulletP>(), ReinforcedIronBulletBonus);
            player.GetModPlayer<StatPlayer>().SetRangerExtraProjectile(AmmoID.Bullet, ModContent.ProjectileType<Tomb_BulletP>(), TombBulletBonus);

            if(!WorldGen.crimson)
                player.GetModPlayer<StatPlayer>().SetRangerExtraProjectile(AmmoID.Bullet, ProjectileID.CursedBullet, CorruptBulletBonus);
            else
                player.GetModPlayer<StatPlayer>().SetRangerExtraProjectile(AmmoID.Bullet, ProjectileID.IchorBullet, CorruptBulletBonus);

            player.GetModPlayer<StatPlayer>().AddProjectilePenetrationChance(DamageClass.Ranged, pierceChance);
        }
    }
}