using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            ChallengeWk1();
        }


        public static void ChallengeWk1()
        {

            int m, n, i, j;
            Console.Write("Enter Number Of Rows And Columns Of Matrices A and B : ");
            m = Convert.ToInt16(Console.ReadLine());
            n = Convert.ToInt16(Console.ReadLine());
            int[,] A = new int[10, 10];

            // getting input of 3*3 or 2*2 matrix
            Console.Write("\nEnter The First Matrix : ");
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < n; j++)
                {
                    A[i, j] = Convert.ToInt16(Console.ReadLine());
                }
            }
            //storing row and column having 0 from input
            Console.WriteLine("\nInput Matrix A : ");
            Dictionary<int, int> l = new Dictionary<int, int>();
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < n; j++)
                {
                    if (A[i, j] == 0)
                    {

                        l.Add(i, j);
                    }
                    Console.Write(A[i, j] + "\t");
                }
                Console.WriteLine();
            }

            //forming/replacing the new matrix with 0 rows and column 
            foreach (var item in l)
            {

                for (i = 0; i < m; i++)
                {
                    for (j = 0; j < n; j++)
                    {
                        if (i == item.Key)
                            A[item.Key, j] = 0;

                        if (j == item.Value)
                            A[i, item.Value] = 0;

                        // Console.Write(A[i, j] + "\t");
                    }
                    //Console.WriteLine();
                }


            }

            //printing the output
            Console.WriteLine("\nOutput Matrix A : ");
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < n; j++)
                {
                    Console.Write(A[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.ReadLine();

        }
    }
}
