using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Tools;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Multiclass
{
    public class SolverCursor : ModProjectile
    {
        public override string Texture => "RemnantOfTheAncientsMod/Assets/PlaceHolder";
        public override void SetStaticDefaults()
        {
            // //DisplayName.SetDefault("SkyCutterS"); 

        }
        public override void SetDefaults()
        {
            Projectile.width = 15;
            Projectile.height = 15;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 20000;
            Projectile.light = 1.75f;
            Projectile.extraUpdates = 1;
            // Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.scale = 1f;
            AIType = -1;
        }
        public string GetTexture()
        {
            switch (RemnantPlayer.SolverMode)
            {
                case 0:
                    return "RemnantOfTheAncientsMod/Content/Projectiles/Multiclass/SolverMove";
                case 1:
                    return "RemnantOfTheAncientsMod/Content/Projectiles/Multiclass/SolverBreak";
                default:
                    return "RemnantOfTheAncientsMod/Assets/PlaceHolder";
            }
        }
        public static NPC target = null;
        public Tile tile_;
        public override void AI()
        {
            CheckActive();
            Player player = Main.player[Projectile.owner];
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.position = Main.MouseWorld;
                Projectile.netUpdate = true;
            }
            // Projectile.position = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            if (RemnantPlayer.SolverMode == 0)
            {

                Rectangle hitbox = Projectile.Hitbox;
                if (Main.mouseLeft)
                {
                    if (target == null)
                    {
                        foreach (NPC n in Main.npc)
                        {
                            if (n.active && !n.boss && n.lifeMax <= 1000)
                            {
                                if (hitbox.Intersects(n.Hitbox))
                                {
                                    target = n;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (target.life <= 0 || !target.active || !hitbox.Intersects(target.Hitbox))
                        {
                            target = null;
                        }
                        if (target != null)
                        {
                            target.Center = Projectile.Center/* - new Vector2((float)(target.width / 2), (float)(target.height / 2))*/;
                            target.netUpdate = true;
                        }
                    }
                }
            }
            else if (RemnantPlayer.SolverMode == 1)
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    if (Main.mouseLeft)
                    {
                        int i = (int)(Projectile.Center.X / 16f);
                        int j = (int)(Projectile.Center.Y / 16f);
                        // Ensure that all tile coordinates are within the world bounds

                        if (Utils1.GetBlockPickPower(Main.tile[i, j], j) <= Utils1.GetBestPickPower(player))
                        {
                            WorldGen.KillTile(i, j);
                            if (!Main.tile[i, j].HasTile && Main.netMode != NetmodeID.SinglePlayer)
                                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j);
                        }
                        else
                        {

                            player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} was consumed by Solver"), player.statLifeMax / 4, player.direction, false, false, -1, false, 10000, 1, 2);
                            Dust.NewDust(player.position, 10, 10, DustID.Electric, 0, 0, 0, Color.Red, 1.0f);
                        }
                    }
                }
            }
        }
        public void CheckActive()
        {
            if (Main.player[Projectile.owner].HeldItem.type != ModContent.ItemType<SolverHand>())
            {
                Projectile.Kill();
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            return /*target.friendly &&*/ Main.myPlayer == Projectile.owner && Main.mouseRight;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
        }

        public override void OnKill(int timeLeft)
        {
            //SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            const int NUM_DUSTS = 20;
            for (int i = 0; i < ModContent.GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(NUM_DUSTS); i++)
            {
                int p1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, (int)Projectile.localAI[1], 0f, 0f, 100, default, 1f);
                Main.dust[p1].velocity = Projectile.velocity;
                Main.dust[p1].noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var texture = ModContent.Request<Texture2D>(GetTexture());

            Vector2 position = Projectile.Center - Main.screenPosition - new Vector2(10, 10);
            Vector2 origin = new(texture.Width() * 0.5f, texture.Height() * 0.5f);
            //Main.spriteBatch.Draw((Texture2D)texture, position, null, Color.Purple, Projectile.rotation, origin,Projectile.scale * 0.5f, SpriteEffects.None, 0f);
            return false;// base.PreDraw(ref lightColor);
        }
    }
    [Autoload(Side = ModSide.Client)]
    public class SolverOverrideCursor : ModSystem
    {
        private static float cursorScale = 1f;
        private static Vector2 CursorSize = new(32, 32);
        private static Vector2 cursorPosition = Vector2.Zero;
        private static LegacyGameInterfaceLayer cursorLayer;

        private Asset<Texture2D>[] cursorTextures =
        [
            ModContent.Request<Texture2D>("Terraria/Images/UI/Cursor_0"),
            ModContent.Request<Texture2D>("Terraria/Images/UI/Cursor_0"),
            ModContent.Request<Texture2D>("Terraria/Images/UI/Cursor_0"),
        ];
        public override void Load()
        {
            IncertListOfAssets();
            var texture = ModContent.Request<Texture2D>("Terraria/Images/UI/Cursor_0");
            Rectangle srcRect = new(0, 0, 32, 32);
            Vector2 center = new(0, 0);

            cursorLayer = new LegacyGameInterfaceLayer($"{nameof(Mod)}: My Cursor", () =>
            {
                foreach (var t in cursorTextures)
                {
                    if (!t.IsLoaded)
                        return true;
                }
                if (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<SolverHand>())
                {
                    switch (RemnantPlayer.SolverMode)
                    {
                        case 0:
                            texture = cursorTextures[1];
                            CursorSize = new(54, 58);
                            center = new(-20, -20);
                            break;
                        case 1:
                            texture = cursorTextures[2];
                            CursorSize = new(54, 58);
                            center = new(-20, -20);
                            break;
                        default:
                            texture = cursorTextures[0];
                            CursorSize = new(32, 32);
                            center = new(0, 0);
                            break;
                    }
                }
                else
                {
                    texture = cursorTextures[0];
                }
                if (!texture.IsLoaded)
                {
                    return true;
                }
                srcRect = new Rectangle(0, 0, (int)CursorSize.X, (int)CursorSize.Y);
                var basePosition = Main.MouseScreen + cursorPosition + center;
                var drawColor = Color.Purple;//new Color(255, 255, 255, 255);

                Main.spriteBatch.Draw((Texture2D)texture, basePosition, srcRect, drawColor, 0f, Vector2.Zero, cursorScale, SpriteEffects.None, 0f);
                return true;
            }, InterfaceScaleType.UI);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            List<string> vanillaCursors = [
                "Cursor",
                "Cursor Info",
                "Mouse Item"
            ];
            if (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<SolverHand>())
            {
                foreach (string cursorName in vanillaCursors)
                {
                    int cursorIndex = layers.FindIndex(layer => layer.Name == $"Vanilla: {cursorName}");
                    if (cursorIndex != -1)
                    {
                        layers.RemoveAt(cursorIndex);
                    }
                }

                int defaultCursorIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Cursor");
                if (defaultCursorIndex != -1)
                {
                    layers.Insert(defaultCursorIndex, cursorLayer);
                }
                else
                {
                    layers.Add(cursorLayer);
                }
            }
        }
        public void IncertListOfAssets()
        {
            cursorTextures[0] = ModContent.Request<Texture2D>("Terraria/Images/UI/Cursor_0");
            cursorTextures[1] = Mod.Assets.Request<Texture2D>("Content/Projectiles/Multiclass/SolverMove");
            cursorTextures[2] = Mod.Assets.Request<Texture2D>("Content/Projectiles/Multiclass/SolverBreak");
        }
    }
}
