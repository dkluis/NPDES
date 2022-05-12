// ReSharper disable InconsistentNaming

using System.Numerics;

namespace Libraries;

public class RData25Rec
{
    public int ID { get; set; }   
    public string ProfileID { get; set; } = string.Empty;
    public string B2 { get; set; } = string.Empty;
    public string B3 { get; set; } = string.Empty;
    public string C1 { get; set; } = string.Empty;
    public string C2 { get; set; } = string.Empty;
    public string C3 { get; set; } = string.Empty;
    public string CS1 { get; set; } = string.Empty;
    public string CS2 { get; set; } = string.Empty;
    public string CS3 { get; set; } = string.Empty;
    public string D1 { get; set; } = string.Empty;
    public string D2 { get; set; } = string.Empty;
    public string D3 { get; set; } = string.Empty;
    public string D4 { get; set; } = string.Empty;
    public string DS1 { get; set; } = string.Empty;
    public string DS2 { get; set; } = string.Empty;
    public string RCLeader { get; set; } = string.Empty;
}

public class ContainerTypeCodeRec
{
    public string ContainerType { get; set; } = string.Empty;
    public string Abrv { get; set; } = string.Empty;
}

public class WasteShipmentsByYearRec
{
    public string ProfileNum { get; set; } = string.Empty;
    public string WasteName { get; set; } = string.Empty;
    public int Year { get; set; }
    public double TotalQuantity { get; set; }
    public double AvgQuantity { get; set; }
    public double MinQuantity { get; set; }
    public double MaxQuantity { get; set; }
    public BigInteger NumberOfShipments { get; set; }
}

public class DrumTrackingRec
{
    public string DrumNumber { get; set; } = string.Empty;
    public string ProfileNumber { get; set; } = string.Empty;
    public string HAZNON { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string SourceProcess { get; set; } = string.Empty;
    public string SourceActivity { get; set; } = string.Empty;
    public DateTime AccumStartDate { get; set; }
    public DateTime ShipppedOffSite { get; set; }
    public string Comments { get; set; } = string.Empty;
    public string CostCenter { get; set; } = string.Empty;
    public string SourceDept { get; set; } = string.Empty;
    public string DrumType { get; set; } = string.Empty;
    public bool verified { get; set; }
    public string Location { get; set; } = string.Empty;
    public string AccumulationArea { get; set; } = string.Empty;
}

