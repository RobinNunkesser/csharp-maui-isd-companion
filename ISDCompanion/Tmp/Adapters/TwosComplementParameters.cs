using Italbytz.Ports.Exam.ComputingSystems;

namespace Italbytz.Adapters.Exam.ComputingSystems;

public class TwosComplementParameters(byte positiveBinary)
    : ITwosComplementParameters
{
    public byte PositiveBinary { get; set; } = positiveBinary;
    public TwosComplementParameters()
        : this((byte)new Random().Next(1,127))
    {
    }
}