using System;
using Terraria;

class DesertAnnihilator_Movment
{
    public NPC npc { get; set; }

    public DesertAnnihilator_Movment(NPC npc)
    {
        this.npc = npc;
    }
    float inertia => 1.3f;//0.95f;
    public void MovementAI()
    {
        ResetNPCFrameAndRotation();

        // Handle behavior when npc is wet
        if (npc.wet) HandleWetBehavior();

        // Reset aiAction and set ai[2] if ai[2] is 0f
        InitializeTargeting();

        // Handle movement when npc velocity.Y is 0f
        if (npc.velocity.Y == 0f)
        {
            if (npc.collideY && npc.oldVelocity.Y != 0f && Collision.SolidCollision(npc.position, npc.width, npc.height))
            {
                npc.position.X -= npc.velocity.X + npc.direction;
            }

            if (npc.ai[3] == npc.position.X)
            {
                npc.direction *= -1;
                npc.ai[2] = 200f;
            }

            npc.ai[3] = 0f;
            npc.velocity.X *= inertia;

            if (Math.Abs(npc.velocity.X) < 0.1) npc.velocity.X = 0f;

            int jumpType = Main.rand.NextBool(4) ? 3 : 2;

            //UpdateAnimation(TextureType.Jump);

            HandlejumpTypeBehavior(jumpType);


        }
        else if (npc.target < 255 && ((npc.direction == 1 && npc.velocity.X < 3f) || (npc.direction == -1 && npc.velocity.X > -3f)))
        {
            HandleXMovement();
        }
    }


    // Handles resetting npc frame and rotation
    private void ResetNPCFrameAndRotation()
    {
        //npc.frame.Y = 0;
        //npc.frameCounter = 0.0;
        npc.rotation = 0f;
    }

    // Handles behavior when npc is wet
    private void HandleWetBehavior()
    {
        if (npc.collideY) npc.velocity.Y = -2f;

        if (npc.velocity.Y < 0f && npc.ai[3] == npc.position.X)
        {
            npc.direction *= -1;
            npc.ai[2] = 200f;
        }
        if (npc.velocity.Y > 0f) npc.ai[3] = npc.position.X;
        if (npc.velocity.Y > 2f) npc.velocity.Y *= 0.9f;

        npc.velocity.Y -= 0.5f;
        if (npc.velocity.Y < -4f) npc.velocity.Y = -4f;
        if (npc.ai[2] == 1f) npc.TargetClosest();

    }

    // Handles resetting aiAction and setting ai[0] and ai[2]
    private void InitializeTargeting()
    {
        npc.aiAction = 0;
        if (npc.ai[2] == 0f)
        {
            npc.ai[2] = 1f;
            npc.TargetClosest();
        }
    }


    // Handles behavior based on the value of num34

    private void HandlejumpTypeBehavior(int jumpType)
    {
        npc.netUpdate = true;

        if (npc.ai[2] == 1f) npc.TargetClosest();

        if (jumpType == 3)
        {
            npc.velocity.Y = -8f;
            npc.velocity.X += 3 * npc.direction;
            npc.ai[3] = npc.position.X;
        }
        else
        {
            npc.velocity.Y = -6f;
            npc.velocity.X += 2 * npc.direction;

        }
    }

    // Handles X movement logic
    private void HandleXMovement()
    {
        if (npc.collideX && Math.Abs(npc.velocity.X) == 0.2f)
        {
            npc.position.X -= 1.4f * npc.direction;//1.4
        }

        if (npc.collideY && npc.oldVelocity.Y != 0f && Collision.SolidCollision(npc.position, npc.width, npc.height))
        {
            npc.position.X -= npc.velocity.X + npc.direction;
        }

        float acceleration = 0.6f * Main.player[npc.target].maxRunSpeed;
        float deceleration = 0.97f;

        if ((npc.direction == -1 && npc.velocity.X < 0.01f) || (npc.direction == 1 && npc.velocity.X > -0.01f))
        {
            npc.velocity.X += acceleration * npc.direction;
        }
        else
        {
            npc.velocity.X *= deceleration;
        }
    }
}