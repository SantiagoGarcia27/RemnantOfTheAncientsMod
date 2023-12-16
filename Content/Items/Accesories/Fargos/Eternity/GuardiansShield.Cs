using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.Global;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common;
using System.Collections.Generic;
using Terraria.ID;

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
            Item.rare = -12;     
            Item.accessory = true;
            Item.defense = 5;
            Item.GetGlobalItem<CustomTooltip>().EternityItem = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<RemnantPlayer>().EnemyProjectilesScaleBouns -= ProjectileSizeBonus / 100;
            player.GetModPlayer<RemnantPlayer>().EnemyProjectilesSpeedScaleBouns -= ProjectileSpeedBonus / 100f;
            player.moveSpeed *= 1f + MovmentSpeedBonusBonus / 100;

            player.GetModPlayer<RemnantPlayer>().MinionsBuffInflict.Add(ModContent.BuffType<Burning_Sand>());
            player.buffImmune[ModContent.BuffType<Burning_Sand>()] = true;
            player.GetModPlayer<RemnantPlayer>().DesertMedalion = true;

            player.GetModPlayer<RemnantPlayer>().FrostBarrier = true;
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
            .AddIngredient(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.FargosSoulMod, "DeviatingEnergy"), 5)
            .AddTile(ExternalModCallUtils.GetTileFromMod(RemnantOfTheAncientsMod.FargowiltasMod, "CrucibleCosmosSheet"))
            .Register();
        }
    }
}