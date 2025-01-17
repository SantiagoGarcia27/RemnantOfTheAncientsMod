using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Daylight;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon;
using RemnantOfTheAncientsMod.Content.Projectiles.Fargos;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos
{
    [ExtendsFromMod("FargowiltasSouls")]
    public class DaylightEnchant : BaseEnchant
    {
      
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod) != null;
        }
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        [ExtendsFromMod("FargowiltasSouls")]
        public class DaylightEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ForceOfRemantsHeader>();
            public override int ToggleItemType => ModContent.ItemType<DaylightEnchant>();


            Projectile Sunflowerproj = null;
            public override void PostUpdateEquips(Player player)
            {
                RemnantPlayer.DaylightArmorSetBonus = true;
                player.GetModPlayer<RemnantFargosSoulsPlayer>().DaylightEnchantment = true;
                if (Main.dayTime)
                {

                    if (player.ownedProjectileCounts[ModContent.ProjectileType<FloatingSunFlowerMinion>()] <= 0)
                        Sunflowerproj = Projectile.NewProjectileDirect(Projectile.GetSource_None(), player.position, Vector2.Zero, ModContent.ProjectileType<FloatingSunFlowerMinion>(), 0, 0, player.whoAmI);
                }
                else
                {
                    if (Sunflowerproj != null)
                    {
                        Sunflowerproj.Kill();
                        Sunflowerproj = null;
                    }
                    player.manaCost -= 0.05f;
                }
            }
        }
        public override void SetDefaults()
		{
			Item.width = 26; 
			Item.height = 26;
			Item.maxStack = 1; 
			Item.value = Item.buyPrice(0, 1, 10, 72);  
			Item.rare = 3;
			Item.accessory = true;
			Item.defense = 0;
		}
        public override Color nameColor => new Color(148, 214, 107);
		[JITWhenModsEnabled("FargowiltasSouls")]
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.AddEffect<DaylightEffect>(base.Item);
        }
        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            Utils1.FindAndReplaceTooltip(tooltips, "&0", Utils1.TooltipHotkeyString(FargosKeybindSystem.ToggNightTp), "Tooltip1");
            base.SafeModifyTooltips(tooltips);
        }
        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient<DaylightHat>()
            .AddIngredient<Daylight_chesplate>()
            .AddIngredient<Daylight_Legging>()
            .AddIngredient<BrightDaisy>()
            .AddIngredient<SunflowerStaff>()
            .AddTile(TileID.DemonAltar)
			.Register();
		}
	}
}
