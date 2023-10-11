using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Content.Projectiles.HeldItem;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
    public class Bloodtray : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Blood Ray");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Blood Ray");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Rayon de sang");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Rayo de sangre");
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }


        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.mana = 9;
            Item.rare = ItemRarityID.Blue;
            Item.width = 46;
            Item.height = 54;
            Item.useTime = 25;
            Item.UseSound = SoundID.Item34;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shootSpeed = 14f;
            Item.useAnimation = 49;
            Item.shoot = ProjectileID.RubyBolt;
            Item.value = Item.sellPrice(silver: 30);
            Item.autoReuse = true;
            Item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Item.damage = 30;
                Item.useTime = 23;
            }
        }

        public override bool AltFunctionUse(Player player) => true;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            if (player.altFunctionUse == 2)
            {
                Vector2 vector2 = velocity;

                //if (Charge++ >= 240) // after 120 ticks (== 2 seconds) in inventory, reset the attack pattern
                //    Charge = 0;
                float attackSpeedBonus = player.GetAttackSpeed(DamageClass.Generic);
                Item.useTime = (int)(65 / attackSpeedBonus);
                Item.useAnimation = (int)(65 / attackSpeedBonus);
                Item.mana = 30;

                if (player.ownedProjectileCounts[ModContent.ProjectileType<BloodRayHeldProj>()] <= 0)
                {
                    for (int proj = 0; proj <= Main.maxProjectiles; proj++)
                    {
                        if (Main.projectile[proj].type == ModContent.ProjectileType<BloodRayHeldProj>() && Main.projectile[proj].owner == player.whoAmI)
                        {
                            Main.projectile[proj].Kill();
                        }
                    }
                }

                if (player.ownedProjectileCounts[ModContent.ProjectileType<BloodRayHeldProj>()] <= 0)
                {
                    Projectile.NewProjectile(source, player.position, Vector2.Zero, ModContent.ProjectileType<BloodRayHeldProj>(), 0, 0, Main.myPlayer, velocity.X, velocity.Y, attackSpeedBonus); 
                }

                return false;
            }
            else
            {
                Item.mana = 9;
                Item.useAnimation = 49;
                Item.useTime = RemnantOfTheAncientsMod.CalamityMod != null ? 23 : 25;
              
                return true;
            }

        }
        float RangeMax = 10;
        public float fade = 2.6f;

        
        //public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        //{
        //    Player player = Main.player[Main.myPlayer];
        //    var texture = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Projectiles/Summon/Minioms/AreaEffect");
        //    Vector2 origin = new Vector2(texture.Width() * 0.5f, texture.Height() * 0.5f);//0.5
        //    if (Charge < 180)
        //    {
        //        Color color = new Color(Color.Green.R, Color.Green.G, Color.Green.B, 20) * fade;
        //        Main.spriteBatch.Draw((Texture2D)texture, player.Center - Main.screenPosition, null, color, 0f, origin, 3.2f, SpriteEffects.None, 0f);
        //    }
        //    return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        //}
        public void SpawnParticles(Player player,Vector2 position)
        {
            const int NUM_DUSTS = 150;
            for (int j = 0; j < ModContent.GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(NUM_DUSTS); j++)
            {
                Vector2 Position = Main.rand.NextVector2CircularEdge(player.width + 100, player.height + 100) + player.position;
                Vector2 Velocity = Position.DirectionTo(position) * 1f;

                var a = Dust.NewDustPerfect(Position, DustID.CrimsonTorch, Velocity, 100, Color.Red, 3f);
                a.noGravity = true;
                var m = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(new Rectangle(0, -150, 0, player.height + 230)) + player.position, DustID.CrimsonTorch, Velocity, 100, Color.Red, 3f);
                m.noGravity = true;
                var k = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(new Rectangle(-110, 0, player.width + 200, 0)) + player.position, DustID.CrimsonTorch, Velocity, 100, Color.Red, 3f);
                k.noGravity = true;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CrimtaneBar, 10)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}
