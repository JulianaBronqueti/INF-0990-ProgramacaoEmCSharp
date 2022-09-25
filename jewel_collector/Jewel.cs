/// <summary>
/// A classe Jewel herda de IItem e implementa as jóias, guardando sua posição e tipo, além de métodos para se interagir com estas e obter informações sobre elas.
/// </summary>
public class Jewel: IItem{
    int[] position;
    JewelType jewel_type;

    public Jewel(int[] position_input, JewelType jewel_type_input){
        position = position_input;
        jewel_type = jewel_type_input;
    }

    /// <summary>
    /// O método ToString é um método herdado da interface IItem, que diz como o item tem que ser escrito no mapa (que é onde uso o método ToString para os itens).
    /// </summary>
    /// <returns>Como o item jóia tem que ser interpretado em string.</returns>
    public override string ToString(){
        switch(jewel_type){
            case JewelType.Red:
                return "JR";
            case JewelType.Green:
                return "JG";
            case JewelType.Blue:
                return "JB";
            default:
                return "--";            
        }
    }

    /// <summary>
    /// O método useItem é um método herdado da interface IItem, o qual vai definir como aquele item é usado. No caso das jóias, estas podem dar energia (jóia azul) e ser coletadas (todas a jóias). Portanto esse método verifica se a jóia é a azul ou não, e chama o método collect da classe Robot para coletar a jóia.
    /// </summary>
    /// <param name="robot">Como é o método collect da classe Robot que coleta as jóias, ou seja, as salva na mochila do robô, então é necessário que se tenha a instância de robô utilizada.</param>
    public void useItem(Robot robot){
        if(jewel_type == JewelType.Blue){
            robot.setEnergy(5);
        }
        robot.collect(jewel_type);
    }

    /// <summary>
    /// O método isCollectable é herdado da interface IItem, e indica se o item é coletável ou somente usável.
    /// </summary>
    /// <returns>Se o item for usável, retorna verdadeiro, caso contrário retorna falso. Como nesse caso estamos falando de jóias, essa classe sempre retorna verdadeiro nesse método.</returns>
    public bool isCollectable(){
        return true;
    }

    /// <summary>
    /// O método getType retorna o tipo da jóia em questão, entre vermelha, azul e verde.
    /// </summary>
    /// <returns>Tipo da jóia.</returns>
    public JewelType getType(){
        return jewel_type;
    }
}