using Italbytz.Ports.Exam.ComputingSystems;

namespace Italbytz.Adapters.Exam.ComputingSystems;

public class BinaryToDecimalParameters : IBinaryToDecimalParameters
{
    public byte Binary { get; set; }
    
    public BinaryToDecimalParameters()
        : this((byte)new Random().Next(1,256))
    {
    }

    public BinaryToDecimalParameters(byte binary)
    {
        this.Binary = binary;
    }
    
}