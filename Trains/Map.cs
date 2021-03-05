using System;
using System.Collections.Generic;
using System.Text;

namespace Trains
{
    class Map
    {
        public Edge[] edges;

        // Finds the distance of a route.
        public int getRouteDistance(Route route)
        {
            // Total distance sum
            int total = 0;

            List<char> cities = route.cities;

            // Iterate through the end points by skipping the first index.
            for (int index = 1; index < cities.Count; index++)
            {
                // Find the start of the edge by looking at the previous stop index. 
                char current_start = cities[index - 1];
                // Find the current end point.
                char current_end = cities[index];

                bool found_edge = false;
                // Look for an edge with a matching start and end point.
                for (int index2 = 0; index2 < edges.Length; index2++)
                {
                    Edge edge = edges[index2];
                    if (edge.start == current_start)
                    {
                        if (edge.end == current_end)
                        {
                            // matching edge
                            total += edge.length;
                            found_edge = true;
                        }
                    }
                }
                if (!found_edge)
                {
                    return -1;
                }
            }
            return total;
        }

        // Returns the distance of the route or prints NO SUCH ROUTE.
        public string StringRouteDistance(Route route)
        {
            int distance = getRouteDistance(route);
            string output = distance < 0 ? "NO SUCH ROUTE" : distance.ToString();
            return output;
        }

        // Finds routes from start to end with a minimum and maximum number of stops.
        public List<Route> FindRoutesWithStops(char start, char end, int min, int max)
        {
            // List of routes that are still being explored
            List<Route> explore_routes = new List<Route>();

            List<char> starting_city = new List<char>() { start };

            explore_routes.Add(new Route(starting_city));

            List<Route> routes_found = new List<Route>();

            // Find potential paths from starting city up to the limit of stops.
            for (int stop_count = 1; stop_count <= max; stop_count++)
            {
                // List of routes to explore after the current list.
                List<Route> explore_routes_next = new List<Route>();

                // For each route find potential next edge
                foreach (Route route in explore_routes)
                {
                    int current_city_index = route.cities.Count - 1;
                    char current_city = route.cities[current_city_index];
                    // Find each edge starting from the end of the current route.
                    foreach (Edge edge in edges)
                    {
                        if (edge.start == current_city)
                        {
                            // Add a new route, the same as the current one, but with this edge added to the route.
                            Route new_route = route.DeepCopy();
                            new_route.cities.Add(edge.end);
                            explore_routes_next.Add(new_route);

                            // If the new route ends in the correct city and meets the minimum stop count.
                            if (edge.end == end && stop_count >= min)
                            {
                                // Add the new route to routes found.
                                routes_found.Add(new_route);
                            }

                        }
                    }
                }

                // Reset the list of routes to explore in the next loop.
                explore_routes = explore_routes_next;
            }

            return routes_found;
        }

        // Gets a count of routes from start to end with a minimum and maximum number of stops.
        public int CountRoutesWithStops(char start, char end, int min, int max)
        {
            return FindRoutesWithStops(start, end, min, max).Count;
        }

        // Finds the length of the shortest route from start to end.
        public string FindShortestRouteLength(char start, char end)
        {
            // List of routes that are still being explored
            List<Route> explore_routes = new List<Route>();

            List<char> starting_city = new List<char>() { start };

            explore_routes.Add(new Route(starting_city, 0));

            // initialize at max integer.
            int shortest_route_length = 2147483647;

            // Search until there are no more potentially shorter routes left in the explore_routes list.
            while (explore_routes.Count > 0)
            {
                // List of routes to explore after the current list.
                List<Route> explore_routes_next = new List<Route>();

                // For each route find potential next edge
                foreach (Route route in explore_routes)
                {
                    int current_city_index = route.cities.Count - 1;
                    char current_city = route.cities[current_city_index];
                    // Find each edge starting from the end of the current route.
                    foreach (Edge edge in edges)
                    {
                        // if the edge starts in the current city in the route
                        if (edge.start == current_city)
                        {
                            // The following if condition exists, because the fastest route is never going to the same city twice.
                            // if the edge doesn't lead to a city already in the route or leads to the starting city.
                            if (route.cities.IndexOf(edge.end) == -1 || route.cities.IndexOf(edge.end) == 0)
                            {
                                // Add a new route, the same as the current one, but with this edge added to the route.
                                Route new_route = route.DeepCopy();
                                new_route.cities.Add(edge.end);
                                new_route.length += edge.length;

                                if (new_route.length < shortest_route_length)
                                {
                                    // If edge leads to the end city.
                                    if (edge.end == end)
                                    {
                                        // Set new shortest length.
                                        shortest_route_length = new_route.length;
                                    }
                                    else
                                    {
                                        // Add to list of routes to explore further.
                                        explore_routes_next.Add(new_route);
                                    }
                                }
                            }
                        }
                    }
                }

                // These routes don't end at the correct city, but still could potentially lead to a shorter path.
                explore_routes = explore_routes_next;
            }

            string shortest_route_length_string;
            if (shortest_route_length == 2147483647)
            {
                shortest_route_length_string = "NO SUCH ROUTE";
            }
            else
            {
                shortest_route_length_string = shortest_route_length.ToString();
            }

            return shortest_route_length_string;
        }


        // Finds routes from start to end with a length shorter than the limit.
        public List<Route> FindRoutesShorterThan(char start, char end, int limit)
        {
            // List of routes that need to be explored further.
            List<Route> explore_routes = new List<Route>();

            // Initialize the explore_routes list with the starting city.
            List<char> starting_city = new List<char>() { start };
            explore_routes.Add(new Route(starting_city, 0));

            // List of routes shorter than limit.
            List<Route> found_routes = new List<Route>();

            // Search until there are no more potential routes that could be less than the limit.
            while (explore_routes.Count > 0)
            {
                // List of routes to explore after the current list.
                List<Route> explore_routes_next = new List<Route>();

                // For each route in the routes to explore.
                foreach (Route route in explore_routes)
                {
                    // Find the last city in the current route.
                    int current_city_index = route.cities.Count - 1;
                    char current_city = route.cities[current_city_index];

                    // Find each edge starting from the end of the current route.
                    foreach (Edge edge in edges)
                    {
                        // if the edge starts in the current city in the route
                        if (edge.start == current_city)
                        {
                            // Add a new route, the same as the current one, but with this edge added to the route.
                            Route new_route = route.DeepCopy();
                            new_route.cities.Add(edge.end);
                            new_route.length += edge.length;

                            // If the new route is less than the limit.
                            if (new_route.length < limit)
                            {
                                // If edge leads to the end city.
                                if (edge.end == end)
                                {
                                    // Add to found route list
                                    found_routes.Add(new_route);
                                }

                                // Add all new routes to list of routes to explore further.
                                explore_routes_next.Add(new_route);

                            }
                        }
                    }
                }

                // These routes don't end at the correct city, but still could potentially lead to the city while under the limit.
                explore_routes = explore_routes_next;
            }

            return found_routes;
        }

        // Counts the routes from start to end with a length shorter than the limit.
        public int CountRoutesShorterThan(char start, char end, int limit)
        {
            List<Route> routes = FindRoutesShorterThan(start, end, limit);
            return routes.Count;
        }

    }
}
