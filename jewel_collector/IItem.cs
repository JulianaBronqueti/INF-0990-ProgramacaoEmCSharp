public interface IItem{

    public string ToString(){
        return "--";
    }

    public void useItem(Robot robot){}
    public bool isCollectable(){
        return false;
    }
    
}