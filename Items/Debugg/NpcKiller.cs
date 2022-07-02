using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;
using opswordsII.Projectiles;
using opswordsII.Items.ammo;

namespace opswordsII.Items.Debugg
{
    public class NpcKiller : ModItem
    {
        /*public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("OpswordsIIDebugMod") != null;
        }*/
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("NpcKiller");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            Item.damage = 999999999;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 2; 
            Item.height = 2; 
            Item.useTime = 25; 
            Item.useAnimation = 25;
            Item.useStyle = 5; 
            Item.noMelee = true;
            Item.scale = 0.68f;
            Item.knockBack = 5;
            Item.value = 20000; 
            Item.rare = 1;
            Item.UseSound = SoundID.Item38;
            Item.autoReuse = false;
            Item.shoot = 10;
            Item.shootSpeed = 30f;
           // Item.useAmmo = AmmoID.Bullet;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
}
