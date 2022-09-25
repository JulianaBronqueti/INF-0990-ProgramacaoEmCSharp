public class Robot: IItem{
    int[] position;
    List<JewelType> bag = new List<JewelType>();
    int energy = 5;

    public Robot(int[] position_input){
        position = position_input;
    }

    public int[] getPosition(){
        return position;
    }

    public void setPosition(int [] new_position){
        position = new_position;
    }

    public void setEnergy(int value){
        energy += value;
    }

    public int getEnergy(){
        return energy;
    }

    public void collect(JewelType jewel){
        bag.Add(jewel);
    }

    public int getTotalJewels(){
        return bag.Count;        
    }

    public int getBagValue(){
        int total = 0;
        foreach(JewelType jewel in bag){
            total += (int)jewel;
        }
        return total;
    }

    public override string ToString(){
        return "ME";
    }
}