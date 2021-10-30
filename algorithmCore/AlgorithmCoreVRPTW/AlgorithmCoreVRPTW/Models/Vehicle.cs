namespace AlgorithmCoreVRPTW.Models
{
    public class Vehicle
    {
        public Vehicle(int id, int capacity, int currentLoad, double currentTime)
        {
            Id = id;
            Capacity = capacity;
            CurrentLoad = currentLoad;
            CurrentTime = currentTime;
        }

        public Vehicle()
        {
        }

        public Vehicle Clone()
        {
            return new Vehicle()
            {
                Id = this.Id,
                Capacity = this.Capacity,
                CurrentLoad = this.CurrentLoad,
                CurrentTime = this.CurrentTime
            };
        }

        public int Id { get; set; }
        public int Capacity { get; set; }
        public int CurrentLoad { get; set; }
        public double CurrentTime { get; set; }
    }
}