public class Robot{
    int[] position;
    List<JewelType> bag = new List<JewelType>();

    public Robot(int[] position_input){
        position = position_input;
    }

    public int[] getPosition(){
        return position;
    }

    public void setPosition(int [] new_position){
        position = new_position;
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
}