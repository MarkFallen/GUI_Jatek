using LifeAtNik.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeAtNik.Interface
{
    public interface IGameControl
    {
        void Move(Direction dir);
    }
}
