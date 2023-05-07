using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.Debugg
{
    public class NpcKiller : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("OpswordsIIDebugMod", out mod);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("NpcKiller");
        }
        public override void SetDefaults()
        {
            Item.damage = 999999999;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 2; 
            Item.height = 2; 
            Item.useTime = 25; 
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot; 
            Item.noMelee = true;
            Item.scale = 0.68f;
            Item.knockBack = 5;
            Item.value = 20000; 
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item38;
            Item.autoReuse = false;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 30f;
           // Item.useAmmo = AmmoID.Bullet;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
}
