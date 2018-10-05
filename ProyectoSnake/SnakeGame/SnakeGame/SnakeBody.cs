using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class SnakeBody
    {
        protected int x, y, ancho;
        
        public SnakeBody()
        {
            ancho = 20;
            
        }
        public Boolean colision(SnakeBody datos)
        {
            bool choque = true;
            int diferenciaX = Math.Abs(this.x - datos.x);
            int diferenciaY = Math.Abs(this.y - datos.y);
            if (diferenciaX >= 0 && diferenciaX < ancho && diferenciaY >= 0 && diferenciaY < ancho)
            {
                return choque;

            }
            else
            {
                return !choque;
            }
            
        }
    }
}
