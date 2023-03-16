using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Projectiles.Mage;

namespace RemnantOfTheAncientsMod.Items.Magic
{
	public class Bloodtray : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Blood Ray");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Blood Ray");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Rayon de sang");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Rayo de sangre");
			Item.staff[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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

			if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				Item.damage = 30;
				Item.useTime = 23;
			}
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(5);

            position += Vector2.Normalize(velocity) * 1f;
			if (Main.rand.NextFloat() <= (float)1 / 10)
			{
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
					int p = Projectile.NewProjectile(source, position, perturbedSpeed * 10, ModContent.ProjectileType<CrimsomBolt>(), damage, knockback, player.whoAmI);
					Main.projectile[p].stepSpeed = 10f;
					Main.projectile[p].GetAlpha(Color.Red);
				}
                const int NUM_DUSTS = 150;
                for (int j = 0; j < ModContent.GetInstance<RemnantOfTheAncientsMod>().ParticlleMetter(NUM_DUSTS); j++)
                {
                    Vector2 Position = Main.rand.NextVector2CircularEdge(player.width + 100, player.height + 100) + player.position;
                    Vector2 Velocity = Position.DirectionTo(position) * 1f;

                    var a = Dust.NewDustPerfect(Position, DustID.CrimsonTorch, Velocity, 100, Color.Red, 3f);
                    a.noGravity = true;
                    var m = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(new Rectangle(0,-150,0, player.height + 230)) + player.position, DustID.CrimsonTorch, Velocity, 100, Color.Red, 3f);
                    m.noGravity = true;
                    var k = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(new Rectangle(-110, 0, player.width + 200, 0)) + player.position, DustID.CrimsonTorch, Velocity, 100, Color.Red, 3f);
                    k.noGravity = true;
                }
            }
            //Projectile.NewProjectile(source, position, velocity, type, damage, 1, player.whoAmI);
            return true;
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
