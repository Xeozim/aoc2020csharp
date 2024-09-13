// Day 1
/* Puzzle Description
--- Day 4: Passport Processing ---
You arrive at the airport only to realize that you grabbed your North Pole Credentials instead of
your passport. While these documents are extremely similar, North Pole Credentials aren't issued by
a country and therefore aren't actually valid documentation for travel in most of the world.

It seems like you're not the only one having problems, though; a very long line has formed for the
automatic passport scanners, and the delay could upset your travel itinerary.

Due to some questionable network security, you realize you might be able to solve both of these
problems at the same time.

The automatic passport scanners are slow because they're having trouble detecting which passports
have all required fields. The expected fields are as follows:

byr (Birth Year)
iyr (Issue Year)
eyr (Expiration Year)
hgt (Height)
hcl (Hair Color)
ecl (Eye Color)
pid (Passport ID)
cid (Country ID)
Passport data is validated in batch files (your puzzle input). Each passport is represented as a
sequence of key:value pairs separated by spaces or newlines. Passports are separated by blank lines.

Here is an example batch file containing four passports:

ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in
The first passport is valid - all eight fields are present. The second passport is invalid - it is
missing hgt (the Height field).

The third passport is interesting; the only missing field is cid, so it looks like data from North
Pole Credentials, not a passport at all! Surely, nobody would mind if you made the system
temporarily ignore missing cid fields. Treat this "passport" as valid.

The fourth passport is missing two fields, cid and byr. Missing cid is fine, but missing any other
field is not, so this passport is invalid.

According to the above rules, your improved system would report 2 valid passports.

Count the number of valid passports - those that have all required fields. Treat cid as optional.
In your batch file, how many passports are valid?

*/

class Passport(int? byr, int? iyr, int? eyr, string? hgt, string? hcl, string? ecl, string? pid, int? cid)
{
    int? byr = byr;
    int? iyr = iyr;
    int? eyr = eyr;
    string? hgt = hgt;
    string? hcl = hcl;
    string? ecl = ecl;
    string? pid = pid;
    int? cid = cid;

    public int? Byr { get => byr; set => byr = value; }
    public int? Iyr { get => iyr; set => iyr = value; }
    public int? Eyr { get => eyr; set => eyr = value; }
    public string? Hgt { get => hgt; set => hgt = value; }
    public string? Hcl { get => hcl; set => hcl = value; }
    public string? Ecl { get => ecl; set => ecl = value; }
    public string? Pid { get => pid; set => pid = value; }
    public int? Cid { get => cid; set => cid = value; }

    public bool IsValid() => byr.HasValue && iyr.HasValue && eyr.HasValue && hgt != null && hcl != null && ecl != null && pid != null;
}

// TODO: make an interface for common day methods
abstract class Day4 : Day
{
    // Test data
    // TODO: Look at NUnit or similar test framework
    protected override IEnumerable<char[]> TestInput { get; } = 
@"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in".Split("\n").Select(x => x.ToArray());

    protected override int DayNumber { get; } = 4;

    protected override string InputFilepath => "Day4Input.txt";

    private object? DictGetOrNull(Dictionary<string,object> dict, string key){
        if (dict.TryGetValue(key, out object? obj)){
            return obj;
        } else {
            return null;
        }
    }

    public IEnumerable<Passport> ParsePassports(){
        var fieldData = new Dictionary<string, object>();
        var passports = new List<Passport>();
        var inputArr = inputLines.ToArray(); 
        var lineCount = inputLines.Count();

        for (int i=0; i<lineCount; i++){
            // Try to read data from the current line
            ReadOnlySpan<char> line = inputArr[i];

            var pos = 0;
            while (pos < line.Length){
                var subline = line[pos..];
                var nextColonIdx = subline.IndexOf(':');
                if (nextColonIdx == -1) { break; } // No more fields

                var fieldName = subline[(nextColonIdx-3)..nextColonIdx].ToString();
                var nextSpaceIdx = subline.IndexOf(' ');
                var valueEndIdx = nextSpaceIdx == -1 ? subline.Length-1 : nextSpaceIdx;
                var fieldValue = subline[(nextColonIdx+1)..valueEndIdx];

                switch (fieldName)
                {
                    case "byr":
                    case "iyr":
                    case "eyr":
                    case "cid":
                        fieldData[fieldName] = int.Parse(fieldValue);
                        break;
                    case "hgt":
                    case "hcl":
                    case "ecl":
                    case "pid":
                        fieldData[fieldName] = fieldValue.ToString();
                        break;
                    default:
                        Console.WriteLine($"Unexpected field name in passport input: {fieldName}");
                        break;
                }

                pos += valueEndIdx+1;
            }
            
            // When we hit an empty line or the final line, create a passport with the current set
            // of data then wipe the data
            if (line.IsWhiteSpace() || i == lineCount-1){
                passports.Add(new Passport(
                    (int?)DictGetOrNull(fieldData,"byr"),
                    (int?)DictGetOrNull(fieldData,"iyr"),
                    (int?)DictGetOrNull(fieldData,"eyr"),
                    (string?)DictGetOrNull(fieldData,"hgt"),
                    (string?)DictGetOrNull(fieldData,"hcl"),
                    (string?)DictGetOrNull(fieldData,"ecl"),
                    (string?)DictGetOrNull(fieldData,"pid"),
                    (int?)DictGetOrNull(fieldData,"cid")));
                
                fieldData.Clear();
            }
        }

        return passports;
    }
}

class Day4PartOne : Day4
{
    protected override ulong TestOutput { get; } = 2;

    public Day4PartOne(): base() {}

    public override ulong Run()
    {
        Console.WriteLine("Day 4 Part One");
        var passports = ParsePassports();

        if (IsTestMode){
            foreach (Passport p in passports){
                Console.WriteLine("========Passport========");
                Console.WriteLine($"byr: {p.Byr}");
                Console.WriteLine($"iyr: {p.Iyr}");
                Console.WriteLine($"eyr: {p.Eyr}");
                Console.WriteLine($"hgt: {p.Hgt}");
                Console.WriteLine($"hcl: {p.Hcl}");
                Console.WriteLine($"ecl: {p.Ecl}");
                Console.WriteLine($"pid: {p.Pid}");
                Console.WriteLine($"cid: {p.Cid}");
            }
        }

        return (ulong) passports.Where(x => x.IsValid()).Count();
    }
}


class Day4PartTwo : Day4
{
    protected override ulong TestOutput { get; } = 0;

    public Day4PartTwo(): base() {}

    public override ulong Run()
    {
        throw new NotImplementedException();
    }

}