using LifeAtNik.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LifeAtNik
{
    /// <summary>
    /// Interaction logic for FightWindow.xaml
    /// </summary>
    public partial class FightWindow : Window
    {
        static Random r = new Random();
        int szam;
        public FightWindow(string map)
        {
            InitializeComponent();
            QuestionLogic qlogic = new QuestionLogic();
            Exercise e = qlogic.GiveQuestion(map);
            q.Content = e.Question;
            szam = r.Next(1, 5);
            switch (szam)
            {
                case 1:
                    bt_q1.Content = e.Right;
                    bt_q2.Content = e.Wrongs[0];
                    bt_q3.Content = e.Wrongs[1];
                    bt_q4.Content = e.Wrongs[2];
                    break;
                case 2:
                    bt_q2.Content = e.Right;
                    bt_q1.Content = e.Wrongs[0];
                    bt_q3.Content = e.Wrongs[1];
                    bt_q4.Content = e.Wrongs[2];
                    break;
                case 3:
                    bt_q3.Content = e.Right;
                    bt_q2.Content = e.Wrongs[0];
                    bt_q1.Content = e.Wrongs[1];
                    bt_q4.Content = e.Wrongs[2];
                    break;
                default:
                    bt_q4.Content = e.Right;
                    bt_q2.Content = e.Wrongs[0];
                    bt_q3.Content = e.Wrongs[1];
                    bt_q1.Content = e.Wrongs[2];
                    break;
            }
        }
        public void Check(object sender, RoutedEventArgs e)
        {
            Button b = (sender as Button);

            if (int.Parse(b.Tag.ToString()) == szam)
            {
                //jó válasz
                MessageBox.Show("Jó válasz!");
                this.DialogResult = true;
            }
            else
            {
                //rossz válasz
                MessageBox.Show("Rossz válasz!");
                this.DialogResult = false;
            }
        }
    }
}
