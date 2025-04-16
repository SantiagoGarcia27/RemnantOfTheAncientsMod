using InfernumMode.Core.Netcode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles
{

    public class BoberHook : ModProjectile
	{
		public override void SetStaticDefaults()
		{ 
             ProjectileID.Sets.DrawScreenCheckFluff[Projectile.type] = 3000;
		}
        public override string Texture => SetTexture();
        string SetTexture()
		{
            Player player = Main.player[Projectile.owner];
			Projectile baseProj = ContentSamples.ProjectilesByType[player.HeldItem.shoot];
            if (baseProj == null) return ProjectileLoader.GetProjectile(ProjectileID.BobberWooden).Texture;
            string Texture = baseProj.type < ProjectileID.Count ? "Terraria/Images/Projectile_" + baseProj.type : ProjectileLoader.GetProjectile(baseProj.type).Texture;
            return Texture;
        }

		public override void SetDefaults()
		{  
            Projectile.width = 5;      
			Projectile.height = 5;  
			Projectile.friendly = true;     
			Projectile.tileCollide = true; 
			Projectile.penetrate = -1;     
			Projectile.timeLeft = 80;  
			Projectile.ignoreWater = true;
            Projectile.netImportant = true;
            AIType = -1;
		}
        
	
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
            state = HookState.Attached;
           // SpawnLine();
            return false;
		}
		enum HookState {Throw, Pull, Attached };

		HookState state = HookState.Throw;
		public override void AI()
		{
           //Vector2 lineOriginOffset = new(Projectile.localAI[0],Projectile.localAI[1]);
           // Color lineColor = al;
            
            int HookLeght = 300;
			Player player = Main.player[Projectile.owner];
			if (state == HookState.Attached)
			{
				Projectile.timeLeft = 10;
                Projectile.velocity = Vector2.Zero;

				if(player.Distance(Projectile.Center) > HookLeght)
                {
					Vector2 limit = (player.Center - Projectile.Center);
					limit.Normalize();
                    player.velocity -= limit;
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.PlayerControls, number: player.whoAmI);
                }
            }
			else if (state == HookState.Pull)
			{
                Projectile.velocity = (player.Center - Projectile.Center);
                Projectile.velocity.Normalize();
                Projectile.velocity *= 20;
				if (player.Distance(Projectile.Center) < 10)
				{
					Projectile.Kill();
				}
			}
			if (player.ownedProjectileCounts[Type] >= 1 && Main.mouseRight && state == HookState.Attached || (state != HookState.Attached && Projectile.timeLeft <= 40))
			{
				state = HookState.Pull;

            }


			base.AI();
		}
        Color lineColor = Color.White;
        Texture2D texture = null;
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 origin;
            SpriteEffects effects;
            Player player = Main.player[Projectile.owner];
          

            if (Projectile.velocity.X != 0 || Projectile.velocity.Y != 0)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(270f);
            }
            if (Projectile.spriteDirection > 0)
            {
                origin = new Vector2(0, Projectile.height);
               // rotationOffset = MathHelper.ToRadians(180f);
                effects = SpriteEffects.None;
            }
            else
            {
                origin = new Vector2(Projectile.width, Projectile.height);
               // rotationOffset = MathHelper.ToRadians(135f);
                effects = SpriteEffects.FlipHorizontally;
            }
            if(texture == null)
             texture = ModContent.Request<Texture2D>(Texture).Value;

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, default, lightColor * Projectile.Opacity, Projectile.rotation, origin, Projectile.scale, effects, 0);
   
          

            Utils.DrawLine(Main.spriteBatch, Projectile.Center, Main.player[Projectile.owner].Center, lineColor, lineColor, 2f);
            // Since we are doing a custom draw, prevent it from normally drawing
            return false;
        }    
        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }
        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];
            Projectile baseProj = ContentSamples.ProjectilesByType[player.HeldItem.shoot];
            if (baseProj != null)
            {
                if (baseProj.bobber)
                {


                    ModItem modItem = player.HeldItem.ModItem;
                    if (modItem != null)
                    {
                        Vector2 ofset = Vector2.Zero;
                        modItem.ModifyFishingLine(baseProj, ref ofset, ref lineColor);
                        texture = ModContent.Request<Texture2D>(ProjectileLoader.GetProjectile(baseProj.type).Texture).Value;
                    }
                    else
                    {
                        if (player.HeldItem.type == ItemID.GoldenFishingRod)
                        {
                            lineColor = Color.Cyan;
                        }
                        else if (player.HeldItem.type == ItemID.HotlineFishingHook)
                        {
                            lineColor = Color.Gold;
                        }
                        else if (player.HeldItem.type == ItemID.ScarabFishingRod)
                        {
                            lineColor = Color.DarkSlateBlue;
                        }
                        texture = ModContent.Request<Texture2D>("Terraria/Images/Projectile_" + baseProj.type).Value;
                    }
                }
            }
            base.OnSpawn(source);
        }
    }
}
