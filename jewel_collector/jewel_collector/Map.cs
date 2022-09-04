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