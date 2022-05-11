// ReSharper disable InconsistentNaming

using System.Numerics;

namespace Libraries;

public class RData25Rec
{
    public int ID { get; set; }   
    public string? ProfileID { get; set; }   
    public string? B2 { get; set; }
    public string? B3 { get; set; }
    public string? C1 { get; set; }
    public string? C2 { get; set; }
    public string? C3 { get; set; }
    public string? CS1 { get; set; }
    public string? CS2 { get; set; }
    public string? CS3 { get; set; }
    public string? D1 { get; set; }
    public string? D2 { get; set; }
    public string? D3 { get; set; }
    public string? D4 { get; set; }
    public string? DS1 { get; set; }
    public string? DS2 { get; set; }
    public string? RCLeader { get; set; }
}

public class ContainerTypeCodeRec
{
    public string? ContainerType { get; set; }
    public string? Abrv { get; set; }
}

public class WasteShipmentsByYearRec
{
    public string? ProfileNum { get; set; }
    public string? WasteName { get; set; }
    public int Year { get; set; }
    public double TotalQuantity { get; set; }
    public double AvgQuantity { get; set; }
    public double MinQuantity { get; set; }
    public double MaxQuantity { get; set; }
    public BigInteger NumberOfShipments { get; set; }
}

public class DrumTrackingRec
{
    public string? DrumNumber { get; set; }
    public string? ProfileNumber { get; set; }
    public string? HAZNON { get; set; }
    public string? ContactPerson { get; set; }
    public string? SourceProcess { get; set; }
    public string? SourceActivity { get; set; }
    public DateTime AccumStartDate { get; set; }
    public DateTime ShipppedOffSite { get; set; }
    public string? Comments { get; set; }
    public string? CostCenter { get; set; }
    public string? SourceDept { get; set; }
    public string? DrumType { get; set; }
    public bool verified { get; set; }
    public string? Location { get; set; }
    public string? AccumulationArea { get; set; }
}

