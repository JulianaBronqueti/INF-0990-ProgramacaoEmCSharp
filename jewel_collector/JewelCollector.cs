/*! \mainpage Juliana Bronqueti - Projeto INF0990 - Curso Tecnologias Microsoft
 *
 * \section intro_sec Introduction
 *
 * Projeto apresentado a matéria INF-0990 - Programação em C# do curso de Tecnologias Microsoft do Instituto de Computação da UNICAMP.
 * Trata-se do jogo JewelCollector, cujas especificações estão nas páginas \link https://moodle.lab.ic.unicamp.br/moodle/mod/page/view.php?id=14149 \endlink e \link https://moodle.lab.ic.unicamp.br/moodle/mod/page/view.php?id=14181 \endlink
 *
 * \section install_sec Observações
 *
 * O código foi feito para coletar o tabuleiro do usuário. Para o caso de teste dado na descrição de trabalho, o tipo da entrada aceita pelo programa é:

    10\n
    6\n
    Red - (1, 9)\n
    Red - (8, 8)\n
    Green - (9, 1)\n
    Green - (7, 6)\n
    Blue - (3, 4)\n
    Blue - (2, 1)\n
    12\n
    Water - (5, 0)\n
    Water - (5, 1)\n
    Water - (5, 2)\n
    Water - (5, 3)\n
    Water - (5, 4)\n
    Water - (5, 5)\n
    Water - (5, 6)\n
    Tree - (5, 9)\n
    Tree - (3, 9)\n
    Tree - (8, 3)\n
    Tree - (2, 5)\n
    Tree - (1, 4)\n
    (0, 0)\n


    Colocando essa entrada desta forma será possível iniciar o jogo.
 *
 */

/// <summary>
/// JewelCollector: classe de início do programa, a qual receberá os parametros do usuário para criação do tabuleiro e instanciará os principais elementos do jogo (mapa e robô).
/// </summary>
public class JewelCollector {

    static bool running = true;
    static Map? map;

    /// <summary>
    /// O método EnterCommands será chamado assim que houver um disparo de evento que indica que por algum motivo o jogo acabou, seja porque o jogador ganhou, seja porque sua energia acabou.
    /// </summary>
    /// <param name="reason">O parâmetro reason indica justamente o motivo pelo qual o jogo acabou.</param>
    static void EnterCommands(string reason){
        if(reason == "win"){
            Console.WriteLine("You win! Congratulations!");
            Console.WriteLine("New Game: ");            
            randomMap(); 
            running = true;
        }
        else if(reason == "energy"){
            Console.WriteLine("Robot without energy! Game over!");
            running = false;
        }
    }

    /// <summary>
    /// O método randomMap gera um mapa aleatório assim que a fase inicial é concluída. Neste o mapa aumenta suas dimensões em 1 unidade, até o limite máximo de (30, 30) unidades e a quantidade de jóias aumenta proporcionalmente.
    /// </summary>
    static void randomMap(){
        Random rnd = new Random();
        // A cada nova fase, o mapa aumenta suas dimensões em 1 unidade, até o limite máximo de (30, 30) unidades
        int dimension = map!.getSize()+1;
        if(dimension <= 30){
            map = new Map(dimension);
        }
        else{
            map = new Map(30);
        }
        
        // Quantidade de jóias: mínimo 20% e máximo 70% da dimensão do board
        int max_jewels = 7*dimension/10;
        int min_jewels = 2*dimension/10;
        int n_jewels = rnd.Next(min_jewels, max_jewels);

        for(int i=0; i<n_jewels; i++){
            int x_position_jewel = rnd.Next(0, dimension-1);
            int y_position_jewel = rnd.Next(1, dimension-1);
            int[] position_jewel = new int[2] {x_position_jewel, y_position_jewel};

            int jewel_type_number = rnd.Next(0,3);
            string jewel_type;
            switch(jewel_type_number){
                case 0:
                    jewel_type = "Red";
                    break;
                case 1:
                    jewel_type = "Green";
                    break;
                case 2:
                    jewel_type = "Blue";
                    break;
                default:
                    jewel_type = "Red";
                    break;
            }
            map.insertJewel(jewel_type, position_jewel);
        }

        // Quantidade de obstáculos: mínimo 1, máximo metade da dimensão do board
        int max_obstacles = dimension/2;
        int n_obstacles = rnd.Next(1, max_jewels);

        for(int i=0; i<n_obstacles; i++){
            // As posições dos obstáculos são no mínimo 2,2 para que o robô possa se movimentar
            int x_position_obstacle = rnd.Next(2, dimension-1);
            int y_position_obstacle = rnd.Next(2, dimension-1);
            int[] position_obstacle = new int[2] {x_position_obstacle, y_position_obstacle};

            int obstacle_type_number = rnd.Next(0,3);
            string obstacle_type;
            switch(obstacle_type_number){
                case 0:
                    obstacle_type = "Water";
                    break;
                case 1:
                    obstacle_type = "Tree";
                    break;
                case 2:
                    obstacle_type = "Radioactive";
                    break;
                default:
                    obstacle_type = "Water";
                    break;
            }
            map.insertObstacles(obstacle_type, position_obstacle);
        }

        // Robô sempre iniciará na posição 0,0
        int[] position = new int[2] {0, 0};
        map.insertRobot(position);

        Console.WriteLine("Let's start!!");
        map.GameOver += EnterCommands;
    }

    /// <summary>
    /// O método newMap o mapa inicial a partir das entradas do usuário. No arquivo READ.ME tem um exemplo das entradas a serem colocadas para gerar esse mapa, conforme especificado no roteiro do projeto.
    /// </summary>
    static void newMap(){
        Console.WriteLine("Enter the board dimension: ");
        int dimension = Convert.ToInt32(Console.ReadLine());

        map = new Map(dimension);

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
    }

    /// <summary>
    /// O método Main inicializará o programa. Deixei a cargo da Main chamar a função newMap() para gerar o mapa inicial, assim como subscrever o EnterCommands no evento GameOver da classe Map. Além disso, este método é responsável por fazer a coleta das entradas do usuário para interação do robô com o mapa.
    /// </summary>
    public static void Main() {
        newMap();
        map!.GameOver += EnterCommands;

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