using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using System.IO;

public class InfernalTyrant_MovmentModule
{
    public InfernalTyrant_MovmentModule(InfernalTyrant_AuxiliaryModule auxiliaryModule,TyrantBaseWorm worm)
    {
        auxModule = auxiliaryModule;
        this.worm = worm;
    }

    InfernalTyrant_AuxiliaryModule auxModule;
    TyrantBaseWorm worm;

    private int movementPhase;
    private int movementTimer;
    public bool IsPhase2 => auxModule.IsPhase2;
    internal void UpdateMovement()
    {
        if (!IsPhase2)
        {
            //worm.Acceleration = 0.245f;
            return;
        }
        InfernalTyrant_AuxiliaryModule.LifeSpeed(worm);

        movementTimer--;
        if (movementTimer <= 0)
        {
            int maxPhases = auxModule.IsEnraged ? 3 : 2;
            movementPhase = (movementPhase + 1) % maxPhases;
            movementTimer = movementPhase switch
            {
                0 => 180,
                1 => auxModule.IsEnraged ? 120 : 150,
                2 => 100,
                _ => 180
            };
            auxModule.npc.netUpdate = true;
        }
    }

    public void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(movementPhase);
        writer.Write(movementTimer);
    }

    public void ReceiveExtraAI(BinaryReader reader)
    {
        movementPhase = reader.ReadInt32();
        movementTimer = reader.ReadInt32();
    }
}