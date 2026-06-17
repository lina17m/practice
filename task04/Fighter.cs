namespace task04
{
    public class Fighter : ISpaceship
    {
        public int Speed => 100;
        public int FirePower => 20;

        public int EngineFatigue { get; private set; }
        public int CurrentAngle { get; private set; }
        public int HeatLevel { get; private set; }

        public void MoveForward()
        {
            EngineFatigue += 10; 
        }

        public void Rotate(int angle)
        {
            CurrentAngle = (CurrentAngle + angle) % 360;
            if (CurrentAngle < 0) CurrentAngle += 360;
        }

        public void Fire()
        {
            if (HeatLevel < 100)
            {
                HeatLevel += 10;
            }
        }
    }
}
