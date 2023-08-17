using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic.Sergy
{
    [AutoloadEquip(EquipType.Legs)]
    public class Flower_Defender_Legs : ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Sangar´s head");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tête de Sangar");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cabeza de Sangar");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            
        }
        public override void SetDefaults()
        {
            Item.Size = new(30,30);
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Cyan;
        }
        
        public override void UpdateVanity(Player player)
        {
           
            base.UpdateVanity(player);
        }
    }
}