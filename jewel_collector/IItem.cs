/// <summary>
/// A interface IItem conecta todos os itens presentes no mapa (jóias, obstáculos, robô e espaços vazios). É feita para que o método de se salvar os itens em cada posição do mapa seja consistente.
/// </summary>
public interface IItem{

    /// <summary>
    /// O método ToString é um método que diz como o item tem que ser escrito no mapa (que é onde uso o método ToString para os itens).
    /// </summary>
    /// <returns>Por padrão retorna como um campo vazio, isso porque o método é sobrescrito pelas classes que extendem IItem.</returns>
    public string ToString(){
        return "--";
    }

    /// <summary>
    /// O método useItem vai definir como o item é usado. 
    /// </summary>
    /// <param name="robot">Como é o robô que vai interagir com os itens, geralmente esses impactam o robô, alterando alguma de suas propriedas. Portanto é necessário se ter acesso a instância de robô em questão.</param>
    public void useItem(Robot robot){}

    /// <summary>
    /// O método isCollectable indica se o item é coletável ou somente usável.
    /// </summary>
    /// <returns>Se o item for usável, retorna verdadeiro, caso contrário retorna falso.</returns>
    public bool isCollectable(){
        return false;
    }
    
}