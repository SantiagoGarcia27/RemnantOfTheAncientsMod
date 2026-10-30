using CalamityMod.Items.LoreItems;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.CalamityLore;

public class LoreGemstoneCrusher : CalamityLoreItem
{
    public override int TrophyType => ModContent.ItemType<CrusherTrophy>();
    public override int Rare => ItemRarityID.Green;
}
public class LoreDesertAniquilator : CalamityLoreItem
{
    public override int TrophyType => ModContent.ItemType<DesertTrophy>();
    public override int Rare => ItemRarityID.Orange;
}
public class LoreFrozen : CalamityLoreItem
{
    public override int TrophyType => ModContent.ItemType<FrostTrophy>();
    public override int Rare => ItemRarityID.Pink;
}
public class LoreInfernalTyrant : CalamityLoreItem
{
    public override int TrophyType => ModContent.ItemType<InfernalTrophy>();
    public override int Rare => ItemRarityID.Lime;
}

[ExtendsFromMod("CalamityMod")]
public abstract class CalamityLoreItem : LoreItem
{
    public virtual int TrophyType { get; set; }
    public virtual int Rare { get; set; } = ItemRarityID.Orange;
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
    }

    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.TryGetMod("CalamityMod", out mod);
    }

    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 20;
        Item.rare = Rare;
        Item.consumable = false;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(TrophyType)
            .AddTile(TileID.Bookcases)
            .Register();
    }
}
