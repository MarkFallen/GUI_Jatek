using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeAtNik.Logics
{
    public class Exercise
    {
        public string Question { get; set; }

        public string Right { get; set; }

        public string[] Wrongs { get; set; }

        public Exercise()
        {
        }

        public Exercise(string q, string r, string[] w)
        {
            Wrongs = w;
            Question = q;
            Right = r;
        }

    }
    public class QuestionLogic
    {
        public string DirPath
        {
            get { return Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString(); }
        }

        static Random r = new Random();

        private List<Exercise> f01;
        public QuestionLogic()
        {
            f01 = new List<Exercise>();
            LoadQuestions();
        }

        public Exercise GiveQuestion(string map)
        {
            Exercise tmp = new Exercise();

            switch (map)
            {
                case "F01":
                    return f01[r.Next(0, f01.Count)];
                //case "F01":
                //    return f01[r.Next(0, f01.Count)];
                //case "F01":
                //    return f01[r.Next(0, f01.Count)];
                //case "F01":
                //    return f01[r.Next(0, f01.Count)];
                default:
                    return f01[0];
            }

        }
        public void LoadQuestions()
        {
            string[] lines = File.ReadAllLines(Path.Combine(DirPath, "Questions", "f01.txt"));

            for (int i = 0; i < lines.Length; i++)
            {
                //1 sor: KÉRDÉS;JÓ;ROSSZ;ROSSZ;ROSSZ;
                string[] sor = lines[i].Split(';');
                string[] rosszak = new string[] { sor[2], sor[3], sor[4] };
                Exercise tmp = new Exercise(sor[0], sor[1], rosszak);
                f01.Add(tmp);
            }

        }

    }
}
