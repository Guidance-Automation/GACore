namespace GACore;

public class FleetManagementMetadata
{
    public string ProductName { get; }

    public SemVer Version { get; }

    public FleetManagementMetadata(string? productName, SemVer? version)
    {
        if (string.IsNullOrEmpty(productName))
            throw new ArgumentOutOfRangeException(nameof(productName));

        ProductName = productName;

        Version = version ?? throw new ArgumentNullException(nameof(version));
    }

    public override string ToString()
    {
        return $"{ProductName} - {Version}";
    }
}