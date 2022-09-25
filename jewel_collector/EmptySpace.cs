/// <summary>
/// A classe EmptySpace herda de IItem e simboliza um espaço vazio no mapa.
/// </summary>
public class EmptySpace: IItem{

    public override string ToString(){
        return "--";
    }
}