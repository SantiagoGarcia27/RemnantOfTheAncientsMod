using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Content.Projectiles.Melee.Spear;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.Spear
{
    public class StoneImpaler : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SkipsInitialUseSound[Type] = true;
            ItemID.Sets.Spears[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Blue; 
            Item.value = Item.sellPrice(silver: 60);

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 32;
            Item.useTime = 32;
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;

            Item.damage = 15;
            Item.knockBack = 6.5f;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            RemnantGlobalItem.isSpear(Item);
            Item.shootSpeed = 3.7f;
            Item.shoot = ModContent.ProjectileType<StoneImpalerP>();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override bool? UseItem(Player player)
        {
            if (!Main.dedServ && Item.UseSound.HasValue)
            {
                SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
            }

            return null;
        }
    }
}
