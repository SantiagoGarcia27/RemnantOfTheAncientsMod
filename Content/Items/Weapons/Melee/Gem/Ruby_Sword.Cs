using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.Gem
{
    public class Ruby_Sword : ModItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Ruby Sword");
            // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée rubis");
            // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de rubí");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.DamageType = DamageClass.Melee;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.knockBack = 1;
            Item.value = 1400;
            Item.rare = 1;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Item.autoReuse = true;
                Item.scale = 2f;
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<SpiritGemSword>()] <= 4)
            {
                Vector2 Position = Main.rand.NextVector2CircularEdge(target.width + 100, target.height + 100) + target.position;
                Vector2 Velocity = Position.DirectionTo(target.position) * 1.01f;
                Projectile.NewProjectileDirect(Projectile.GetSource_None(), Position, Velocity, ModContent.ProjectileType<SpiritGemSword>(), damageDone / 2, Main.myPlayer, player.whoAmI, 0, 4);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.GoldBar, 8)
            .AddIngredient(ItemID.Ruby, 10)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}