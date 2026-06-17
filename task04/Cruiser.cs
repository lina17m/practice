namespace task04
{
    public class Cruiser : ISpaceship
    {
        public int Speed => 50;
        public int FirePower => 100;

        public int Fuel { get; private set; } = 1000;
        public int CurrentAngle { get; private set; }
        public int RocketCount { get; private set; } = 20;

        public void MoveForward()
        {
            if (Fuel >= 50)
            {
                Fuel -= 50;
            }
        }

        public void Rotate(int angle)
        {
            int actualRotation = angle;
            if (actualRotation > 45) actualRotation = 45;
            if (actualRotation < -45) actualRotation = -45;

            CurrentAngle = (CurrentAngle + actualRotation) % 360;
            if (CurrentAngle < 0) CurrentAngle += 360;
        }

        public void Fire()
        {
            if (RocketCount > 0)
            {
                RocketCount--;
            }
        }
    }
}
