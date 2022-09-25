public class Jewel: IItem{
    int[] position;
    JewelType jewel_type;

    public Jewel(int[] position_input, JewelType jewel_type_input){
        position = position_input;
        jewel_type = jewel_type_input;
    }

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

    public void useItem(Robot robot){
        if(jewel_type == JewelType.Blue){
            robot.setEnergy(5);
        }
        robot.collect(jewel_type);
    }

    public bool isCollectable(){
        return true;
    }
}