using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.ID;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class TrowerEmblem : ModItem
    {
       
        public override void SetStaticDefaults()
        {
            string TextUs = ModLoader.TryGetMod("CalamityMod", out _) ? "rogue" : "throwing";
            string TextFr = ModLoader.TryGetMod("CalamityMod", out _) ? "voyou" : "lancer";
            string TextEs = ModLoader.TryGetMod("CalamityMod", out _) ? "de picaro" : "lanzable";

            DisplayName.SetDefault("Thrower Emblem");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Emblème du lanceur");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Emblema de Lanzador");
            Tooltip.SetDefault(tooltip());

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public static string tooltip()
        {
            string option;
            if (ModLoader.TryGetMod("CalamityMod", out _))
            {
               DamageClass Rogue = ExternalModCallUtils.GetDamageClassFromMod(RemnantOfTheAncientsMod.CalamityMod, "RogueDamageClass");
                option = Utils1.IncreasedDamageByTooltip(15, Rogue);
            }
            else 
            {
                option = Utils1.IncreasedDamageByTooltip(15, DamageClass.Throwing);
            }
            return option;
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