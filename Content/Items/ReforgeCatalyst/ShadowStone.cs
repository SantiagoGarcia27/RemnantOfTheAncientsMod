using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using SangarUtilities.Common.UtilsTweaks;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.ReforgeCatalyst
{
    [JITWhenModsEnabled("CalamityMod")]
    public class ShadowStone : ModReforgeCatalyst
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("CalamityMod", out mod);
        }
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 34;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Green;
            Item.SetCatalyst(true);

            Item.SetApplyPrice(Utils1.FormatMoney(Gold: 70));
            Item.SetReforges(CallUtils.TryGetPrefixFromMod(RemnantOfTheAncientsMod.RemnantOfTheAncients, "Shadow"));
        }
    }
}
