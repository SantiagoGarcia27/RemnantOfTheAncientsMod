using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity
{
    [ExtendsFromMod("FargowiltasSouls")]
    public class StoneRing : SoulsItem
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
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
            Item.GetGlobalItem<CustomTooltip>().EternityItem = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<StoneBandEffect>(Item);
            player.AddEffect<StoneBandSappireEffect>(Item);
        }
    }

    [ExtendsFromMod("FargowiltasSouls")]
    public class StoneBandEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<GuardiansShieldHeader>();
        public override int ToggleItemType => ModContent.ItemType<StoneRing>();
        public override void PostUpdateEquips(Player player) =>  player.GetModPlayer<RemnantFargosSoulsPlayer>().StoneBand = true;
        
    }
    [ExtendsFromMod("FargowiltasSouls")]
    public class StoneBandSappireEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<GuardiansShieldHeader>();
        public override int ToggleItemType => ModContent.ItemType<StoneRing>();

        public override void PostUpdateEquips(Player player) => player.GetModPlayer<RemnantFargosSoulsPlayer>().OrbitalSaphireCount += 1;
    }
}