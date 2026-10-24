using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity
{
    [JITWhenModsEnabled("FargowiltasSouls")]
    public class StoneBand : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod);
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
       
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 2, 70, 72);
            Item.rare = ItemRarityID.Expert;     
            Item.accessory = true;
            Item.GetGlobalItem<CustomTooltip>().EternityItem = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<StoneBandEffect>(Item);
        }
    }

    [ExtendsFromMod("FargowiltasSouls")]
    public class StoneBandEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<GuardiansShieldHeader>();
        public override int ToggleItemType => ModContent.ItemType<StoneBand>();

        public override void PostUpdateEquips(Player player)
        {
            player.GetModPlayer<RemnantFargosSoulsPlayer>().StoneBand = true;
        }
    }
}