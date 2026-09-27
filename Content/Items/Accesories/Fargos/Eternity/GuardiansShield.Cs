using CalamityMod.Items.Accessories;
using CalamityMod.Items.SummonItems;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using RemnantOfTheAncientsMod.Common;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using SangarUtilities.Common.UtilsTweaks;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity
{
    public class GuardiansShield : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod);
        }
        int ProjectileSizeBonus = 50;
        int ProjectileSpeedBonus = 10;
        int MovmentSpeedBonusBonus = 10;
        int DurationSeconds = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ProjectileSizeBonus, ProjectileSpeedBonus,MovmentSpeedBonusBonus);
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
       
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 16, 70, 72);
            Item.rare = ItemRarityID.Expert;     
            Item.accessory = true;
            Item.defense = 5;
            Item.GetGlobalItem<CustomTooltip>().EternityItem = true;
        }
        [JITWhenModsEnabled("FargowiltasSouls")]
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // player.GetModPlayer<RemnantFargosSoulsPlayer>().FrostBarrierEffect(player, hideVisual);       
            // player.GetModPlayer<RemnantFargosSoulsPlayer>().DesertMedalionEffect(player);
            // ModContent.GetInstance<DesertMedallion>().UpdateAccessory(player, hideVisual);

            RemnantFargosSoulsPlayer.ProjectileSpeedBonus = ProjectileSpeedBonus;
            RemnantFargosSoulsPlayer.ProjectileSizeBonus = ProjectileSizeBonus;
            player.AddEffect<DesertMedalionEffect>(base.Item);
            player.AddEffect<FrostBarrierEffect>(base.Item);
            player.GetModPlayer<RemnantFargosSoulsPlayer>().FrostBarrier = true;
            player.AddEffect<TyrantMarkEnemyProjectileSizeEffect>(base.Item);
            player.AddEffect<TyrantMarkEnemyProjectileSpeedEffect>(base.Item);
            TyrantsMark.AddEffects(player, MovmentSpeedBonusBonus);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utils1.FindAndReplaceTooltip(tooltips, "&0", Utils1.TooltipHotkeyString(FargosKeybindSystem.TogleFrostBarrier));
            Utils1.FindAndReplaceTooltip(tooltips, "&1", DurationSeconds.ToString());
            base.ModifyTooltips(tooltips);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<DesertMedalion>(), 1)
            .AddIngredient(ModContent.ItemType<FrostBarrier>(), 1)
            .AddIngredient(ModContent.ItemType<TyrantsMark>(), 1)
            .AddIngredient(CallUtils.TryGetItemFromMod(RemnantOfTheAncientsMod.FargosSoulMod, "DeviatingEnergy"), 5)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}