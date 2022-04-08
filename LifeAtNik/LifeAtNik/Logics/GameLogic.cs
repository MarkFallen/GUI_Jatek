using LifeAtNik.Enums;
using LifeAtNik.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeAtNik.Logics
{
    public class GameLogic : IGameControl, IGameModel
    {
        public TileType[,] GameMatrix { get; set; }


        public void Move(Direction direction)
        {
            var coords = WhereAmI();
            int i = coords[0];
            int j = coords[1];
            int old_i = i;
            int old_j = j;
            switch (direction)
            {
                case Direction.up:
                    if (i - 1 >= 0)
                    {
                        i--;
                    }
                    break;
                case Direction.down:
                    if (i + 1 < GameMatrix.GetLength(0))
                    {
                        i++;
                    }
                    break;
                case Direction.left:
                    if (j - 1 >= 0)
                    {
                        j--;
                    }
                    break;
                case Direction.right:
                    if (j + 1 < GameMatrix.GetLength(1))
                    {
                        j++;
                    }
                    break;
                default:
                    break;
            }
            if (GameMatrix[i, j] == TileType.floor )
            {
                if (GameMatrix[old_i, old_j] == TileType.openDoorWithPlayer)
                {
                    GameMatrix[i, j] = TileType.player;
                    GameMatrix[old_i, old_j] = TileType.openDoor;
                }
                else
                {
                    GameMatrix[i, j] = TileType.player;
                    GameMatrix[old_i, old_j] = TileType.floor;
                }
            }
            else if (GameMatrix[i, j] == TileType.openDoor)
            {
                GameMatrix[i, j] = TileType.openDoorWithPlayer;
                GameMatrix[old_i, old_j] = TileType.floor;
            }
        }

        private int[] WhereAmI()
        {
            for (int i = 0; i < GameMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < GameMatrix.GetLength(1); j++)
                {
                    if (GameMatrix[i, j] == TileType.player)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[] { -1, -1 };
        }
        private void LoadNext(string path)
        {
            string[] lines = File.ReadAllLines(path);
            GameMatrix = new TileType[int.Parse(lines[0]), int.Parse(lines[1])];
            for (int i = 0; i < GameMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < GameMatrix.GetLength(1); j++)
                {
                    GameMatrix[i, j] = ConvertToEnum(lines[i + 2][j]);
                }
            }
        }
        private TileType ConvertToEnum(char v)
        {
            switch (v)
            {
                case 'W': return TileType.wall;
                case 'P': return TileType.player;
                case 'C': return TileType.chair;
                case 'D': return TileType.desk;
                case 'A': return TileType.door;
                case 'E': return TileType.enemy;
                case 'G': return TileType.glassWall;
                case 'L': return TileType.levelswap;
                case 'T': return TileType.table;
                case 'O': return TileType.openDoor;
                default:
                    return TileType.floor;
            }
        }

        
    }
}
