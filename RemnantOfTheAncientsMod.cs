using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Items.Items;
using System;
using Terraria.GameContent;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.InfernumBossIntroScreen;
using FargowiltasSouls.Core.Toggler;
using System.IO;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using System.Linq;

namespace RemnantOfTheAncientsMod
{
    class RemnantOfTheAncientsMod : Mod
    {
        public static Mod CalamityMod;
        public static Mod BossChecklist;
        public static Mod ThoriumMod;
        public static Mod TerrariaOverhaul;
        public static Mod InfernumMod;
        public static Mod Census;
        public static Mod RemnantOfTheAncients;
        public static Mod FargosSoulMod;
        public static Mod FargowiltasMod;
        public static Mod AlchemistNPCMod;
        public static Mod RemnantOfTheAncientsMusic;
        public static Mod MeleeWeaponEffects;
        public static Mod Terraria;
        public static bool DebuggMode;
        public static int CustomCurrencyId;
        public static int MaxRarity = GetMaxRarity();
        public static int MaxPlayers = 0;

        public static string PlaceHolderPath = $"RemnantOfTheAncientsMod/Assets/PlaceHolder";
        public static string PlaceHolderWithoutModPath = $"Assets/PlaceHolder";

        public static string MagicPixelPath = $"RemnantOfTheAncientsMod/Assets/MagicPixelColored";
        public RemnantOfTheAncientsMod()
        {

        }

