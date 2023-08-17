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
           // //DisplayName.SetDefault("Mechanical Personal Life Safe System");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Système mécanique de sécurité personnelle");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Sistema mecanizado de seguridad personal");
            //Tooltip.SetDefault("Summon two probes to protect you"+
                //"\nGreen shoots a green laser towards the player which will heal them"+
                //"\nWhile blue will intercept enemy projectiles from time to time");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoquez deux drones pour vous protéger" +
                //"\nVert tire un laser vert vers le joueur qui le soignera" +
                //"\nTandis que le bleu interceptera de temps en temps les projectiles ennemis");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca dos sondas para protegerte" +
                //"\nLa verde dispara lasers curativos hacia el jugador" +
                //"\nMientras que la azul dispara un rayo que intercepta los proyectiles enemigos de vez en cuando");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<RemnantPlayer>().HealingDrone = true;
            player.GetModPlayer<RemnantPlayer>().InterceptionDrone = true;
            // player.GetModPlayer<RemnantPlayer>().SpawnMinionItem(player);
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && (Main.projectile[i].type == ModContent.ProjectileType<InterceptionDrone>() || Main.projectile[i].type == ModContent.ProjectileType<HealingDrone>()))
                {
                    return;
                }
                else
                {
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<InterceptionDrone>()] < 1)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_None(), player.Center, Vector2.Zero, ModContent.ProjectileType<InterceptionDrone>(), 1, 0, Main.myPlayer);
                    }
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<HealingDrone>()] < 1)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_None(), player.Center, Vector2.Zero, ModContent.ProjectileType<HealingDrone>(), 1, 0, Main.myPlayer);
                    }
                }
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
