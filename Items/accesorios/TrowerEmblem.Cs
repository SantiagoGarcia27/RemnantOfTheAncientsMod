using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Items.accesorios
{
    public class TrowerEmblem : ModItem
    {
       
        public override void SetStaticDefaults()
        {
            string TextUs = ModLoader.TryGetMod("CalamityMod", out _) ? "rogue" : "throwing";
            string TextFr = ModLoader.TryGetMod("CalamityMod", out _) ? "voyou" : "lancer";
            string TextEs = ModLoader.TryGetMod("CalamityMod", out _) ? "de picaro" : "lanzable";

            DisplayName.SetDefault("Thrower Emblem");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Emblème du Lanceur");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema de Lanzador");
            Tooltip.SetDefault("Increases "+ TextUs + " damage by 15% ");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente les dégâts de "+ TextFr +" de 15%");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el daño "+ TextEs + " en un 15%");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = new Item(ItemID.WarriorEmblem).value;
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Throwing) *= 1.15f;
        }
    }
}