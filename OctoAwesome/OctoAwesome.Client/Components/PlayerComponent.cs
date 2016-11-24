﻿using OctoAwesome.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using engenious;
using OctoAwesome.EntityComponents;

namespace OctoAwesome.Client.Components
{
    internal sealed class PlayerComponent : GameComponent
    {
        private new OctoGame Game;

        private IResourceManager resourceManager;

        #region External Input

        public Vector2 HeadInput { get; set; }

        public Vector2 MoveInput { get; set; }

        public bool InteractInput { get; set; }

        public bool ApplyInput { get; set; }

        public bool JumpInput { get; set; }

        public bool FlymodeInput { get; set; }

        public bool[] SlotInput { get; private set; } = new bool[10];

        public bool SlotLeftInput { get; set; }

        public bool SlotRightInput { get; set; }

        #endregion

        public Entity CurrentEntity { get; private set; }

        public HeadComponent CurrentEntityHead { get; private set; }

        // public ActorHost ActorHost { get; private set; }

        public Index3? SelectedBox { get; set; }

        public Vector2? SelectedPoint { get; set; }

        public OrientationFlags SelectedSide { get; set; }

        public OrientationFlags SelectedEdge { get; set; }

        public OrientationFlags SelectedCorner { get; set; }

        public PlayerComponent(OctoGame game, IResourceManager resourceManager)
            : base(game)
        {
            this.resourceManager = resourceManager;
            Game = game;
        }

        public void SetEntity(Entity entity)
        {
            CurrentEntity = entity;

            if (CurrentEntity == null)
            {
                CurrentEntityHead = null;
            }
            else
            {
                // Map other Components
                CurrentEntityHead = CurrentEntity.Components.GetComponent<HeadComponent>();
                if (CurrentEntityHead == null) CurrentEntityHead = new HeadComponent();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!Enabled)
                return;

            if (CurrentEntity == null)
                return;

            CurrentEntityHead.Angle += (float)gameTime.ElapsedGameTime.TotalSeconds * HeadInput.X;
            CurrentEntityHead.Tilt += (float)gameTime.ElapsedGameTime.TotalSeconds * HeadInput.Y;
            CurrentEntityHead.Tilt = Math.Min(1.5f, Math.Max(-1.5f, CurrentEntityHead.Tilt));
            HeadInput = Vector2.Zero;

            //ActorHost.Move = MoveInput;
            //MoveInput = Vector2.Zero;

            //if (JumpInput)
            //    ActorHost.Jump();
            //JumpInput = false;

            //if (InteractInput && SelectedBox.HasValue)
            //    ActorHost.Interact(SelectedBox.Value);
            //InteractInput = false;

            //if (ApplyInput && SelectedBox.HasValue)
            //    ActorHost.Apply(SelectedBox.Value, SelectedSide);
            //ApplyInput = false;

            //if (FlymodeInput)
            //    ActorHost.Player.FlyMode = !ActorHost.Player.FlyMode;
            //FlymodeInput = false;

            //if (ActorHost.Player.Tools != null && ActorHost.Player.Tools.Length > 0)
            //{
            //    if (ActorHost.ActiveTool == null) ActorHost.ActiveTool = ActorHost.Player.Tools[0];
            //    for (int i = 0; i < Math.Min(ActorHost.Player.Tools.Length, SlotInput.Length); i++)
            //    {
            //        if (SlotInput[i])
            //            ActorHost.ActiveTool = ActorHost.Player.Tools[i];
            //        SlotInput[i] = false;
            //    }
            //}

            // Index des aktiven Werkzeugs ermitteln
            //int activeTool = -1;
            //List<int> toolIndices = new List<int>();
            //if (ActorHost.Player.Tools != null)
            //{
            //    for (int i = 0; i < ActorHost.Player.Tools.Length; i++)
            //    {
            //        if (ActorHost.Player.Tools[i] != null)
            //            toolIndices.Add(i);

            //        if (ActorHost.Player.Tools[i] == ActorHost.ActiveTool)
            //            activeTool = toolIndices.Count - 1;
            //    }
            //}

            //if (SlotLeftInput)
            //{
            //    if (activeTool > -1)
            //        activeTool--;
            //    else if (toolIndices.Count > 0)
            //        activeTool = toolIndices[toolIndices.Count - 1];
            //}
            //SlotLeftInput = false;

            //if (SlotRightInput)
            //{
            //    if (activeTool > -1)
            //        activeTool++;
            //    else if (toolIndices.Count > 0)
            //        activeTool = toolIndices[0];
            //}
            //SlotRightInput = false;

            //if (activeTool > -1)
            //{
            //    activeTool = (activeTool + toolIndices.Count) % toolIndices.Count;
            //    ActorHost.ActiveTool = ActorHost.Player.Tools[toolIndices[activeTool]];
            //}
        }
    }
}
