using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones
{
    public class Darksign : ModItem
    {
       // public override bool IsLoadingEnabled(Mod mod) => false;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        int damageBonu = 20;
        int lifeBonus = 20;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(damageBonu, lifeBonus);
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BuilderPotion);
            Item.value = 0;
            Item.buffType = ModContent.BuffType<DarksignBuff>();
            Item.buffTime = Utils1.FormatTimeToTick(0, 0, 5, 0);
        }

    }
    public class Endless_Darksign : ModelEndless
    {
        int damageBonu = 20;
        int lifeBonus = 20;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(damageBonu, lifeBonus);
        public override Item ItemBase => new(ModContent.ItemType<Darksign>());
        public override void SetStaticDefaults()
        {
        }
    }
}