using LifeAtNik.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LifeAtNik.Controller
{
    class GameController
    {
        IGameControl control;

        public GameController(IGameControl control)
        {
            this.control = control;
        }

        public void KeyPressed(Key key)
        {
            switch (key)
            {
                case Key.Up:
                    control.Move(Enums.Direction.up);
                    break;
                case Key.Down:
                    control.Move(Enums.Direction.down);
                    break;
                case Key.Left:
                    control.Move(Enums.Direction.left);
                    break;
                case Key.Right:
                    control.Move(Enums.Direction.right);
                    break;
            }
        }
    }
}
