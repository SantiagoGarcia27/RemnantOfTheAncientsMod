using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
	public class ButterflySword : ModItem
	{

        private static Asset<Texture2D> texture;
        private static Asset<Texture2D> textureDagger;
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 8;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.sellPrice(silver: 27);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.scale = 1f;
		}
		public override bool AltFunctionUse(Player player) => true;
        public override bool CanShoot(Player player)
        {
			if (player.ownedProjectileCounts[Item.shoot] > 1) return false;		
            return base.CanShoot(player);
        }

		int HitCounter = 0;
		int HitCounterMax = 3;

		bool ShortMode = false;
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
            int ProjType = ModContent.ProjectileType<NaturalSpiritButerfly>();
			if (!ShortMode && HitCounter < HitCounterMax)
			{
				HitCounter++;
                return;
			}
			else
			{
				if (player.ownedProjectileCounts[ProjType] < 1)
				{
                    const int NUM_DUSTS = 20;
                    for (int i = 0; i < RemnantOfTheAncientsMod.ParticleMeter(NUM_DUSTS); i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(3f, 18f);

                        Dust dust = Dust.NewDustPerfect(player.Center + new Vector2(4,0).ToCoordenatePosition() * player.direction, DustID.JungleSpore, velocity, Scale: 1.5f);

                        dust.noGravity = true;
                    }

                    Projectile.NewProjectile(Item.GetSource_FromThis(), player.Center, Vector2.Zero, ProjType, Item.damage / 2, 1, player.whoAmI);
                    ChangeMode(player, true);
                }
				else
				{
					HitCounter = 0;
                }
            }
		}
        public override bool CanUseItem(Player player)
        {
            ChangeMode(player, ShortMode);
            return true;
        }

        private void ChangeMode(Player player, bool newMode)
        {
            if (ShortMode == newMode)
                return;

            ShortMode = newMode;

            Item.useStyle = ShortMode ? ItemUseStyleID.Thrust : ItemUseStyleID.Swing;

            if (player.HeldItem == Item)
            {
                player.itemAnimation = 0;
                player.itemTime = 0;
            }
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (ShortMode) damage.Base *= 0.75f;
            base.ModifyWeaponDamage(player, ref damage);
        }

        public override void UpdateInventory(Player player)
        {
            int ProjType = ModContent.ProjectileType<NaturalSpiritButerfly>();
            if (player.ownedProjectileCounts[ProjType] < 1)
            {
                ChangeMode(player, false);
            }
            base.UpdateInventory(player);
        }

        public override void Load()
        {
            if (Main.dedServ)
                return;

            texture = ModContent.Request<Texture2D>(
                Texture,
                AssetRequestMode.ImmediateLoad
            );

            textureDagger = ModContent.Request<Texture2D>(
                Texture + "_Dagger",
                AssetRequestMode.ImmediateLoad
            );
        }
        public override void HoldItem(Player player)
        {

            if (Main.dedServ)
                return;

            ChangeMode(player, ShortMode);

           // TextureAssets.Item[Type] = ShortMode ? textureDagger : texture;
        }

        public override void Unload()
        {
            texture = null;
            textureDagger = null;
        }

        public override bool ModifyItemDraw(ref PlayerDrawSet drawInfo, ref DrawData drawData, ref DrawData? coloredDrawData, ref DrawData? glowMaskDrawData)
        {
            if (!ShortMode)
                return true;

            Texture2D dagger = textureDagger.Value;

            // Dimensiones de la textura original
            float oldWidth = drawData.sourceRect?.Width ?? drawData.texture.Width;

            float oldHeight = drawData.sourceRect?.Height ?? drawData.texture.Height;

            // Adaptar el punto de origen a la nueva textura
            drawData.origin = new Vector2(
                drawData.origin.X * dagger.Width / oldWidth,
                drawData.origin.Y * dagger.Height / oldHeight
            );

            drawData.texture = dagger;
            drawData.sourceRect = new Rectangle(0, 0, dagger.Width, dagger.Height);

            // Mantener la capa de color, si existe
            if (coloredDrawData.HasValue)
            {
                DrawData colored = coloredDrawData.Value;
                colored.texture = dagger;
                colored.sourceRect = drawData.sourceRect;
                colored.origin = drawData.origin;
                coloredDrawData = colored;
            }

            return true;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (!ShortMode)
                return true;

            Texture2D dagger = textureDagger.Value;

            float ratio = MathHelper.Min(
                (float)frame.Width / dagger.Width,
                (float)frame.Height / dagger.Height
            );

            spriteBatch.Draw(dagger, position, null, drawColor, 0f, new Vector2(dagger.Width / 2f, dagger.Height / 2f), scale * ratio, SpriteEffects.None, 0f);

            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            if (!ShortMode)
                return true;

            Texture2D dagger = textureDagger.Value;

            Vector2 origin = new(
                dagger.Width / 2f,
                dagger.Height / 2f
            );

            Vector2 position = Item.Bottom - Main.screenPosition - new Vector2(0f, origin.Y * scale);

            spriteBatch.Draw(dagger, position, null, alphaColor, rotation, origin, scale, SpriteEffects.None, 0f);

            return false;
        }
    }
}

