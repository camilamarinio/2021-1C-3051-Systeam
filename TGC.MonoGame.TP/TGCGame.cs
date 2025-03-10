﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Cam;

namespace TGC.MonoGame.TP
{
    /// <summary>
    ///     Esta es la clase principal  del juego.
    ///     Inicialmente puede ser renombrado o copiado para hacer más ejemplos chicos, en el caso de copiar para que se
    ///     ejecute el nuevo ejemplo deben cambiar la clase que ejecuta Program <see cref="Program.Main()" /> linea 10.
    /// </summary>
    public class TGCGame : Game
    {
        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public const string ContentFolderMusic = "Music/";
        public const string ContentFolderSounds = "Sounds/";
        public const string ContentFolderSpriteFonts = "SpriteFonts/";
        public const string ContentFolderTextures = "Textures/";

        /// <summary>
        ///     Constructor del juego.
        /// </summary>
        public TGCGame()
        {
            // Maneja la configuracion y la administracion del dispositivo grafico.
            Graphics = new GraphicsDeviceManager(this);
            // Descomentar para que el juego sea pantalla completa.
            // Graphics.IsFullScreen = true;
            // Carpeta raiz donde va a estar toda la Media.
            Content.RootDirectory = "Content";
            // Hace que el mouse sea visible.
            IsMouseVisible = true;
        }

        private GraphicsDeviceManager Graphics { get; }
        private SpriteBatch SpriteBatch { get; set; }
        private Model Sing { get; set; }
        private Model Trench { get; set; }
        private Model ball { get; set; }
        private Model cube { get; set; }
        

        private Effect Effect { get; set; }
        private float Rotation { get; set; }
        private Matrix World { get; set; }
        private Matrix View { get; set; }
        private Matrix Projection { get; set; }

        private Texture2D SingTexture { get; set; }
        private Texture2D UnSplash { get; set; }
        private Texture2D Duy { get; set; }
        private Texture2D Paper { get; set; }
        private Texture2D Metal { get; set; }

        private CamComun camara { get; set; }

        /// <summary>
        ///     Se llama una sola vez, al principio cuando se ejecuta el ejemplo.
        ///     Escribir aqui el codigo de inicializacion: el procesamiento que podemos pre calcular para nuestro juego.
        /// </summary>
        protected override void Initialize()
        {
            camara = new CamComun(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, -5));
            // La logica de inicializacion que no depende del contenido se recomienda poner en este metodo.
            
            // Apago el backface culling.
            // Esto se hace por un problema en el diseno del modelo del logo de la materia.
            // Una vez que empiecen su juego, esto no es mas necesario y lo pueden sacar.
            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;
            // Seria hasta aca.

            // Configuramos nuestras matrices de la escena.
            //Original
            World = Matrix.Identity;
            View = Matrix.CreateLookAt(Vector3.UnitZ * 150, Vector3.Zero, Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1, 250);

            base.Initialize();
        }

        /// <summary>
        ///     Se llama una sola vez, al principio cuando se ejecuta el ejemplo, despues de Initialize.
        ///     Escribir aqui el codigo de inicializacion: cargar modelos, texturas, estructuras de optimizacion, el procesamiento
        ///     que podemos pre calcular para nuestro juego.
        /// </summary>
        protected override void LoadContent()
        {




            // Aca es donde deberiamos cargar todos los contenido necesarios antes de iniciar el juego.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // Cargo el modelo del logo.
            Sing = Content.Load<Model>(ContentFolder3D + "tgc-logo/StreetSign");
            Trench = Content.Load<Model>(ContentFolder3D + "StarWars/Trench2/Trench");
            ball = Content.Load<Model>(ContentFolder3D + "Marble/FigurasGeometricas/sphere");
            cube = Content.Load<Model>(ContentFolder3D + "Marble/FigurasGeometricas/cube");
           


            UnSplash = Content.Load<Texture2D>(ContentFolderTextures + "unsplash");
            Duy = Content.Load<Texture2D>(ContentFolderTextures + "duy");
            Paper = Content.Load<Texture2D>(ContentFolderTextures + "paper");
            Metal = Content.Load<Texture2D>(ContentFolderTextures + "metal");
            // Cargo un efecto basico propio declarado en el Content pipeline.
            // En el juego no pueden usar BasicEffect de MG, deben usar siempre efectos propios.
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            // Asigno el efecto que cargue a cada parte del mesh.
            // Un modelo puede tener mas de 1 mesh internamente.

            foreach (var mesh in Sing.Meshes) { 
            // Un mesh puede tener mas de 1 mesh part (cada 1 puede tener su propio efecto).
                foreach (var meshPart in mesh.MeshParts) {
                    SingTexture = ((BasicEffect)meshPart.Effect).Texture;
                    meshPart.Effect = Effect;
                } 
            }
            
            foreach (var mesh in Trench.Meshes)
            {
                // Un mesh puede tener mas de 1 mesh part (cada 1 puede tener su propio efecto).
                foreach (var meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Effect;
                }
            }

            foreach (var mesh in ball.Meshes)
            {
                // Un mesh puede tener mas de 1 mesh part (cada 1 puede tener su propio efecto).
                foreach (var meshPart in mesh.MeshParts)
                {
                   // SingTexture = ((BasicEffect)meshPart.Effect).Texture;
                    meshPart.Effect = Effect;
                }
            }

            foreach (var mesh in cube.Meshes)
            {
                // Un mesh puede tener mas de 1 mesh part (cada 1 puede tener su propio efecto).
                foreach (var meshPart in mesh.MeshParts)
                {
                   // Duy = ((BasicEffect)meshPart.Effect).Texture;
                    meshPart.Effect = Effect;
                }
            }

           

            base.LoadContent();
        }

