public class Map{
    int size;
    IItem[,] map;
    List<Jewel> jewels = new List<Jewel>();
    List<Obstacle> obstacles = new List<Obstacle>();
    Robot? robot;

    public Map(int size_input){
        size = size_input;
        map = new IItem[size, size];
        for(int i=0; i<size; i++){
            for(int j=0; j<size; j++){
                map[i,j] = new EmptySpace();
            }
        }
    } 

    public void insertJewel(string jewel_input, int[] position){
        switch(jewel_input){
            case "Red":
                // jewels.Add(new Jewel(position, JewelType.Red));
                map[position[0], position[1]] = new Jewel(position, JewelType.Red);
                Console.WriteLine("Red added");
                break;
            case "Green":
                // jewels.Add(new Jewel(position, JewelType.Red));
                map[position[0], position[1]] = new Jewel(position, JewelType.Green);
                Console.WriteLine("Green added"); 
                break;
            case "Blue":
                // jewels.Add(new Jewel(position, JewelType.Red));
                map[position[0], position[1]] = new Jewel(position, JewelType.Blue);
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
                // obstacles.Add(new Obstacle(position, ObstacleType.Water));
                map[position[0], position[1]] = new Obstacle(position, ObstacleType.Water);
                Console.WriteLine("Water added");
                break;
            case "Tree":
                // obstacles.Add(new Obstacle(position, ObstacleType.Tree));
                map[position[0], position[1]] = new Obstacle(position, ObstacleType.Tree);
                Console.WriteLine("Tree added");
                break;
            case "Radioactive":
                // obstacles.Add(new Obstacle(position, ObstacleType.Tree));
                map[position[0], position[1]] = new Obstacle(position, ObstacleType.Radioactive);
                Console.WriteLine("Radioactive added");
                break;
            default:
                Console.WriteLine("Invalid Obstacle");
                break;
        }
    }

    public void insertRobot(int[] position){
        robot = new Robot(position);
        map[position[0], position[1]] = new Robot(position);
    }

    public int getSize(){
        return size;
    }

    void checkEmptyPosition(int x, int y){
        if(map[x,y].GetType().ToString() != "EmptySpace"){
            throw new InvalidDataException();
        }
    }

    public void moveRobot(string command){
        if(robot!.getEnergy() >= 1){
            int[] new_position = new int[2];
            int[] robot_position = robot!.getPosition();

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

            /// <summary>
            /// Como pedido no enunciado foi usado o tratamento por exceções para quando o robô tenta se deslocar para uma posição fora dos limites do mapa e quando o robô tenta se deslocar para uma posição ocupada por outro item.
            /// </summary>
            /// <value></value>
            try{
                checkEmptyPosition(new_position[0], new_position[1]);
                map[robot_position[0], robot_position[1]] = map[new_position[0], new_position[1]];
                map[new_position[0], new_position[1]] = robot;
                robot.setPosition(new_position);
                robot.setEnergy(-1);
            }
            catch(IndexOutOfRangeException){
                Console.WriteLine("Can't move out of the map.");
            }
            catch(InvalidDataException){
                Console.WriteLine("Can't move, item (jewel or obstacle) ahead.");
            }

            // if(new_position[0] < 0 || new_position[0] >= size || new_position[1] < 0 || new_position[1] >= size || map[new_position[0],new_position[1]].GetType().ToString() != "EmptySpace"){
            //     Console.WriteLine("Can't move.");
            // }
            // else{
            //     map[robot_position[0], robot_position[1]] = map[new_position[0], new_position[1]];
            //     map[new_position[0], new_position[1]] = robot;
            //     robot.setPosition(new_position);
            //     robot.setEnergy(-1);
            // }
        }
        else{
            Console.WriteLine("Robot without energy! Game over!");
        }
    }

    public void useItem(){
        int[] robot_position = robot!.getPosition();
        for(int i = -1; i < 2; i++){
            for(int j = -1; j < 2; j++){
                if(robot_position[0]+i < 0 || robot_position[0]+i >= size || robot_position[1]+j < 0 || robot_position[1]+j >= size){
                    continue;
                }
                else{
                    map[robot_position[0]+i, robot_position[1]+j].useItem(robot);
                    if(map[robot_position[0]+i, robot_position[1]+j].isCollectable()){
                        map[robot_position[0]+i, robot_position[1]+j] = new EmptySpace();
                    }
                    // if(map[robot_position[0]+i, robot_position[1]+j].GetType().ToString() == "JR"){
                    //     robot.collect(JewelType.Red);
                        
                    // } else if(map[robot_position[0]+i, robot_position[1]+j].GetType().ToString() == "JG"){
                    //     robot.collect(JewelType.Green);
                    //     map[robot_position[0]+i, robot_position[1]+j] = new EmptySpace();
                    // } else if(map[robot_position[0]+i, robot_position[1]+j].GetType().ToString() == "JB"){
                    //     robot.collect(JewelType.Blue);
                    //     robot.setEnergy(5);
                    //     map[robot_position[0]+i, robot_position[1]+j] = new EmptySpace();
                    // } else if(map[robot_position[0]+i, robot_position[1]+j].GetType().ToString() == "$$"){
                    //     robot.setEnergy(3);
                    // }
                }
                
            }
        }
    }

    // public void collectJewel(){
    //     int[] robot_position = robot!.getPosition();
    //     for(int i = -1; i < 2; i++){
    //         for(int j = -1; j < 2; j++){
    //             if(robot_position[0]+i < 0 || robot_position[0]+i >= size || robot_position[1]+j < 0 || robot_position[1]+j >= size){
    //                 continue;
    //             }
    //             else{
    //                 if(map[robot_position[0]+i, robot_position[1]+j] == "JR"){
    //                     robot.collect(JewelType.Red);
    //                     map[robot_position[0]+i, robot_position[1]+j] = "--";
    //                 } else if(map[robot_position[0]+i, robot_position[1]+j] == "JG"){
    //                     robot.collect(JewelType.Green);
    //                     map[robot_position[0]+i, robot_position[1]+j] = "--";
    //                 } else if(map[robot_position[0]+i, robot_position[1]+j] == "JB"){
    //                     robot.collect(JewelType.Blue);
    //                     map[robot_position[0]+i, robot_position[1]+j] = "--";
    //                 }
    //             }
                
    //         }
    //     }
    // }

    public void printMap(){
        for(int i=0; i<size; i++){
            for(int j=0; j<size; j++){
                Console.Write(String.Format("{0} ", map[i,j].ToString()));
            }
            Console.WriteLine();
        }
        Console.WriteLine($"Robot energy: {robot!.getEnergy()}");
        Console.WriteLine($"Bag total items: {robot!.getTotalJewels().ToString()} | Bag total value: {robot.getBagValue().ToString()}");
    }
}