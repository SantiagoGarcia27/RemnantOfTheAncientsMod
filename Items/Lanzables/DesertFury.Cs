using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Lanzables
{
    public class DesertFury : ModItem
	{
		public override void SetStaticDefaults()
		{
	  	    DisplayName.SetDefault("Desert Fury");
            Tooltip.SetDefault("The power of the earth will indulge in this weapon.");
 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"fureur du désert");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La puissance de la terre se livrera à cette arme");

            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Furia desertica");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"El poder de la tierra se consentra en este arma");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

		public override void SetDefaults()
		{ 
	        Item.damage = 210;           
            Item.DamageType = DamageClass.Throwing;       
            Item.noMelee = true;
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 20;       
            Item.useAnimation = 20;   
            Item.useStyle = ItemUseStyleID.Swing;      
            Item.knockBack = 6;
            Item.value = Item.sellPrice(silver:1100);
            Item.rare = ItemRarityID.Cyan;
            Item.reuseDelay = 20;   
            Item.UseSound = SoundID.Item18;
            Item.autoReuse = true;
	        Item.shoot = ModContent.ProjectileType<DesertFuryP>();
            Item.shootSpeed = 28f;     
            Item.useTurn = true;
            Item.maxStack = 1;     
            Item.consumable = false; 
            Item.noUseGraphic = true;
		} 
	}
}
