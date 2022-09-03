public class JewelCollector {

    public static void Main() {

        bool running = true;

        Console.WriteLine("Enter the board dimension: ");
        int dimension = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Insert the number of jewels: ");
        int n_jewels = Convert.ToInt32(Console.ReadLine());

        for(int i=0; i<n_jewels; i++){
            Console.WriteLine("Insert the type of jewel and position: ");
            string jewel_position = Console.ReadLine();
        }

        Console.WriteLine("Insert the number of obstacles: ");
        int n_obstacles = Convert.ToInt32(Console.ReadLine());

        for(int i=0; i<n_obstacles; i++){
            Console.WriteLine("Insert the type of jewel and position: ");
            string obstacle_position = Console.ReadLine();
        }

        Console.WriteLine("Insert the robot inicial position: ");
        string robot_position = Console.ReadLine();

        Console.WriteLine("Let's start!!");

        do {

            Console.WriteLine("Enter the command: ");
            string command = Console.ReadLine();

            if (command.Equals("quit")) {
                running = false;
            } else if (command.Equals("w")) {
                
            } else if (command.Equals("a")) {
                
            } else if (command.Equals("s")) {
            
            } else if (command.Equals("d")) {
            
            } else if (command.Equals("g")) {
                
            }
        } while (running);
    }
}