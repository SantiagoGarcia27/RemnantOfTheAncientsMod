using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Projectiles.Melee;
using Terraria.DataStructures;

namespace RemnantOfTheAncientsMod.Items.Mele
{
    public class BurningJungle : ModItem
    {
        public override void SetStaticDefaults()
        {
            ////DisplayName.SetDefault("Burning Jungle");
            ////Tooltip.SetDefault("Inflict poison and fire on enemies");

            ////DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Jungle brûlante");
            ////Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Infliger du poison et tirer sur les ennemis");

            ////DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Jungla Ardiente");
            ////Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inflije veneno y fuego a los enemigos");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.FieryGreatsword); 
            Item.DamageType = DamageClass.Melee;
            Item.width = 130;
            Item.height = 160;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.scale += 0.50f;
            Item.UseSound = SoundID.Item45;
            Item.autoReuse = true;
            Item.shoot = Item.shoot = ModContent.ProjectileType<MoltenGrassSwordLeaft>();
            Item.shootSpeed= 20f;

            /*if(RemnantOfTheAncientsMod.CalamityMod != null)
{
                Item.damage = 50;
                Item.scale = 1.70f;
            }*/
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position + (new Vector2(3*16,0)* player.direction), velocity, type, (int)(damage * 0.25f), knockback, Main.myPlayer, -0.07f * player.direction,0,0);
            return false;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 40);
            target.AddBuff(BuffID.Poisoned, 40);
            if (new RemnantOfTheAncientsMod().ParticleMeter(4) != 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_None(), target.position, new Vector2(0f, 0f), ProjectileID.Volcano, (int)(Item.damage * 0.5f), 0);
            }
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(6 - new RemnantOfTheAncientsMod().ParticleMeter(5)) && new RemnantOfTheAncientsMod().ParticleMeter(5) != 0) Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Pixie);
            if (Main.rand.NextBool(6 - new RemnantOfTheAncientsMod().ParticleMeter(5)) && new RemnantOfTheAncientsMod().ParticleMeter(5) != 0) Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.GrassBlades);

        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FieryGreatsword, 1)
            .AddIngredient(ItemID.BladeofGrass, 1)
            .AddTile(TileID.Hellforge)
            .Register();
        }
    }
}
