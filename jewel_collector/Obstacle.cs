/// <summary>
/// A classe Obstacle herda de IItem e implementa os obstáculos, guardando sua posição e tipo, além de métodos para se interagir com estes e obter informações sobre eles.
/// </summary>
public class Obstacle: IItem{
    int[] position;
    ObstacleType obstacle_type;

    public Obstacle(int[] position_input, ObstacleType obstacle_type_input){
        position = position_input;
        obstacle_type = obstacle_type_input;
    }

    /// <summary>
    /// O método ToString é um método herdado da interface IItem, que diz como o item tem que ser escrito no mapa (que é onde uso o método ToString para os itens).
    /// </summary>
    /// <returns>Como o item obstáculo tem que ser interpretado em string.</returns>
    public override string ToString(){
        switch(obstacle_type){
            case ObstacleType.Tree:
                return "$$";
            case ObstacleType.Water:
                return "##";
            case ObstacleType.Radioactive:
                return "!!";
            default:
                return "--";            
        }
    }

    /// <summary>
    /// O método useItem é um método herdado da interface IItem, o qual vai definir como aquele item é usado. No caso dos obstáculos, estes podem dar energia (obstáculo árvore). Portanto esse método verifica se o obstáculo é a árvore ou não, chamando o método setEnergy da classe Robot se positivo, para dar energia ao robô.
    /// </summary>
    /// <param name="robot">Como é o método setEnergy da classe Robot que atualiza a energia do robô, então é necessário que se tenha a instância de robô utilizada.</param>
    public void useItem(Robot robot){
        if(obstacle_type == ObstacleType.Tree){
            robot.setEnergy(3);
        }
    }

    /// <summary>
    /// O método isCollectable é herdado da interface IItem, e indica se o item é coletável ou somente usável.
    /// </summary>
    /// <returns>Se o item for usável, retorna verdadeiro, caso contrário retorna falso. Como nesse caso estamos falando de obstáculos, essa classe sempre retorna falso nesse método.</returns>
    public bool isCollectable(){
        return false;
    }
}