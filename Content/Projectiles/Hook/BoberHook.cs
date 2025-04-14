using InfernumMode.Core.Netcode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 origin;
            SpriteEffects effects;
            Player player = Main.player[Projectile.owner];
            Projectile baseProj = ContentSamples.ProjectilesByType[player.HeldItem.shoot];

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

            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, default, lightColor * Projectile.Opacity, Projectile.rotation, origin, Projectile.scale, effects, 0);

            Color lineColor = Color.White;
            if (baseProj != null)
            {
                ModItem modItem = player.HeldItem.ModItem;
                if (modItem != null) 
                {
                    Vector2 ofset = Vector2.Zero;
                    modItem.ModifyFishingLine(baseProj, ref ofset, ref lineColor);
                }
                else
                {
                    if(player.HeldItem.type == ItemID.GoldenFishingRod)
                    {
                        lineColor = Color.Cyan;
                    }
                    else if(player.HeldItem.type == ItemID.HotlineFishingHook)
                    {
                        lineColor = Color.Gold;
                    }
                    else if (player.HeldItem.type == ItemID.ScarabFishingRod)
                    {
                        lineColor = Color.DarkSlateBlue;
                    }
                }
            }

            Utils.DrawLine(Main.spriteBatch, Projectile.Center, Main.player[Projectile.owner].Center, lineColor, lineColor, 2f);
            // Since we are doing a custom draw, prevent it from normally drawing
            return false;
        }
        //void SpawnLine()
        //{
        //    Player player = Main.player[Projectile.owner];
        //    float Distance = player.Distance(Projectile.Center);
        //    int nodeNumber = (int)(Distance);

        //    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BoberHookLine>(), 0, 0, player.whoAmI,0, nodeNumber);

        //}
        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }
    }

    //public class BoberHookLine : ModProjectile
    //{
    //    public override string Texture => RemnantOfTheAncientsMod.PlaceHolderPath;

    //    Projectile lastProjectile = null;
    //    Projectile nextProjectile = null;
    //    public override void SetDefaults()
    //    {
    //        Projectile.width = 5;
    //        Projectile.height = 5;
    //        Projectile.friendly = true;
    //        Projectile.tileCollide = true;
    //        Projectile.penetrate = -1;
    //        Projectile.timeLeft = 2000;
    //        Projectile.ignoreWater = true;
    //        Projectile.netImportant = true;
    //        AIType = -1;
    //    }

    //    public override void AI()
    //    {
    //        Player player = Main.player[Projectile.owner];
    //        Projectile end = Main.projectile.FirstOrDefault(p => p.type == ModContent.ProjectileType<BoberHook>());
    //        if (RemnantPlayer.Nodes.Count > 0)
    //        {
    //            int index = RemnantPlayer.Nodes.IndexOf(Projectile);
    //            if (nextProjectile != null && index > -1 && end != null)
    //            {
    //                Vector2 vector = player.Center - end.Center;
    //                Vector2 spacing = new Vector2(vector.X / (RemnantPlayer.Nodes.Count - 1), vector.Y / (RemnantPlayer.Nodes.Count - 1));
    //                Vector2 pos = end.Center + (spacing * (index));

    //                Projectile.velocity = pos - Projectile.Center;


    //            }
    //        }
    //        if (player.ownedProjectileCounts[ModContent.ProjectileType<BoberHook>()] < 1) Projectile.Kill();
    //        base.AI();
    //    }
    //    public override bool OnTileCollide(Vector2 oldVelocity)
    //    {
    //        return false;
    //    }
    //    public override bool PreDrawExtras()
    //    {
    //        if (nextProjectile != null)
    //        {                
    //            Color color = Color.White;
    //            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(RemnantOfTheAncientsMod.MagicPixelPath); 
    //            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition,null,color,0f,texture.Size() / 2f, Projectile.scale,SpriteEffects.None,1f);
    //          // Utils1.DrawLine(Main.spriteBatch, start, end,color: color,splits: splits,scale: scale);
    //        }
    //        return base.PreDrawExtras();
    //    }
    //    public override void OnSpawn(IEntitySource source)
    //    {
    //        float counter = Projectile.ai[0];
    //        float counterMax = Projectile.ai[1];

    //        Utils1.AddSecure(RemnantPlayer.Nodes, Main.projectile[Projectile.whoAmI]);
    //        int index = RemnantPlayer.Nodes.IndexOf(Projectile);
    //        Projectile proj = null;
    //        if (counter < counterMax - 1)
    //        {       
    //            SpawnNode(ref proj);
    //        }
    //        if (index == 0)
    //        {
    //            lastProjectile = null;
    //            if(proj != null)
    //            nextProjectile = proj;
    //        }
    //        else if(index <= RemnantPlayer.Nodes.Count - 2)
    //        {
    //            lastProjectile = RemnantPlayer.Nodes[index - 1];
    //            if (proj != null)
    //                nextProjectile = proj;
    //        }
    //        else if(index == RemnantPlayer.Nodes.Count - 1)
    //        {
    //            Projectile next = Main.projectile.FirstOrDefault(p => p.type == ModContent.ProjectileType<BoberHook>());

    //            lastProjectile = RemnantPlayer.Nodes[index - 1];
    //            nextProjectile = next != null? next : null;
    //        }
    //        void SpawnNode(ref Projectile Proj)
    //        {
    //            var p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BoberHookLine>(), 0, 0, Projectile.owner, ai0: counter + 1,ai1: counterMax);
    //            Proj = Main.projectile[p];
    //        }
    //        base.OnSpawn(source);
    //    }  
    //}
}
