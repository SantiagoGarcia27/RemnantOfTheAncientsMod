using RemnantOfTheAncientsMod.Common.RemPlayer;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Core
{
    public abstract class CoreItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = 45000;
            Item.rare = ItemRarityID.Red;
            Item.expert = true;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!hideVisual) player.GetModPlayer<DrawEffectPlayer>().activeCores.Add(Texture);
        }
        public override void UpdateVanity(Player player)
        {
            player.GetModPlayer<DrawEffectPlayer>().activeCores.Add(Texture);
        }
    }
}