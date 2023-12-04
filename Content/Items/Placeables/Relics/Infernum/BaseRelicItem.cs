using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.Relics.Infernum
{
    [JITWhenModsEnabled("InfernumMode")]
    public abstract class BaseRelicItem : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("InfernumMode", out mod);
        }
        public virtual string PersonalMessage => null;

        public virtual Color? PersonalMessageColor => null;

        public abstract string DisplayNameToUse { get; }

        public abstract int TileID { get; }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault(DisplayNameToUse);
            // Tooltip.SetDefault("GetsChanged");
            Item.ResearchUnlockCount = 1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine obj = tooltips.FirstOrDefault((x) => x.Name == "Tooltip0" && x.Mod == "Terraria");
            obj.Text = PersonalMessage ?? InfernumMode.Utilities.InfernalRelicText;
            obj.OverrideColor = PersonalMessageColor ?? Color.DarkRed;
        }
        [JITWhenModsEnabled("InfernumMode")]
        public override void SetDefaults()
        {
            int rarity = 0;
            try
            {
                rarity = ExternalModCallUtils.GetRarityFromMod(RemnantOfTheAncientsMod.InfernumMod, "InfernumRedRarity");
            }
            catch
            {
                rarity = 0;
            }
            Item.DefaultToPlaceableTile(TileID, 0);
            Item.width = 30;
            Item.height = 44;
            Item.maxStack = 999;
            Item.rare = rarity;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }
    }
}
