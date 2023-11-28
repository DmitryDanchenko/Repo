namespace SendMsgToRabbit.Enums
{
    public enum ReasonsIsNotNormalized
    {
        /// <summary>
        /// Дефолтное значение
        /// </summary>
        None = 0,

        /// <summary>
        /// Выход  за пределы диапазона рабочих скоростей
        /// </summary>
        ExceedingSpeedLimit = 1,

        /// <summary>
        /// Неравномерность проезда
        /// </summary>
        UnevenPassage = 2,

        /// <summary>
        /// Наличие дефекта на поверхности катания (ДПК)
        /// </summary>
        SurfaceDefect = 3,

        /// <summary>
        /// Выход за пределы диапазона рабочих температур 
        /// </summary>
        ExceedingTemperature = 4,

        /// <summary>
        /// Превышение максимальной массы вагона
        /// </summary>
        ExceedingMaxWagonsWeight = 5
    }
}
