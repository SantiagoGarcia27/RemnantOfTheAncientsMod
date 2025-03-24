using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Projectiles.Multiclass;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Content.Projectiles.Ranger;
using RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Bows
{
    public class EmeraldAndScarlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 12;
            Item.height = 38;
            Item.maxStack = 1;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 8;
            Item.value = Item.sellPrice(0, 10, 0, 0); 
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 10f;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.GetGlobalItem<BowRework>().IsBow = true;
            Item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if(Main.mouseLeft && Main.mouseRight)
            {
                Item.damage = 50;
                Item.useTime = 10;
                Item.useAnimation = 18;
                Item.shoot = ProjectileID.PurificationPowder;
                Item.shootSpeed = 15f;
            }
            else if (player.altFunctionUse == 2)
            {
                Item.damage = 30;
                Item.useTime = 8;
                Item.useAnimation = 18;
                Item.shoot = ProjectileID.PurpleLaser;
                Item.shootSpeed = 20f;
            }
            else
            {
                Item.damage = 70;
                Item.useTime = 18;
                Item.useAnimation = 18;
                Item.shoot = ProjectileID.CursedFlameFriendly;
                Item.shootSpeed = 10f;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<EmeraldAndScarletHeldProjectile>()] <= 0)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<EmeraldAndScarletHeldProjectile>(), 0, 0, player.whoAmI);
            }
            if (type == ProjectileID.PurpleLaser)
            {
                int ColorId = Utils1.GetColorID(Color.Red);
                Projectile.NewProjectile(source, position, velocity * 10, ModContent.ProjectileType<GenericDamageBream>(), damage, knockback, player.whoAmI, 0, ColorId);

                return false;
            }
            else if (type == ProjectileID.PurificationPowder)
            {
                int ColorId = Utils1.GetColorID(Color.GreenYellow);
                Projectile.NewProjectile(source, position, velocity * 10, ModContent.ProjectileType<GenericDamageBream>(), damage, knockback, player.whoAmI,0, ColorId);

                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);

        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            string path = "RemnantOfTheAncientsMod/Content/Items/Weapons/Ranger/Bows/";

            if (!(Main.mouseLeft && Main.mouseRight))
            {
                string currentBow = "Emerald";
                if (Main.LocalPlayer.altFunctionUse == 2)
                {
                    currentBow = "Scarlet";
                }
                Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(path + currentBow);

                spriteBatch.Draw(texture, position, frame, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
                return false;
            }
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
        //public override void HoldItem(Player player)
        //{
        //    string path = "RemnantOfTheAncientsMod/Content/Items/Weapons/Ranger/Bows/";

        //    if (!(Main.mouseLeft && Main.mouseRight))
        //    {
        //        string currentBow = "Emerald";
        //        if (Main.LocalPlayer.altFunctionUse == 2)
        //        {
        //            currentBow = "Scarlet";
        //        }
        //        Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(path + currentBow);

        //        int scale = 1;
        //        Vector2 position = Main.screenPosition - player.position;

               
        //       // Main.spriteBatch.Draw(texture, position, null, Color.White, 0f, Utils.Size(texture) * 0.5f, scale, SpriteEffects.None, 1f);
     
        //    }
        //    base.HoldItem(player);
        //}

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SoulofSight, 5)
            .AddIngredient(ItemID.TitaniumBar, 15)
            .AddIngredient(ItemID.Emerald, 5)
            .AddIngredient(ItemID.Ruby, 5)
            .AddTile(TileID.MythrilAnvil)
            .Register();

            CreateRecipe()
           .AddIngredient(ItemID.SoulofSight, 5)
           .AddIngredient(ItemID.AdamantiteBar, 15)
           .AddIngredient(ItemID.Emerald, 5)
           .AddIngredient(ItemID.Ruby, 5)
           .AddTile(TileID.MythrilAnvil)
           .Register();
        }
    }
}
