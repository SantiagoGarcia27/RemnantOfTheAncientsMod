using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Minions.acorns;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Summon.Minioms;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms.Saplings
{
    public class SaplingMinion : AccornAi
    {
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Animated Sapling Minion");
			Main.projFrames[Projectile.type] = 6;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}
		public float MovmentSpeed = 9f;
		public float MaxRadious = 100f;
		public float ShootSpeed = 4f;
		public float ShootCouldown;
		public float ChanceWave;
		public int ShootType = ProjectileType<AcornLeaftShoot>();
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 24;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10000;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.scale = 2f;
			Projectile.DamageType = DamageClass.Summon;
			inertia = 100f;//20
			Projectile.scale = 1f;
			shoot = ProjectileType<AcornLeaftShoot>();
			shootSpeed = 9f;
			shootCool = 100f;
			viewDist = 100f;
			if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				shootCool = 70f;
				Projectile.tileCollide = false;
			}
		}
        public int shootCount = 0;
       
        //public override void AI()
        //{
        //	Player player = Main.player[Projectile.owner];

        //          NPC closestNPC = FindClosestNPC(MaxRadious);
        //          if (closestNPC != null) Attack(player);


        //          Movment(player);
        //	CheckActive();
        //}
        //      bool target = false;
        //      public void Attack(Player player)
        //{
        //          Vector2 targetPos = Projectile.position;
        //          float targetDist = MaxRadious;


        //          Projectile.tileCollide = true;
        //          if (player.HasMinionAttackTargetNPC)
        //          {
        //              NPC npc = Main.npc[player.MinionAttackTargetNPC];
        //              if (Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
        //              {
        //                  targetDist = Vector2.Distance(Projectile.Center, targetPos);
        //                  targetPos = npc.Center;
        //                  target = true;
        //              }
        //          }
        //          else
        //          {
        //              for (int k = 0; k < 200; k++)
        //              {
        //                  NPC npc = Main.npc[k];
        //                  if (npc.CanBeChasedBy(this, false))
        //                  {
        //                      float distance = Vector2.Distance(npc.Center, Projectile.Center);
        //                      if ((distance < targetDist || !target) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
        //                      {
        //                          targetDist = distance;
        //                          targetPos = npc.Center;
        //                          target = true;
        //                      }
        //                  }
        //              }
        //          }


        //          Vector2 shootVel = targetPos - Projectile.Center;
        //          if (shootVel == Vector2.Zero) shootVel = new Vector2(0f, 1f);
        //          shootVel.Normalize();
        //          shootVel *= ShootSpeed;
        //          int p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, shootVel.X, shootVel.Y, ShootType, Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
        //          Main.projectile[p].timeLeft = 300;
        //          Main.projectile[p].netUpdate = true;
        //          Projectile.netUpdate = true;
        //      }

        //public void Movment(Player player)
        //{
        //	var distance = Projectile.Distance(player.Center);
        //	//MovmentSpeed = Main.player[Projectile.owner].moveSpeed;

        //          if ((int)distance >= 40 && (int)distance >= 49)
        //          {
        //			Projectile.velocity = (player.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * MovmentSpeed;
        //			//Projectile.rotation = Projectile.velocity.ToRotation();
        //          }
        //	else Projectile.velocity = Main.player[Projectile.owner].velocity;
        //}
        public override void CheckActive()
		{
			Player player = Main.player[Projectile.owner];
			RemnantPlayer modPlayer = player.GetModPlayer<RemnantPlayer>();
			if (player.dead || !player.active) player.ClearBuff(BuffType<SaplingMinionBuff>());
			if (player.HasBuff(BuffType<SaplingMinionBuff>())) Projectile.timeLeft = 2;
		}
        //public NPC FindClosestNPC(float maxDetectDistance)
        //{
        //    NPC closestNPC = null;
        //    float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
        //    for (int k = 0; k < Main.maxNPCs; k++)
        //    {
        //        NPC target = Main.npc[k];
        //        if (target.CanBeChasedBy())
        //        {
        //            float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);
        //            if (sqrDistanceToTarget < sqrMaxDetectDistance)
        //            {
        //                sqrMaxDetectDistance = sqrDistanceToTarget;
        //                closestNPC = target;
        //            }
        //        }
        //    }
        //    return closestNPC;
        //}

    }
}
