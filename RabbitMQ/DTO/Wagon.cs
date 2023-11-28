using SendMsgToRabbit.Enums;
using System.Collections.Generic;

namespace SendMsgToRabbit.DTO
{
    /// <summary>
    /// Подвижная Единица в составе
    /// </summary>
    public class Wagon
    {
        /// <summary>
        /// Индекс подвижной единицы (номерация с 0)
        /// </summary>
        public int WagonIndex { get; init; }

        /// <summary>
        /// Тип подвижной единицы
        /// </summary>
        public  WagonType WagonType { get; init; }

        /// <summary>
        /// Класс точности результата измерения массы вагона 
        /// </summary>
        public Accuracy AccuracyClass { get; init; }

        /// <summary>
        /// Масса вагона, т (округленная до цены деления)
        /// </summary>
        public float WagonWeight { get; init; }

        /// <summary>
        /// Допускаемая погрешность измерения массы вагона
        /// </summary>
        public float MeasurementError { get; init; }

        /// <summary>
        /// Скорость вагона, км/ч
        /// </summary>
        public float WagonSpeed { get; init; }

        /// <summary>
        /// Ускорение вагона, м/с2
        /// </summary>
        public float WagonAcceleration { get; init; }

        /// <summary>
        /// Инвормация по осям
        /// </summary>
        public List<Axle> Axles { get; init; }

        /// <summary>
        /// Относительная разность сумм колес между левой и правой стороной ((сумма левых - сумма правых)/общую сумму)
        /// </summary>
        public float DifferenceLeftAndRightWheels { get; init; }

        /// <summary>
        /// Перечень причин из-за которых погрешность измерения не нормируется (в случае если погрешность не нормируются)
        /// </summary>
        public List<ReasonsIsNotNormalized> ReasonsIsNotNormalized { get; init; }

        /// <summary>
        /// Расшифровка причины ненормируемой погрешности с указанием влияющих факторов
        /// </summary>
        public List<string> InfoReasonsIsNotNormalized { get; init; }
    }
}
