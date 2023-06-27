using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class magic_stick : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic Stick");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bâton magique");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Palo Mágico");

            Tooltip.SetDefault(tooltip());

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public static string tooltip()
        {
            return Utils1.IncreasedDamageByTooltip(10, DamageClass.Magic);
        }
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 0, 70, 72);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) *= 1.10f;
        }
    }
}