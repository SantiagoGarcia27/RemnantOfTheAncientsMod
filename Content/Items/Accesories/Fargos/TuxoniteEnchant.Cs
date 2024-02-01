using FargowiltasSouls.Core.AccessoryEffectSystem;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Tuxonite;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos
{
    public class TuxoniteEnchant : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod);
        }
        int CritBonus = 30;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(CritBonus);
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 0, 2, 72);
            Item.rare = 1;
            Item.accessory = true;
        }
        [JITWhenModsEnabled("FargowiltasSouls")]
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<TuxoniteEffect>(base.Item);
           // player.GetModPlayer<RemnantFargosSoulsPlayer>().TuxoniteEnchant(player, CritBonus);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<TuxoniteHelmet>()
            .AddIngredient<Tuxonite_chesplate>()
            .AddIngredient<Tuxonite_Legging>()
            .AddIngredient<minigun>()
            .AddIngredient(ItemID.SpikyBall, 10)
            .AddTile(TileID.DemonAltar)
            .Register();
        }
    }
}
