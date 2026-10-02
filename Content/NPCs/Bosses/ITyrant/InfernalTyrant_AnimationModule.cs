using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using System.IO;
using Terraria;

class InfernalTyrant_AnimationModule
{ 

    private InfernalTyrant_AuxiliaryModule auxModule { get; set; }
    private NPC npc  => auxModule.npc;
    private bool _shootTelegraphing { get; set; } = false;
    public bool ShootTelegraphing 
    { 
        get => _shootTelegraphing;
        set
        {
            _shootTelegraphing = value;
            npc.netUpdate = true;
        }
        
    }

    public InfernalTyrant_AnimationModule(InfernalTyrant_AuxiliaryModule auxiliaryModule)
    {
        auxModule = auxiliaryModule;
    }

    public void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(_shootTelegraphing);
    }

    public void ReceiveExtraAI(BinaryReader reader)
    {
        _shootTelegraphing = reader.ReadBoolean();
    }
}