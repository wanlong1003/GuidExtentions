public static class GuidExtentions
{
    public static string ToShortString(this Guid guid)
    {
        string encoded = Convert.ToBase64String(guid.ToByteArray());

        encoded = encoded
            .Replace("/", "_")
            .Replace("+", "-");

        return encoded.Substring(0, 22);
    }


    public static Guid CreateFromShortString(string value)
    {
        // avoid parsing larger strings/blobs
        if (value?.Length != 22)
        {
            throw new ArgumentException(
                $"A ShortGuid must be exactly 22 characters long. Received a {value?.Length ?? 0} character string.",
                paramName: nameof(value)
            );
        }

        string base64 = value
            .Replace("_", "/")
            .Replace("-", "+") + "==";

        byte[] blob = Convert.FromBase64String(base64);
        var guid = new Guid(blob);

        var sanityCheck = guid.ToShortString();
        if (sanityCheck != value)
        {
            throw new FormatException(
                $"Invalid strict ShortGuid encoded string. The string '{value}' is valid URL-safe Base64, " +
                $"but failed a round-trip test expecting '{sanityCheck}'."
            );
        }

        return guid;
    }
}