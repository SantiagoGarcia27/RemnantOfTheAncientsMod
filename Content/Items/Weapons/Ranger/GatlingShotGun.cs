using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.Global.Items;
using CalamityMod;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger
{
    public class GatlingShotGun : ModItem
    {
        public override void SetStaticDefaults()
        {     
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public int NotConsumeAmmoChance = 80;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(NotConsumeAmmoChance);
        public override void SetDefaults()
        {
            Item.DefaultToRangedWeapon(ProjectileID.PurificationPowder, AmmoID.Bullet, 6, 10f, true);
            Item.SetWeaponValues(13, 1f);
            Item.SetShopValues((Terraria.Enums.ItemRarityColor)ItemRarityID.LightPurple, Item.sellPrice(0, 6, 2, 0));
            Item.Size = new Vector2(80, 40);      
            Item.DamageType = DamageClass.Ranged;        
            Item.UseSound = SoundID.Item10;
            Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Legendary;
            Item.GetGlobalItem<CustomTooltip>().LegendaryDrop = true;
            Item.scale = 0.6f;
            new Shotgun(Item, true, 4, -1, 40,false);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, 0);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= NotConsumeAmmoChance/100f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
        }
        public override bool CanUseItem(Player player)
        {
            if(NPC.downedMoonlord)
            {
                Item.damage = 50;
            }
            else if(NPC.downedPlantBoss)
            {
                Item.damage = 30;
            }
            else if(RemnantDownedBossSystem.downedFrozen)
            {
                Item.damage = 20;
            }
            return base.CanUseItem(player);
        }
    }
}
