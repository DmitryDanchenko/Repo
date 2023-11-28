using SendMsgToRabbit.Enums;
using System;
using System.Collections.Generic;

namespace SendMsgToRabbit.DTO
{
    public class TrainPassage
    {
        /// <summary>
        /// Модификация средства измерения
        /// </summary>
        public string ModificationOfSi { get; init; }

        /// <summary>
        /// Hомер ФИФ (Федеральный информационный фонд по обеспечению единства измерений)
        /// </summary>
        public int FifNumber { get; init; }

        /// <summary>
        /// Заводской номер весов
        /// </summary>
        public int ScaleNumber { get; init; }

        /// <summary>
        /// Дата поверки весов
        /// </summary>
        public string VerificationDate { get; init; }

        /// <summary>
        /// Дата окончаня срока действительности поверки
        /// </summary>
        public string DateExpiration { get; init; }

        /// <summary>
        /// Уникальный Id состава
        /// </summary>
        public long TrainId { get; init; }

        /// <summary>
        ///  Дата и время проезда состава
        /// </summary>
        public DateTime TrainPassageDate { get; init; }

        /// <summary>
        /// Направление движения состава
        /// </summary>
        public Direction DirectionOfMovement { get; init; }

        /// <summary>
        /// Список подвижных единиц
        /// </summary>
        public List<Wagon> Wagons { get; init; }

    }
}
