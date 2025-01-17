using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Desert;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos
{
    [ExtendsFromMod("FargowiltasSouls")]
    public class SandHeraldEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod) != null;
        }
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 26; 
			Item.height = 26;
			Item.maxStack = 1; 
			Item.value = Item.buyPrice(0, 0, 2, 72);  
			Item.rare = 3;
			Item.accessory = true;
		}
        public override Color nameColor => new Color(242, 155, 34);
        [JITWhenModsEnabled("FargowiltasSouls")]
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.AddEffect<SandHeraldEffect>(base.Item);
        }

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient<Desert_herald_Helmet>()
            .AddIngredient<Desert_herald_chesplate>()
            .AddIngredient<Desert_herald_Greaves>()
			.AddIngredient(ItemID.ThunderSpear)
            .AddIngredient(ItemID.Cactus,20)
            .AddIngredient<AncientT>()
            .AddTile(TileID.DemonAltar)
			.Register();
		}
        [ExtendsFromMod("FargowiltasSouls")]
        public class SandHeraldEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ForceOfRemantsHeader>();
            public override int ToggleItemType => ModContent.ItemType<SandHeraldEnchant>();

            Projectile CactusBoulderproj = null;
            public static int counter = 0;
            public static int counterMax = (int)Utils1.FormatTimeToTick(0, 0, 0, 1);
            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<RemnantPlayer>().MinionsBuffInflict.Add(BuffID.Electrified);
                player.GetModPlayer<RemnantPlayer>().AllClassBuffInflict.Add(BuffID.Electrified);

                int cactusDamage = getCactusDamage(player)/2;
                CactusBoulderproj = player.GetModPlayer<RemnantPlayer>().SpawnProjectileOnMouse(ProjectileID.RollingCactus, cactusDamage, CactusBoulderproj);


                if(player.FargoSouls().WizardEnchantActive)
                {
                    if (counter++ >= counterMax)
                    {
                        int proj = Projectile.NewProjectile(Projectile.GetSource_None(), CactusBoulderproj.Center, new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), ProjectileID.RollingCactusSpike, cactusDamage / 4, 0, player.whoAmI);
                        Main.projectile[proj].hostile = false;
                        Main.projectile[proj].friendly = true;
                        int proj2 = Projectile.NewProjectile(Projectile.GetSource_None(), CactusBoulderproj.Center, new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), ProjectileID.RollingCactusSpike, cactusDamage / 4, 0, player.whoAmI);
                        Main.projectile[proj2].hostile = false;
                        Main.projectile[proj2].friendly = true;
                        counter = 0;
                    }
                }
            }
            internal int getCactusDamage(Player player)
            {
                int playerDamage = Main.LocalPlayer.HeldItem.damage;
                if (playerDamage <= 0)
                    return 20;
                if (playerDamage >= 100 )
                {
                    if (player.FargoSouls().WizardEnchantActive)
                        return playerDamage;
                    return 100;
                }
                return playerDamage;
            }
        }
    }
}
