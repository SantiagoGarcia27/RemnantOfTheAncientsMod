using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Items.tresure_bag;
using RemnantOfTheAncientsMod.Items.Mele.saber;
using RemnantOfTheAncientsMod.Items.Magic;
using RemnantOfTheAncientsMod.Items.Ranger.Rep;
using RemnantOfTheAncientsMod.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Items.Bloques.Relics;
using RemnantOfTheAncientsMod.Items.Bloques;
using RemnantOfTheAncientsMod.Items.Summon;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.VanillaChanges;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.GameContent.ItemDropRules;

using RemnantOfTheAncientsMod.World;
using RemnantOfTheAncientsMod.Common.Systems;
using Terraria.Audio;

namespace RemnantOfTheAncientsMod.NPCs.ITyrant
{
    [AutoloadBossHead]
	internal class InfernalTyrantHead : Worm1
	{
		//public override string Texture => "Terraria/NPC_" + NPCID.DiggerHead;

		public override void SetDefaults() {
			NPC.CloneDefaults(NPCID.DiggerHead);
			NPC.aiStyle = -1;
			NPC.width = 50;//105
			NPC.height = 40;//103
			NPC.boss = true;
			NPC.lifeMax = (int)NpcChanges1.ExpertLifeScale(30000, "MyBoss"); 
			NPC.damage = (int)NpcChanges1.ExpertDamageScale(300, "MyBoss");
			NPC.defense = TyrantArmor(999);//
			NPC.scale = 2.50f;
			NPC.npcSlots = 20f;
            NPC.lavaImmune = true;
			Music = MusicLoader.GetMusicSlot(Mod,"Sounds/Music/Infernal_Tyrant");
		}

		public override void Init() {
			base.Init();
			head = true;
		}

		private int attackCounter;
		public override void SendExtraAI(BinaryWriter writer) {
			writer.Write(attackCounter);
		}

		public override void ReceiveExtraAI(BinaryReader reader) {
			attackCounter = reader.ReadInt32();
		}

