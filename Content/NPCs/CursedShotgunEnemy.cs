using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;
using Microsoft.Xna.Framework;
using System;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon;

namespace RemnantOfTheAncientsMod.Content.NPCs
{
    public class CursedShotgunEnemy : ModNPC
	{
		public override void SetStaticDefaults()
		{	
            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Velocity = 1f 
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            NPC.spriteDirection = NPC.direction;
		}

		public override void SetDefaults()
		{
			NPC.width = 22;
			NPC.height = 12;
			NPC.damage = 45;
			NPC.defense = 10;
			NPC.lifeMax = 200;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 25f;
			NPC.knockBackResist = 0.2f;
			NPC.aiStyle = 1;
			NPC.noTileCollide = true;
			AIType = -1;
			NPC.scale = 1.7f;
			AnimationType = -1;
			NPC.noGravity = true;
		}
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange([
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("A fearless slime with a stolen helmet"),
            ]);
        }
        public override void ModifyNPCLoot(NPCLoot NPCLoot)
		{
			NPCLoot.Add(ItemDropRule.Common(ItemID.Shotgun, 10));
			NPCLoot.Add(ItemDropRule.Common(ModContent.ItemType<CursedShotgunStaff>(), 3));
            //NPCLoot.Add(ItemDropRule.Common(ModContent.ItemType<ReinforcedIronOre>()));
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit)
        {
			if(Main.rand.NextBool(2))
				target.AddBuff(BuffID.Cursed, Utils1.FormatTimeToTick(0,0,0,3));
            base.OnHitNPC(target, hit);
        }
        enum Attacks
		{
            Spin,
            Shoot,
			Dash,			
			MoveToOposite
		}

		float shootTimmer = 0;
		float shootTimmerMax = Utils1.FormatTimeToTick(0, 0, 0, 3);
		float spinTimmer = 0;
		float spinTimmerMax = Utils1.FormatTimeToTick(0, 0, 0, 1f);
		float dashTimmer = 0;
		float dashTimmerMax = Utils1.FormatTimeToTick(0, 0, 0, 4);
		

        Attacks currentAttck = 0;
		public override void AI()
		{
			Player target = Main.player[NPC.target];
            int shootDistance = 300;
			float distanceToPlayer = Vector2.Distance(NPC.Center, target.Center);
            bool inRangeToShoot = distanceToPlayer < shootDistance;

			Color color = target.ZoneCrimson? Color.Red : Color.Purple;

            Lighting.AddLight(NPC.Center, color.ToVector3());
			if (currentAttck == Attacks.Shoot || currentAttck == Attacks.Dash)
			{
				NPC.rotation = MathHelper.Lerp(NPC.rotation, NPC.AngleTo(target.Center) + MathF.Tau, 0.2f);
			}
			if (currentAttck == Attacks.Shoot)
			{
				if (shootTimmer >= shootTimmerMax)
				{
					int ProjectileAmmount = 5;
					shootTimmer = 0;

					for (int i = 0; i < ProjectileAmmount; i++)
					{
						Vector2 Velocity = Vector2.Normalize(target.Center - NPC.Center) * 4.5f;
						Vector2 newVelocity = Velocity.RotatedByRandom(MathHelper.ToRadians(15));

						// Decrease velocity randomly for nicer visuals.
						newVelocity *= 1f - Main.rand.NextFloat(0.3f);

						int projType = ProjectileID.Bullet;

						if(Main.rand.NextBool(4))
						{
							if (target.ZoneCorrupt)
								projType = ProjectileID.CursedBullet;
							else if (target.ZoneCrimson)
                                projType = ProjectileID.IchorBullet;
						}


						var p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, newVelocity, projType, 20, 10, Main.myPlayer);
						Main.projectile[p].hostile = true;
						Main.projectile[p].friendly = false;
						Main.projectile[p].tileCollide = false;
					}
					currentAttck = Attacks.Spin;
				}
				else
				{
					shootTimmer++;
				}
			}
			else if (currentAttck == Attacks.Dash)
			{
				if (!inRangeToShoot)
				{
					if (dashTimmer <= dashTimmerMax)
					{
						NPC.velocity = Vector2.Normalize(target.Center - NPC.Center) * 8.5f;
						dashTimmer++;
					}
					else
					{
						dashTimmer = 0;
						currentAttck = Attacks.Spin;
					}
				}
                else
				{
                    dashTimmer = 0;
                    currentAttck = Attacks.Shoot;
					shootTimmer = Utils1.FormatTimeToTick(0, 0, 0, 2);
                }
				
            }
			else if(currentAttck == Attacks.Spin)
			{
				NPC.velocity = Vector2.Zero;
				if (spinTimmer <= spinTimmerMax)
				{
					NPC.rotation += 0.3f;
					spinTimmer++;
				}
				else
				{
					spinTimmer = 0;
					if (!inRangeToShoot)
						currentAttck = Attacks.Dash;
					else
						currentAttck = Attacks.MoveToOposite;
				}
            }
			else if(currentAttck == Attacks.MoveToOposite)
			{
				if(!inRangeToShoot)
                    currentAttck = Attacks.Dash;
				else
				{

					//Vector2 newPos = Main.rand.NextBool() ? NPC.Center : target.Center;
					Vector2 newPos = NPC.Center;

                    float moveStrenght = 50;
					if(target.position.X < NPC.position.X)
					{
						newPos.X -= moveStrenght * 16;
                    }
					else
					{
                        newPos.X += moveStrenght * 16;
                    }
                    NPC.rotation = MathHelper.Lerp(NPC.rotation, NPC.AngleTo(newPos) + MathF.Tau, 0.2f);
                    NPC.velocity = newPos - NPC.Center;
					NPC.velocity.Normalize();
					NPC.velocity *= 10;
                    currentAttck = Attacks.Shoot;
                }
            }

            base.AI();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Player targer = Main.LocalPlayer;

            if ((targer.ZoneCorrupt || targer.ZoneCrimson) && Main.hardMode && spawnInfo.SpawnTileY > Main.worldSurface)
				return 1 / 80f;
			return 0;
        }
	}
}
