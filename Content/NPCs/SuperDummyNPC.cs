using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.Chat;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace RemnantOfTheAncientsMod.Content.NPCs
{
    public class SuperDummyNPC : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.TargetDummy];
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            NPC.spriteDirection = NPC.direction;
        }
        bool hitAnimation = false;
        public static int OldDef = 0;
        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 46;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 9999999;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.friendly = false;
            AIType = -1;
            NPC.netAlways = true;
            NPC.aiStyle = 0;
            NPC.knockBackResist = 0;
        }
        public override void UpdateLifeRegen(ref int damage)
        {
            NPC.lifeRegen += 2000000;
        }
        public override void AI()
        {
            if (hitAnimation) Animate();
            int mode = (int)NPC.ai[0];
            if (mode == 0)
            {
                NPC.defense = 0;
            }
            else if (mode == 1)
            {
                NPC.defense = Main.player[Main.myPlayer].statDefense;
            }
            else if (mode == 2)
            {
                NPC.defense = 0;
                NPC.width = 230;
                NPC.height = 230;
                NPC.scale = 5;
            }
            else if (mode == 3)
            {
                NPC.defense = Main.player[Main.myPlayer].statDefense;
                NPC.width = 230;
                NPC.height = 230;
                NPC.scale = 5;
            }
        }

       
        string direction = "Nada";
        int currentFrame = 0;
        Dictionary<string, List<int>> frames = new()
        {
            {"Derecha", [0,1,2,3,4]},
            {"Izquierda", [0,5,6,7,8]},
            {"Nada", [0,1]},
        };
        public void Animate()
        { 
            NPC.frameCounter++;
            if (NPC.frameCounter >= 5)
            {
                NPC.frameCounter = 0;

                NPC.frame.Y = frames[direction][currentFrame] * 50;
                currentFrame++;


                if (NPC.frame.Y >= frames[direction].Max() * 46)
                {
                    NPC.frame.Y = 0;
                    hitAnimation = false;
                }
            } 
        }
        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            hitAnimation = true;
            currentFrame = 0;
            SetDirection(hit.HitDirection);
            if (NPC.defense < OldDef || NPC.defense > OldDef)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Defense: " + NPC.defense.ToString() + " [i:156]"), Color.Blue);
                base.OnHitByItem(player, item, hit, damageDone);
                OldDef = NPC.defense;
            }
        }
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            hitAnimation = true;
            currentFrame = 0;
            SetDirection(hit.HitDirection);
            if (NPC.defense < OldDef || NPC.defense > OldDef)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Defense: " + NPC.defense.ToString() + " [i:156]"), Color.Blue);
                base.OnHitByProjectile(projectile, hit, damageDone);
                OldDef = NPC.defense;
            }

        }
        public override void OnKill()
        {
           // ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Small with player defense"), Color.Red);
            base.OnKill();
        }
        void SetDirection(int dir)
        {
            switch(dir)
            {
                case 1:
                    direction = "Derecha";
                    break;
                case -1:
                    direction = "Izquierda";
                    break;
                default:
                    direction = "Nada";
                    break;
            }
        }

        public override bool CheckDead()
        {
            if (NPC.lifeRegen < 0)
            {
                NPC.life = NPC.lifeMax;

                return false;
            }
            return true;
        }
    }
}
