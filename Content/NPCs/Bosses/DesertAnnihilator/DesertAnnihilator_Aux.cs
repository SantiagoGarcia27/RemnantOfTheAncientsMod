using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

class DesertAnnihilator_Aux
{
    public NPC npc {  get; set; }

    public Player CurrentTarget => npc != null ? Main.player[npc.target] : Main.player[0];
    private bool _BossIsInRage = false;
    public bool BossIsInRage
    {
        get => _BossIsInRage;
        set
        {
            _BossIsInRage = value;
            if (value) SoundEngine.PlaySound(SoundID.Roar);
        }
    }

    public DesertAnnihilator_Aux(NPC npc)
    {
        this.npc = npc;
    }
    public static Vector2 GetSecurePosition(Vector2 position)
    {
        Vector2 newPos;
        int blockIncrement = 0;

        position = new(Math.Abs(position.X), Math.Abs(position.Y));

        if (!CoordHasTile(position) && !CoordHasLiquid(position)) return position;

        do
        {
            float positionY = position.Y - (blockIncrement++).ToCoordinatePosition();
            newPos = new(position.X, positionY);
        } while (CoordHasTile(newPos) || CoordHasLiquid(newPos));

        if (newPos.Y < 0) newPos.Y *= -1;
        return newPos;
    }
    public static bool CoordHasTile(Vector2 pos) => Collision.SolidCollision(pos, 6 * 16, 6 * 16);
    public static bool CoordHasLiquid(Vector2 pos)
    {
        if (Collision.LavaCollision(pos, 6 * 16, 6 * 16) || Collision.WetCollision(pos, 6 * 16, 6 * 16)) return true;
        if (Main.tile[(new Point((int)pos.X / 16, (int)(pos.Y - 5 * 16) / 16))].LiquidAmount > 0) return true;
        return false;
    }
}