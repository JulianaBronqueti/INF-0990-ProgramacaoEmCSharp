public class Robot{
    int[] position;
    JewelType[] bag;

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

    }

    public int getTotalJewels(){
        return 0;
    }

    public int getBagValue(){
        return 0;
    }
}