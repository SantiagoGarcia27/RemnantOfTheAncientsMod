using System.Collections.Generic;
using FargowiltasSouls.Common.Graphics.Shaders;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity
{
    [ExtendsFromMod("FargowiltasSouls")]
    [AutoloadEquip(EquipType.Wings)]
	public class SoulOfDivinity : FlightMasteryWings
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod);
		}
		protected override bool HasSupersonicSpeed => true;

		//public override bool Eternity => true;

		//public override int NumFrames => 10;

		public static int WingSlotID { get; private set; }

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			WingSlotID = Item.wingSlot;
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 10));
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
		}

		public override void SafeModifyTooltips(List<TooltipLine> tooltips)
		{		
		}

		public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
		{
			if (line.Mod == "Terraria" && line.Name == "ItemName")
			{
				Main.spriteBatch.End();
				Main.spriteBatch.Begin((SpriteSortMode)1, null, null, null, null, null, Main.UIScaleMatrix);
				ShaderManager.GetShaderIfExists("Text").SetMainColor(new Color(42, 42, 99)).SetSecondaryColor(FargowiltasSouls.FargowiltasSouls.EModeColor())
				.Apply(setCommonParams: true, "PulseUpwards");
				Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2((float)line.X, (float)line.Y), Color.White);
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(0, null, null, null, null, null, Main.UIScaleMatrix);
				return false;
			}
			return true;
		}
        //public override bool WingUpdate(Player player, bool inUse)
        //{
        //    int WingTicks = (!inUse) ? 8 : 5;
        //    if (player.velocity.Y != 0f)
        //    {
        //        player.wingFrameCounter++;
        //        if (player.wingFrameCounter > WingTicks)
        //        {
        //            player.wingFrameCounter = 0;
        //            if (player.wingFrame++ >= 5)
        //            {
        //                player.wingFrame = 0;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        player.wingFrame = 5;
        //    }
        //    return true;
        //}

        public override void SetDefaults()
		{
			base.SetDefaults();
			Item.rare = 10;
            Item.value = 200000000;
			Item.shieldSlot = 5;
			Item.defense = 100;
			Item.useStyle = 4;
			Item.useTime = 180;
			Item.useAnimation = 180;
			Item.UseSound = SoundID.Item6;
		}

		public override void UseItemFrame(Player player)
		{
			SandsofTime.Use(player);
		}

		public override bool? UseItem(Player player)
		{
			return true;
		}

		public override void UpdateInventory(Player player)
		{
			ModContent.GetInstance<EternitySoul>().UpdateInventory(player);
		}

		public override void UpdateVanity(Player player)
		{
			ModContent.GetInstance<EternitySoul>().UpdateInventory(player);
		}
       
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			// Draw the periodic glow effect behind the item when dropped in the world (hence PreDrawInWorld)
			Texture2D texture = TextureAssets.Item[Item.type].Value;

			Rectangle frame;

			if (Main.itemAnimations[Item.type] != null)
			{
				// In case this item is animated, this picks the correct frame
				frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
			}
			else
			{
				frame = texture.Frame();
			}

			Vector2 frameOrigin = frame.Size() / 2f;
			Vector2 offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
			Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

			float time = Main.GlobalTimeWrappedHourly;
			float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

			time %= 4f;
			time /= 2f;

			if (time >= 1f)
			{
				time = 2f - time;
			}

			time = time * 0.5f + 0.5f;

			for (float i = 0f; i < 1f; i += 0.25f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(255, 239, 120, 50), rotation, frameOrigin, scale, SpriteEffects.None, 0);
			}

			for (float i = 0f; i < 1f; i += 0.34f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(252, 244, 179, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
			}

			return true;
		}
    

    public override void UpdateAccessory(Player player, bool hideVisual)
		{
            ModContent.GetInstance<ForceOfRemants>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GuardiansShield>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<EternitySoul>().UpdateAccessory(player, hideVisual);
			player.GetModPlayer<RemnantFargosSoulsPlayer>().SoulOfDiviniy(player);
            player.GetModPlayer<RemnantPlayer>().Inmortal = true;
           // WingUpdate(player, true);

        }

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient<The_Legion>()
			.AddIngredient(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.FargosSoulMod, "EternitySoul"))
			.AddTile(ExternalModCallUtils.GetTileFromMod(RemnantOfTheAncientsMod.FargowiltasMod, "CrucibleCosmosSheet"))
			.Register();
		}
	}
}