        /// <summary>
        ///     Se llama en cada frame.
        ///     Se debe escribir toda la logica de computo del modelo, asi como tambien verificar entradas del usuario y reacciones
        ///     ante ellas.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            // Aca deberiamos poner toda la logica de actualizacion del juego.

            //cam
            camara.upDate(gameTime);

            View = Matrix.CreateLookAt(camara.getcamPosition(), camara.getcamTarget(),
                         Vector3.Up);

            // Capturar Input teclado
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                //Salgo del juego.
                Exit();

            float totalTime= Convert.ToSingle(gameTime.TotalGameTime.TotalSeconds);
            // Basado en el tiempo que paso se va generando una rotacion.
            Rotation = totalTime;

            //Effect.Parameters["Time"].SetValue(totalTime);

            base.Update(gameTime);
        }

        /// <summary>
        ///     Se llama cada vez que hay que refrescar la pantalla.
        ///     Escribir aqui el codigo referido al renderizado.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            // Aca deberiamos poner toda la logia de renderizado del juego.
            //GraphicsDevice.Clear(Color.White);
            GraphicsDevice.Clear(Color.Black);
            // Para dibujar le modelo necesitamos pasarle informacion que el efecto esta esperando.
            Effect.Parameters["View"].SetValue(View);
            Effect.Parameters["Projection"].SetValue(Projection);
            //Effect.Parameters["DiffuseColor"]?.SetValue(Color.DarkBlue.ToVector3());
            var rotationMatrix = Matrix.CreateRotationY(Rotation);
            var rotationMatrixZ = Matrix.CreateRotationZ(Rotation);

            foreach (var mesh in Sing.Meshes)
            {
                World = mesh.ParentBone.Transform * Matrix.CreateScale(0.003f) * rotationMatrix;
                Effect.Parameters["World"].SetValue(World);
                Effect.Parameters["ModelTexture"].SetValue(SingTexture);
                //Effect.Parameters["ModelTexture"].SetValue(Paper);
                mesh.Draw();
            }

            
            foreach (var mesh in Trench.Meshes)
            {
                World = mesh.ParentBone.Transform * Matrix.CreateScale(0.005f) * Matrix.CreateTranslation(Vector3.Up * -1.9F);
                Effect.Parameters["World"].SetValue(World);
                Effect.Parameters["ModelTexture"].SetValue(Metal);
                mesh.Draw();
            }
            for (var i=0; i<=8;i++)
            {
                foreach (var mesh in Trench.Meshes)
                {
                    World = mesh.ParentBone.Transform * Matrix.CreateScale(0.005f) * Matrix.CreateTranslation(Vector3.Up * -1.9F) * Matrix.CreateTranslation(Vector3.Backward * (5.9F + i*6));
                    Effect.Parameters["World"].SetValue(World);
                    Effect.Parameters["ModelTexture"].SetValue(Metal);
                    mesh.Draw();
                }
            }
            foreach (var mesh in ball.Meshes)
            {
                World = mesh.ParentBone.Transform* Matrix.CreateScale(0.004f) * rotationMatrix * Matrix.CreateTranslation(Vector3.Left * 1.1F) * Matrix.CreateTranslation(Vector3.Down * 1.2F);
                Effect.Parameters["World"].SetValue(World);
                Effect.Parameters["ModelTexture"].SetValue(UnSplash);
                mesh.Draw();
            }

            foreach (var mesh in ball.Meshes)
            {
                World = mesh.ParentBone.Transform * Matrix.CreateScale(0.004f) * rotationMatrixZ * Matrix.CreateTranslation(Vector3.Left * 1.9F) * Matrix.CreateTranslation(Vector3.Down * 1.2F);
                Effect.Parameters["World"].SetValue(World);
                Effect.Parameters["ModelTexture"].SetValue(Paper);
                mesh.Draw();
            }
            foreach (var mesh in ball.Meshes)
            {
                World = mesh.ParentBone.Transform * Matrix.CreateScale(0.004f) * rotationMatrixZ * Matrix.CreateTranslation(Vector3.Left * 0.2F) * Matrix.CreateTranslation(Vector3.Down * 1.2F);
                Effect.Parameters["World"].SetValue(World);
                Effect.Parameters["ModelTexture"].SetValue(Duy);
                mesh.Draw();
            }
            /*
            foreach (var mesh in cube.Meshes)
            {
                World = mesh.ParentBone.Transform * Matrix.CreateScale(0.4f) * Matrix.CreateTranslation(Vector3.Left * 2.4F);
                Effect.Parameters["World"].SetValue(World);
                Effect.Parameters["ModelTexture"].SetValue(Duy);
                mesh.Draw();
            }*/


        }  

        /// <summary>
        ///     Libero los recursos que se cargaron en el juego.
        /// </summary>
        protected override void UnloadContent()
        {
            // Libero los recursos.
            Content.Unload();

            base.UnloadContent();
        }
    }
}