using LifeAtNik.Enums;
using LifeAtNik.Interface;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LifeAtNik.Logics
{

    public class GameLogic : ObservableObject, IGameControl, IGameModel, INotifyPropertyChanged
    {
        

        //külön mátrixot használunk a map-nek meg a playernek, meg az npc-knek

        private string OnWhichMapAmI = "aula";
        private string GoingDirection = "stay";

        private int ero = 50;
        private int tudas = 0;
        private int motiv = 100;

        private int answered;

        public int Answered
        {
            get { return answered; }
            set { answered = value; }
        }

        public bool Done { get { return answered == 4; } }
        // TODO => go tom the next level

        public int Ero { get { return ero; } set { ero = int.Parse(value.ToString()); } }
        public int Tudas { 
            get { 
                return tudas; 
            } 
            set { 
                tudas = int.Parse(value.ToString()); 
                OnPropertyChanged("tudas"); 
            } 
        }
        public int Motiv { get { return motiv; } set { motiv = int.Parse(value.ToString()); } }

        public string goingDirection { get { return GoingDirection; } }

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
            answered = 0;
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
                        GoingDirection = "up";
                        i--;
                    }
                    break;
                case Direction.down:
                    if (i + 1 < GameMatrix.GetLength(0))
                    {
                        GoingDirection = "down";
                        i++;
                    }
                    break;
                case Direction.left:
                    if (j - 1 >= 0)
                    {
                        GoingDirection = "left";
                        j--;
                    }
                    break;
                case Direction.right:
                    if (j + 1 < GameMatrix.GetLength(1))
                    {
                        GoingDirection = "right";
                        j++;
                    }
                    break;
                default:
                    GoingDirection = "stay";
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
            else if (GameMatrix[i, j] == TileType.stairs_end)
            {

                if (OnWhichMapAmI == "aula")
                {
                    OnWhichMapAmI = "first_floor";
                    LoadLevel(Path.Combine(DirPath, "Levels", "first_floor.lvl"));
                    //LoadLevel(Path.Combine(DirPath, "Levels", "lol.lvl"));
                }
                else if (OnWhichMapAmI == "first_floor")
                {
                    OnWhichMapAmI = "aula";
                    LoadLevel(Path.Combine(DirPath, "Levels", "aula.lvl"));
                    WhereAmI[0] = 3;
                    WhereAmI[1] = 17;
                }
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
                        if (answered == 2)
                        {
                            MessageBox.Show("Gratulálok, sikeresen kijártad az egyetemet!");
                            
                        }
                        else
                        {
                            MessageBox.Show("Még nem válaszoltál meg minden kérdést!", "Life At Nik");
                        }
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
            else if (GameMatrix[i, j] == TileType.enemy)
            {
                // FightWindow megnyitása
                //GameLogic dummy = this;
                //FightWindow asd = new FightWindow(OnWhichMapAmI, ref dummy);
                FightWindow asd = new FightWindow(OnWhichMapAmI);
                bool valasz = (bool)asd.ShowDialog();
                if (valasz)
                {
                    answered++;
                    tudas += 25;
                    GameMatrix[i, j] = TileType.floor;
                    NotifyPropertyChanged();
                }
            }
            else if (GameMatrix[i, j] == TileType.end_floor)
            {
                if (OnWhichMapAmI == "first_floor")
                {
                    OnWhichMapAmI = "first_floor1";
                    LoadLevel(Path.Combine(DirPath, "Levels", "first_floor1.lvl"));
                    WhereAmI[0] = 1;
                    WhereAmI[1] = 7;
                }
                else if (OnWhichMapAmI == "first_floor1")
                {
                    OnWhichMapAmI = "first_floor";
                    LoadLevel(Path.Combine(DirPath, "Levels", "first_floor.lvl"));
                    WhereAmI[0] = 10;
                    WhereAmI[1] = 7;
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
                case 'B': return TileType.stairs_end;
                case 'H': return TileType.end_floor;
                default:
                    return TileType.floor;
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        //{
        //    if (object.Equals(storage, value)) return false;
        //    storage = value;
        //    this.OnPropertyChaned(propertyName);
        //    return true;
        //}

        //private void OnPropertyChaned(string propertyName)
        //{
        //    var eventHandler = this.PropertyChanged;
        //    if (eventHandler != null)
        //        eventHandler(this, new PropertyChangedEventArgs(propertyName));
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
