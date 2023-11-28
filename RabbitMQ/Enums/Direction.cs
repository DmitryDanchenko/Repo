namespace SendMsgToRabbit.Enums
{
    public enum Direction
    {
        /// <summary>
        /// Дефолтное значение
        /// </summary>
        None = 0,

        /// <summary>
        /// Движение в прямом направлении
        /// </summary>
        Forvard = 1,

        /// <summary>
        /// Движение в обратном направлении
        /// </summary>
        Reverse = 2,

        /// <summary>
        /// Направление движения не определенно
        /// </summary>
        Undefined = 3
    }
}
