using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.VanillaChanges;

namespace RemnantOfTheAncientsMod.Items.Mele
{
    public class TheSpiker : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Spiker");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Le Spiker");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La punzante");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;	
		}
		public static int counter = 0;
		public static int counter2 = 0;

		private int oldDamage;
		public override void SetDefaults()
		{
			Item.damage = 180;
			oldDamage = Item.damage;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 80;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.knockBack = 1;
			Item.useStyle = ItemUseStyleID.Thrust;
			Item.value = Item.sellPrice(gold: 30);
			Item.scale = 2.0f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.GetGlobalItem<GlobalItem1>().customRarity = CustomRarity.Legendary;
			Item.GetGlobalItem<GlobalItem1>().LegendaryDrop = true;
		}
		public override bool AltFunctionUse(Player player) => true;
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse != 2)
			{
                Item.useStyle = ItemUseStyleID.Thrust;
                if (counter <= 3) ModifyWeapon(false, ModContent.ProjectileType<InfernalSpike_f>(), 0f);
				else ModifyWeapon(true, ModContent.ProjectileType<InfernalSpikeF_f>(), 0f);                  
			}
			else
			{
                Item.useStyle = ItemUseStyleID.Swing;
                if (counter <= 3) ModifyWeapon(false, ModContent.ProjectileType<InfernalBall_f>(), 20f);
				else ModifyWeapon(false, ModContent.ProjectileType<InfernalBallF_f>(), 20f);
			}			
			return base.CanUseItem(player);
		}
		public void ModifyWeapon(bool strong, int proj, float speed)
		{
			if(strong)
			{
                Item.damage = oldDamage * 2;
                Item.shoot = proj;
				Item.shootSpeed = speed;
                counter = 0;
            }
			else
			{
                counter++;
                Item.damage = oldDamage;    
                Item.shoot = proj;
                Item.shootSpeed = speed;
            }

		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit) => target.defense = target.defense / 2;
    }
}
