using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using FargowiltasSouls.Core.AccessoryEffectSystem;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity
{
    public class DesertMedalion : ModItem
    {

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod);
        }
        //string Key = Utils1.TooltipHotkeyString(FargosKeybindSystem.TogleFrostBarrier);
        int DurationSeconds = 10;
       // public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Utils1.TooltipHotkeyString(FargosKeybindSystem.TogleFrostBarrier), DurationSeconds);
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Ankh Amulet");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Amulette Ankh");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Amuleto de cruz ansata");
            //Tooltip.SetDefault("Increases maximum mana by 20,"
            ////+ "\nmana regeneration rate"
            ////+ "\nand armor penetration by 5."
            ////+ "\nprovide immunity to lava and fire blocks"
            ////+ "\nyou have the effect of Sweetheart Necklace, Charm of Myths,"
            ////+ "\nArctic Diving Gear and Star Veil"
            ////+ "\nGrants immunity to most debuffs");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente le maximum de mana de 20,"
            ////+ "\ntaux de régénération de mana"
            ////+ "\net pénétration d'armure de 5."
            ////+ "\nfournir une immunité à la lave et aux blocs de feu"
            ////+ "\nvous avez pour effet Collier chérie, Charme des mythes,"
            ////+ "\nÉquipement de plongée arctique et voile étoilé"
            ////+ "\nAccorde l'immunité à la plupart des debuffs");
            ////Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el maná máximo en 20,"
            ////+ "\nla regeneración de maná"
            ////+ "\ny penetración de armadura en 5."
            ////+ "\nproporciona inmunidad a la lava y los bloques de fuego"
            ////+ "\notorga el efecto de Collar de cariño, talismán de mitos"
            ////+ "\nEquipo de buceo ártico y Velo de estrella"
            ////+ "\nOtorga inmunidad a la mayoría de estados alterados.");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
       
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 6, 70, 72);
            Item.rare = -12;     
            Item.accessory = true;
            Item.GetGlobalItem<CustomTooltip>().EternityItem = true;
        }
        //public override void AddRecipes()
        //{
        //   CreateRecipe()
        //   .AddIngredient(ItemID.AnkhCharm, 1)
        //   .AddIngredient(ModContent.ItemType<Exotic_Amulet>(), 1)
        //   .AddTile(TileID.MythrilAnvil)
        //   .Register();
        //}
        [JITWhenModsEnabled("FargowiltasSouls")]
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DesertMedalionEffect>(base.Item);
           // player.GetModPlayer<RemnantFargosSoulsPlayer>().DesertMedalionEffect(player);
        }
        //public override void ModifyTooltips(List<TooltipLine> tooltips)
        //{
        //   Utils1.FindAndReplaceTooltip(tooltips,"{0}", Utils1.TooltipHotkeyString(FargosKeybindSystem.TogleFrostBarrier));
        //    Utils1.FindAndReplaceTooltip(tooltips,"{1}", DurationSeconds.ToString());
        //    base.ModifyTooltips(tooltips);
        //}
    }
}