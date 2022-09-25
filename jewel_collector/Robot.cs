/// <summary>
/// A classe Robot herda de IItem e implementa o robô, guardando sua posição, mochila (lista de jóias coletada) e nível de energia.
/// </summary>
public class Robot: IItem{
    int[] position;
    List<JewelType> bag = new List<JewelType>();
    int energy = 5;

    public Robot(int[] position_input){
        position = position_input;
    }

    /// <summary>
    /// O método getPosition retorna a posição atual do robô.
    /// </summary>
    /// <returns>Posição atual do robô.</returns>
    public int[] getPosition(){
        return position;
    }

    /// <summary>
    /// O método setPosition atualiza a posição atual do robô.
    /// </summary>
    /// <param name="new_position">O parâmetro indica a posição para a qual se tem que atualizar.</param>
    public void setPosition(int [] new_position){
        position = new_position;
    }

    /// <summary>
    /// O método setPosition atualiza a energia atual do robô.
    /// </summary>
    /// <param name="value">O parâmetro indica quanto se tem que incrementar ou decrementar da energia atual.</param>
    public void setEnergy(int value){
        energy += value;
    }

    /// <summary>
    /// O método getEnergy retorna a quantidade de energia atual do robô.
    /// </summary>
    /// <returns>Energia atual do robô.</returns>
    public int getEnergy(){
        return energy;
    }

    /// <summary>
    /// O método collect implementa a coleta da jóia e coloca esta na mochila do robô.
    /// </summary>
    /// <param name="jewel">Jóia a ser guardada.</param>
    public void collect(JewelType jewel){
        bag.Add(jewel);
    }

    /// <summary>
    /// O método getTotalJewels serve para saber a quantidade de jóias coletadas até então.
    /// </summary>
    /// <returns>Quantidade de jóias coletadas.</returns>
    public int getTotalJewels(){
        return bag.Count;        
    }

    /// <summary>
    /// O método getBagValue serve para saber o valor total das jóias coletadas até então.
    /// </summary>
    /// <returns>Valor total das jóias coletadas.</returns>
    public int getBagValue(){
        int total = 0;
        foreach(JewelType jewel in bag){
            total += (int)jewel;
        }
        return total;
    }

    /// <summary>
    /// O método ToString é um método herdado da interface IItem, que diz como o item tem que ser escrito no mapa (que é onde uso o método ToString para os itens).
    /// </summary>
    /// <returns>Como o item robô tem que ser interpretado em string.</returns>
    public override string ToString(){
        return "ME";
    }
}