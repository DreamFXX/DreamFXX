class SubstanceType
{
    public int Id { get; set; } // Get primary Key from Main table
    public string? Substance { get; set; }

    public double Dosage { get; set; }
    public string? Unit { get; set; }

    public DateTime DateTime { get; set; }
}