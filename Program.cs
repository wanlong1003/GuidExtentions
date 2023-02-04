for (var i = 0; i < 100; i++)
{
    var guid = Guid.NewGuid();
    var shortGuid = guid.ToShortString();

    var checkGuid = GuidExtentions.CreateFromShortString(shortGuid);

    System.Console.WriteLine($"{guid} : {shortGuid}  {guid == checkGuid}");
}



