using LifeAtNik.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeAtNik.Interface
{
    public interface IGameModel
    {
        TileType[,] GameMatrix { get; set; }
        int[,] CharMatrix { get; set; }
        TileType[,] NpcMatrix { get; set; }
        int[] WhereAmI { get; set; }
        string DirPath { get; }
        string goingDirection { get; }
    }
}
