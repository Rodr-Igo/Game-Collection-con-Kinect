using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SnakeGame
{
    class cuerpo : SnakeBody
    {
        cuerpo siguiente;
        public cuerpo(int x, int y)
        {
            this.x = x;
            this.y = y;
            siguiente = null;
            

        }

        public void dibujar(Graphics map)
        {
            if (siguiente != null)
            {
                siguiente.dibujar(map); 
            }
            map.FillRectangle(new SolidBrush(Color.Blue), this.x, this.y, this.ancho, this.ancho );

        }
        public void SetPositionxy(int x, int y)
        {
            if (siguiente != null)
            {
                siguiente.SetPositionxy(this.x, this.y);
            }
            this.x = x;
            this.y = y;
        }
        public void agregar()
        {
            if (siguiente == null)
            {
                siguiente = new cuerpo(this.x, this.y);
            }
            else
            {
                siguiente.agregar();
            }
        }
        public int getX()
        {
            return this.x;
        }
        public int getY()
        {
            return this.y;
        }
        public cuerpo verSiguiente()
        {
            return siguiente;
        }
    }
}
