using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Kinect;
using System.Linq;
using System;
using System.IO;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace KukulcanGame
{
 
    public class Game1 : Game
    {
        /// <summary>
        /// El graphics se genera por defecto para tomar los recursos de la pantalla al igual que 
        /// el spriteBatch c:
        /// </summary>
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        /// <summary>
        /// Esta variable nos ayuda a determinar si en la seleccion de mano el usuario levanto 
        /// ambas manos, cosa que no debe hacer
        /// </summary>
        bool manosArriba = false;

        /// <summary>
        /// Lista donde se almacenan los puntajes que retorna un metodo de la clase BDcomun
        /// </summary>
        List<string> lstPuntajes;

        /// <summary>
        /// Declaramos los tipos de texturas que vamos a manejar en el proyecto
        /// las texturas son las imagenes que vamos a estar usando
        /// </summary>
        Texture2D genericTile, btJugar, btScores,btExit, head, bg, tile,U,D,R,L;
        Texture2D btnJugarMouse, btnScoresMouse, btnExitMouse, btnCreditosMosue;
        Texture2D brick, natureLeftTop, natureLeftBot, natureRightTop;
        Texture2D splashScrren, logoMonkey, creditos, backGroundMenu;
        Texture2D btnAuxJugar, btnAuxScores, btnAuxExit, btnAuxCreditos,btnAuxMenu;
        Texture2D br1, br2, br3, br4, br5, natureLeftTop2, btnMenu, btnMenuMouse;
        Texture2D ImagenManoDerecha, top5, logoSnake, flechaUpAux, flechaUpMouse, flechaDownAux, flechaDownMouse; //JEF 
        List<Texture2D> lstManoCargando; //JEF
        Texture2D flechaUp, flechaDown, Rh, Lh, RhS, LhS, RhA, LhA, comida;
        Texture2D flechaUp2, flechaDown2;

        /// <summary>
        /// Definimos los vectores para los objetos que vamos a utilizar en el juego
        /// y tambien la Lista de vectores para el Snake. Los vectores 2D es donde se van a guardar
        /// las posiciones de cada objeto
        /// </summary>
        List<Vector2> snakePosition;
        Vector2 food, vcJugar, vcScores, vcExit;
        Vector2 vcSplash, vcCreditos,menu,vcMenu;
        Vector2 vcAumentarLetra1, vcAumentarLetra2, vcDisminuirLetra1, vcDisminuirLetra2, vcGuardarPuntaje, vcLetra1, vcLetra2 ; //JEF

        /// <summary>
        /// Definimos el random para generar posiciones aleatorias para la comida
        /// </summary>
        Random rPosition = new Random();

        /// <summary>
        /// Definimos los rectangulos para poder generar las intersecciones del evento del kinect
        /// </summary>
        Rectangle rcJugar, rcScores, rcExit, rcCreditos,rcMenu, rcMenu2;
        Rectangle rcAumentarLetra1, rcAumentarLetra2, rcDisminuirLetra1, rcDisminuirLetra2, rcGuardarPuntaje; //JEF

        /// <summary>
        /// Variables a utilizar en el juego siendo Direccion:
        /// 0 = arriba, 1 = abajo, 2 = izquierda y 3 = derecha
        ///puntaje, ultima direccion y el tiempo
        ///bSize es un valor generico para los tiles que manejamos que son de 50x50 pixeles
        ///sSize es el size de la culebrita y las variables de ajuste son para pintar el cuerpo 
        ///del jugador
        /// </summary>
        int direccion = 3, ltd = -1, lastT = -1, puntaje, bSize = 50,sSize=49, ajuste = 910, ajusteV = 220;
        /// <summary>
        /// Estas variables son necesarias para la animacion de la mano cuando se poscisiona sobre un boton en 
        /// la pantalla del juego 
        /// </summary>
        int frames = 0, fps = 24, increase = 0, control = 0;//JEF
        int velocidad = 450;
        /// <summary>
        /// Generamos una variable que toma el estado del mouse para generar las intersecciones
        /// creo que ya no se utiliza en la version del kinect
        /// </summary>
        MouseState raton;
        /// <summary>
        /// Variable para determinar si el juego termina
        /// </summary>
        bool gameOver=false;
        /// <summary>
        /// Definimos variables donde guardaremos los sonidos que hemos de usar para el juego 
        /// tanto como la parte de comer, como los bricks, como el splash y la musica de fondo
        /// </summary>
        SoundEffect audioSplash;
        SoundEffectInstance SplashAudio;
        SoundEffect[] sonidos;
        SoundEffectInstance[] efectosSonidos;
        Song fondo;
        /// <summary>
        /// Declaramos un objeto de tipo Kinect para leer su informacion
        /// </summary>
        KinectSensor kinect;
        /// <summary>
        /// Definimos los rectangulos del skeleton para seguirlo y poder dibujarlo 
        /// </summary>
        Rectangle rectanguloCabeza, rectanguloHombroDerecho
            , rectanguloHombroIzquierdo, rectanguloCodoIzquierdo, rectanguloCodoDerecho, rectanguloRodillaDerecho, rectanguloRodillaIzquierdo
            , rectangulopPieDerecho, rectanguloPieIzquierdo, rectanguloTorso, rectanguloManoIzquierda, rectanguloHombroCentro, rectanguloCursor;
        /// <summary>
        /// Spritesfots y puntos de las articulaciones del cuerpo del jugador tomados del kinect
        /// </summary>
        /// 
        ColorImagePoint puntoManoDerecha; //JEF
        ColorImagePoint puntoManoIzquierda; //JEF
        ColorImagePoint puntoTorso; //JEF
        ColorImagePoint puntoHombroDerecho;
        ColorImagePoint puntoHombroIzquierdo;
        ColorImagePoint puntoCabeza;
        ColorImagePoint puntoCodoIzquierdo;
        ColorImagePoint puntoCodoDerecho;
        ColorImagePoint puntoRodillaDerecho;
        ColorImagePoint puntoRodillaIzquierdo;
        ColorImagePoint puntoPieDerecho;
        ColorImagePoint puntoPieIzquierdo;
        ColorImagePoint puntoHombroCentro;

        /// <summary>
        /// Variables usadas para guardar la profundidad de algunos puntos del cuerpo necesarios
        /// </summary>
        float ZDerecha, ZIzquierda, ZTorso; //JEF

        /// <summary>
        /// Variables usadas para guardar el nombre del usuario
        /// </summary>
        char letra1, letra2;
        string nombreUsuario="";
        /// <summary>
        /// Variable de texto para la impresion de letras en la pantalla
        /// </summary>
        SpriteFont texto, textoAlerta;
        SpriteFont mayan, Title;
        SpriteFont mayanBig;
        /// <summary>
        /// este struct ya no se necesito porque usamos una bd
        /// </summary>
        public struct puntos
        {
             public int puntuacion;
             public string name;
        }
        puntos[] topFive = new puntos[5];
        
        /// <summary>
        /// Definimos un numero de estados que puede tomar nuestro GameState que serian 
        /// nuestras ventanas e inicializamos en SplashScreen
        /// </summary>
        enum GameState
        {
            SplashScreen,
            Lobyy,
            SelectHand,
            Game,
            Scores,
            SaveScores,
            Credits
        }
        
        GameState pantallas = GameState.SplashScreen;

        /// <summary>
        /// Nuestro constructor del juego ejecuta en estos graficos el juego y 
        /// define la carpeta de contenido de donde se tomaran los recursos del juego
        /// Tambien se definen las dimensiones de la ventana dadas en Pixeles de 1500x750 pixeles
        /// </summary>
        public Game1()
        {
            /// <summary>
            /// Este codigo hace cosas muy simples, como generar la ventana de graficos
            /// ponerle nombre al juego y definir el nombre del Content usado para las imagenes
            /// algunas cosas se hacen por defecto
            /// </summary>
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Kukulcan Game";
            graphics.PreferredBackBufferHeight = 750;
            graphics.PreferredBackBufferWidth = 1500;
            /// <summary>
            /// tomamos valores para las letras del nombre del jugador
            /// 
            /// Tambien posicionamos el juego en el centro de la pantalla
            /// </summary>
            letra1 = (char)65;
            letra2 =(char)65;

            Window.Position = new Point(
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (graphics.PreferredBackBufferWidth / 2),
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (graphics.PreferredBackBufferHeight / 2));
            // Aqui iniciamos la lista de imagenes que tomara el cursor cuando haga un mouseOver
            lstManoCargando = new List<Texture2D>();
        }

        /// <summary>
        /// Iniciamos la lectura del kinect en este metodo
        /// </summary>
        protected override void Initialize()
        {
            leerKinect();//JEF

            base.Initialize();
        }

        /// <summary>
        /// Cargamos el contenido de cada textura con los graficos guardados en el content y sonidos
        /// </summary>
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            /// <summary>
            /// Realizamos la carga de contenidos que estan en el content para asignarselos a las variables definidas
            /// en la parte superior del codigo 
            /// </summary>
            #region Texturas
            genericTile = Content.Load<Texture2D>("Tile");
            comida = Content.Load<Texture2D>("elote");
            bg = Content.Load<Texture2D>("Bg");
            Rh = Content.Load<Texture2D>("Rh");
            RhA = Rh;
            RhS = Content.Load<Texture2D>("RhSelect");
            Lh = Content.Load<Texture2D>("Lh");
            LhA = Lh;
            Title = Content.Load<SpriteFont>("Titulo");
            LhS = Content.Load<Texture2D>("LhSelect");
            logoSnake = Content.Load<Texture2D>("logo");
            head = Content.Load<Texture2D>("HeadR");
            R = Content.Load<Texture2D>("HeadR");
            L = Content.Load<Texture2D>("HeadL");
            D = Content.Load<Texture2D>("HeadD");
            U = Content.Load<Texture2D>("HeadU");
            tile = Content.Load<Texture2D>("TileSnake");
            btJugar = Content.Load<Texture2D>("btnJugar");
            btnMenu = Content.Load<Texture2D>("btnMenu");
            btnMenuMouse = Content.Load<Texture2D>("btnMenuMouse");
            btnAuxMenu = Content.Load<Texture2D>("btnMenu");
            btnAuxJugar = Content.Load<Texture2D>("btnJugar");
            btnJugarMouse = Content.Load<Texture2D>("btnJugarMouse");
            btScores = Content.Load<Texture2D>("btnPuntajes");
            btnAuxScores = Content.Load<Texture2D>("btnPuntajes");
            btnScoresMouse = Content.Load<Texture2D>("btnPuntajesMouse");
            btExit = Content.Load<Texture2D>("btnSalir");
            btnAuxExit = Content.Load<Texture2D>("btnSalir");
            btnExitMouse = Content.Load<Texture2D>("btnSalirMouse");
            brick = Content.Load<Texture2D>("brick");
            natureLeftBot = Content.Load<Texture2D>("planLeftbot");
            natureLeftTop = Content.Load<Texture2D>("planLeftTop");
            natureRightTop = Content.Load<Texture2D>("planRightTop");
            natureLeftTop2 = Content.Load<Texture2D>("planRightLeft2");
            splashScrren = Content.Load<Texture2D>("imagenSplash");
            logoMonkey = Content.Load<Texture2D>("MonkeyLogo");
            creditos = Content.Load<Texture2D>("btnCreditos");
            btnAuxCreditos = Content.Load<Texture2D>("btnCreditos");
            btnCreditosMosue = Content.Load<Texture2D>("btnCreditosMouse");
            backGroundMenu = Content.Load<Texture2D>("backGroundMenu");
            br1 = Content.Load<Texture2D>("brick1");
            br2 = Content.Load<Texture2D>("brick2");
            br3 = Content.Load<Texture2D>("brick3");
            br4 = Content.Load<Texture2D>("brick4");
            br5 = Content.Load<Texture2D>("brick5");
            mayan = Content.Load<SpriteFont>("Mayan");
            mayanBig = Content.Load<SpriteFont>("MayanBig");
            top5 = Content.Load<Texture2D>("top5");
            flechaUp = Content.Load<Texture2D>("brickTop");
            flechaDown = Content.Load<Texture2D>("brickBot");
            flechaDownMouse = Content.Load<Texture2D>("brickBotMouse");
            flechaUpMouse = Content.Load<Texture2D>("brickTopMouse");
            flechaDownAux = flechaDown;
            flechaUpAux = flechaUp;
            flechaDown2 = flechaDown;
            flechaUp2 = flechaUp;

            audioSplash = Content.Load<SoundEffect>("sonido");
            SplashAudio = audioSplash.CreateInstance();
            sonidos= new SoundEffect[3];
            sonidos[0] = Content.Load<SoundEffect>("boton");
            sonidos[1] = Content.Load<SoundEffect>("comida");
            sonidos[2] = Content.Load<SoundEffect>("explosion");

            efectosSonidos = new SoundEffectInstance[3];
            efectosSonidos[0] = sonidos[0].CreateInstance();
            efectosSonidos[1] = sonidos[1].CreateInstance();
            efectosSonidos[2] = sonidos[2].CreateInstance();


            ImagenManoDerecha = base.Content.Load<Texture2D>("handRight");//JEF

            for (int i = 1; i < 6; i++)
            {//JEF
                lstManoCargando.Add(base.Content.Load<Texture2D>("manoLoad" + i));//JEF
            }//JEF

         

            texto = base.Content.Load<SpriteFont>("File");//JEF
            textoAlerta = base.Content.Load<SpriteFont>("FontAlert");//JEF
                                                                     //JEF

            #endregion
            /// <summary>
            /// Definimos los rectangulos de algunos botones que usaremos en las pantallas del juego
            /// al igual que los sonidos y musica de fondo 
            /// </summary>
            vcSplash = new Vector2(365,20);
            vcJugar = new Vector2(215, 390);
            vcScores = new Vector2(615,390);
            vcExit = new Vector2(1015,390);
            vcCreditos = new Vector2(1100, 650);
            vcMenu = new Vector2(605,630);
            menu = new Vector2(0,0);

            vcAumentarLetra1 = new Vector2(550, 75); //JEF
            vcAumentarLetra2 = new Vector2(850, 75); //JEF
            vcDisminuirLetra1 = new Vector2(550, 425); //JEF
            vcDisminuirLetra2 = new Vector2(850, 425); //JEF
            vcLetra1 = new Vector2(550, 250);
            vcLetra2 = new Vector2(850, 250);
            vcGuardarPuntaje = new Vector2(600, 650); //JEF

            fondo = Content.Load<Song>("fondoCancion");
            MediaPlayer.Play(fondo);
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.IsRepeating = true;

        }

        /// <summary>
        /// En este metodo generamos la lectura del kinect Mediante un manejo de exepciones 
        /// PD: Borra todo lo que diga Jeff y lo que no funcione ya en el codigo, por favor,
        /// por ejemplo, lo del struct y sus metodos y variables, eso no lo utilizamos c:
        /// </summary>
        private void leerKinect()
        {//JEF
            try//JEF
            { // TODO: Add your initialization logic here
                kinect = KinectSensor.KinectSensors.FirstOrDefault();//JEF

                kinect.SkeletonStream.Enable();//JEF
                kinect.Start();//JEF
                kinect.SkeletonFrameReady += Kinect_SkeletonFrameReady;//JEF
            }
            catch (Exception ex)//JEF
            {
                System.Windows.MessageBox.Show("Fallo al iniciar kinect" + ex.Message, "Visor Kinect");//JEF
                leerKinect();//JEF
            }//JEF

        }

        /// <summary>
        /// En este metodo se realiza el trackeo de las articulaciones del jugador
        /// definiendo los puntos declarados en la parte de arriba del codigo
        /// </summary>
        private void Kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)//JEF
        {//JEF
            
            Skeleton[] esqueletos = null;//JEF

            using (SkeletonFrame frameEsqueleto = e.OpenSkeletonFrame())//JEF
            {
                if (frameEsqueleto != null)//JEF
                {//JEF
                    esqueletos = new Skeleton[frameEsqueleto.SkeletonArrayLength];//JEF
                    frameEsqueleto.CopySkeletonDataTo(esqueletos);//JEF
                }//JEF
            }//JEF

            if (esqueletos == null)//JEF
            {//JEF
                return;//JEF
            }//JEF
            foreach (Skeleton esqueleto in esqueletos)//JEF
            {//JEF
                if (esqueleto.TrackingState == SkeletonTrackingState.Tracked)//JEF
                {//JEF
                 /// <summary>
                 /// Aqui solo estan trackeando todas las articulaciones del cuerpo del jugador
                 /// </summary>
                    Joint handJointRight = esqueleto.Joints[JointType.HandRight];//JEF
                    Joint handJointLeft = esqueleto.Joints[JointType.HandLeft];//JEF
                    Joint Torso = esqueleto.Joints[JointType.Spine];
                    Joint hombroDerecho = esqueleto.Joints[JointType.ShoulderRight];
                    Joint hombroIzquierdo = esqueleto.Joints[JointType.ShoulderLeft];
                    Joint cabeza = esqueleto.Joints[JointType.Head];
                    Joint codoIzquierdo = esqueleto.Joints[JointType.ElbowLeft];
                    Joint codoDerecho = esqueleto.Joints[JointType.ElbowRight];
                    Joint rodillaIzquierdo = esqueleto.Joints[JointType.KneeLeft];
                    Joint rodillaDerecho = esqueleto.Joints[JointType.KneeRight];
                    Joint pieIzquierdo = esqueleto.Joints[JointType.FootLeft];
                    Joint pieDerecho = esqueleto.Joints[JointType.FootRight];
                    Joint hombroCentro = esqueleto.Joints[JointType.ShoulderCenter];
                    /// <summary>
                    /// Esta seccion toma los valores de cada articulacion y se las asigna
                    /// a cada ColorImagePointer declarado en la seccion superior
                    /// </summary>
                    ZDerecha = handJointRight.Position.Z;
                    ZIzquierda = handJointLeft.Position.Z;
                    ZTorso = Torso.Position.Z;
                    puntoManoDerecha = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(handJointRight.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoManoIzquierda = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(handJointLeft.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoHombroDerecho = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(hombroDerecho.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoHombroIzquierdo = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(hombroIzquierdo.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoCabeza = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(cabeza.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoCodoDerecho = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(codoDerecho.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoCodoIzquierdo = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(codoIzquierdo.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoRodillaDerecho = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(rodillaDerecho.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoRodillaIzquierdo = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(rodillaIzquierdo.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoPieDerecho = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(pieDerecho.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoPieIzquierdo = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(pieIzquierdo.Position, ColorImageFormat.RgbResolution640x480Fps30);
                    puntoTorso = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(Torso.Position, ColorImageFormat.RawBayerResolution640x480Fps30);
                    puntoHombroCentro = kinect.CoordinateMapper.MapSkeletonPointToColorPoint(hombroCentro.Position, ColorImageFormat.RgbResolution640x480Fps30);

                }
            }
            /// <summary>
            /// En esta seccion lo que hace es definir el size del cursor posicionado en pantalla 
            /// con la mano, ya que si no, su movimiento seria muy limitado
            /// </summary>
            if (pantallas!=GameState.Game)
            {
                if (puntoManoDerecha.X < 200) { puntoManoDerecha.X = 200; }
                else
                {
                    if (puntoManoDerecha.X > 500) { puntoManoDerecha.X = 500; }
                }
                puntoManoDerecha.X = (5 * (puntoManoDerecha.X)) - 1000;
                if (puntoManoDerecha.Y < 30) { puntoManoDerecha.Y = 30; }
                else
                {
                    if (puntoManoDerecha.Y > 380) { puntoManoDerecha.Y = 380; }
                }
                puntoManoDerecha.Y = ((15 * puntoManoDerecha.Y) - 450) / 7;
            }


        }
       /// <summary>
       /// Este metodo termina la ejecucion del kinect
       /// </summary>
        protected override void EndRun()//JEF
        {//JEF

            kinect.Stop();//JEF
        }
        /// <summary>
        ///  Definimos las coordenadas de los rectangulos del cuerpo del skeleton para dibujarlo en la seccion del juego 
        /// </summary>
        public void rectangulosSkeleton()
        {
            #region RecSkeleton
            rectanguloCursor = new Rectangle((int)puntoManoDerecha.X, (int)puntoManoDerecha.Y, 60, 60);//JEF
            rectanguloCabeza = new Rectangle((int)puntoCabeza.X, (int)puntoCabeza.Y, 60, 60);
            rectanguloCodoDerecho = new Rectangle((int)puntoCodoDerecho.X, (int)puntoCodoDerecho.Y, 60, 60);
            rectanguloCodoIzquierdo = new Rectangle((int)puntoCodoIzquierdo.X, (int)puntoCodoIzquierdo.Y, 60, 60);
            rectanguloHombroDerecho = new Rectangle((int)puntoHombroDerecho.X, (int)puntoHombroDerecho.Y, 60, 60);
            rectanguloHombroIzquierdo = new Rectangle((int)puntoHombroIzquierdo.X, (int)puntoHombroIzquierdo.Y, 60, 60);
            rectanguloManoIzquierda = new Rectangle((int)puntoManoIzquierda.X, (int)puntoManoIzquierda.Y, 60, 60);
            rectangulopPieDerecho = new Rectangle((int)puntoPieDerecho.X, (int)puntoPieDerecho.Y, 60, 60);
            rectanguloPieIzquierdo = new Rectangle((int)puntoPieIzquierdo.X, (int)puntoPieIzquierdo.Y, 60, 60);
            rectanguloRodillaDerecho = new Rectangle((int)puntoRodillaDerecho.X, (int)puntoRodillaDerecho.Y, 60, 60);
            rectanguloRodillaIzquierdo = new Rectangle((int)puntoRodillaIzquierdo.X, (int)puntoRodillaIzquierdo.Y, 60, 60);
            rectanguloTorso = new Rectangle((int)puntoTorso.X, (int)puntoTorso.Y, 60, 60);
            rectanguloHombroCentro = new Rectangle((int)puntoHombroCentro.X, (int)puntoHombroCentro.Y, 60, 60);
            #endregion
        }
        ///<summary>
        /// Controlamos el juego del snake mediante los movimientos definidos arriba,abajo,izquierda y derecha
        /// </summary>
        public void controlKinect()
        {
            if (control == 0) { 
            if (puntoManoDerecha.Y < 77 && ltd != 1)
                direccion = 0;
            else
            if (puntoManoDerecha.Y >= 340 && ltd != 0)
                direccion = 1;
            else
            if (puntoManoDerecha.X <= 156 && ltd != 3)
                direccion = 2;
            else
            if (puntoManoDerecha.X >= 434 && ltd != 2)
                direccion = 3;
            }
            else if (control == 1)
            {
                if (puntoManoIzquierda.Y < 77 && ltd != 1)
                    direccion = 0;
                else
            if (puntoManoIzquierda.Y >= 340 && ltd != 0)
                    direccion = 1;
                else
            if (puntoManoIzquierda.X <= 156 && ltd != 3)
                    direccion = 2;
                else
            if (puntoManoIzquierda.X >= 434 && ltd != 2)
                    direccion = 3;
            }
        }
        /// <summary>
        /// Inicializamos el juego cargando la lista de vectores para nuestro snake, le asignamos una direccion
        /// y vamos moviendo esa lista de vectores, creamos la comida e iniciamos el puntaje en 0
        /// </summary>
        public void startGame()
        {
            snakePosition = new List<Vector2>();
            direccion = 3;
            ltd = 3;
            for (int i = 0; i < 5; i++)
            { snakePosition.Add(new Vector2(9 - i, 12)); }
            resetComida();
            puntaje = 0;
        }
        /// <summary>
        /// Este metodo reinicia la posicion de la comida en la pantalla
        /// </summary>
        public void resetComida()
        {
            food = new Vector2(
                rPosition.Next(1, (900 - 1) / bSize),
                rPosition.Next(1, (650 - 1) / bSize));
        }
        /// <summary>
        /// Este metodo recibe como @parametro gameTime que es el tiempo de ejecucion del juego, es necesario
        /// para validar los movimientos del juego y controlar las posciones
        /// </summary>
        /// <param name="gameTime"></param>
        public void gameSnake(GameTime gameTime)
        {
            /// <summary>
            /// Validamos si la comida aparece en el cuerpo del snake, si aparece dentro, la 
            /// reseteamos y aparecemos la comida en otro lugar
            /// </summary>
            for (int i = 1; i < snakePosition.Count; i++)
            {
                if (snakePosition[i].X == food.X && snakePosition[i].Y == food.Y)
                {
                    resetComida();
                }
            }
            ///Llamamos a la funcion que controla el snake mediante el kinect
            ///tambien tenemos la del teclaro, pero no la usamos con el kinect
            controlKinect();
            controlTeclado();
            /// <summary>
            /// Si apretamos ESC salimos automaticamente del juego
            /// </summary>
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ///Aqui determinamos mediante el Bool si el juego termino, si si nos llevara a la ventana donde
            ///el usuario debe guardad su puntaje, si no, el juego continua
            if (gameOver) {
                gameOver = false;
                pantallas = GameState.SaveScores;
            }
            /// <summary>
            /// En esta seccion del metodo se controla la velocidad del juego, tomamos la velocidad del gameTime
            /// y la distribuimos con los frames para que el snake vaya un poco mas lento, pero por cada 
            /// 5 elititos el snake va mas rapido, tambien se determina la direccion del snake 
            /// mediante los movimientos del jugador
            /// </summary>
            if (gameTime.TotalGameTime.TotalMilliseconds > lastT + velocidad)
            {
                ltd = direccion;
                for (int i = snakePosition.Count - 1; i > 0; i--)
                {
                    snakePosition[i] = snakePosition[i - 1];
                }
                switch (direccion)
                {
                    case 0:
                        head = U;
                        snakePosition[0] += new Vector2(0, -1);
                        break;
                    case 1:
                        head = D;
                        snakePosition[0] += new Vector2(0, 1);
                        break;
                    case 2:
                        head = L;
                        snakePosition[0] += new Vector2(-1, 0);
                        break;
                    case 3:
                        head = R;
                        snakePosition[0] += new Vector2(1, 0);
                        break;
                }
                /// <summary>
                /// Agreamos un nuevo cuadrito al snake al determinar si hay una colision entre la comida y la cabeza del snake
                /// incrementamos el puntaje, agregamos un sonido y validamos el puntaje para ver si ira mas 
                /// rapido el snake 
                /// </summary>
                if (snakePosition[0].X == food.X && snakePosition[0].Y == food.Y)
                {
                    snakePosition.Add(snakePosition[snakePosition.Count - 1]);
                    resetComida();
                    efectosSonidos[1].Play();
                    
                    puntaje=puntaje+10;

                    if (puntaje==50|| puntaje == 100 || puntaje == 150)
                    {
                        velocidad -= 100;
                    }

                }
                /// <summary>
                /// Validamos la colision del snake con su propio cuerpo, si choca la cabeza con alguna parte de su cuerpo
                /// el juego habra terminado y cargamos las letras de la siguiente pantalla
                /// </summary>
                for (int i = 1; i < snakePosition.Count; i++)
                {
                    if (snakePosition[0].X == snakePosition[i].X && snakePosition[0].Y == snakePosition[i].Y)
                    {
                        gameOver = true;
                        efectosSonidos[2].Play();
                        letra1 = (char)65;
                        letra2 = (char)65;
                    }
                }
                /// <summary>
                /// Si el snake choca con las paredes, pierde y cargamos las letras de la siguiente pantalla
                /// </summary>
                if (snakePosition[0].X < 1 || snakePosition[0].Y < 1 ||
                    snakePosition[0].Y  > (650/ bSize) ||
                    snakePosition[0].X  > (900 / bSize))
                {
                    gameOver = true;
                    efectosSonidos[2].Play();
                    letra1 = (char)65;
                    letra2 = (char)65;
                }
                lastT = (int)gameTime.TotalGameTime.TotalMilliseconds;
            }

        }
        /// <summary>
        /// Este metodo sirve para controlar el juego con el teclado 
        /// </summary>
        public void controlTeclado()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && ltd != 1)
                direccion = 0;
            else
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && ltd != 0)
                direccion = 1;
            else
                if (Keyboard.GetState().IsKeyDown(Keys.Left) && ltd != 3)
                direccion = 2;
            else
                   if (Keyboard.GetState().IsKeyDown(Keys.Right) && ltd != 2)
                direccion = 3;
        }
        /// <summary>
        /// Este metodo de spriteBatches nos sirve para pintar los tiles del snake por cada posicion que tenga del vector
        /// pinta la cabeza, el cuerpo y la comida en el campo
        /// </summary>
        public void drawSnake()
        {
            int cont = 0;
            foreach (Vector2 posicion in snakePosition)
            {
                if (cont != 0)
                {
                    spriteBatch.Draw(tile, new Rectangle((int)posicion.X * 50, (int)posicion.Y * 50, sSize, sSize), Color.White);
                }
                cont++;
            }
            spriteBatch.Draw(head, new Rectangle((int)snakePosition[0].X * 50, (int)snakePosition[0].Y * 50, sSize, sSize), Color.White);
            spriteBatch.Draw(tile, new Rectangle((int)snakePosition[snakePosition.Count - 1].X * 50, (int)snakePosition[snakePosition.Count - 1].Y * 50, sSize, sSize), Color.White);
            spriteBatch.Draw(comida, new Rectangle((int)food.X * 50, (int)food.Y * 50, sSize, sSize), Color.White);
        }
        /// <summary>
        /// Este metodo nos dibuja a la persona en el cuadro de abajo del juego
        /// </summary>
        public void drawSkeletonTracking()
        {
            /// <summary>
            /// Esta seccion solamente dibuja el mapa donde aparece la persona y determina el color 
            /// que debe tomar dependiendo de la direccion que tenga el snake
            /// </summary>
            #region MapaPersona
            if (direccion == 0)
            {
                spriteBatch.Draw(genericTile, new Rectangle(1100, 300, 250, 4), Color.Blue);  // mano <300 arriba
                spriteBatch.Draw(genericTile, new Rectangle(1100, 300, 4, 275), Color.Green); // mano < 1100 izquierda
                spriteBatch.Draw(genericTile, new Rectangle(1100, 575, 250, 4), Color.Red); // mano >575 abajo 
                spriteBatch.Draw(genericTile, new Rectangle(1350, 300, 4, 275), Color.Green); //mano > 1350 derecha
            }
            if (direccion == 1)
            {
                spriteBatch.Draw(genericTile, new Rectangle(1100, 300, 250, 4), Color.Red);  // mano <300 arriba
                spriteBatch.Draw(genericTile, new Rectangle(1100, 300, 4, 275), Color.Green); // mano < 1100 izquierda
                spriteBatch.Draw(genericTile, new Rectangle(1100, 575, 250, 4), Color.Blue); // mano >575 abajo 
                spriteBatch.Draw(genericTile, new Rectangle(1350, 300, 4, 275), Color.Green); //mano > 1350 derecha
            }
            if (direccion == 2)
            {
                spriteBatch.Draw(genericTile, new Rectangle(1100, 300, 250, 4), Color.Green);  // mano <300 arriba
                spriteBatch.Draw(genericTile, new Rectangle(1100, 300, 4, 275), Color.Blue); // mano < 1100 izquierda
                spriteBatch.Draw(genericTile, new Rectangle(1100, 575, 250, 4), Color.Green); // mano >575 abajo 
                spriteBatch.Draw(genericTile, new Rectangle(1350, 300, 4, 275), Color.Red); //mano > 1350 derecha
            }
            if (direccion == 3)
            {
                spriteBatch.Draw(genericTile, new Rectangle(1100, 300, 250, 4), Color.Green);  // mano <300 arriba
                spriteBatch.Draw(genericTile, new Rectangle(1100, 300, 4, 275), Color.Red); // mano < 1100 izquierda
                spriteBatch.Draw(genericTile, new Rectangle(1100, 575, 250, 4), Color.Green); // mano >575 abajo 
                spriteBatch.Draw(genericTile, new Rectangle(1350, 300, 4, 275), Color.Blue); //mano > 1350 derecha
            }

            #endregion
            /// <summary>
            /// Aqui dibujamos el cuerpo de la persona desde sus articulaciones hasta las lineas que 
            /// las unen, o sea, las lineas negritas que aparecen 
            /// </summary>
            #region Skeleton

            if (((puntoCabeza.X + ajuste) >= 1000 && (puntoCabeza.Y + ajusteV) <= 700))
            {
                spriteBatch.Draw(genericTile, new Rectangle(puntoCabeza.X + ajuste, puntoCabeza.Y + ajusteV, 20, 20), Color.Green);

                if (puntoManoDerecha.X < 1450)
                {
                    spriteBatch.Draw(genericTile, new Rectangle(puntoHombroDerecho.X + ajuste, puntoHombroDerecho.Y + ajusteV, 20, 20), Color.Green);
                    spriteBatch.Draw(genericTile, new Rectangle(puntoManoDerecha.X + ajuste, puntoManoDerecha.Y + ajusteV, 20, 20), Color.Green);

                    spriteBatch.Draw(genericTile, new Rectangle(puntoHombroCentro.X + ajuste, puntoHombroCentro.Y + ajusteV, 20, 20), Color.Green);
                    spriteBatch.Draw(genericTile, new Rectangle(puntoCodoDerecho.X + ajuste, puntoCodoDerecho.Y + ajusteV, 20, 20), Color.Green);
                    Primitives2D.DrawLine(spriteBatch, puntoHombroCentro.X + ajuste + 10, puntoHombroCentro.Y + ajusteV + 10, puntoHombroDerecho.X + 10 + ajuste, puntoHombroDerecho.Y + ajusteV, Color.Black, 5f);
                    Primitives2D.DrawLine(spriteBatch, puntoHombroDerecho.X + ajuste + 10, puntoHombroDerecho.Y + ajusteV + 10, puntoCodoDerecho.X + 10 + ajuste, puntoCodoDerecho.Y + ajusteV + 10, Color.Black, 5f);
                    Primitives2D.DrawLine(spriteBatch, puntoCodoDerecho.X + ajuste + 10, puntoCodoDerecho.Y + ajusteV + 10, puntoManoDerecha.X + 10 + ajuste, puntoManoDerecha.Y + ajusteV + 10, Color.Black, 5f);
                }
                else
                {

                }

                if (puntoManoIzquierda.X < 1000)
                {
                    spriteBatch.Draw(genericTile, new Rectangle(puntoManoIzquierda.X + ajuste, puntoManoIzquierda.Y + ajusteV, 20, 20), Color.Green);
                    spriteBatch.Draw(genericTile, new Rectangle(puntoHombroIzquierdo.X + ajuste, puntoHombroIzquierdo.Y + ajusteV, 20, 20), Color.Green);
                    spriteBatch.Draw(genericTile, new Rectangle(puntoHombroCentro.X + ajuste, puntoHombroCentro.Y + ajusteV, 20, 20), Color.Green);
                    spriteBatch.Draw(genericTile, new Rectangle(puntoCodoIzquierdo.X + ajuste, puntoCodoIzquierdo.Y + ajusteV, 20, 20), Color.Green);
                    Primitives2D.DrawLine(spriteBatch, puntoHombroCentro.X + ajuste + 10, puntoHombroCentro.Y + ajusteV + 10, puntoHombroIzquierdo.X + 10 + ajuste, puntoHombroIzquierdo.Y + ajusteV, Color.Black, 5f);
                    Primitives2D.DrawLine(spriteBatch, puntoHombroIzquierdo.X + ajuste + 10, puntoHombroIzquierdo.Y + ajusteV + 10, puntoCodoIzquierdo.X + 10 + ajuste, puntoCodoIzquierdo.Y + ajusteV + 10, Color.Black, 5f);
                    Primitives2D.DrawLine(spriteBatch, puntoCodoIzquierdo.X + ajuste + 10, puntoCodoIzquierdo.Y + ajusteV + 10, puntoManoIzquierda.X + 10 + ajuste, puntoManoIzquierda.Y + ajusteV + 10, Color.Black, 5f);
                }
                else
                {

                }

                spriteBatch.Draw(genericTile, new Rectangle(puntoPieDerecho.X + ajuste, puntoPieDerecho.Y + ajusteV, 20, 20), Color.Green);
                spriteBatch.Draw(genericTile, new Rectangle(puntoPieIzquierdo.X + ajuste, puntoPieIzquierdo.Y + ajusteV, 20, 20), Color.Green);
                spriteBatch.Draw(genericTile, new Rectangle(puntoRodillaDerecho.X + ajuste, puntoRodillaDerecho.Y + ajusteV, 20, 20), Color.Green);
                spriteBatch.Draw(genericTile, new Rectangle(puntoRodillaIzquierdo.X + ajuste, puntoRodillaIzquierdo.Y + ajusteV, 20, 20), Color.Green);
                spriteBatch.Draw(genericTile, new Rectangle(puntoTorso.X + ajuste, puntoTorso.Y + ajusteV, 20, 20), Color.Green);

                Primitives2D.DrawLine(spriteBatch, puntoCabeza.X + ajuste + 10, puntoCabeza.Y + ajusteV + 10, puntoHombroCentro.X + 10 + ajuste, puntoHombroCentro.Y + ajusteV, Color.Black, 5f);
                Primitives2D.DrawLine(spriteBatch, puntoTorso.X + ajuste + 10, puntoTorso.Y + ajusteV - 10, puntoRodillaDerecho.X + 10 + ajuste, puntoRodillaDerecho.Y + ajusteV, Color.Black, 5f);
                Primitives2D.DrawLine(spriteBatch, puntoTorso.X + ajuste + 10, puntoTorso.Y + ajusteV - 10, puntoRodillaIzquierdo.X + 10 + ajuste, puntoRodillaIzquierdo.Y + ajusteV, Color.Black, 5f);
                Primitives2D.DrawLine(spriteBatch, puntoRodillaIzquierdo.X + ajuste + 10, puntoRodillaIzquierdo.Y + ajusteV - 10, puntoPieIzquierdo.X + 10 + ajuste, puntoPieIzquierdo.Y + ajusteV, Color.Black, 5f);
                Primitives2D.DrawLine(spriteBatch, puntoRodillaDerecho.X + ajuste + 10, puntoRodillaDerecho.Y + ajusteV - 10, puntoPieDerecho.X + 10 + ajuste, puntoPieDerecho.Y + ajusteV, Color.Black, 5f);
                Primitives2D.DrawLine(spriteBatch, puntoHombroCentro.X + ajuste + 10, puntoHombroCentro.Y + ajusteV - 10, puntoTorso.X + 10 + ajuste, puntoTorso.Y + ajusteV, Color.Black, 5f);

            }
            #endregion
        }
        /// <summary>
        /// Este metodo verifica que no haya dos manos levantadas, ya que no puedes jugar con dos manos y recibe 
        /// como parametro el tiempo del juego
        /// </summary>
        /// <param name="gameTime"></param>
        public void verificarMano(GameTime gameTime)
        {
            
            bool manoArrriba = false;

            if(puntoManoDerecha.Y < puntoCabeza.Y && puntoManoIzquierda.Y < puntoCabeza.Y)
            {
                manosArriba = true; 
            }
            if(puntoManoDerecha.Y > puntoCabeza.Y && puntoManoIzquierda.Y > puntoCabeza.Y)
            {
                manosArriba = false;
            }
            if (puntoManoDerecha.Y < puntoCabeza.Y && manosArriba == false)
            {
                increase += gameTime.ElapsedGameTime.Milliseconds;

                Lh = LhS;
                manoArrriba = true;
                if (increase >= 24000 / fps)
                {
                    control = 0;
                    velocidad = 450;
                    startGame();

                    pantallas = GameState.Game;
                    increase = 0;
                }
            }

            if (puntoManoIzquierda.Y < puntoCabeza.Y && manosArriba == false)
            {
                increase += gameTime.ElapsedGameTime.Milliseconds;//JEF
                Rh = RhS;
                manoArrriba = true;
                if (increase >= 24000 / fps)//JEF
                {
                    control = 1;
                    velocidad = 450;
                    startGame();
                    pantallas = GameState.Game;
                    increase = 0;//JEF
                }

            }
            if (manoArrriba == false)
            {
                Rh = RhA;
                Lh = LhA;
                increase = 0;
            }
        }
        /// <summary>
        /// Esta funcion es el bucle para la logica del juego, en este apartado se ejectutan las acciones del juego
        /// actualmente lu usamos para definir los estados del proyecto que vamos a usar 
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if ( Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            /// <summary>
            /// Este switch nos sirve para determinar los estados del juego, aqui se desarrolla la logica del juego
            /// y es el loop que nos ayuda a mover los vectores y rectangulos del juego
            /// Todo el juego se desarrolla en este loop y el de draw
            /// Cada case es un estado del juego, en cada case esta la logica utilizada en cada seccion, se realizaron 
            /// metodos para volver menos grande esta seccion, anque se espera que se pueda optimizar y volver modular
            /// </summary>
            switch (pantallas)
            {
                case GameState.SplashScreen:
                    SplashAudio.Play();
                    if (gameTime.TotalGameTime.Seconds >= 3)
                    {
                        SplashAudio.Stop();
                        pantallas = GameState.Lobyy;
                    }
                    break;
                case GameState.Lobyy:
                    //IsMouseVisible = true;
                    bool interseccionBoton = false;
                    rcJugar = new Rectangle((int)vcJugar.X,(int)vcJugar.Y,300,97);
                    rcScores = new Rectangle((int)vcScores.X,(int)vcScores.Y,300,97);
                    rcExit = new Rectangle((int)vcExit.X, (int)vcExit.Y,300,97);
                    rcCreditos = new Rectangle((int)vcCreditos.X,(int)vcCreditos.Y,300,57);
                    rectanguloCursor = new Rectangle((int)puntoManoDerecha.X, (int)puntoManoDerecha.Y, 60, 60);//JEF

                    //Modificado
                    if (rcJugar.Intersects(rectanguloCursor))
                    {
                        btJugar = btnJugarMouse;
                        interseccionBoton = true;
                        increase += gameTime.ElapsedGameTime.Milliseconds;//JEF
                        if (increase >= 10000 / fps)//JEF
                        {
                            if (frames >= lstManoCargando.Count - 1)
                            {//JEF
                                    pantallas = GameState.SelectHand;
                                efectosSonidos[0].Play();
                                frames = 0;//JEF
                            }
                            else
                            {//JEF
                                frames++;//JEF
                            }//JEF
                            increase = 0;//JEF
                        }
                    }

                    if (rcScores.Intersects(rectanguloCursor))
                    {
                        btScores = btnScoresMouse;
                        interseccionBoton = true;
                        increase += gameTime.ElapsedGameTime.Milliseconds;//JEF




                        if (increase >= 10000 / fps)//JEF
                        {
                            if (frames >= lstManoCargando.Count - 1)
                            {//JEF


                                pantallas = GameState.Scores;
                                efectosSonidos[0].Play();
                                frames = 0;//JEF


                                try
                                {
                                    lstPuntajes = new List<string>();
                                    lstPuntajes = BDcomun.cargarPuntajes();
                                    nombreUsuario = "";
                                }
                                catch (Exception ex)
                                {

                                    nombreUsuario = ex.Message;

                                }


                            }
                            else
                            {//JEF
                                frames++;//JEF
                            }//JEF
                            increase = 0;//JEF
                        }
                    }

                    if (rcExit.Intersects(rectanguloCursor))
                    {
                        btExit = btnExitMouse;
                        interseccionBoton = true;
                        increase += gameTime.ElapsedGameTime.Milliseconds;//JEF
                        if (increase >= 10000 / fps)//JEF
                        {
                            if (frames >= lstManoCargando.Count - 1)
                            {//JEF
                                MediaPlayer.Stop();
                                Exit();
                                frames = 0;//JEF
                            }
                            else
                            {//JEF
                                frames++;//JEF
                            }//JEF
                            increase = 0;//JEF
                        }
                    }

                    if (rcCreditos.Intersects(rectanguloCursor))
                    {
                        creditos = btnCreditosMosue;
                        interseccionBoton = true;
                        increase += gameTime.ElapsedGameTime.Milliseconds;//JEF
                        if (increase >= 10000 / fps)//JEF
                        {
                            if (frames >= lstManoCargando.Count - 1)
                            {//JEF
                                pantallas = GameState.Credits;
                                efectosSonidos[0].Play();
                                frames = 0;//JEF
                            }
                            else
                            {//JEF
                                frames++;//JEF
                            }//JEF
                            increase = 0;//JEF
                        }
                    }

                    if (interseccionBoton == false)
                    {
                        btJugar = btnAuxJugar;
                        btScores = btnAuxScores;
                        btExit = btnAuxExit;
                        creditos = btnAuxCreditos;
                        frames = 0;
                        increase = 0;
                    }
                    break;
                case GameState.SelectHand:
                    IsMouseVisible = !true;
                    verificarMano(gameTime);

                    break;
                case GameState.Game:
                    IsMouseVisible = !true;
                    gameSnake(gameTime);
                    break;
                case GameState.Credits:

                    //IsMouseVisible = true;
                    bool interseccionBoton2 = false;
                    //raton = Mouse.GetState();
                    rcMenu = new Rectangle((int)vcMenu.X,(int)vcMenu.Y,300,97);
                    //Rectangle mouse2 = new Rectangle((int)raton.X, (int)raton.Y, 3, 3);
                    rectanguloCursor = new Rectangle((int)puntoManoDerecha.X, (int)puntoManoDerecha.Y, 60, 60);//JEF

                    if (rcMenu.Intersects(rectanguloCursor))
                    {
                        btnMenu = btnMenuMouse;
                        interseccionBoton2 = true;
                        increase += gameTime.ElapsedGameTime.Milliseconds;//JEF
                        if (increase >= 10000 / fps)//JEF
                        {
                            if (frames >= lstManoCargando.Count - 1)
                            {//JEF
                                pantallas = GameState.Lobyy;
                                efectosSonidos[0].Play();
                                frames = 0;//JEF
                            }
                            else
                            {//JEF
                                frames++;//JEF
                            }//JEF
                            increase = 0;//JEF
                        }
                    }
                    if (interseccionBoton2 == false)
                    {
                        btnMenu = btnAuxMenu;
                        frames = 0;
                        increase = 0;
                    }

                    break;
                case GameState.Scores:
                    interseccionBoton2 = false;
                    mostrarPuntajes();
                 
                    raton = Mouse.GetState();
                    rcMenu2 = new Rectangle((int)vcMenu.X, (int)vcMenu.Y, 300, 97);
                    rectanguloCursor = new Rectangle((int)puntoManoDerecha.X, (int)puntoManoDerecha.Y, 60, 60);//JEF

                    if (rcMenu2.Intersects(rectanguloCursor))
                    {
                        
                        btnMenu = btnMenuMouse;
                        interseccionBoton2 = true;

                        increase += gameTime.ElapsedGameTime.Milliseconds;//JEF

                        if (increase >= 10000 / fps)//JEF
                        {

                            if (frames >= lstManoCargando.Count - 1)
                            {//JEF
                                pantallas = GameState.Lobyy;
                                efectosSonidos[0].Play();
                                frames = 0;//JEF
                            }
                            else
                            {//JEF
                                frames++;//JEF
                            }//JEF
                            increase = 0;//JEF
                        }
                    }

                    if (interseccionBoton2 == false)
                    {
                        btnMenu = btnAuxMenu;
                        frames = 0;
                        increase = 0;
                    }
                    break;
                case GameState.SaveScores:
                    
                    rcAumentarLetra1 = new Rectangle((int)vcAumentarLetra1.X, (int)vcAumentarLetra1.Y, 100, 100); //JEF
                    rcAumentarLetra2 = new Rectangle((int)vcAumentarLetra2.X, (int)vcAumentarLetra2.Y, 100, 100); //JEF
                    rcDisminuirLetra1 = new Rectangle((int)vcDisminuirLetra1.X, (int)vcDisminuirLetra1.Y, 100, 100); //JEF
                    rcDisminuirLetra2 = new Rectangle((int)vcDisminuirLetra2.X, (int)vcDisminuirLetra2.Y, 100, 100); //JEF
                    rcGuardarPuntaje = new Rectangle((int)vcGuardarPuntaje.X, (int)vcGuardarPuntaje.Y, 300, 57); //JEF
                    rectanguloCursor = new Rectangle((int)puntoManoDerecha.X, (int)puntoManoDerecha.Y, 60, 60);//JEF

                    if (rcAumentarLetra1.Intersects(rectanguloCursor))
                    {
                        flechaUp = flechaUpMouse;
                        increase += gameTime.ElapsedGameTime.Milliseconds;//JEF
                        if (increase >= 20000 / fps){
                            if (letra1 == 90)
                            {
                                letra1 =(char) 65;
                            }
                            else
                            {
                                letra1++;
                            }
                            increase = 0;//JEF
                        }
                    }
                    else
                    {
                        flechaUp = flechaUpAux;
                        if (rcAumentarLetra2.Intersects(rectanguloCursor))
                        {
                            flechaUp2 = flechaUpMouse;
                            increase += gameTime.ElapsedGameTime.Milliseconds;//JEF
                            if (increase >= 20000 / fps)
                            {
                                if (letra2 == 90)
                                {
                                    letra2 = (char)65;
                                }
                                else
                                {
                                    letra2++;
                                }
                                 increase = 0;//JEF
                            }
                        }
                        else
                        {
                            flechaUp2 = flechaUpAux;
                            if (rcDisminuirLetra1.Intersects(rectanguloCursor))
                            {
                                flechaDown = flechaDownMouse;
                                increase += gameTime.ElapsedGameTime.Milliseconds;//JEF
                                if (increase >= 20000 / fps)
                                {
                                    if (letra1 == 65)
                                    {
                                        letra1 =(char) 90;
                                    }
                                    else {
                                        letra1--;
                                    }
                                    increase = 0;//JEF
                                }
                            }
                            else
                            {
                                flechaDown = flechaDownAux;
                                if (rcDisminuirLetra2.Intersects(rectanguloCursor))
                                {
                                    flechaDown2 = flechaDownMouse;
                                    increase += gameTime.ElapsedGameTime.Milliseconds;//JEF
                                    if (increase >= 20000 / fps)
                                    {
                                        if (letra2 == 65)
                                        {
                                            letra2 =(char) 90;
                                        }
                                        else
                                        {
                                            letra2--;
                                        }
                                        increase = 0;//JEF
                                    }
                                }
                                else
                                {
                                    flechaDown2 = flechaDownAux;
                                    if (rcGuardarPuntaje.Intersects(rectanguloCursor))
                                    {

                                        btScores = btnScoresMouse;
                                        increase += gameTime.ElapsedGameTime.Milliseconds;//JEF

                                        if (increase >= 20000 / fps)//JEF
                                        {

                                            if (frames >= lstManoCargando.Count - 1)
                                            {//JEF


                                                nombreUsuario = Convert.ToString(letra1) + Convert.ToString(letra2);

                                                BDcomun.guardarPuntaje(nombreUsuario, puntaje);

                                                efectosSonidos[0].Play();


                                                try
                                                {
                                                    lstPuntajes = new List<string>();
                                                    lstPuntajes = BDcomun.cargarPuntajes();
                                                    nombreUsuario = "";
                                                }
                                                catch (Exception ex)
                                                {

                                                    nombreUsuario = ex.Message;

                                                }

                                                pantallas = GameState.Scores;

                                                frames = 0;//JEF
                                            }
                                            else
                                            {//JEF
                                                frames++;//JEF
                                            }//JEF
                                            increase = 0;//JEF
                                        }
                                    }
                                    else {
                                        btScores = btnAuxScores;
                                        frames = 0;
                                        increase = 0;
                                    }

                                }
                            }
                        }
                    }
                    break;
            }


            base.Update(gameTime);
        }
        /// <summary>
        /// Este metodo dibuja los contornos de la pantalla en el state de juego y seleccion de mano
        /// cuando @dato == true dibuja la seccion del juego
        /// cuando @dato == false dibuja la seccion de seleccion de manos
        /// </summary>
        /// <param name="dato"></param>
        public void border(bool dato)
        {
            if (dato)
            {
                for (int i = 1; i < 14; i++)
                {
                    if (i % 2 == 0)
                    {
                        spriteBatch.Draw(br3, new Rectangle(1500 - (bSize * 11), i * bSize, bSize, bSize), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(brick, new Rectangle(1500 - (bSize * 11), i * bSize, bSize, bSize), Color.White);
                    }
                }
                for (int i = 20; i < 29; i++)
                {
                    if (i % 2 == 0)
                    {
                        spriteBatch.Draw(br3, new Rectangle(i * bSize, 0 + (bSize * 3), bSize, bSize), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(brick, new Rectangle(i * bSize, 0 + (bSize * 3), bSize, bSize), Color.White);
                    }
                }
            }
            else
            {
                for (int i =0; i<15; i++)
                {
                    if (i % 2 == 0)
                    {
                        spriteBatch.Draw(br3, new Rectangle(0 + (bSize * 15)-25, i * bSize, bSize, bSize), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(brick, new Rectangle(0 + (bSize * 15)-25, i * bSize, bSize, bSize), Color.White);
                    }
                }
            }
            for (int i = 1; i < 14; i++)
            {
                if (i % 2 == 0)
                {
                    spriteBatch.Draw(br4, new Rectangle(0, i * bSize, bSize, bSize), Color.White);
                }
                else
                {
                    spriteBatch.Draw(brick, new Rectangle(0, i * bSize, bSize, bSize), Color.White);
                }
                if (i % 2 == 0)
                {
                    spriteBatch.Draw(br1, new Rectangle(1500 - bSize, i * bSize, bSize, bSize), Color.White);
                }
                else
                {
                    spriteBatch.Draw(brick, new Rectangle(1500 - bSize, i * bSize, bSize, bSize), Color.White);
                }


            }
            for (int i = 0; i < 30; i++)
            {
                if (i % 2 == 0)
                {
                    spriteBatch.Draw(br5, new Rectangle(i * bSize, 0, bSize, bSize), Color.White);
                }
                else
                {
                    spriteBatch.Draw(brick, new Rectangle(i * bSize, 0, bSize, bSize), Color.White);
                }
                if (i % 2 == 0)
                {
                    spriteBatch.Draw(br2, new Rectangle(i * bSize, 750 - bSize, bSize, bSize), Color.White);
                }
                else
                {
                    spriteBatch.Draw(brick, new Rectangle(i * bSize, 750 - bSize, bSize, bSize), Color.White);
                }

            }
            

        }
        /// <summary>
        /// Este metodo ya no se utiliza, borralo plox
        /// </summary>
        public void mostrarPuntajes()
        {
            StreamReader lectura = new StreamReader("puntajes.txt");
            for (int i = 0; i<5; i++)
            {
                topFive[i].puntuacion = int.Parse(lectura.ReadLine());
                topFive[i].name = lectura.ReadLine();
            }
            lectura.Close();
        }
        /// <summary>
        ///  ni este, x2 
        /// </summary>
        /// <param name="puntaje"></param>
        /// <param name="nombre"></param>
        public void actualizarTop(int puntaje, char[] nombre)
        {
            StreamReader lectura = File.OpenText("puntajes.txt");
            StreamWriter temp = File.CreateText("temp.txt");
            lectura.Close();
            temp.Close();
        }
        /// <summary>
        /// Este metodo solo dibuja el cuadro utilizado en la pantalla puntajes, que es el cuadrado de los tiles 
        /// </summary>
        public void cuadroPuntajes()
        {
            for (int i = 8; i < 22; i++)
            {
                spriteBatch.Draw(brick, new Rectangle(i * bSize, 150, bSize, bSize), Color.White);
                spriteBatch.Draw(brick, new Rectangle(i * bSize, 550, bSize, bSize), Color.White);
            }
            for (int i = 4; i < 11; i++)
            {
                spriteBatch.Draw(brick, new Rectangle(1050, i * bSize, bSize, bSize), Color.White);
                spriteBatch.Draw(brick, new Rectangle(400, i * bSize, bSize, bSize), Color.White);
            }
        }
        /// <summary>
        /// Este metodo recibe como parametro el tiempo de ejecucion del juego
        /// otro metodo nos ayuda a dibujar todo, al igual que el Update este esta seccionado por un switch y cases
        /// dependiendo del gameState es la pantalla que va a dibujar, todos los dibujos de sprites tienen que ir en 
        /// este metodo y siempre inicia con un spriteBatch.Begin(); y terminamos con un spriteBatch.End();
        /// 
        /// Cada estructura de spriteBatch.Draw() esta dada de la siguiente manera
        /// spriteBatch.Draw(parametroDeTextura,parametroDeVector,parametroDeColor);
        /// Siempre debe recibir una textura para poder tener algo que dibujar
        /// Siempre debe tener una posicion donde dibujar, para eso se le pasa el vector
        /// El vector se puede inicalizar arriba o bien, en esta seccion, pero es mejor arriba
        /// asi en esta seccion solamente se llama
        /// El color siempre tiene que ir, si no pasas ese parametro tendras un warning, si corre
        /// pero no es lo mas optimo, para que no cambie el color de la textura que tenemos le pasamos
        /// como parametro el Color.White que ya viene por defecto, pero podemos generar directamente 
        /// el color pasando parametros de color RGB al constructor de la clase new Color(Red, Green, Blue)
        /// tomando en cuenta que es del 0 al 255 en cada uno o bien del 00000 al FFFFFF
        /// 
        /// Cada spriteBatch.DrawString() esta dada de la siguiente manera
        /// spriteBatch.DrawString(tipoDeFuente,"el texto a imprimir",vectorDePosicion, colorDelTexto);
        /// La diferencia mas significativa del Draw regular es el tipo de fuente y el texto, lo demas es
        /// muy similar
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin();
            switch (pantallas)
            {
                case GameState.SplashScreen:
                    spriteBatch.Draw(bg, menu, Color.White);
                    spriteBatch.Draw(splashScrren,vcSplash, Color.White);
                    break;
                case GameState.Lobyy:
                    bool interseccion = false;
                    spriteBatch.Draw(backGroundMenu, menu, Color.White);
                    spriteBatch.DrawString(Title,"KUKULCAN GAME", new Vector2(470,250), new Color(208, 196, 188));
                    spriteBatch.Draw(btScores,vcScores, Color.White);
                    spriteBatch.Draw(btJugar, vcJugar, Color.White);
                    spriteBatch.Draw(btExit,vcExit, Color.White);
                    spriteBatch.Draw(creditos,vcCreditos, Color.White);

                    if (rcJugar.Intersects(rectanguloCursor))//JEF
                    {
                        interseccion = true;
                        spriteBatch.Draw(lstManoCargando[frames], rectanguloCursor, Color.White);//JEF

                    }

                    if (rcScores.Intersects(rectanguloCursor))//JEF
                    {
                        interseccion = true;
                        spriteBatch.Draw(lstManoCargando[frames], rectanguloCursor, Color.White);//JEF

                    }

                    if (rcCreditos.Intersects(rectanguloCursor))//JEF
                    {
                        interseccion = true;
                        spriteBatch.Draw(lstManoCargando[frames], rectanguloCursor, Color.White);//JEF

                    }

                    if (rcExit.Intersects(rectanguloCursor))//JEF
                    {
                        interseccion = true;
                        spriteBatch.Draw(lstManoCargando[frames], rectanguloCursor, Color.White);//JEF

                    }
                    if (interseccion == false)
                    {
                        spriteBatch.Draw(ImagenManoDerecha, rectanguloCursor, Microsoft.Xna.Framework.Color.White);//JEF
                    }
                    if (ZTorso < 1.5 && ZTorso > 0)
                    {//JEF
                        spriteBatch.DrawString(textoAlerta, "RETROCEDA UNOS CENTIMETROS", new Vector2(100, 370), Color.Red);
                        //JEF
                    }else{
                        if (ZTorso > 2.5) {
                            spriteBatch.DrawString(textoAlerta, "AVANCE UNOS CENTIMETROS", new Vector2(100, 370), Color.Red);

                        }

                    }//JEF

                  
                    break;
                case GameState.SelectHand:
                    spriteBatch.Draw(bg, menu, Color.White);
                    spriteBatch.Draw(Rh, new Rectangle(75,60,600,600), Color.White);
                    spriteBatch.Draw(Lh, new Rectangle(825, 60, 600, 600), Color.White);
                    border(!true);
                    if (manosArriba == true)
                    {
                        spriteBatch.DrawString(textoAlerta, "LEVANTE SOLO UNA MANO", new Vector2(100, 370), Color.Red);
                    }
                    break;
                case GameState.Game:
                    spriteBatch.Draw(bg, menu, Color.White);
                    border(true);
                    spriteBatch.DrawString(Title, "Puntaje: "+puntaje, new Vector2(1080, 80), new Color(208, 196, 188));
                    drawSkeletonTracking();
                    drawSnake();
                    break;
                case GameState.Credits:
                    spriteBatch.Draw(bg, menu, Color.White);
                    spriteBatch.Draw(natureLeftTop2, new Rectangle(-220, -57, 912, 538), Color.White);
                    spriteBatch.Draw(natureRightTop, new Rectangle(808, -57, 912, 538), Color.White);
                    spriteBatch.Draw(creditos, new Rectangle(620, 190, 250, 47), Color.White);
                    spriteBatch.Draw(logoMonkey, new Rectangle(615, 35, 280, 110), Color.White);
                    spriteBatch.DrawString(mayan, "Líder de proyecto:", new Vector2(170, 315), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Magdiel Pech ", new Vector2(170, 365), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Desarrolladores:", new Vector2(480, 315), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Alvar Peniche", new Vector2(480, 365), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Rodrigo Euan", new Vector2(480, 425), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Diseñadores:", new Vector2(780, 315), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Huriata Bonilla", new Vector2(780, 365), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Luis Ávila", new Vector2(780, 425), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Audio y sonidos:", new Vector2(1070, 315), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Luis Ávila", new Vector2(1070, 365), new Color(208, 196, 188));
                    spriteBatch.DrawString(mayan, "Agradecimientos especiales:   Edgar Cambranes", new Vector2(330, 520), new Color(208, 196, 188));
                    spriteBatch.Draw(btnMenu, vcMenu, Color.White);

                    if (rcMenu.Intersects(rectanguloCursor))//JEF
                    {
                        interseccion = true;
                        spriteBatch.Draw(lstManoCargando[frames], rectanguloCursor, Color.White);//JEF

                    }
                    else
                    {
                        spriteBatch.Draw(ImagenManoDerecha, rectanguloCursor, Microsoft.Xna.Framework.Color.White);//JEF
                    }
                    break;
                case GameState.Scores:
                    spriteBatch.Draw(bg, menu, Color.White);
                    spriteBatch.DrawString(mayan, nombreUsuario, new Vector2(100, 50), new Color(208, 196, 188));

                    cuadroPuntajes();
                    spriteBatch.DrawString(mayan, "TOP:      NOMBRE:     PUNTAJE:", new Vector2(510, 210), new Color(208, 196, 188));
                    if (lstPuntajes.Count() > 0)
                    {
                        int y = 200;
                        for (int i = 0; i < lstPuntajes.Count(); i++)
                        {
                            spriteBatch.DrawString(mayan, lstPuntajes[i], new Vector2(530, y += 50), new Color(208, 196, 188));
                        }
                    }
                    else {

                        spriteBatch.DrawString(mayan, "CARGANDO PUNTAJES...", new Vector2(530, 400), new Color(208, 196, 188));

                    }



                    spriteBatch.Draw(natureLeftTop2, new Rectangle(-220, -57, 912, 538), Color.White);
                    spriteBatch.Draw(natureRightTop, new Rectangle(808, -57, 912, 538), Color.White);
                    spriteBatch.Draw(top5, new Rectangle(600, 35, 300, 57), Color.White);
                    spriteBatch.Draw(btnMenu, vcMenu, Color.White);
    
                    if (rcMenu2.Intersects(rectanguloCursor))//JEF
                    {
                        spriteBatch.Draw(lstManoCargando[frames], rectanguloCursor, Color.White);//JEF
                    }
                    else
                    {//JEF
                        spriteBatch.Draw(ImagenManoDerecha, rectanguloCursor, Microsoft.Xna.Framework.Color.White);//JEF
                    }
                    break;

                case GameState.SaveScores:
                    spriteBatch.Draw(bg, menu, Color.White);
                    spriteBatch.Draw(natureLeftTop2, new Rectangle(-220, -57, 912, 538), Color.White);
                    spriteBatch.Draw(natureRightTop, new Rectangle(808, -57, 912, 538), Color.White);
                    spriteBatch.Draw(flechaUp, rcAumentarLetra1, new Color(208, 196, 188));
                    spriteBatch.Draw(flechaUp2, rcAumentarLetra2, new Color(208, 196, 188));
                    spriteBatch.Draw(flechaDown, rcDisminuirLetra1, new Color(208, 196, 188));
                    spriteBatch.Draw(flechaDown2, rcDisminuirLetra2, new Color(208, 196, 188));
                    spriteBatch.Draw(btScores, vcMenu, Color.White);
                    spriteBatch.DrawString(mayanBig,Convert.ToString(letra1), vcLetra1, new Color(208, 196, 188));
                    spriteBatch.DrawString(mayanBig, Convert.ToString(letra2), vcLetra2, new Color(208, 196, 188));

                    spriteBatch.DrawString(mayan, "Puntaje obtenido: "+ Convert.ToInt32(puntaje), new Vector2(600, 10), Microsoft.Xna.Framework.Color.White);


                    if (rcGuardarPuntaje.Intersects(rectanguloCursor))//JEF
                    {
                        spriteBatch.Draw(lstManoCargando[frames], rectanguloCursor, Color.White);//JEF

                    }
                    else
                    {//JEF
                        spriteBatch.Draw(ImagenManoDerecha, rectanguloCursor, Microsoft.Xna.Framework.Color.White);//JEF

                    }
                    if (ZTorso < 1.5 && ZTorso > 0)
                    {//JEF
                        spriteBatch.DrawString(textoAlerta, "RETROCEDA UNOS CENTIMETROS", new Vector2(100, 370), Microsoft.Xna.Framework.Color.Red);
                        //JEF
                    }//JEF

                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
