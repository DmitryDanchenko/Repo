using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigHelper.Presenters;

using CvLab.Tensor.TrainDatasetsLib.Enums;

namespace ConfigHelper.Models;
public class KvToTonneCoefficient
{
    private const double CoeffLysenco = 0.0037;
    private const double CoeffKukin = 0.0012;
    private const int BoxSensorsCount = 4;

    public int IdSensor { get; }

    public double Coeff { get; }

    public string IpAndPort { get; }

    public KvToTonneCoefficient(int idSensor, double coeff, string ipAndPort)
    {
        IdSensor = idSensor < 1 || idSensor > 4 ? throw new ArgumentException("incorrect Id sensor") : idSensor;
        Coeff = coeff;
        IpAndPort = ipAndPort;
    }

    public KvToTonneCoefficient(int idSensor, string ipAndPort, ProtocolVer protocol)
    {
        IdSensor = idSensor < 1 || idSensor > 4 ? throw new ArgumentException("incorrect Id sensor") : idSensor;
        IdSensor = idSensor;
        IpAndPort = ipAndPort;
        if (protocol == ProtocolVer.Lysenko)
            Coeff = CoeffLysenco;
        else
            Coeff = CoeffKukin;
    }

    public static List<KvToTonneCoefficient> AddMissingDefaltCoeffs(List<KvToTonneCoefficient> coeffs, ProtocolVer protokol)
    {
        for (var i = 0; i < BoxSensorsCount; i++)
        {
            if (!coeffs.Any(s => s.IdSensor == i + 1))
            {
                var coeff = new KvToTonneCoefficient(i + 1, coeffs.First().IpAndPort, protokol);
                coeffs.Add(coeff);
            }
        }
        return coeffs.OrderBy(x => x.IdSensor).ToList();
    }
}

