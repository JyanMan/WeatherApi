public class ForecastMain
{
    public CurrentConditions? CurrentConditions { get; set; }
    public string? Description { get; set; }
}

public class CurrentConditions
{
    public string? Datetime { get; set; }
    public float Temp { get; set; }
    public float Humidity { get; set; }
    public float Feelslike { get; set; }
    public float Dew { get; set; }
    public float Pressure { get; set; }
    public float Visibility { get; set; }
    public float UvIndex { get; set; }
    public string? Conditions { get; set; }

}