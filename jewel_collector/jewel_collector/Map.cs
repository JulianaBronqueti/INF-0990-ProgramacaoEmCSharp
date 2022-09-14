public class Map{
    int size;
    string[,] map;
    List<Jewel> jewels = new List<Jewel>();
    List<Obstacle> obstacles = new List<Obstacle>();
    Robot robot;

    public Map(int size_input){
        size = size_input;
        map = new string[size, size];
        for(int i=0; i<size; i++){
            for(int j=0; j<size; j++){
                map[i,j] = "--";
            }
        }
    } 

    public void insertJewel(string jewel_input, int[] position){
        switch(jewel_input){
            case "Red":
                jewels.Add(new Jewel(position, JewelType.Red));
                map[position[0], position[1]] = "JR";
                Console.WriteLine("Red added");
                break;
            case "Green":
                jewels.Add(new Jewel(position, JewelType.Red));
                map[position[0], position[1]] = "JG";
                Console.WriteLine("Green added"); 
                break;
            case "Blue":
                jewels.Add(new Jewel(position, JewelType.Red));
                map[position[0], position[1]] = "JB";
                Console.WriteLine("Blue added");
                break;
            default:
                Console.WriteLine("Invalid Jewel");
                break;
        }
    }

    public void insertObstacles(string obstacle_input, int[] position){
        switch(obstacle_input){
            case "Water":
                obstacles.Add(new Obstacle(position, ObstacleType.Water));
                map[position[0], position[1]] = "##";
                Console.WriteLine("Water added");
                break;
            case "Tree":
                obstacles.Add(new Obstacle(position, ObstacleType.Tree));
                map[position[0], position[1]] = "$$";
                Console.WriteLine("Tree added");
                break;
            default:
                Console.WriteLine("Invalid Obstacle");
                break;
        }
    }

    public void insertRobot(int[] position){
        robot = new Robot(position);
        map[position[0], position[1]] = "ME";
    }

    public int getSize(){
        return size;
    }

    public void moveRobot(string command){
        int[] new_position = new int[2];
        int[] robot_position = robot.getPosition();

        if (command.Equals("w")) {
            new_position[0] = robot_position[0] - 1;
            new_position[1] = robot_position[1];
        } else if (command.Equals("a")) {
            new_position[0] = robot_position[0];
            new_position[1] = robot_position[1] - 1;
        } else if (command.Equals("s")) {
            new_position[0] = robot_position[0] + 1;
            new_position[1] = robot_position[1];
        } else if (command.Equals("d")) {
            new_position[0] = robot_position[0];
            new_position[1] = robot_position[1] + 1;        
        }

        if(new_position[0] < 0 || new_position[0] >= size || new_position[1] < 0 || new_position[1] >= size || map[new_position[0],new_position[1]] == "JR" || map[new_position[0],new_position[1]] == "JG" || map[new_position[0],new_position[1]] == "JB" || map[new_position[0],new_position[1]] == "##" || map[new_position[0],new_position[1]] == "$$"){
            Console.WriteLine("Can't move.");
        }
        else{
            map[robot_position[0], robot_position[1]] = "--";
            map[new_position[0], new_position[1]] = "ME";
            robot.setPosition(new_position);
        }
    }

    public void collectJewel(){
        int[] robot_position = robot.getPosition();
        for(int i = -1; i < 2; i++){
            for(int j = -1; j < 2; j++){
                if(robot_position[0]+i < 0 || robot_position[0]+i >= size || robot_position[1]+j < 0 || robot_position[1]+j >= size){
                    continue;
                }
                else{
                    if(map[robot_position[0]+i, robot_position[1]+j] == "JR"){
                        Console.WriteLine("aqui");
                        robot.collect(JewelType.Red);
                        map[robot_position[0]+i, robot_position[1]+j] = "--";
                    } else if(map[robot_position[0]+i, robot_position[1]+j] == "JG"){
                        robot.collect(JewelType.Green);
                        map[robot_position[0]+i, robot_position[1]+j] = "--";
                    } else if(map[robot_position[0]+i, robot_position[1]+j] == "JB"){
                        robot.collect(JewelType.Blue);
                        map[robot_position[0]+i, robot_position[1]+j] = "--";
                    }
                }
                
            }
        }
    }

    public void printMap(){
        for(int i=0; i<size; i++){
            for(int j=0; j<size; j++){
                Console.Write(String.Format("{0} ", map[i,j]));
            }
            Console.WriteLine();
        }
        Console.WriteLine($"Bag total items: {robot.getTotalJewels().ToString()} | Bag total value: {robot.getBagValue().ToString()}");
    }
}