using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using System.IO;
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

        static float MaxLeght = DistanceUtils.ToCoordenatePosition(19);
        static float MinLeght = DistanceUtils.ToCoordenatePosition(1);
        float CurrentLength = MaxLeght;
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
            currentState = HookState.Attached;
            return false;
		}
		enum HookState {Throw, Pull, Attached };

		
        private HookState _currentState = HookState.Throw;
        private HookState currentState
        {
            get => _currentState;
            set {
                if (_currentState == value)
                    return;

                _currentState = value;
                if (Main.netMode != NetmodeID.MultiplayerClient) Projectile.netUpdate = true;
            }
        }
        float stabilization = 0f; 
		public override void AI()
		{

			Player player = Main.player[Projectile.owner];
            UpdateLeght(player);

            if (currentState == HookState.Attached)
			{

				Projectile.timeLeft = 10;
                Projectile.velocity = Vector2.Zero;
                float distance = player.Distance(Projectile.Center);
                Vector2 puntoReposo = new(Projectile.Center.X, Projectile.Center.Y + CurrentLength);
                if (distance > CurrentLength)
                {
                    Vector2 limit = Vector2.Normalize(player.Center - Projectile.Center);
                    player.velocity -= limit;


                    
                    /*// Recolocar exactamente sobre el círculo
                    Vector2 dir = Vector2.Normalize(player.Center - Projectile.Center);

                    float radial = Vector2.Dot(player.velocity, dir);
                    Vector2 tangential = player.velocity - dir * radial;

                    if (distance > CurrentLength)
                    {

                        float error = distance - CurrentLength;

                        if (error > 0)
                        {
                            player.Center -= dir * error;
                        }

                        if (radial > 0)
                            radial = 0;

                        player.velocity = (tangential + dir * radial) * 1.03f;
                    }*/

                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.PlayerControls, number: player.whoAmI);
                }
            }
			else if (currentState == HookState.Pull)
			{
                Projectile.velocity = (player.Center - Projectile.Center);
                Projectile.velocity.Normalize();
                Projectile.velocity *= 20;
				if (player.Distance(Projectile.Center) < 10)
				{
					Projectile.Kill();
				}
			}
			if (player.ownedProjectileCounts[Type] >= 1 && Main.mouseRight && currentState == HookState.Attached || (currentState != HookState.Attached && Projectile.timeLeft <= 40))
			{
				currentState = HookState.Pull;

            }


			base.AI();
		}

        float increment = 3f;
        private void UpdateLeght(Player player) {
            if (player.controlDown && CurrentLength + increment < MaxLeght) 
                CurrentLength += increment;
            if (player.controlUp && CurrentLength - increment > MinLeght) 
                CurrentLength -= increment;
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
                effects = SpriteEffects.None;
            }
            else
            {
                origin = new Vector2(Projectile.width, Projectile.height);
                effects = SpriteEffects.FlipHorizontally;
            }
            if(texture == null)
             texture = ModContent.Request<Texture2D>(Texture).Value;

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, default, lightColor * Projectile.Opacity, Projectile.rotation, origin, Projectile.scale, effects, 0);
   
            Utils.DrawLine(Main.spriteBatch, Projectile.Center, Main.player[Projectile.owner].Center, lineColor, lineColor, 2f);
            return false;
        }    
        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((byte)currentState);
            base.SendExtraAI(writer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            _currentState = (HookState)reader.ReadByte();

            base.ReceiveExtraAI(reader);
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
