using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.Content.Projectiles
{

    public class BoberHook : ModProjectile
	{
		public override void SetStaticDefaults()
		{ 
             ProjectileID.Sets.DrawScreenCheckFluff[Projectile.type] = 3000;
		}
        public override string Texture => SetTexture();

        private const float SwingControlAcceleration = 0.08f;
        private const float MaximumSwingSpeed = 24f;

        private static readonly float MaxLength = DistanceUtils.ToCoordenatePosition(19);
        private static readonly float MinLength = DistanceUtils.ToCoordenatePosition(1);
        private float CurrentLength = MaxLength;
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
		public override void AI()
		{

			Player player = Main.player[Projectile.owner];
            UpdateLeght(player);


            if (currentState == HookState.Attached)
			{

				Projectile.timeLeft = 10;
                Projectile.velocity = Vector2.Zero;
                ApplySwingPhysics(player);
            }
            /*if (currentState == HookState.Attached)
            {

                Projectile.timeLeft = 10;
                Projectile.velocity = Vector2.Zero;
                float distance = player.Distance(Projectile.Center);
                if (distance > CurrentLength)
                {
                    Vector2 limit = Vector2.Normalize(player.Center - Projectile.Center);
                    player.velocity -= limit;

                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.PlayerControls, number: player.whoAmI);
                }
            }*/
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

       
        private void ApplySwingPhysics(Player player)
        {
            Vector2 rope = player.Center - Projectile.Center;
            float distance = rope.Length();

            if (distance < 0.001f)
                return;

            Vector2 ropeDirection = rope / distance;
            Vector2 tangent = new(-ropeDirection.Y, ropeDirection.X);

            // Pequeña holgura para evitar microcorrecciones constantes.
            const float Slack = 2f;
            if(distance > CurrentLength * 1.3f)
            {
                Vector2 limit = Vector2.Normalize(player.Center - Projectile.Center);
                player.velocity -= limit;
                return;
            }

            if (distance > CurrentLength + Slack)
            {
                float radialVelocity = Vector2.Dot(player.velocity, ropeDirection);

                // No cancelar completamente la velocidad radial.
                if (radialVelocity > 0f)
                    player.velocity -= ropeDirection * radialVelocity * 0.92f;

                // Tensión suave.
                float stretch = distance - CurrentLength - Slack;
                player.velocity -= ropeDirection * Math.Min(stretch * 0.15f, 1.2f);
            }

            // Control del jugador.
            if (player.controlLeft != player.controlRight)
            {
                float input = player.controlRight ? 1f : -1f;

                float tangentInput = Vector2.Dot(Vector2.UnitX * input, tangent);

                float tangentialSpeed = Math.Abs(Vector2.Dot(player.velocity, tangent));

                // Cuanto más rápido vas, un poco más eficaz es bombear.
                float pump = SwingControlAcceleration + tangentialSpeed * 0.015f;

                player.velocity += tangent * tangentInput * pump;
            }

            // Límite de velocidad suave.
            float tangentialVelocity = Vector2.Dot(player.velocity, tangent);

            if (Math.Abs(tangentialVelocity) > MaximumSwingSpeed)
            {
                float excess = Math.Abs(tangentialVelocity) - MaximumSwingSpeed;

                player.velocity -= tangent *
                    Math.Sign(tangentialVelocity) *
                    excess *
                    0.20f;
            }

            player.fallStart = (int)(player.position.Y / 16f);

            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.PlayerControls, number: player.whoAmI);
        }

        private void UpdateLeght(Player player)
        {
            // Only the owner (or the server) changes the requested rope length.
            // Other clients receive the value via SendExtraAI.
            if (Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI != Main.myPlayer)
                return;

            const float increment = 3f;
            float previousLength = CurrentLength;
            if (player.controlDown)
                CurrentLength = MathHelper.Min(CurrentLength + increment, MaxLength);
            if (player.controlUp)
                CurrentLength = MathHelper.Max(CurrentLength - increment, MinLength);

            if (CurrentLength != previousLength)
                Projectile.netUpdate = true;
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
            writer.Write(CurrentLength);
            writer.Write(lineColor.R);
            writer.Write(lineColor.G);
            writer.Write(lineColor.B);
            writer.Write(lineColor.A);
            base.SendExtraAI(writer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            _currentState = (HookState)reader.ReadByte();
            CurrentLength = reader.ReadSingle();
            Color color = new(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
            lineColor = color;
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
