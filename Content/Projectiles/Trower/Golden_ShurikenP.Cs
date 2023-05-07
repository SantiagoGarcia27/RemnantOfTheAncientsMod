using RemnantOfTheAncientsMod.Content.Items.Weapons.Throwing;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Trower
{

    public class Golden_ShurikenP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Golden_ShurikenP"); //projectile name
        }
        public override void SetDefaults()
        {

            Projectile.CloneDefaults(ProjectileID.Shuriken);
            AIType = ProjectileID.Shuriken;
        }
        public override void Kill(int timeLeft)
        {


            if (Projectile.owner == Main.myPlayer)
            {
                // Drop a javelin item, 1 in 18 chance (~5.5% chance)
                int item = Main.rand.NextBool(3)? Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.getRect(), ItemType<Golden_Shuriken>()): 0;
                
                if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
                }
            }
        }
        /*  public override bool PreKill(int timeLeft) {
Projectile.type = ProjectileID.Shuriken;
return true;

}*/
    }
}