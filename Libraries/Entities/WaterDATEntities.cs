// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedType.Global
namespace Libraries.Entities;

public class ARCOSampInfoRec
{
    public string HLALABID { get; set; } = string.Empty;
    public string OBJID { get; set; } = string.Empty;
    public string PERMNUM { get; set; } = string.Empty;
    public string ORDERNUM { get; set; } = string.Empty;
    public string SAMPLEID { get; set; } = string.Empty;
    public string SAMPTYPE { get; set; } = string.Empty;
    public string SAMPBY { get; set; } = string.Empty;
    public DateTime COLLDATE { get; set; }
    public DateTime COLLTIME { get; set; }
    public DateTime SAMPDATE { get; set; }
    public string LABNAME { get; set; } = string.Empty;
    public DateTime RECDATE { get; set; }
    public string COMMENT { get; set; } = string.Empty;
    public DateTime ENTERDATE { get; set; }
    public string SOURCE { get; set; } = string.Empty;
    
}

public class ARCOParamRec
{
    public string HLALABID { get; set; } = string.Empty;
    public string PARAM { get; set; } = string.Empty;
    public int FIELDNUM { get; set; }
    public string LABRESULT { get; set; } = string.Empty;
    public string LABUNIT { get; set; } = string.Empty;
    public string LABQUAL { get; set; } = string.Empty;
    public double RESULT { get; set; }
    public string UNIT { get; set; } = string.Empty;
    public string QUAL { get; set; } = string.Empty;
    public string METHOD { get; set; } = string.Empty;
    public DateTime ANALDATE { get; set; }
    public string ANALYST { get; set; } = string.Empty;
    public string DATAUSE { get; set; } = string.Empty;
}