using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Night;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones;
using RemnantOfTheAncientsMod.Content.Items.Tools;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos
{
    [ExtendsFromMod("FargowiltasSouls")]
    public class NightEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod) != null;
        }
        [ExtendsFromMod("FargowiltasSouls")]
        public class NightEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ForceOfRemantsHeader>();
            public override int ToggleItemType => ModContent.ItemType<NightEnchant>();
            [JITWhenModsEnabled("FargowiltasSouls")]
            public override void PostUpdateEquips(Player player)
            {
                RemnantFargosSoulsPlayer.NightTpCouldownMax = player.FargoSouls().WizardEnchantActive ? (int)Utils1.FormatTimeToTick(0, 0, 0, 30):(int)Utils1.FormatTimeToTick(0, 0, 1, 0);
                if (!Main.dayTime)
                {
                    player.opacityForAnimation = RemnantFargosSoulsPlayer.NightTpCouldown == 0 ? 0.5f : 1;
                    player.aggro -= 500;
                    player.GetModPlayer<RemnantFargosSoulsPlayer>().NightTp = true;
                }
                else
                {
                    player.opacityForAnimation = 1;
                    player.GetModPlayer<RemnantFargosSoulsPlayer>().NightTp = false;
                }
            }
        }
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override Color nameColor => new(133, 15, 191);
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 1, 10, 72);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.defense = 0;
        } 
        [JITWhenModsEnabled("FargowiltasSouls")]
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<NightEffect>(base.Item);
        }  
        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            Utils1.FindAndReplaceTooltip(tooltips, "&0", Utils1.TooltipHotkeyString(FargosKeybindSystem.ToggNightTp), "Tooltip1");
            base.SafeModifyTooltips(tooltips);
        }
        
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<NightHelmetMe>()
            .AddIngredient<Nightchesplate>()
            .AddIngredient<NightLeggings>()
            .AddIngredient<night_pickaxe>()
            .AddIngredient<EndlessInvisibility>()
            .AddTile(TileID.DemonAltar)
            .Register();
        }
    }   
}
