#region File Description
//-----------------------------------------------------------------------------
// FireParticleSystem.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace TyphoonRollers
{
    /// <summary>
    /// Custom particle system for creating a flame effect.
    /// </summary>
    class EnemyAttackParticleSystem : ParticleSystem
    {
        public EnemyAttackParticleSystem(Game1 game, ContentManager content)
            : base(game, content)
        { }


        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "ParticleTexture/EnemyAttackParticle";

            settings.MaxParticles = 3000;

            settings.Duration = TimeSpan.FromSeconds(0.1);

            settings.DurationRandomness = 1;

            settings.MinHorizontalVelocity = 0;
            settings.MaxHorizontalVelocity = 10;

            settings.MinVerticalVelocity = -10;
            settings.MaxVerticalVelocity = 10;

            // Set gravity upside down, so the flames will 'fall' upward.
            settings.Gravity = new Vector3(0, 15, 0);

            settings.MinColor = new Color(255, 255, 255, 10);
            settings.MaxColor = new Color(255, 255, 255, 40);

            settings.MinStartSize = 10;
            settings.MaxStartSize = 20;

            settings.MinEndSize = 25;
            settings.MaxEndSize = 40;

            // Use additive blending.
            settings.BlendState = BlendState.Additive;
        }
    }
}
