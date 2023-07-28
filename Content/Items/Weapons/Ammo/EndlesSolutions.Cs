using Terraria.Localization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo
{
    public class EndlesGreenSolution : EndlessSolutions
    {
        public override Item ItemBase => new(ItemID.GreenSolution);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless green solution");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Solution verte sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Solución verde eterna");
        }
    }
    public class EndlesBlueSolution : EndlessSolutions
    {
        public override Item ItemBase => new(ItemID.BlueSolution);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless blue solution");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Solution bleu sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Solución azul eterna");
        }
    }
    public class EndlesPurpleSolution : EndlessSolutions
    {
        public override Item ItemBase => new(ItemID.PurpleSolution);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless purple solution");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Solution violet sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Solución morada eterna");
        }
    }
    public class EndlesRedSolution : EndlessSolutions
    {
        public override Item ItemBase => new(ItemID.RedSolution);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless red solution");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Solution rouge sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Solución roja eterna");
        }
    }
    public class EndlesDarkBlueSolution : EndlessSolutions
    {
        public override Item ItemBase => new(ItemID.DarkBlueSolution);
        public override string TexturePath => null;
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Endless dark blue solution");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Solution bleu foncé sans fin");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Solución azul obscura eterna");
        }
    }

    public abstract class EndlessSolutions : ModItem
    {
        public abstract Item ItemBase { get; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public abstract string? TexturePath { get; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public override string Texture => GetTextureFromItem(ItemBase);
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemBase.type);
            Item.maxStack = 1;
            Item.consumable = false;
            Item.value += Item.sellPrice(0, 1, 0, 0);
            Item.rare += 1;
        }
        public string GetTextureFromItem(Item item)
        {
            string Texture;
            if (TexturePath != null)
            {
                Texture = TexturePath;
            }
            else Texture = item.type < ItemID.Count ? "Terraria/Images/Item_" + item.type : ItemLoader.GetItem(item.type).Texture;
            return Texture;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemBase.type, 3996)
            .AddTile(TileID.CrystalBall)
            .Register();
        }
    }
}
