using System.ComponentModel.DataAnnotations;

namespace SendMsgToRabbit.Enums
{
    public enum Accuracy
    {
        /// <summary>
        /// Дефолтное значение
        /// </summary>
        None = 0,

        /// <summary>
        /// Не удалось присвоить класс точности
        /// </summary>
        Undefined = 1,

        /// <summary>
        /// Класс точности не нормируется
        /// Погрешность измерения может превышать максимальную допускаемую
        /// </summary>
        [Display (Name = "the error is not normalized")]
        Class_Undefined = 2,

        /// <summary>
        /// Класс точности 0,2
        /// </summary>
        [Display(Name = "0,2")]
        Class_0_2 = 3,

        /// <summary>
        /// Класс точности 0,5
        /// </summary>
        [Display(Name = "0,5")]
        Class_0_5 = 4,

        /// <summary>
        /// Класс точности 1
        /// </summary>
        [Display (Name ="1,0")]
        Class_1 = 5,

        /// <summary>
        /// Класс точности 2
        /// </summary>
        [Display(Name = "2,0")]
        Class_2 = 6,

        /// <summary>
        /// Класс точности 5
        /// </summary>
        [Display(Name = "5,0")]
        Class_5 = 7,
    }
}
