using Namespaces;
public class JewelCollector {

    public static void Main() {

        bool running = true;

        Console.WriteLine("Enter the board dimension: ");
        int dimension = Convert.ToInt32(Console.ReadLine());

        Map map = new Map(dimension);

        Console.WriteLine("Insert the number of jewels: ");
        int n_jewels = Convert.ToInt32(Console.ReadLine());

        for(int i=0; i<n_jewels; i++){
            Console.WriteLine("Insert the type of jewel and position: ");
            string jewel_position = Console.ReadLine();
            var charsToRemove = new string[] {",", "(", ")"};
            foreach (var c in charsToRemove)
            {
                jewel_position = jewel_position.Replace(c, string.Empty);
            }
            string[] jewel_position_array = jewel_position.Split();
            // foreach (var c in jewel_position_array)
            // {
            //     Console.WriteLine(c.ToString());
            // }
            int x_position = Convert.ToInt32(jewel_position_array[2]);
            int y_position = Convert.ToInt32(jewel_position_array[3]);
            if(x_position<0 || y_position<0 || x_position>=dimension || y_position>=dimension){
                // Console.WriteLine("Invalid position.");
                // Console.WriteLine($"x position {x_position}");
                // Console.WriteLine($"y position {y_position}");
                i = i - 1;
            }
            else{
                int[] position = new int[2] {x_position, y_position};
                map.insertJewel(jewel_position_array[0], position);
            }            
        }

        map.printMap();

        // Console.WriteLine("Insert the number of obstacles: ");
        // int n_obstacles = Convert.ToInt32(Console.ReadLine());

        // for(int i=0; i<n_obstacles; i++){
        //     Console.WriteLine("Insert the type of jewel and position: ");
        //     string obstacle_position = Console.ReadLine();
        // }

        // Console.WriteLine("Insert the robot inicial position: ");
        // string robot_position = Console.ReadLine();

        // Console.WriteLine("Let's start!!");

        // do {

        //     Console.WriteLine("Enter the command: ");
        //     string command = Console.ReadLine();

        //     if (command.Equals("quit")) {
        //         running = false;
        //     } else if (command.Equals("w")) {
                
        //     } else if (command.Equals("a")) {
                
        //     } else if (command.Equals("s")) {
            
        //     } else if (command.Equals("d")) {
            
        //     } else if (command.Equals("g")) {
                
        //     }
        // } while (running);
    }
}