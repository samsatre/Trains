# Trains
Assumtions: 
1: I assume that the lines are each less than the max integer size and that no one tries to calculate anything that would sum to an amount past the max integer size either.
2: I assume good user input and didn't implement regex input checking.


This program uses three classes

Edge
The edge is used to represent a single train line from one city to one other city.
start - A char representing the starting city
end - A char representing the ending city
length - An int contianing the length of the edge.

Route
The route is used to represent a path for the train to travel through multiple cities.
cities - A List of chars for the cities in the route 
length - An in containing the length of the whole route (optional)

Map 
This holds all the edges in a List. It is also contains the methods used to create user output.
Edges - A List of edges to represent all the train lines.

