using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Trains
{

    class Trains
    {

        static void Main(string[] args)
        {
            String input;
            while(true)
            {
                Console.WriteLine("Enter comma separated edges like \"AB5, BC7\" or \"exit\" to end the program:");
                input = Console.ReadLine();

                if (input.Contains("exit"))
                {
                    break;
                }

                // Remove spaces from input.
                string pattern = "\\s";
                string replacement = "";
                Regex regex = new Regex(pattern);
                String inputNoSpace = regex.Replace(input, replacement);

                // Split input into an array of strings. For example: ["AB5","BC9"].
                String[] inputArray = inputNoSpace.Split(',');

                Edge[] edges = new Edge[inputArray.Length];
                for (int index = 0; index < inputArray.Length; index++)
                {
                    string inputElement = inputArray[index];

                    // get city characters
                    char[] charArray = inputElement.ToCharArray();
                    char start = charArray[0];
                    char end = charArray[1];

                    // get integer lenght
                    pattern = "\\d+";
                    regex = new Regex(pattern);
                    MatchCollection integerMatches = regex.Matches(inputElement);
                    string lengthString = integerMatches[0].ToString();
                    int length = Convert.ToInt32(lengthString);
                    edges[index] = new Edge(start, end, length);
                }
                Map map = new Map();
                map.edges = edges;


                //1
                List<char> cities1 = new List<char>{ 'A', 'B', 'C' };
                Route route1 = new Route(cities1);
                string output1 = map.StringRouteDistance(route1);
                Console.WriteLine("Outout #1: {0}", output1);

                //2
                List<char> cities2 = new List<char> { 'A', 'D' };
                Route route2 = new Route(cities2);
                string output2 = map.StringRouteDistance(route2);
                Console.WriteLine("Outout #2: {0}", output2);

                //3
                List<char> cities3 = new List<char> { 'A', 'D', 'C' };
                Route route3 = new Route(cities3);
                string output3 = map.StringRouteDistance(route3);
                Console.WriteLine("Outout #3: {0}", output3);

                //4
                List<char> cities4 = new List<char> { 'A', 'E', 'B', 'C', 'D' };
                Route route4 = new Route(cities4);
                string output4 = map.StringRouteDistance(route4);
                Console.WriteLine("Outout #4: {0}", output4);

                //5
                List<char> cities5 = new List<char> { 'A', 'E', 'D' };
                Route route5 = new Route(cities5);
                string output5 = map.StringRouteDistance(route5);
                Console.WriteLine("Outout #5: {0}", output5);

                //6
                int result6 = map.CountRoutesWithStops('C', 'C', 0, 3);
                Console.WriteLine("Outout #6: {0}", result6);

                //7
                int result7 = map.CountRoutesWithStops('A', 'C', 4, 4);
                Console.WriteLine("Outout #7: {0}", result7);

                //8
                string result8 = map.FindShortestRouteLength('A', 'C');
                Console.WriteLine("Outout #8: {0}", result8);

                //9
                string result9 = map.FindShortestRouteLength('B', 'B');
                Console.WriteLine("Outout #9: {0}", result9);

                //10
                int result10 = map.CountRoutesShorterThan('C', 'C', 30);
                Console.WriteLine("Outout #10: {0}", result10);

                if (input.Contains("exit"))
                {
                    break;
                }
            }
        }
    }
}
