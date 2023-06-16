using TableLib;


public class Driver
{
    public static void Main()
    {
        TableLib.Table sampleTable = TableLib.Table.ReadCsv("asset/sample.csv");
        sampleTable.SortByCol(1);
        sampleTable.Print();
        sampleTable.WriteCsv("sample_out.csv");
    }
}
