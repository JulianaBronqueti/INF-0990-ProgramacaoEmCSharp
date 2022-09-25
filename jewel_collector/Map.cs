public delegate void GameOverHandler(string reason);

/// <summary>
/// A classe Map implementa o mapa, com a criação deste e a inserção das jóias e dos obstáculos. Além disso, é responsável por gerenciar a movimentação do robô (verificar se é válida ou não) e verificar se este pode utilizar algum item que está a seu alcance. Como principal funcionalidade é responsável também por imprimir o estado atual do mapa.
/// </summary>
public class Map{
    int size;
    IItem[,] map;
    /// <summary>
    /// Como pedido no enunciado, estou usando uma coleção para salvar as jóias presentes no mapa. Quando essa lista estiver vazia, o usuário irá para a próxima fase do jogo.
    /// </summary>
    /// <typeparam name="JewelType">O tipo de cada item da lista será JewelType, que contém o tipo da jóia em questão.</typeparam>
    List<JewelType> jewels = new List<JewelType>();
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

    /// <summary>
    /// O método insertJewel é responsável por inserir a jóia na posição, ambas dadas nos argumentos, no mapa. 
    /// </summary>
    public void insertJewel(string jewel_input, int[] position){
        if(map[position[0], position[1]].ToString() == "--"){
            switch(jewel_input){
                case "Red":
                    jewels.Add(JewelType.Red);
                    map[position[0], position[1]] = new Jewel(position, JewelType.Red);
                    Console.WriteLine("Red added");
                    break;
                case "Green":
                    jewels.Add(JewelType.Green);
                    map[position[0], position[1]] = new Jewel(position, JewelType.Green);
                    Console.WriteLine("Green added"); 
                    break;
                case "Blue":
                    jewels.Add(JewelType.Blue);
                    map[position[0], position[1]] = new Jewel(position, JewelType.Blue);
                    Console.WriteLine("Blue added");
                    break;
                default:
                    Console.WriteLine("Invalid Jewel");
                    break;
            }
        }
    }

    /// <summary>
    /// O método insertObstacles é responsável por inserir o obstáculo na posição, ambas dadas nos argumentos, no mapa.
    /// </summary>
    public void insertObstacles(string obstacle_input, int[] position){
        if(map[position[0], position[1]].ToString() == "--"){
            switch(obstacle_input){
                case "Water":
                    map[position[0], position[1]] = new Obstacle(position, ObstacleType.Water);
                    Console.WriteLine("Water added");
                    break;
                case "Tree":
                    map[position[0], position[1]] = new Obstacle(position, ObstacleType.Tree);
                    Console.WriteLine("Tree added");
                    break;
                case "Radioactive":
                    map[position[0], position[1]] = new Obstacle(position, ObstacleType.Radioactive);
                    Console.WriteLine("Radioactive added");
                    break;
                default:
                    Console.WriteLine("Invalid Obstacle");
                    break;
            }
        }
    }

    public void insertRobot(int[] position){
        robot = new Robot(position);
        map[position[0], position[1]] = new Robot(position);
    }

    public int getSize(){
        return size;
    }

    /// <summary>
    /// O método checkPosition é responsável por conferir a posição para onde o robô vai, e verificar se está próximo de um elemento radioativo (perdendo 10 de energia), assim como se vai para a própria posição do elemento radioativo (perdendo 30 de energia) ou se está tentando ir para uma posição onde já há uma jóia ou obstáculo (lançando uma exceção que será tratada no método moveRobot da classe Map).
    /// </summary>
    void checkPosition(int x, int y){
        for(int i = -1; i < 2; i++){
            for(int j = -1; j < 2; j++){
                if(x+i < 0 || x+i >= size || y+j < 0 || y+j >= size){
                    continue;
                }
                else if(map[x+i, y+j].ToString() == "!!"){
                    robot!.setEnergy(-10);
                }
            }
        }
        if(map[x,y].ToString() == "!!"){
            robot!.setEnergy(-30);
            map[x,y] = new EmptySpace();
        }
        else if(map[x,y].GetType().ToString() != "EmptySpace"){
            throw new InvalidDataException();
        }
    }

    /// <summary>
    /// O método moveRobot é responsável por, a partir do comando dado como argumento, verificar se o robô ainda pode se movimentar (energia > 0) e se a nova posição é válida. Nesse método é utilizado tratamento de exceções justamente para lidar com o robô tentando passar dos limites do mapa ou ir para uma posição já ocupada. Como esse método verifica o nível de energia do robô, se este for menor ou igual a 0, ele é responsável por disparar o evento de GameOver.
    /// </summary>
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
            try{
                checkPosition(new_position[0], new_position[1]);
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
        }
        if(robot.getEnergy() < 1){
            GameOver!("energy");
        }
    }

    /// <summary>
    /// GameOver é a variável de evento que será acionada quando houver perda ou ganho do jogo.
    /// </summary>
    public event GameOverHandler? GameOver;

    /// <summary>
    /// O método useItem é responsável por, a partir da posição do robô, verificar se há alguma posição adjacente a ele que exista um item usável, como as jóias que são coletadas e a jóia azul e o obstáculo árvore que podem ser usados para recuperar energia. É possível trabalhar dessa forma, pois estou usando iterface IItem para trabalhar com as jóias e os obstáculos conjuntamente. Como esse método coleta as jóias, removendo-as do mapa, consegue verificar se já foram todas coletadas, e em caso positivo pode disparar o evento de GameOver como ganho pelo jogador, podendo ir para a próxima fase.
    /// </summary>
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
                        Jewel jewel_to_remove = (Jewel)map[robot_position[0]+i, robot_position[1]+j];
                        jewels.Remove(jewel_to_remove.getType());
                        map[robot_position[0]+i, robot_position[1]+j] = new EmptySpace();
                        if(jewels.Count == 0){
                            GameOver!("win");
                        }   
                    }
                }
                
            }
        }
    }

    /// <summary>
    /// O método printMap é responsável por imprimir o mapa, se baseando em que tipo de item tem em cada posição da matriz. Além disso, imprime a quantidade de energia do robô, quantidade de itens na mochila do robô e o valor total das jóias coletadas.
    /// </summary>
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