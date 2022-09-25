public class Obstacle: IItem{
    int[] position;
    ObstacleType obstacle_type;

    public Obstacle(int[] position_input, ObstacleType obstacle_type_input){
        position = position_input;
        obstacle_type = obstacle_type_input;
    }

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

    public void useItem(Robot robot){
        if(obstacle_type == ObstacleType.Tree){
            robot.setEnergy(3);
        }
    }

    public bool isCollectable(){
        return false;
    }
}