using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.GameContent.Creative;
using CalamityMod;
using CalamityMod.CalPlayer;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Night
{
	[AutoloadEquip(EquipType.Head)]
	public class NightHelmetL : ModItem
	{
		public override void SetStaticDefaults()
		{	
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 50000;
			Item.rare = ItemRarityID.Orange;
			Item.defense = 9;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Throwing) *= 1.13f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<Nightchesplate>() && legs.type == ModContent.ItemType<NightLeggings>();
		}
        int DefenseBonus = 5;
        int ThrowerDamageSetBonus = 6;
        int SpeedBonus = 5;
		int StealthBonus = 80;
		public override void UpdateArmorSet(Player player)
		{

			player.setBonus = this.GetLocalization("SetBonus").WithFormatArgs(DefenseBonus, ThrowerDamageSetBonus, SpeedBonus).Value;
			player.setBonus = RemnantOfTheAncientsMod.CalamityMod != null? 
				this.GetLocalization("SetBonus").WithFormatArgs(DefenseBonus, ThrowerDamageSetBonus, SpeedBonus).Value : 
				this.GetLocalization("SetBonusCalamity").WithFormatArgs(DefenseBonus, ThrowerDamageSetBonus, SpeedBonus, StealthBonus).Value;  // "+5 Defense, Increases throwable damage by 6%, Increases movement speed";
            player.GetDamage(DamageClass.Throwing) += ThrowerDamageSetBonus/100f;
			player.statDefense += DefenseBonus;
			player.moveSpeed += SpeedBonus/100f;

			if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				ArmorSetCalamity(player);
			}
		}
		[JITWhenModsEnabled("CalamityMod")]
		public void ArmorSetCalamity(Player player)
		{
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
                CalamityPlayer calamityPlayer = player.Calamity();
				calamityPlayer.umbraphileSet = true;
				calamityPlayer.rogueStealthMax += StealthBonus / 100f;
				//player.setBonus = "+5 Defense, Increases throwable damage by 6%, Increases movement speed and +80 maximum stealth";
				player.Calamity().wearingRogueArmor = true;
			}
		}

		public override void AddRecipes()
		{
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                CreateRecipe()
                 .AddRecipeGroup("anyCorruptHelmet")
                 .AddIngredient(ItemID.JungleHat)
                 .AddIngredient(ItemID.NecroHelmet)
                 .AddIngredient(ItemID.MoltenHelmet)
                 .AddIngredient(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PurifiedGel"), 5)
                 .AddTile(TileID.DemonAltar)
                 .Register();

                CreateRecipe()
                .AddIngredient(ModContent.ItemType<NightBar>(), 15)
                .AddIngredient(ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PurifiedGel"), 5)
                .AddTile(TileID.DemonAltar)
                .Register();
            }
            else
            {
                CreateRecipe()
                .AddRecipeGroup("anyCorruptHelmet")
                 .AddIngredient(ItemID.JungleHat)
                 .AddIngredient(ItemID.NecroHelmet)
                 .AddIngredient(ItemID.MoltenHelmet)
                .AddTile(TileID.DemonAltar)
                .Register();

                CreateRecipe()
                .AddIngredient(ModContent.ItemType<NightBar>(), 15)
                .AddTile(TileID.DemonAltar)
                .Register();
            }
        }
	}
}