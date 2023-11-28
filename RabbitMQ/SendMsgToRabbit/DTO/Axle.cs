namespace SendMsgToRabbit.DTO
{
    public class Axle
    {
        /// <summary>
        /// Индекс оси в вагоне (в каждом вагоне индексация с 0) 
        /// </summary>
        public int AxleIndex { get; init; }

        /// <summary>
        /// Расстояние до предыдущей оси, м
        /// </summary>
        public float DistanceToPreviousAxle { get; init; }

        /// <summary>
        /// Информация по левому колесу
        /// </summary>
        public Wheel LeftWheel { get; init; }

        /// <summary>
        /// Информация по правому колесу
        /// </summary>
        public Wheel RightWheel { get; init; }
    }
}
