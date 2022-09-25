using Namespaces;
/// <summary>
/// JewelCollector: classe de início do programa, a qual receberá os parametros do usuário para criação do tabuleiro e instanciará os principais elementos do jogo (mapa e robô).
/// </summary>
public class JewelCollector {

    static bool running = true;

    static void EnterCommands(string reason){
        if(reason == "win"){
            Console.WriteLine("You win! Congratulations!");
            Console.WriteLine("New Game: ");
            running = true;
        }
        else if(reason == "energy"){
            Console.WriteLine("Robot without energy! Game over!");
            running = false;
        }
    }

    public static void Main() {

        Console.WriteLine("Enter the board dimension: ");
        int dimension = Convert.ToInt32(Console.ReadLine());

        Map map = new Map(dimension);

        Console.WriteLine("Insert the number of jewels: ");
        int n_jewels = Convert.ToInt32(Console.ReadLine());

        var charsToRemove = new string[] {",", "(", ")"};

        for(int i=0; i<n_jewels; i++){
            Console.WriteLine("Insert the type of jewel and position: ");
            string? jewel_position = Console.ReadLine();
            foreach (var c in charsToRemove)
            {
                jewel_position = jewel_position!.Replace(c, string.Empty);
            }
            string[] jewel_position_array = jewel_position!.Split();
            int x_position_jewel = Convert.ToInt32(jewel_position_array[2]);
            int y_position_jewel = Convert.ToInt32(jewel_position_array[3]);
            if(x_position_jewel<0 || y_position_jewel<0 || x_position_jewel>=dimension || y_position_jewel>=dimension){
                Console.WriteLine("Invalid position.");
                i = i - 1;
            }
            else{
                int[] position_jewel = new int[2] {x_position_jewel, y_position_jewel};
                map.insertJewel(jewel_position_array[0], position_jewel);
            }            
        }

        Console.WriteLine("Insert the number of obstacles: ");
        int n_obstacles = Convert.ToInt32(Console.ReadLine());

        for(int i=0; i<n_obstacles; i++){
            Console.WriteLine("Insert the type of obstacle and position: ");
            string? obstacle_position = Console.ReadLine();
            foreach (var c in charsToRemove)
            {
                obstacle_position = obstacle_position!.Replace(c, string.Empty);
            }
            string[] obstacle_position_array = obstacle_position!.Split();
            int x_position_obstacle = Convert.ToInt32(obstacle_position_array[2]);
            int y_position_obstacle = Convert.ToInt32(obstacle_position_array[3]);
            if(x_position_obstacle<0 || y_position_obstacle<0 || x_position_obstacle>=dimension || y_position_obstacle>=dimension){
                Console.WriteLine("Invalid position.");
                i = i - 1;
            }
            else{
                int[] position = new int[2] {x_position_obstacle, y_position_obstacle};
                map.insertObstacles(obstacle_position_array[0], position);
            }
        }

        Console.WriteLine("Insert the robot inicial position: ");
        string? robot_position = Console.ReadLine();
        foreach (var c in charsToRemove)
        {
            robot_position = robot_position!.Replace(c, string.Empty);
        }
        string[] robot_position_array = robot_position!.Split();
        int x_position = Convert.ToInt32(robot_position_array[0]);
        int y_position = Convert.ToInt32(robot_position_array[1]);
        if(x_position<0 || y_position<0 || x_position>=dimension || y_position>=dimension){
            Console.WriteLine("Invalid position.");
        }
        else{
            int[] position = new int[2] {x_position, y_position};
            map.insertRobot(position);
        }

        Console.WriteLine("Let's start!!");

        map.GameOver += EnterCommands;

        do {
            map.printMap();    
            Console.WriteLine("Enter the command: ");
            string? command = Console.ReadLine();

            if (command!.Equals("quit")) {
                running = false;
            } else if (command.Equals("w") || command.Equals("a") || command.Equals("s") || command.Equals("d")) {
                map.moveRobot(command);
            } else if (command.Equals("g")){
                map.useItem();
            }
        } while (running);
        
    }
}