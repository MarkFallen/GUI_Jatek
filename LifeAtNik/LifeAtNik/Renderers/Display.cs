using LifeAtNik.Enums;
using LifeAtNik.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LifeAtNik.Logics;
using System.Timers;

namespace LifeAtNik.Renderers
{
    public class Display : FrameworkElement
    {
        IGameModel model;
        Size size;


        public void Resize(Size size)
        {
            this.size = size;
        }

        public void SetupModel(IGameModel model)
        {
            this.model = model;
        }



        bool step_down = false;
        bool step_up = false;
        bool step_left = false;
        bool step_right = false;


        protected override void OnRender(DrawingContext drawingContext)
        {


            base.OnRender(drawingContext);
            if (model != null && size.Width > 50 && size.Height > 50)
            {
                double rectWidth = size.Width / model.GameMatrix.GetLength(1);
                double rectHeight = size.Height / model.GameMatrix.GetLength(0);


                // Kristof -> ezt kiszedtem
                //drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.Black, 0),
                //    new Rect(0, 0, size.Width, size.Height));


                //map kirajzolása
                for (int i = 0; i < model.GameMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < model.GameMatrix.GetLength(1); j++)
                    {
                        ImageBrush brush = new ImageBrush();
                        switch (model.GameMatrix[i, j])
                        {
                            case Enums.TileType.wall:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "wall.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case Enums.TileType.floor:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "floor.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case Enums.TileType.desk:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "desk.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case Enums.TileType.door:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "door.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case Enums.TileType.chair:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "chair.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case Enums.TileType.glassWall:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "glass.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case Enums.TileType.table:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "table.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case Enums.TileType.stairs:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "stairs.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case Enums.TileType.stairs1:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "stairs1.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case Enums.TileType.stairs_end:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "stairs.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case Enums.TileType.end_floor:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "floor.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case Enums.TileType.enemy:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "enemy01.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            default:
                                break;
                        }

                        drawingContext.DrawRectangle(brush
                                    , new Pen(Brushes.Black, 0),
                                    new Rect(j * rectWidth, i * rectHeight, rectWidth, rectHeight)
                                    );
                    }
                }


                //player kirajzolása






                if (model.goingDirection == "stay")
                {
                    ImageBrush playerbrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "player.png"), UriKind.RelativeOrAbsolute)));
                    drawingContext.DrawRectangle(playerbrush, new Pen(Brushes.Black, 0), new Rect(model.WhereAmI[1] * rectWidth, model.WhereAmI[0] * rectHeight, rectWidth, rectHeight));
                }
                else if (model.goingDirection == "up")
                {
                    if (step_up)
                    {
                        ImageBrush playerbrushB = new ImageBrush(new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "playerB.png"), UriKind.RelativeOrAbsolute)));
                        drawingContext.DrawRectangle(playerbrushB, new Pen(Brushes.Black, 0), new Rect(model.WhereAmI[1] * rectWidth, model.WhereAmI[0] * rectHeight, rectWidth, rectHeight));
                        step_up = false;
                    }
                    else
                    {
                        ImageBrush playerbrushBR = new ImageBrush(new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "playerBR.png"), UriKind.RelativeOrAbsolute)));
                        drawingContext.DrawRectangle(playerbrushBR, new Pen(Brushes.Black, 0), new Rect(model.WhereAmI[1] * rectWidth, model.WhereAmI[0] * rectHeight, rectWidth, rectHeight));
                        step_up = true;
                    }
                }
                else if (model.goingDirection == "down")
                {
                    if (step_down)
                    {
                        ImageBrush playerbrushR1 = new ImageBrush(new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "playerR1.png"), UriKind.RelativeOrAbsolute)));
                        drawingContext.DrawRectangle(playerbrushR1, new Pen(Brushes.Black, 0), new Rect(model.WhereAmI[1] * rectWidth, model.WhereAmI[0] * rectHeight, rectWidth, rectHeight));
                        step_down = false;
                    }
                    else
                    {
                        ImageBrush playerbrushR2 = new ImageBrush(new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "playerR2.png"), UriKind.RelativeOrAbsolute)));
                        drawingContext.DrawRectangle(playerbrushR2, new Pen(Brushes.Black, 0), new Rect(model.WhereAmI[1] * rectWidth, model.WhereAmI[0] * rectHeight, rectWidth, rectHeight));
                        step_down = true;
                    }

                    //ImageBrush playerbrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "player.png"), UriKind.RelativeOrAbsolute)));
                    //drawingContext.DrawRectangle(playerbrush, new Pen(Brushes.Black, 0), new Rect(model.WhereAmI[1] * rectWidth, model.WhereAmI[0] * rectHeight, rectWidth, rectHeight));



                }
                else if (model.goingDirection == "left")
                {
                    if (step_left)
                    {
                        ImageBrush playerbrushLR1 = new ImageBrush(new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "playerLR1.png"), UriKind.RelativeOrAbsolute)));
                        drawingContext.DrawRectangle(playerbrushLR1, new Pen(Brushes.Black, 0), new Rect(model.WhereAmI[1] * rectWidth, model.WhereAmI[0] * rectHeight, rectWidth, rectHeight));
                        step_left = false;
                    }
                    else
                    {
                        ImageBrush playerbrushLR2 = new ImageBrush(new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "playerLR2.png"), UriKind.RelativeOrAbsolute)));
                        drawingContext.DrawRectangle(playerbrushLR2, new Pen(Brushes.Black, 0), new Rect(model.WhereAmI[1] * rectWidth, model.WhereAmI[0] * rectHeight, rectWidth, rectHeight));
                        step_left = true;
                    }
                }
                else if (model.goingDirection == "right")
                {
                    if (step_right)
                    {
                        ImageBrush playerbrushRR1 = new ImageBrush(new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "playerRR1.png"), UriKind.RelativeOrAbsolute)));
                        drawingContext.DrawRectangle(playerbrushRR1, new Pen(Brushes.Black, 0), new Rect(model.WhereAmI[1] * rectWidth, model.WhereAmI[0] * rectHeight, rectWidth, rectHeight));
                        step_right = false;
                    }
                    else
                    {
                        ImageBrush playerbrushRR2 = new ImageBrush(new BitmapImage(new Uri(Path.Combine(model.DirPath, "PNGs", "playerRR2.png"), UriKind.RelativeOrAbsolute)));
                        drawingContext.DrawRectangle(playerbrushRR2, new Pen(Brushes.Black, 0), new Rect(model.WhereAmI[1] * rectWidth, model.WhereAmI[0] * rectHeight, rectWidth, rectHeight));
                        step_right = true;
                    }
                }
            }
        }
    }
}
