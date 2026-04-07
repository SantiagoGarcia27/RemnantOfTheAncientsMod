using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class SolarGlove : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(25);
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 36;
            Item.value = Item.buyPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FireGauntlet, 1)
            .AddIngredient(ItemID.SunStone, 1)
            .AddIngredient(ItemID.FragmentSolar, 10)
            .AddIngredient(ItemID.LunarBar, 5)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) += .14f;
            player.kbGlove = true;
            player.autoReuseGlove = true;
            player.meleeScaleGlove = true;
            player.GetAttackSpeed(DamageClass.Melee) += 0.14f;
            player.magmaStone = true;
            player.skyStoneEffects = true;
            player.GetModPlayer<RemnantPlayer>().MeleeBuffInflict.Add(BuffID.Daybreak);

            /*Mod CalamityMod = ModLoader.GetMod("CalamityMod");
            if (CalamityMod != null)

    player.GetDamage(DamageClass.Melee) += 0.20f; */
        }
    }
}