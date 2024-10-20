using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
    public class DealersPeacemaker : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public int shootMode = 0;
        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 86;
            Item.height = 30;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 1000;
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.JestersArrow;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Bullet;
            Item.GetGlobalItem<CustomTooltip>().HaveSecondMode = true;
        }
        public override bool? UseItem(Player player)
        {
            if(player.altFunctionUse == 2)
            {
                if (shootMode == 1)
                {
                    shootMode = 0;
                   // Main.NewText("semi automatico");
                }
                else
                {
                    shootMode = 1;
                   // Main.NewText("automatico");
                }
                return false;
            }

            return base.UseItem(player);
        }
        public override void UpdateInventory(Player player)
        {  
            if (shootMode == 0)
            {
                
                Item.autoReuse = false;
                Item.useTime = 20;
                Item.useAnimation = 20;
            }
            else
            {
                Item.useTime = 15;
                Item.useAnimation = 15;
                Item.autoReuse = true;
            }
            base.UpdateInventory(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2) return false;
                return true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= .10f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(shootMode == 0? 0:15));
        }
    }
}