        public override void Load()
        {
            ModLoader.TryGetMod("CalamityMod", out CalamityMod);
            ModLoader.TryGetMod("ThoriumMod", out ThoriumMod);
            ModLoader.TryGetMod("BossChecklist", out BossChecklist);
            ModLoader.TryGetMod("TerrariaOverhaul", out TerrariaOverhaul);
            ModLoader.TryGetMod("Census", out Census);
            ModLoader.TryGetMod("InfernumMode", out InfernumMod);
            ModLoader.TryGetMod("RemnantOfTheAncientsMod", out RemnantOfTheAncients);
            ModLoader.TryGetMod("FargowiltasSouls", out FargosSoulMod);
            ModLoader.TryGetMod("Fargowiltas", out FargowiltasMod);
            ModLoader.TryGetMod("AlchemistNPCLite", out AlchemistNPCMod);
            ModLoader.TryGetMod("RemnantOfTheAncientsMusicMod", out RemnantOfTheAncientsMusic);
            ModLoader.TryGetMod("MeleeWeaponEffects", out MeleeWeaponEffects);
            // ModLoader.TryGetMod("SangarUtilities", out Terraria);

            if (ModContent.GetInstance<Terracoin>() != null)
            {
                ModContent.GetInstance<Terracoin>();
            }

            if (InfernumMod != null)
            {
                IntroScreenManager.Load();
            }

            BackgroundTextureLoader.AddBackgroundTexture(this, PlaceHolderPath);
       



        }
        [JITWhenModsEnabled("FargowiltasSouls")]
        public static void LoadTogglesFromType(Type type)
        {
            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod))
            {
                var a = Activator.CreateInstance(type);
                ToggleCollection toggles = (ToggleCollection)a;
                if (!toggles.Active)
                {
                    return;
                }
                ModContent.GetInstance<RemnantOfTheAncientsMod>().Logger.Info("ToggleCollection found: type");
                foreach (Toggle item in toggles.Load())
                {
                    ToggleLoader.RegisterToggle(item);
                }
            }
        }
        public override void PostSetupContent()
        {
            fastPlataformOverride();
            base.PostSetupContent();
        }
        public static int GetMaxRarity()
        {
            int max = 0;
            for (int i = 0; i < RarityLoader.RarityCount; i++)
            {
                if (max < i) max = i;
            }
            return max;
        }

        public override void Unload()
        {
            RemnantOfTheAncientsMod.BossChecklist = null;
            Array.Resize(ref TextureAssets.GlowMask, GlowMaskID.Count);
        }
        public static short AddGlowMask(string texture)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                string name = texture;
                if (ModContent.RequestIfExists(name, out Asset<Texture2D> asset))
                {
                    int index = TextureAssets.GlowMask.Length;
                    Array.Resize(ref TextureAssets.GlowMask, index + 1);
                    TextureAssets.GlowMask[^1] = asset;
                    return (short)index;
                }
            }
            return -1;
        }
        public static Color GetLightColor(Vector2 position)
        {
            return Lighting.GetColor((int)(position.X / 16f), (int)(position.Y / 16f));
        }

        public static int ParticleMeter(int i)
        {
            float lagLevel = ModContent.GetInstance<ConfigServer>().LagReducer;

            if (lagLevel == 3f)
            {
                return 0;
            }

            return (int)(i / Math.Pow(2, (int)lagLevel));
        }
        public static int ParticleMeter(int total, int first, int second, int off)
        {
            float lagLevel = ModContent.GetInstance<ConfigServer>().LagReducer;

            switch (lagLevel)
            {
                case 0:
                    return total;
                case 1:
                    return first;
                case 2:
                    return second;
                case 3:
                    return off;
                default:
                    return off;
            }
        }
        public static bool ParticleMeterChoice()
        {
            float lagLevel = ModContent.GetInstance<ConfigServer>().LagReducer;

            if (lagLevel == 3f)
            {
                return false;
            }
            if (lagLevel == 0)
            {
                return true;
            }
            if (lagLevel == 1)
            {
                return Main.rand.NextBool(2);
            }
            return Main.rand.NextBool(4);
        }
        public static int ParticleMeter(int i, bool increment)
        {
            float lagLevel = ModContent.GetInstance<ConfigServer>().LagReducer;

            if (lagLevel == 3)
            {
                return 0;
            }
            if (increment)
            {
                if (lagLevel == 2)
                {
                    return i * 4;
                }
                else if (lagLevel == 1)
                {
                    return i * 2;
                }
                else return i;
            }
            else
            {
                return (int)(i / Math.Pow(2, (int)lagLevel));
            }
        }
        public static int MaxPlayerOnline()
        {
            MaxPlayers = 0;
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                if (MaxPlayers < 10)
                {
                    Player plr = Main.player[i];
                    if (plr.active && !plr.dead)
                    {
                        MaxPlayers++;
                    }
                }
            }
            return MaxPlayers - 1;
        }


        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            Netcode.HandlePacket(reader, whoAmI);
        }


        public void fastPlataformOverride()
        {
            try
            {
                var detourManager = MonoMod.RuntimeDetour.DetourManager.GetDetourInfo(typeof(Player).GetMethod("Update"));
                int count = detourManager.Detours.Count();
                if (count >= 1)
                {
                    base.Load();
                    return;
                }
                IL_Player.Update += il =>
                {
                    var c = new ILCursor(il);

                    // These will be always be set by the time they are used,
                    // but C# doesn't know that the predicates will always be called
                    // (unless an error occurs, but code execution will stop here too in that case)
                    int ignorePlatsIndex = default;
                    int fallThroughIndex = default;

                    c.GotoNext(MoveType.After,
                        // We don't actually care about the first 2 parts, but match them anyway,
                        // because the sequence we actaully need may very well appear before.
                        i => i.MatchLdarg0(),
                        i => i.MatchLdfld<Entity>(nameof(Entity.velocity)),
                        i => i.Match(OpCodes.Stloc_S),

                        i => i.MatchLdarg0(),
                        i => i.Match(OpCodes.Ldc_I4_0),
                        i => i.MatchStfld<Player>(nameof(Player.slideDir)),

                        i => i.Match(OpCodes.Ldc_I4_0),
                        i => i.MatchStloc(out ignorePlatsIndex),

                        i => i.MatchLdarg0(),
                        i => i.MatchLdfld<Player>(nameof(Player.controlDown)),
                        i => i.MatchStloc(out fallThroughIndex)
                    );

                    c.Index -= 5;
                    c.RemoveRange(5);

                    c.EmitLdarg0();                                                   // Push the first argument (this)
                    c.EmitLdfld(typeof(Player).GetField(nameof(Player.controlDown))); // Pop, then push the value of controlDown
                    c.EmitDup();                                                      // Duplicate stack value
                    c.EmitStloc(ignorePlatsIndex);                                    // Store stack value in ignorePlats
                    c.EmitStloc(fallThroughIndex);                                    // Store stack value in fallThrough
                };

                base.Load();
            }
            catch (Exception ex)
            {
                Logger.Error("RemnantOfTheAncientsMod: Error on fastPlataformOverride:" + ex);
            }
        }
    }
}