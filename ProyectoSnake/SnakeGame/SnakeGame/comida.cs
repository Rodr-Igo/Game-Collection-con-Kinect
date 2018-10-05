using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SnakeGame
{
    class Comida : SnakeBody
    {
        public Comida()
        {
            this.x = generar(84);
            this.y = generar(33);
        }
        public void dibujar(Graphics comida)
        {
            comida.FillRectangle(new SolidBrush(Color.OrangeRed), this.x, this.y, this.ancho, this.ancho);
        }
        public void nuevaComida()
        {
            this.x = generar(84);
            this.y = generar(33);
        }
        public int generar(int n)
        {
            Random rand = new Random();

            int num = (int)rand.Next(0,n)*10;

            return num;
        }
    }
}
