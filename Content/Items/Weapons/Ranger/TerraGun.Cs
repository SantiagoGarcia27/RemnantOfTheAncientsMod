using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static FargowiltasSouls.FargoSoulsSets;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
    public class TerraGun : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(50);
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 128;
            Item.crit = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 86;
            Item.height = 30;
            Item.useTime = 33;
            Item.useAnimation = 33;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(0, 15, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item40;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Bullet;
            Item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
            Item.GetGlobalItem<RemnantGlobalItem>().CanCharge = true;
            Item.ShotgunStats(BulletAmmount: 4, Unacurency: 5, CustomProjectileShoot: ModContent.ProjectileType<TerraBulletP>());         
        }   
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= .50f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.TacticalShotgun)
            .AddIngredient(ItemID.BrokenHeroSword, 1)
            .AddIngredient(ItemID.Celeb2, 1)
            .AddIngredient(ModContent.ItemType<CelestialAmalgamate>(), 5)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }
    }
}
