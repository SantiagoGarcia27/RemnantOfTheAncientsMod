using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Projectiles;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
    public class TheSpiker : ModItem
    {
        public override void SetStaticDefaults()
        {
            // //DisplayName.SetDefault("The Spiker");
            // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Le Spiker");
            // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La punzante");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public static int counter = 0;
        public static int counter2 = 0;
        public bool spike;
        public bool strong;

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
            Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Legendary;
            Item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
            Item.GetGlobalItem<CustomTooltip>().LegendaryDrop = true;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                spike = true;
                Item.useStyle = ItemUseStyleID.Thrust;
                if (counter < 3)
                {
                    Item.shoot = ModContent.ProjectileType<InfernalSpike_f>();
                    counter++;
                    strong = false;
                }
                else
                {
                    Item.shoot = ModContent.ProjectileType<InfernalSpikeF_f>();
                    counter = 0;
                    strong = true;
                }
            }
            else
            {
                spike = false;
                Item.useStyle = ItemUseStyleID.Swing;
                if (counter2 < 3)
                {
                    Item.shoot = ModContent.ProjectileType<InfernalBall_f>();
                    counter2++;
                    strong = false;
                }
                else
                {
                    Item.shoot = ModContent.ProjectileType<InfernalBallF_f>();
                    counter2 = 0;
                    strong = true;
                }
            }
            return base.CanUseItem(player);
        }
        public void ModifyWeapon(bool strong, int proj, float speed)
        {
            if (strong)
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
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int FinalDamage = strong ? damage * 2 : damage;
            if (spike)
            {
                Projectile.NewProjectile(source, position, velocity * 0, Item.shoot, FinalDamage, 1, player.whoAmI);
            }
            else
            {
                Item.shootSpeed = 8f;
                Projectile.NewProjectile(source, position, velocity, Item.shoot, FinalDamage, 1, player.whoAmI);

            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.defDefense = target.defDefense / 2;
            target.defense = target.defDefense;
        }
    }
}
