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
    public class ReinforcedAmmoBox : ModItem
    {
        public override void SetStaticDefaults()
        { 
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        int pierceBonus = 1;
        int pierceChance = 10;
        int ReinforcedIronBulletBonus = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(pierceChance,pierceBonus, ReinforcedIronBulletBonus);
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.sellPrice(0, 0, 10, 50);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<StatPlayer>().AddProjectilePenetration(DamageClass.Ranged, pierceBonus);
            player.GetModPlayer<StatPlayer>().SetRangerExtraProjectile(AmmoID.Bullet, ModContent.ProjectileType<ReinforcedBulletP>(), ReinforcedIronBulletBonus);
            player.GetModPlayer<StatPlayer>().AddProjectilePenetrationChance(DamageClass.Ranged, pierceChance);
        }
    }
}