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
        private List<Exercise> aula;
        private List<Exercise> first;
        private List<Exercise> first1;
        public QuestionLogic()
        {
            f01 = new List<Exercise>();
            aula = new List<Exercise>();
            first = new List<Exercise>();
            first1= new List<Exercise>();
            LoadQuestions();
        }

        public Exercise GiveQuestion(string map)
        {
            Exercise tmp = new Exercise();

            switch (map)
            {
                case "F01":
                    return f01[r.Next(0, f01.Count)];
                case "first_floor":
                    return first[r.Next(0, first.Count)];
                case "first_floor1":
                    return first1[r.Next(0, first1.Count)];
                default:
                    return aula[r.Next(0, aula.Count)];
            }

        }
        public void LoadQuestions()
        {
            string[] f01qs = File.ReadAllLines(Path.Combine(DirPath, "Questions", "f01.txt"), Encoding.UTF8);
            string[] aulaqs = File.ReadAllLines(Path.Combine(DirPath, "Questions", "aula.txt"), Encoding.UTF8);
            string[] firstqs = File.ReadAllLines(Path.Combine(DirPath, "Questions", "first_floor.txt"), Encoding.UTF8);
            string[] first1qs = File.ReadAllLines(Path.Combine(DirPath, "Questions", "first_floor1.txt"),Encoding.UTF8);

            for (int i = 0; i < f01qs.Length; i++)
            {
                //1 sor: KÉRDÉS;JÓ;ROSSZ;ROSSZ;ROSSZ;
                string[] sor = f01qs[i].Split(';');
                string[] rosszak = new string[] { sor[2], sor[3], sor[4] };
                Exercise tmp = new Exercise(sor[0], sor[1], rosszak);
                f01.Add(tmp);
            }
            for (int i = 0; i < aulaqs.Length; i++)
            {
                //1 sor: KÉRDÉS;JÓ;ROSSZ;ROSSZ;ROSSZ;
                string[] sor = aulaqs[i].Split(';');
                string[] rosszak = new string[] { sor[2], sor[3], sor[4] };
                Exercise tmp = new Exercise(sor[0], sor[1], rosszak);
                aula.Add(tmp);
            }
            for (int i = 0; i < firstqs.Length; i++)
            {
                //1 sor: KÉRDÉS;JÓ;ROSSZ;ROSSZ;ROSSZ;
                string[] sor = firstqs[i].Split(';');
                string[] rosszak = new string[] { sor[2], sor[3], sor[4] };
                Exercise tmp = new Exercise(sor[0], sor[1], rosszak);
                first.Add(tmp);
            }
            for (int i = 0; i < first1qs.Length; i++)
            {
                //1 sor: KÉRDÉS;JÓ;ROSSZ;ROSSZ;ROSSZ;
                string[] sor = first1qs[i].Split(';');
                string[] rosszak = new string[] { sor[2], sor[3], sor[4] };
                Exercise tmp = new Exercise(sor[0], sor[1], rosszak);
                first1.Add(tmp);
            }
        }

    }
}
