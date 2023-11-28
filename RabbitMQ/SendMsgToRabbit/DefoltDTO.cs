using System;
using System.Collections.Generic;

using SendMsgToRabbit.DTO;
using SendMsgToRabbit.Enums;

namespace SendMsgToRabbit
{
    /// <summary>
    /// Создает пример DTO
    /// </summary>
    public class DefoltTrain
    {
        public TrainPassage CreateDto()
        {
            var defoltDto = new TrainPassage()
            {
                ModificationOfSi = "Cv432",
                FifNumber = 2343224,
                ScaleNumber = 6579283,
                VerificationDate = "10.10.2021",
                DateExpiration = "10.10.2022",
                TrainId = 192893,
                TrainPassageDate = new DateTime(),
                DirectionOfMovement = Direction.Forvard,
                Wagons = new List<Wagon>()
            };

            var wagon = new Wagon()
            {
                WagonIndex = 0,
                WagonType = WagonType.Locomotive,
                AccuracyClass = Accuracy.Class_1,
                WagonWeight = 101000,
                MeasurementError = 2000,
                WagonSpeed = 40,
                WagonAcceleration = 0.2F,
                Axles = new List<Axle>()
                {
                    new Axle()
                    {
                        AxleIndex = 0,
                        DistanceToPreviousAxle = 0,
                        LeftWheel = new Wheel(){ WheelLoad = 10000 },
                        RightWheel = new Wheel(){ WheelLoad = 10000 }
                    },
                    new Axle()
                    {
                        AxleIndex = 1,
                        DistanceToPreviousAxle = 1,
                        LeftWheel = new Wheel(){ WheelLoad = 10200 },
                        RightWheel = new Wheel(){ WheelLoad = 10100 }
                    },
                    new Axle()
                    {
                        AxleIndex = 2,
                        DistanceToPreviousAxle = 2,
                        LeftWheel = new Wheel(){ WheelLoad = 10000 },
                        RightWheel = new Wheel(){ WheelLoad = 10000 }
                    },
                    new Axle()
                    {
                        AxleIndex = 3,
                        DistanceToPreviousAxle = 1,
                        LeftWheel = new Wheel(){ WheelLoad = 10000 },
                        RightWheel = new Wheel(){ WheelLoad = 10000 }
                    },
                }
            };
            defoltDto.Wagons.Add(wagon);

            wagon = new Wagon()
            {
                WagonIndex = 1,
                WagonType = WagonType.Wagon,
                AccuracyClass = Accuracy.Class_2,
                WagonWeight = 40000,
                MeasurementError = 1000,
                WagonSpeed = 43,
                WagonAcceleration = 0.1F,
                DifferenceLeftAndRightWheels = 1000,
                ReasonsIsNotNormalized = new List<ReasonsIsNotNormalized>()
                {
                    ReasonsIsNotNormalized.ExceedingSpeedLimit,
                    ReasonsIsNotNormalized.ExceedingTemperature
                },
                Axles = new List<Axle>()
                {
                    new Axle()
                    {
                        AxleIndex = 0,
                        DistanceToPreviousAxle = 0,
                        LeftWheel = new Wheel(){ WheelLoad = 5000 },
                        RightWheel = new Wheel(){ WheelLoad = 5000 }
                    },

                    new Axle()
                    {
                        AxleIndex = 1,
                        DistanceToPreviousAxle = 1,
                        LeftWheel = new Wheel(){ WheelLoad = 5200 },
                        RightWheel = new Wheel(){ WheelLoad = 5100 }
                    },

                    new Axle()
                    {
                        AxleIndex = 2,
                        DistanceToPreviousAxle = 2,
                        LeftWheel = new Wheel(){ WheelLoad = 5000 },
                        RightWheel = new Wheel(){ WheelLoad = 5000 }
                    },

                    new Axle()
                    {
                        AxleIndex = 3,
                        DistanceToPreviousAxle = 1,
                        LeftWheel = new Wheel(){ WheelLoad = 5000 },
                        RightWheel = new Wheel(){ WheelLoad = 5000 }
                    }
                }
            };
            defoltDto.Wagons.Add(wagon);


            wagon = new Wagon()
            {
                WagonIndex = 1,
                WagonType = WagonType.Wagon,
                AccuracyClass = Accuracy.Class_5,
                WagonWeight = 60000,
                MeasurementError = 1000,
                WagonSpeed = 43,
                WagonAcceleration = 0.1F,
                DifferenceLeftAndRightWheels = 600,
                ReasonsIsNotNormalized =
                    new List<ReasonsIsNotNormalized>()
                    {
                    ReasonsIsNotNormalized.ExceedingSpeedLimit,
                    ReasonsIsNotNormalized.ExceedingTemperature
                    },
                Axles = new List<Axle>()
                {
                    new Axle()
                    {
                        AxleIndex = 0,
                        DistanceToPreviousAxle = 0,
                        LeftWheel = new Wheel(){ WheelLoad = 6000 },
                        RightWheel = new Wheel(){ WheelLoad = 6000 }
                    },
                    new Axle()
                    {
                        AxleIndex = 1,
                        DistanceToPreviousAxle = 1,
                        LeftWheel = new Wheel(){ WheelLoad = 6200 },
                        RightWheel = new Wheel(){ WheelLoad = 6100 }
                    },
                    new Axle()
                    {
                        AxleIndex = 2,
                        DistanceToPreviousAxle = 2,
                        LeftWheel = new Wheel(){ WheelLoad = 6000 },
                        RightWheel = new Wheel(){ WheelLoad = 6000 }
                    },
                    new Axle()
                    {
                        AxleIndex = 3,
                        DistanceToPreviousAxle = 1,
                        LeftWheel = new Wheel(){ WheelLoad = 6000 },
                        RightWheel = new Wheel(){ WheelLoad = 6000 }
                    }
                }
            };
            defoltDto.Wagons.Add(wagon);

            wagon = new Wagon()
            {
                WagonIndex = 2,
                WagonType = WagonType.Wagon,
                AccuracyClass = Accuracy.Class_0_2,
                WagonWeight = 60000,
                MeasurementError = 1000,
                WagonSpeed = 44,
                WagonAcceleration = 0.4F,
                DifferenceLeftAndRightWheels = 200,
                ReasonsIsNotNormalized =
                        new List<ReasonsIsNotNormalized>()
                        {
                        ReasonsIsNotNormalized.ExceedingSpeedLimit,
                        ReasonsIsNotNormalized.ExceedingTemperature
                        },
                Axles = new List<Axle>()
                {
                    new Axle()
                    {
                        AxleIndex = 0,
                        DistanceToPreviousAxle = 0,
                        LeftWheel = new Wheel(){ WheelLoad = 6000 },
                        RightWheel = new Wheel(){ WheelLoad = 6000 }
                    },
                    new Axle()
                    {
                        AxleIndex = 1,
                        DistanceToPreviousAxle = 1,
                        LeftWheel = new Wheel(){ WheelLoad = 6200 },
                        RightWheel = new Wheel(){ WheelLoad = 6100 }
                    },
                    new Axle()
                    {
                        AxleIndex = 2,
                        DistanceToPreviousAxle = 2,
                        LeftWheel = new Wheel(){ WheelLoad = 6000 },
                        RightWheel = new Wheel(){ WheelLoad = 6000 }
                    },
                    new Axle()
                    {
                        AxleIndex = 3,
                        DistanceToPreviousAxle = 1,
                        LeftWheel = new Wheel(){ WheelLoad = 6000 },
                        RightWheel = new Wheel(){ WheelLoad = 6000 }
                    }
                }
            };
            defoltDto.Wagons.Add(wagon);

            wagon = new Wagon()
            {
                WagonIndex = 3,
                WagonType = WagonType.Wagon,
                AccuracyClass = Accuracy.Class_5,
                WagonWeight = 60000,
                MeasurementError = 1000,
                WagonSpeed = 43,
                WagonAcceleration = 0.1F,
                DifferenceLeftAndRightWheels = 600,
                ReasonsIsNotNormalized =
                    new List<ReasonsIsNotNormalized>()
                    {
                    ReasonsIsNotNormalized.ExceedingSpeedLimit,
                    ReasonsIsNotNormalized.ExceedingTemperature
                    },
                Axles = new List<Axle>()
                {
                    new Axle()
                    {
                        AxleIndex = 0,
                        DistanceToPreviousAxle = 0,
                        LeftWheel = new Wheel(){ WheelLoad = 6000 },
                        RightWheel = new Wheel(){ WheelLoad = 6000 }
                    },
                    new Axle()
                    {
                        AxleIndex = 1,
                        DistanceToPreviousAxle = 1,
                        LeftWheel = new Wheel(){ WheelLoad = 6200 },
                        RightWheel = new Wheel(){ WheelLoad = 6100 }
                    },
                    new Axle()
                    {
                        AxleIndex = 2,
                        DistanceToPreviousAxle = 2,
                        LeftWheel = new Wheel(){ WheelLoad = 6000 },
                        RightWheel = new Wheel(){ WheelLoad = 6000 }
                    },
                    new Axle()
                    {
                        AxleIndex = 3,
                        DistanceToPreviousAxle = 1,
                        LeftWheel = new Wheel(){ WheelLoad = 6000 },
                        RightWheel = new Wheel(){ WheelLoad = 6000 }
                    }
                }
            };
            defoltDto.Wagons.Add(wagon);

            wagon = new Wagon()
            {
                WagonIndex = 4,
                WagonType = WagonType.Wagon,
                AccuracyClass = Accuracy.Class_0_2,
                WagonWeight = 60000,
                MeasurementError = 1000,
                WagonSpeed = 44,
                WagonAcceleration = 0.4F,
                DifferenceLeftAndRightWheels = 200,
                ReasonsIsNotNormalized =
                    new List<ReasonsIsNotNormalized>()
                    {
                    ReasonsIsNotNormalized.ExceedingSpeedLimit,
                    ReasonsIsNotNormalized.ExceedingTemperature
                    },
                Axles = new List<Axle>()
                {
                    new Axle()
                    {
                        AxleIndex = 0,
                        DistanceToPreviousAxle = 0,
                        LeftWheel = new Wheel(){ WheelLoad = 6000 },
                        RightWheel = new Wheel(){ WheelLoad = 6000 }
                    },
                    new Axle()
                    {
                        AxleIndex = 1,
                        DistanceToPreviousAxle = 1,
                        LeftWheel = new Wheel(){ WheelLoad = 6200 },
                        RightWheel = new Wheel(){ WheelLoad = 6100 }
        },
                    new Axle()
                    {
                        AxleIndex = 2,
                        DistanceToPreviousAxle = 2,
                        LeftWheel = new Wheel(){ WheelLoad = 6000 },
                        RightWheel = new Wheel(){ WheelLoad = 6000 }
                    },
                    new Axle()
                    {
                        AxleIndex = 3,
                        DistanceToPreviousAxle = 1,
                        LeftWheel = new Wheel(){ WheelLoad = 6000 },
                        RightWheel = new Wheel(){ WheelLoad = 6000 }
                    }
                }
            };
            defoltDto.Wagons.Add(wagon);
            return defoltDto;
        }
    }
}
