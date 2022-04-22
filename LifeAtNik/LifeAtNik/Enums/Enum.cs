using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeAtNik.Enums
{
    //case 'W': return TileType.wall;
    //case 'P': return TileType.player;
    //case 'C': return TileType.chair;
    //case 'D': return TileType.desk;
    //case 'A': return TileType.door;
    //case 'E': return TileType.enemy;
    //case 'G': return TileType.glassWall;
    //case 'L': return TileType.levelswap;
    //case 'T': return TileType.table;
    //case 'O': return TileType.openDoor;
    //case 'S': return TileType.stairs;
    //case 'R': return TileType.stairs1;
    //case 'B': return TileType.stairs_end;
    //case 'H': return TileType.end_floor;
    //default:
    //    return TileType.floor;
    public enum TileType
    {
        player, wall, glassWall, floor, enemy, table, chair, desk, door, stairs, stairs1, end_floor, stairs_end, levelswap
    }
    public enum Direction
    {
        up, down, right, left
    }
    public class Enum
    {
    }
}
