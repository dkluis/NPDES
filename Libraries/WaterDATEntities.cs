// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedType.Global
namespace Libraries;

public class ARCOSampInfoRec
{
    public string? HLALABID { get; set; }
    public string? OBJID { get; set; }
    public string? PERMNUM { get; set; }
    public string? ORDERNUM { get; set; }
    public string? SAMPLEID { get; set; }
    public string? SAMPTYPE { get; set; }
    public string? SAMPBY { get; set; }
    public DateTime COLLDATE { get; set; }
    public DateTime COLLTIME { get; set; }
    public DateTime SAMPDATE { get; set; }
    public string? LABNAME { get; set; }
    public DateTime RECDATE { get; set; }
    public string? COMMENT { get; set; }
    public DateTime ENTERDATE { get; set; }
    public string? SOURCE { get; set; }
}

public class ARCOParamRec
{
    public string? HLALABID { get; set; }
    public string? PARAM { get; set; }
    public int FIELDNUM { get; set; }
    public string? LABRESULT { get; set; }
    public string? LABUNIT { get; set; }
    public string? LABQUAL { get; set; }
    public double RESULT { get; set; }
    public string? UNIT { get; set; }
    public string? QUAL { get; set; }
    public string? METHOD { get; set; }
    public DateTime ANALDATE { get; set; }
    public string? ANALYST { get; set; }
    public string? DATAUSE { get; set; }
}