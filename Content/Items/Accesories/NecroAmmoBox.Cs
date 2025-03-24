using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class NecroAmmoBox : ModItem
    {
        public override void SetStaticDefaults()
        { 
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        int pierceBonus = 1;
        int pierceChance = 15;
        int ReinforcedIronBulletBonus = 20;
        int TombBulletBonus = 1;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(pierceChance, pierceBonus,ReinforcedIronBulletBonus,TombBulletBonus);
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.sellPrice(0,1,0, 50);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<ReinforcedAmmoBox>()
            .AddIngredient<BoneBar>(10)
            .AddIngredient<TuxoniteBar>(5)
            .AddIngredient(ItemID.Obsidian,10)
            .AddTile(TileID.BoneWelder)  
            .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<StatPlayer>().AddProjectilePenetration(DamageClass.Ranged, pierceBonus);
            player.GetModPlayer<StatPlayer>().SetRangerExtraProjectile(AmmoID.Bullet, ModContent.ProjectileType<ReinforcedBulletP>(), ReinforcedIronBulletBonus);
            player.GetModPlayer<StatPlayer>().SetRangerExtraProjectile(AmmoID.Bullet, ModContent.ProjectileType<Tomb_BulletP>(), TombBulletBonus);
            player.GetModPlayer<StatPlayer>().AddProjectilePenetrationChance(DamageClass.Ranged, pierceChance);
        }
    }
}