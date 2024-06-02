using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Projectiles.Multiclass;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    internal class MechanicalPersonalLifeSafeSystem : ModItem
    {
        public override void SetStaticDefaults()
        {        
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<RemnantPlayer>().HealingDrone = true;
            player.GetModPlayer<RemnantPlayer>().InterceptionDrone = true;

            if(player.ownedProjectileCounts[ModContent.ProjectileType<InterceptionDrone>()] < 2)
            {
                Projectile.NewProjectile(Projectile.GetSource_None(), player.Center, Vector2.Zero, ModContent.ProjectileType<InterceptionDrone>(), 1, 0, Main.myPlayer, 1 * 10);
                Projectile.NewProjectile(Projectile.GetSource_None(), player.Center, Vector2.Zero, ModContent.ProjectileType<InterceptionDrone>(), 1, 0, Main.myPlayer, 2 * 10);
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<HealingDrone>()] < 2)
            {
                Projectile.NewProjectile(Projectile.GetSource_None(), player.Center, Vector2.Zero, ModContent.ProjectileType<HealingDrone>(), 1, 0, Main.myPlayer, 1 * 1 * 10);
                Projectile.NewProjectile(Projectile.GetSource_None(), player.Center, Vector2.Zero, ModContent.ProjectileType<HealingDrone>(), 1, 0, Main.myPlayer, 1 * 2 * 10);
            }
        }
        public override void SetDefaults()
        {
            Item.height = 34;
            Item.width = 30;
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.master = true;
            Item.defense = 5;
            Item.value = Item.buyPrice(0, 2, 70, 72);
        } 
    }
}
