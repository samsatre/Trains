using System;
using System.Collections.Generic;
using System.Text;

namespace Trains
{
    class Edge
    {
        public char start;   // starting city of the edge
        public char end;  // ending city of the edge
        public int length;   // lenth of the edge

        public Edge(char s, char e, int l)
        {
            start = s;
            end = e;
            length = l;
        }
    }
}