		public override void CustomBehavior() {
			if (Main.netMode != NetmodeID.MultiplayerClient) {
				if (attackCounter > 0) {
					attackCounter--;
				}

				Player target = Main.player[NPC.target];                            
				if (attackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) < 200 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1)) {
					Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
					direction = direction.RotatedByRandom(MathHelper.ToRadians(10));
				}
			}
		}
	}

	internal class InfernalTyrantBody : Worm1
	{
		//public override string Texture => "Terraria/NPC_" + NPCID.DiggerBody;

		public override void SetDefaults() {
			NPC.CloneDefaults(NPCID.DiggerBody);
			NPC.lifeMax = 3000;
			NPC.defense = TyrantArmor(999);//999
			NPC.aiStyle = -1;
			NPC.width = 30;//105
			NPC.height = 33;//103
			NPC.scale = 2.50f;
			NPC.boss = true;
		}
	}

	internal class InfernalTyrantTail : Worm1
	{
		//public override string Texture => "Terraria/NPC_" + NPCID.DiggerTail;

		public override void SetDefaults() {
			NPC.CloneDefaults(NPCID.DiggerTail);
			NPC.lifeMax = 3000;
			NPC.width = 40;//105
			NPC.height = 43;//103
			NPC.defense = 25;
			NPC.aiStyle = -1;
			NPC.scale = 2.50f;
			NPC.boss = true;
			//NPC.color = Color.Aqua;
		}

		public override void Init() {
			base.Init();
			tail = true;
		}
	}

	// I made this 2nd base class to limit code repetition.
	public abstract class Worm1 : Worm
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Infernal Tyrant");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tirano infernal");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tyran infernal");
		}

		public override void Init() {
			minLength = 15;//6
			maxLength = 15;//
			tailType = NPCType<InfernalTyrantTail>();
			bodyType = NPCType<InfernalTyrantBody>();
			headType = NPCType<InfernalTyrantHead>();
			speed = 20;//9.5f
			turnSpeed = 0.245f;//0.045
		}
	}

	public abstract class Worm : ModNPC
	{
		/* ai[0] = follower
		 * ai[1] = following
		 * ai[2] = distanceFromTail
		 * ai[3] = head
		 */
		public bool head;
		public bool tail;
		public int minLength;
		public int maxLength;
		public int headType;
		public int bodyType;
		public int tailType;
		public bool flies = false;
		public bool directional = false;
		public float speed;
		public float turnSpeed;
        public bool CanTp = false;

        [Obsolete]
        public override void AI() {
            float distance = NPC.Distance(Main.player[NPC.target].Center);

            if (NPC.localAI[1] == 0f) {
				NPC.localAI[1] = 1f;
				Init();
			}
			if (NPC.ai[3] > 0f) {
				NPC.realLife = (int)NPC.ai[3];
			}
			if (!head && NPC.timeLeft < 300) {
				NPC.timeLeft = 300;//300
			}
			
			if (Main.player[NPC.target].dead && NPC.timeLeft > 300) {
				NPC.timeLeft = 120;
			}
			if (Main.netMode != NetmodeID.MultiplayerClient) {
				if (!tail && NPC.ai[0] == 0f) {
					if (head) {
						NPC.ai[3] = NPC.whoAmI;
						NPC.realLife = NPC.whoAmI;
						NPC.ai[2] = Main.rand.Next(minLength, maxLength + 1);
						NPC.ai[0] = NPC.NewNPC(NPC.GetSource_FromAI(),(int)(NPC.position.X + NPC.width / 2), (int)(NPC.position.Y + NPC.height), bodyType, NPC.whoAmI);
					}
					else if (NPC.ai[2] > 0f) {
						NPC.ai[0] = NPC.NewNPC(NPC.GetSource_FromAI(),(int)(NPC.position.X + NPC.width / 2), (int)(NPC.position.Y + NPC.height), NPC.type, NPC.whoAmI);
					}
					else {
						NPC.ai[0] = NPC.NewNPC(NPC.GetSource_FromAI(),(int)(NPC.position.X + NPC.width / 2), (int)(NPC.position.Y + NPC.height), tailType, NPC.whoAmI);
					}
					Main.npc[(int)NPC.ai[0]].ai[3] = NPC.ai[3];
					Main.npc[(int)NPC.ai[0]].realLife = NPC.realLife;
					Main.npc[(int)NPC.ai[0]].ai[1] = NPC.whoAmI;
					Main.npc[(int)NPC.ai[0]].ai[2] = NPC.ai[2] - 1f;
					NPC.netUpdate = true;
				}
				if (!head && (!Main.npc[(int)NPC.ai[1]].active || Main.npc[(int)NPC.ai[1]].type != headType && Main.npc[(int)NPC.ai[1]].type != bodyType)) {
					//NPC.life = 0;
					NPC.HitEffect(0, 10.0);
					NPC.active = false;
				}//
				if (!tail && (!Main.npc[(int)NPC.ai[0]].active || Main.npc[(int)NPC.ai[0]].type != bodyType && Main.npc[(int)NPC.ai[0]].type != tailType)) {
					//NPC.life = 0;
					NPC.HitEffect(0, 10.0);
					NPC.active = true;
				}
                
				if (!NPC.active && Main.netMode == NetmodeID.Server) {
					NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, NPC.whoAmI, -1f, 0f, 0f, 0, 0, 0);
				}
			}
			int num180 = (int)(NPC.position.X / 16f) - 1;
			int num181 = (int)((NPC.position.X + NPC.width) / 16f) + 2;
			int num182 = (int)(NPC.position.Y / 16f) - 1;
			int num183 = (int)((NPC.position.Y + NPC.height) / 16f) + 2;
			if (num180 < 0) {
				num180 = 0;
			}
			if (num181 > Main.maxTilesX) {
				num181 = Main.maxTilesX;
			}
			if (num182 < 0) {
				num182 = 0;
			}
			if (num183 > Main.maxTilesY) {
				num183 = Main.maxTilesY;
			}
			bool flag18 = flies;
			if (!flag18) {
				for (int num184 = num180; num184 < num181; num184++) {
					for (int num185 = num182; num185 < num183; num185++) {
						if (Main.tile[num184, num185] != null && (Main.tile[num184, num185].HasUnactuatedTile && (Main.tileSolid[(int)Main.tile[num184, num185].TileType] || Main.tileSolidTop[(int)Main.tile[num184, num185].TileType] && Main.tile[num184, num185].TileType == 0) || Main.tile[num184, num185].LiquidAmount > 64)) {
							Vector2 vector17;
							vector17.X = num184 * 16;
							vector17.Y = num185 * 16;
							if (NPC.position.X + NPC.width > vector17.X && NPC.position.X < vector17.X + 16f && NPC.position.Y + NPC.height > vector17.Y && NPC.position.Y < vector17.Y + 16f) {
								flag18 = true;
								if (Main.rand.NextBool(100) && NPC.behindTiles && Main.tile[num184, num185].HasUnactuatedTile) {
									WorldGen.KillTile(num184, num185, true, true, false);
								}
								if (Main.netMode != NetmodeID.MultiplayerClient && Main.tile[num184, num185].TileType == 2) {
									ushort arg_BFCA_0 = Main.tile[num184, num185 - 1].TileType;
								}
							}
						}
					}
				}
			}
			if (!flag18 && head) {
				Rectangle rectangle = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
				int num186 = 1000;
				bool flag19 = true;
				for (int num187 = 0; num187 < 255; num187++) {
					if (Main.player[num187].active) {
						Rectangle rectangle2 = new Rectangle((int)Main.player[num187].position.X - num186, (int)Main.player[num187].position.Y - num186, num186 * 2, num186 * 2);
						if (rectangle.Intersects(rectangle2)) {
							flag19 = false;
							break;
						}
					}
				}
				if (flag19) {
					flag18 = true;
				}
			}
            if (directional)
            {
				if (NPC.velocity.X < 0f) {
					NPC.spriteDirection = 1;
				}
				else if (NPC.velocity.X > 0f) {
					NPC.spriteDirection = -1;
				}
			}
			float num188 = speed;
			float num189 = turnSpeed;
			Vector2 vector18 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
			float num191 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2;
			float num192 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2;
			num191 = (int)(num191 / 16f) * 16;
			num192 = (int)(num192 / 16f) * 16;
			vector18.X = (int)(vector18.X / 16f) * 16;
			vector18.Y = (int)(vector18.Y / 16f) * 16;
			num191 -= vector18.X;
			num192 -= vector18.Y;
			float num193 = (float)System.Math.Sqrt(num191 * num191 + num192 * num192);
			if (NPC.ai[1] > 0f && NPC.ai[1] < Main.npc.Length) {
				try {
					vector18 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
					num191 = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - vector18.X;
					num192 = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - vector18.Y;
				}
				catch {
				}
				NPC.rotation = (float)Math.Atan2(num192, num191) + 1.57f;
				num193 = (float)Math.Sqrt(num191 * num191 + num192 * num192);
				int num194 = NPC.width;
				num193 = (num193 - num194) / num193;
				num191 *= num193;
				num192 *= num193;
				NPC.velocity = Vector2.Zero;
				NPC.position.X = NPC.position.X + num191;
				NPC.position.Y = NPC.position.Y + num192;
				if (directional) {
					if (num191 < 0f) {
						NPC.spriteDirection = 1;
					}
					if (num191 > 0f) {
						NPC.spriteDirection = -1;
					}
				}
			}
			else {
				if (!flag18) {
					NPC.TargetClosest(true);
					NPC.velocity.Y = NPC.velocity.Y + 0.11f;
					if (NPC.velocity.Y > num188) {
						NPC.velocity.Y = num188;
					}
					if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num188 * 0.4) {
						if (NPC.velocity.X < 0f) {
							NPC.velocity.X = NPC.velocity.X - num189 * 1.1f;
						}
						else {
							NPC.velocity.X = NPC.velocity.X + num189 * 1.1f;
						}
					}
					else if (NPC.velocity.Y == num188) {
						if (NPC.velocity.X < num191) {
							NPC.velocity.X = NPC.velocity.X + num189;
						}
						else if (NPC.velocity.X > num191) {
							NPC.velocity.X = NPC.velocity.X - num189;
						}
					}
					else if (NPC.velocity.Y > 4f) {
						if (NPC.velocity.X < 0f) {
							NPC.velocity.X = NPC.velocity.X + num189 * 0.9f;
						}
						else {
							NPC.velocity.X = NPC.velocity.X - num189 * 0.9f;
						}
					}
				}
				else {
					if (!flies && NPC.behindTiles && NPC.soundDelay == 0) {
						float num195 = num193 / 40f;
						if (num195 < 10f) {
							num195 = 10f;
						}
						if (num195 > 20f) {
							num195 = 20f;
						}
						NPC.soundDelay = (int)num195;
						SoundEngine.PlaySound(SoundID.AbigailCry, NPC.Center);
					}
					num193 = (float)Math.Sqrt(num191 * num191 + num192 * num192);
					float num196 = Math.Abs(num191);
					float num197 = Math.Abs(num192);
					float num198 = num188 / num193;
					num191 *= num198;
					num192 *= num198;
					if (ShouldRun()) {
						bool flag20 = true;
						if (flag20) {
							if (Main.netMode != NetmodeID.MultiplayerClient && NPC.position.Y / 16f > (Main.rockLayer + Main.maxTilesY) / 2.0) {//2
								NPC.active = false;//false
								int num200 = (int)NPC.ai[0];
								while (num200 > 0 && num200 < 200 && Main.npc[num200].active && Main.npc[num200].aiStyle == NPC.aiStyle) {
									int num201 = (int)Main.npc[num200].ai[0];
									Main.npc[num200].active = false;
									NPC.life = 0;
									if (Main.netMode == NetmodeID.Server) {
										NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num200, 0f, 0f, 0f, 0, 0, 0);
									}
									num200 = num201;
								}
								if (Main.netMode == NetmodeID.Server) {
									NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, NPC.whoAmI, 0f, 0f, 0f, 0, 0, 0);
								}
							}
							num191 = 0f;
							num192 = num188;
						}
					}
					bool flag21 = false;
					if (!flag21) {
						if (NPC.velocity.X > 0f && num191 > 0f || NPC.velocity.X < 0f && num191 < 0f || NPC.velocity.Y > 0f && num192 > 0f || NPC.velocity.Y < 0f && num192 < 0f) {
							if (NPC.velocity.X < num191) {
								NPC.velocity.X = NPC.velocity.X + num189;
							}
							else {
								if (NPC.velocity.X > num191) {
									NPC.velocity.X = NPC.velocity.X - num189;
								}
							}
							if (NPC.velocity.Y < num192) {
								NPC.velocity.Y = NPC.velocity.Y + num189;
							}
							else {
								if (NPC.velocity.Y > num192) {
									NPC.velocity.Y = NPC.velocity.Y - num189;
								}
							}
							if (Math.Abs(num192) < num188 * 0.2 && (NPC.velocity.X > 0f && num191 < 0f || NPC.velocity.X < 0f && num191 > 0f)) {
								if (NPC.velocity.Y > 0f) {
									NPC.velocity.Y = NPC.velocity.Y + num189 * 2f;
								}
								else {
									NPC.velocity.Y = NPC.velocity.Y - num189 * 2f;
								}
							}
							if (Math.Abs(num191) < num188 * 0.2 && (NPC.velocity.Y > 0f && num192 < 0f || NPC.velocity.Y < 0f && num192 > 0f)) {
								if (NPC.velocity.X > 0f) {
									NPC.velocity.X = NPC.velocity.X + num189 * 2f;
								}
								else {
									NPC.velocity.X = NPC.velocity.X - num189 * 2f;
								}
							}
						}
						else {
							if (num196 > num197) {
								if (NPC.velocity.X < num191) {
									NPC.velocity.X = NPC.velocity.X + num189 * 1.1f;
								}
								else if (NPC.velocity.X > num191) {
									NPC.velocity.X = NPC.velocity.X - num189 * 1.1f;
								}
								if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num188 * 0.5) {
									if (NPC.velocity.Y > 0f) {
										NPC.velocity.Y = NPC.velocity.Y + num189;
									}
									else {
										NPC.velocity.Y = NPC.velocity.Y - num189;
									}
								}
							}
							else {
								if (NPC.velocity.Y < num192) {
									NPC.velocity.Y = NPC.velocity.Y + num189 * 1.1f;
								}
								else if (NPC.velocity.Y > num192) {
									NPC.velocity.Y = NPC.velocity.Y - num189 * 1.1f;
								}
								if (Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y) < num188 * 0.5) {
									if (NPC.velocity.X > 0f) {
										NPC.velocity.X = NPC.velocity.X + num189;
									}
									else {
										NPC.velocity.X = NPC.velocity.X - num189;
									}
								}
							}
						}
					}
				}
				NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
				if (head) {
					if (flag18) {
						if (NPC.localAI[0] != 1f) {
							NPC.netUpdate = true;
						}
						NPC.localAI[0] = 1f;
					}
					else {
						if (NPC.localAI[0] != 0f) {
							NPC.netUpdate = true;
						}
						NPC.localAI[0] = 0f;
					}
					if ((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f || NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f || NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f || NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f) && !NPC.justHit) {
						NPC.netUpdate = true;
						return;
					}
				}
                FindTarget();

				 NPC.ai[2]++;
				if (NPC.ai[2] % 250 == 100){//90
					for (int i = -2; i <= 3; i++) {  
                    	float Speed = 12f;
                        int damage = (int)NpcChanges1.ExpertDamageScale(30,"MyBoss");
                        Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width * i-1), NPC.position.Y + (NPC.height * i+2));
                        int type = ProjectileType<InfernalBall>();
						SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.Center); 
						float rotation = (float) Math.Atan2(vector8.Y-(Main.player[NPC.target].position.Y+(Main.player[NPC.target].height * 0.5f)), vector8.X-(Main.player[NPC.target].position.X+(Main.player[NPC.target].width * 0.5f)));
                        int num55 = Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y,(float)((Math.Cos(rotation) * Speed)*-1),(float)((Math.Sin(rotation) * Speed)*-1), type, damage, 0f, 0);
                  		 }
					}
				if (NPC.ai[2] % 250 == 100)
                    FireBallStrongIa(12f, (int)NpcChanges1.ExpertDamageScale(70,"MyBoss"),"*",4,3);  
				if (NPC.ai[2] % 250 == 100)
                    FireBallStrongIa(12f, (int)NpcChanges1.ExpertDamageScale(70, "MyBoss"),"/",4,3);

				if (NPC.ai[2] % 200 == 100)
                    SpikeIa(0f, (int)NpcChanges1.ExpertDamageScale(40, "MyBoss"), "Weak",-3,-3);
				if (NPC.ai[2] % 300 == 100)
                    SpikeIa(0f,(int)NpcChanges1.ExpertDamageScale(90, "MyBoss"),"Strong",3,-3);
				if (NPC.ai[2] % 900 == 300)
                    SummonIa(NPCID.Demon);
				if (NPC.ai[2] % 1900 == 200)
                    SummonIa(NPCID.RedDevil);

                TyrantTp();
                LifeSpeed();
			}
			CustomBehavior();
		}
        public void FindTarget()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
                NPC.TargetClosest(true);
        }
        public void TyrantTp()
        {
            Player player = Main.player[NPC.target];
            if (CanTp)
            {
                if (Vector2.Distance(player.Center, NPC.Center) > 500*16 && !Main.player[NPC.target].dead)
                    NPC.Center = player.Center + new Vector2(350, 350);
                if (Main.player[NPC.target].dead)
                    CanTp = false;
            }
            else
            {
                if (Vector2.Distance(player.Center, NPC.Center) <= 10*16)
                    CanTp = true;
            }
        }
        public void FireBallStrongIa(float Speed, int damage, string signo1, int cordx, int cordy)
        {
            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width * cordx), NPC.position.Y + (NPC.height * cordy));  
            if (signo1 == "/")
            {
                vector8 = new Vector2(NPC.position.X + (NPC.width / cordx), NPC.position.Y + (NPC.height / cordy));
            }
            int type = ProjectileType<InfernalBallF>();
			SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.Center);
            float rotation = (float)Math.Atan2(vector8.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f)), vector8.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
            int num55 = Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
        } 
        
		public void SpikeIa(float Speed, int damage,string tipo,int cordx,int cordy)
        {
            if (tipo == "Strong")
            {
                Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width * cordx), NPC.position.Y + (NPC.height * cordy));
                int type = ProjectileType<InfernalSpikeF>();
				SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.Center);
                float rotation = (float)Math.Atan2(vector8.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f)), vector8.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
                int num55 = Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
            }
            else if(tipo == "Weak")
            {
                Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width * cordx), NPC.position.Y + (NPC.height * cordy));
                int type = ProjectileType<InfernalSpike>();
				SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.Center);
                float rotation = (float)Math.Atan2(vector8.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f)), vector8.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
                int num55 = Projectile.NewProjectile(NPC.GetSource_FromAI(),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
            }
        }
        public void SummonIa(int Npc)
        {
            NPC.NewNPC(NPC.GetSource_FromAI(),(int)NPC.position.X, (int)NPC.position.Y, Npc);
        }
		public void LifeSpeed()
        {
			if (NPC.life < NPC.lifeMax / 2)
			{
				speed = speed = 40;//9.5f
				turnSpeed = 0.15f;
				NPC.defense = TyrantArmor(999);
			}
			if (NPC.life < NPC.lifeMax / 4)
			{
				speed = speed = 60;//9.5f
				turnSpeed = 0.55f;
				NPC.defense = TyrantArmor(999);
			}
			if (NPC.life < NPC.lifeMax / 10)
			{
				turnSpeed = 1.1f;
				NPC.defense = TyrantArmor(999);
			}
			if (NPC.life < NPC.lifeMax / 15 && world1.ReaperMode)
			{
				turnSpeed = 1.5f;
				NPC.defense = TyrantArmor(999);
			}
		}

		public virtual void Init() {
		}

		public virtual bool ShouldRun() {
			return false;
		}

		public virtual void CustomBehavior() {
		}
		
		public int TyrantArmor(int i)
        {
           // Mod CalamityMod = ModLoader.GetMod("CalamityMod");


            if (Main.expertMode || Main.masterMode)
            {
				if (NPC.life > NPC.life / 10) i = i / 3;
                else if (NPC.life > NPC.life / 15 && world1.ReaperMode) i = 0;
            }
            else
            {
				if (NPC.life > NPC.life / 4) i = i / 2;
			}
            int a = 0;

           // if (CalamityMod != null)
               // a = i * 2;
            //else 
				a = i;
			return a;
        }



		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) {
			return head ? (bool?)null : false;
		}
		
		public override void BossLoot(ref string name, ref int potionType)
        {
			DownedBossSystem.downedTyrant = true;
            potionType = ItemID.GreaterHealingPotion;      
            }
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<Spike_saber>(), 3,999999999));
			npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<Tyrant_repeater>(), 3,999999999));
			npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<Tyran_Blast>(), 3,999999999));
			npcLoot.Add(ItemDropRule.NormalvsExpert(ItemType<InfernalMask>(), 7,999999999));
			npcLoot.Add(ItemDropRule.Common(ItemType<InfernalTrophy>(), 10));
			
			npcLoot.Add(ItemDropRule.BossBag(ItemType<infernalBag>()));
			npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ItemType<Tyrant_Relic>()));
		}	
	}
}