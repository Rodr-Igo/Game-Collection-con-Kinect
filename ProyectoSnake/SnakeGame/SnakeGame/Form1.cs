using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        cuerpo cabeza;
        Graphics map;
        Comida comida;
        int xDirection = 20, yDirection = 0, cuadro = 20, puntos = 0;
        bool xMov = true, yMov = true, gameLose = true, pausa = false;
        public Form1()
        {
            InitializeComponent();
            map = game.CreateGraphics();
            cabeza = new cuerpo(20, 20);
            comida = new Comida();
            inicio();
        }
        public void inicio()
        {
            for (int i =0; i<4; i++)
            {
                cabeza.agregar();
            }
        }
        private void game_Click(object sender, EventArgs e)
        {

        }

        private void bucle_Tick(object sender, EventArgs e)
        {
            map.Clear(Color.White);
            cabeza.dibujar(map);
            comida.dibujar(map);
            movimiento();
            colisionCuerpo();
            colisionParedes();
            CordX.Text = cabeza.getX().ToString();
            CordY.Text = cabeza.getY().ToString();
            
            if (cabeza.colision(comida))
            {
                comida.nuevaComida();
                cabeza.agregar();
                puntos+=10;
                puntaje.Text = puntos.ToString();

            }
        }
        public void gameOver()
        {
            puntaje.Text = "0";
            puntos = 0;
            xMov = true;
            yMov = true;
            xDirection = 20;
            yDirection = 0;
            cabeza = new cuerpo(20, 20);
            comida = new Comida();
            inicio();
            


        }
        public void colisionParedes()
        {
            if (cabeza.getX() < 0 || cabeza.getX() > 840 || cabeza.getY() < 0 || cabeza.getY() > 330)
            {
                gameOver();
            }
        }
        public void colisionCuerpo()
        {
            cuerpo temp;
            try
            {
                temp = cabeza.verSiguiente().verSiguiente();
            }
            catch
            {
                temp = null;
            }
            while (temp != null)
            {
                if (cabeza.colision(temp))
                {
                    gameOver();
                }
                else
                {
                    temp = temp.verSiguiente();
                }
            }
        }
        public void movimiento()
        {
            cabeza.SetPositionxy(cabeza.getX() + xDirection, cabeza.getY() + yDirection); 
        }

        private void puntaje_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs tecla)
        {
            
            if (tecla.KeyCode == Keys.Up && xMov)
            {
                yDirection = -cuadro;
                xDirection = 0;
                xMov = false;
                yMov = true;
            }
            if (tecla.KeyCode == Keys.Down && xMov)
            {
                yDirection = cuadro;
                xDirection = 0;
                xMov = false;
                yMov = true;
            }
           
            if (tecla.KeyCode == Keys.Left && yMov)
            {
                yDirection = 0;
                xDirection = -cuadro;
                xMov = true;
                yMov = false;
            }
            if (tecla.KeyCode == Keys.Right && yMov)
            {
                yDirection = 0;
                xDirection = cuadro;
                xMov = true;
                yMov = false;
            }

            if (tecla.KeyCode == Keys.Enter && pausa == false )
            {
                pausa = true;
                int temX=0, temY=0;
                bool xTem, yTem;
                temX = xDirection;
                temY = yDirection;
                xTem = xMov;
                yTem = yMov;
                yDirection = 0;
                xDirection = 0;
                xMov = false;
                yMov = false;
                MessageBox.Show("Juego en pausa prro, eres malo >:v");
                if (tecla.KeyCode == Keys.Enter && pausa == true)
                {
                    pausa = false;
                    xDirection = temX;
                    yDirection = temY;
                    xMov = xTem;
                    yMov = yTem;
                }
            }
        }

    }
}
