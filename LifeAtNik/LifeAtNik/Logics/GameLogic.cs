using LifeAtNik.Enums;
using LifeAtNik.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LifeAtNik.Logics
{
    

    public class GameLogic : IGameControl, IGameModel
    {
        //külön mátrixot használunk a map-nek meg a playernek, meg az npc-knek

        private string OnWhichMapAmI = "aula";
        public Direction GoingDirection;

        public TileType[,] GameMatrix { get; set; } //map
        public int[,] CharMatrix { get; set; } //player -- ahol 1es van a mátrixban, ott van a player, egyébként 0
        public TileType[,] NpcMatrix { get; set; } //npck
        public int[] WhereAmI { get; set ; }

        //ezzel fogjuk tudni elérni a mappákat amiket létrehoztunk a displaynél pl: path + "/PNGs" 
        public string DirPath
        {
            get { return Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString(); }
        }

        public GameLogic()
        {
            LoadLevel(Path.Combine(DirPath, "Levels", "aula.lvl"));
        }

        public void Move(Direction direction)
        {
            var coords = WhereAmI;
            int i = coords[0];
            int j = coords[1];
            int old_i = i;
            int old_j = j;
            switch (direction)
            {
                case Direction.up:
                    if (i - 1 >= 0)
                    {
                        GoingDirection = Direction.up;
                        i--;
                    }
                    break;
                case Direction.down:
                    if (i + 1 < GameMatrix.GetLength(0))
                    {
                        i++;
                        GoingDirection = Direction.down;
                    }
                    break;
                case Direction.left:
                    if (j - 1 >= 0)
                    {
                        j--;
                        GoingDirection = Direction.left;
                    }
                    break;
                case Direction.right:
                    if (j + 1 < GameMatrix.GetLength(1))
                    {
                        j++;
                        GoingDirection = Direction.right;
                    }
                    break;
                default:
                    break;
            }
            if (GameMatrix[i, j] == TileType.floor)
            {
                CharMatrix[old_i, old_j] = 0;
                CharMatrix[i, j] = 1;
                WhereAmI[0] = i;
                WhereAmI[1] = j;

            }
            else if (GameMatrix[i, j] == TileType.stairs) 
            {
                CharMatrix[old_i, old_j] = 0;
                CharMatrix[i, j] = 1;
                WhereAmI[0] = i;
                WhereAmI[1] = j;
            }
            else if (GameMatrix[i, j] == TileType.stairs1)
            {
                CharMatrix[old_i, old_j] = 0;
                CharMatrix[i, j] = 1;
                WhereAmI[0] = i;
                WhereAmI[1] = j;
            }
            else if (GameMatrix[i, j] == TileType.levelswap)
            {

                //TODO
                LoadLevel("lol");
            }
            else if (GameMatrix[i, j] == TileType.door)
            {

                //TODO
                if (OnWhichMapAmI == "aula")
                {
                    if (i == 1 || j == 15)
                    {
                        OnWhichMapAmI = "F01";
                        LoadLevel(Path.Combine(DirPath, "Levels", "F01.lvl"));
                    }
                    else
                    {
                        MessageBox.Show("Its Locked!", "Life At Nik");
                    }
                }
                else if (OnWhichMapAmI == "F01")
                {
                    OnWhichMapAmI = "aula";
                    LoadLevel(Path.Combine(DirPath, "Levels", "aula.lvl"));
                    WhereAmI[0] = 1;
                    WhereAmI[1] = 15;
                }
                
            }
        }
        private void LoadLevel(string path)
        {
            //első sor a pálya mérete, második sor a "player" elhelyezkedése
            string[] lines = File.ReadAllLines(path);
            //kimentem a pálya méretét
            int[] size = new int[] { int.Parse(lines[0].Split()[0]), int.Parse(lines[0].Split()[1])};
            GameMatrix = new TileType[size[0], size[1]];
            //kimentem hol van a player
            int[] coords = new int[] { int.Parse(lines[1].Split()[0]), int.Parse(lines[1].Split()[1]) };
            WhereAmI = coords;
            CharMatrix = new int[size[0], size[1]];
            for (int i = 0; i < GameMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < GameMatrix.GetLength(1); j++)
                {
                    GameMatrix[i, j] = ConvertToEnum(lines[i + 2][j]);
                }
            }
            
            CharMatrix[WhereAmI[0], WhereAmI[1]] = 1;
            //NPCk TODO
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
                case 'S': return TileType.stairs;
                case 'R': return TileType.stairs1;
                default:
                    return TileType.floor;
            }
        }

        
    }
}
