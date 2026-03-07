using CodingPracticeLab1.Problems;

namespace CodingPracticeCN
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Exercise 3 - Coding Practice CN");

            // Test 1
            var medianSolver = new MedianOfTwoSortedArrays();
            double med1 = medianSolver.FindMedianSortedArrays(new[] { 1, 3 }, new[] { 2 });
            Console.WriteLine($"Median of [1,3] & [2] = {med1}");  // 2.0

            // Test 2
            var divSolver = new IntegerDivision();
            Console.WriteLine($"10 / 3 = {divSolver.Divide(10, 3)}");         // 3
            Console.WriteLine($"7 / -3 = {divSolver.Divide(7, -3)}");        // -2

            // Test 3
            var wordSearch = new WordSearch();
            char[][] board = new char[][]
            {
                new char[] { 'A','B','C','E' },
                new char[] { 'S','F','C','S' },
                new char[] { 'A','D','E','E' }
            };
            Console.WriteLine($"Word 'ABCCED' exists: {wordSearch.Exist(board, "ABCCED")}"); // true
            Console.WriteLine($"Word 'SEE' exists: {wordSearch.Exist(board, "SEE")}");       // true
            Console.WriteLine($"Word 'ABCB' exists: {wordSearch.Exist(board, "ABCB")}");     // false

            Console.ReadKey();
        }
    }
}