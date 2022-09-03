public class Obstacle{
    int[] position;
    ObstacleType obstacle_type;

    public Obstacle(int[] position_input, ObstacleType obstacle_type_input){
        position = position_input;
        obstacle_type = obstacle_type_input;
    }
}