using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace TGC.MonoGame.TP.Cam
{
    class CamComun
    {

        private Vector3 camTarget;
        private Vector3 camPosition;
        bool orbit = false;


        public Vector3 getcamTarget(){
            return camTarget;
        }
        public Vector3 getcamPosition()
        {
            return camPosition;
        }
        public CamComun(Vector3 _camTarget, Vector3 _camPosition)
        {
            camTarget = _camTarget;
            camPosition = _camPosition;
        }

        public void upDate(GameTime gameTime)
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                camPosition.X += 0.1f;
                camTarget.X += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                camPosition.X -= 0.1f;
                camTarget.X -= 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                camPosition.Z += 0.1f;
                camTarget.Z += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                camPosition.Z -= 0.1f;
                camTarget.Z -= 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                camPosition.Y += 0.1f;
                camTarget.Y += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                camPosition.Y -= 0.1f;
                camTarget.Y -= 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
               
                Matrix rotationMatrix = Matrix.CreateRotationY(
                                        MathHelper.ToRadians(1f));
                camPosition = Vector3.Transform(camPosition,
                              rotationMatrix);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {

                Matrix rotationMatrix = Matrix.CreateRotationY(
                                        MathHelper.ToRadians(-1f));
                camPosition = Vector3.Transform(camPosition,
                              rotationMatrix);
            }
        }
    }
}
