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

Your puzzle answer was 242.

The first half of this puzzle is complete! It provides one gold star: *

--- Part Two ---
The line is moving more quickly now, but you overhear airport security talking about how passports
with invalid data are getting through. Better add some data validation, quick!

You can continue to ignore the cid field, but each other field has strict rules about what values
are valid for automatic validation:

byr (Birth Year) - four digits; at least 1920 and at most 2002.
iyr (Issue Year) - four digits; at least 2010 and at most 2020.
eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
hgt (Height) - a number followed by either cm or in:
If cm, the number must be at least 150 and at most 193.
If in, the number must be at least 59 and at most 76.
hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
pid (Passport ID) - a nine-digit number, including leading zeroes.
cid (Country ID) - ignored, missing or not.

Your job is to count the passports where all required fields are both present and valid according to
the above rules. Here are some example values:

byr valid:   2002
byr invalid: 2003

hgt valid:   60in
hgt valid:   190cm
hgt invalid: 190in
hgt invalid: 190

hcl valid:   #123abc
hcl invalid: #123abz
hcl invalid: 123abc

ecl valid:   brn
ecl invalid: wat

pid valid:   000000001
pid invalid: 0123456789
Here are some invalid passports:

eyr:1972 cid:100
hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926

iyr:2019
hcl:#602927 eyr:1967 hgt:170cm
ecl:grn pid:012533040 byr:1946

hcl:dab227 iyr:2012
ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277

hgt:59cm ecl:zzz
eyr:2038 hcl:74454a iyr:2023
pid:3556412378 byr:2007
Here are some valid passports:

pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
hcl:#623a2f

eyr:2029 ecl:blu cid:129 byr:1989
iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm

hcl:#888785
hgt:164cm byr:2001 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022

iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719

Count the number of valid passports - those that have all required fields and valid values.
Continue to treat cid as optional. In your batch file, how many passports are valid?

Your puzzle answer was 186.

Both parts of this puzzle are complete! They provide two gold stars: **
*/
namespace aoc2020;

public class Passport(int? byr, int? iyr, int? eyr, string? hgt, string? hcl, string? ecl, string? pid, int? cid)
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

    public bool HasRequiredFields() => byr.HasValue && iyr.HasValue && eyr.HasValue && hgt != null && hcl != null && ecl != null && pid != null;

    public override string ToString()
    {
        return @$"===== Begin Passport =====
  byr: {byr}
  iyr: {iyr}
  eyr: {eyr}
  hgt: {hgt}
  hcl: {hcl}
  ecl: {ecl}
  pid: {pid}
  cid: {cid}";
    }
}

// TODO: make an interface for common day methods
public abstract class Day4 : Day
{
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

    protected override string InputFilepath => "/Users/cod1cbg/src/aoc2020csharp/aoc2020/inputs/Day4Input.txt";

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
                var subline = line[pos..].Trim();
                var nextColonIdx = subline.IndexOf(':');
                if (nextColonIdx == -1) { break; } // No more fields

                var fieldName = subline[(nextColonIdx-3)..nextColonIdx].ToString();
                var nextSpaceIdx = subline.IndexOf(' ');
                var valueEndIdx = nextSpaceIdx == -1 ? subline.Length : nextSpaceIdx;
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

public class Day4PartOne : Day4
{
    protected override ulong TestOutput { get; } = 2;

    public Day4PartOne(): base() {}

    public override ulong Run()
    {
        Console.WriteLine("Day 4 Part One");
        var passports = ParsePassports();

        if (IsTestMode){
            foreach (Passport p in passports){
                Console.WriteLine(p);
            }
        }

        return (ulong) passports.Where(x => x.HasRequiredFields()).Count();
    }
}


public class Day4PartTwo : Day4
{
    protected override IEnumerable<char[]> TestInput { get; } = 
@"eyr:1972 cid:100
hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926

iyr:2019
hcl:#602927 eyr:1967 hgt:170cm
ecl:grn pid:012533040 byr:1946

hcl:dab227 iyr:2012
ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277

hgt:59cm ecl:zzz
eyr:2038 hcl:74454a iyr:2023
pid:3556412378 byr:2007

pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
hcl:#623a2f

eyr:2029 ecl:blu cid:129 byr:1989
iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm

hcl:#888785
hgt:164cm byr:2001 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022

iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719".Split("\n").Select(x => x.ToArray());

    protected override ulong TestOutput { get; } = 4;

    public Day4PartTwo(): base() {}

    // For easier debugging
    [Flags]
    protected enum ValidationResult {
        Valid                   = 0,
        InvalidBirthYear        = 1 << 0,
        InvalidIssueYear        = 1 << 1,
        InvalidExpirationYear   = 1 << 2,
        InvalidHeight           = 1 << 3,
        InvalidHairColor        = 1 << 4,
        InvalidEyeColor         = 1 << 5,
        InvalidPassportID       = 1 << 6,
    }

    protected void PrintValidationResult(Passport p, ValidationResult result, int indent = 2){
        string indentStr = new(' ', indent);
        if (result.HasFlag(ValidationResult.InvalidBirthYear)){
            Console.WriteLine($"{indentStr}Invalid Birth Year: {p.Byr}");
        }
        if (result.HasFlag(ValidationResult.InvalidIssueYear)){
            Console.WriteLine($"{indentStr}Invalid Issue Year: {p.Iyr}");
        }
        if (result.HasFlag(ValidationResult.InvalidExpirationYear)){
            Console.WriteLine($"{indentStr}Invalid Expiration Year: {p.Eyr}");
        }
        if (result.HasFlag(ValidationResult.InvalidHeight)){
            Console.WriteLine($"{indentStr}Invalid Height: {p.Hgt}");
        }
        if (result.HasFlag(ValidationResult.InvalidHairColor)){
            Console.WriteLine($"{indentStr}Invalid Hair Colour: {p.Hcl}");
        }
        if (result.HasFlag(ValidationResult.InvalidEyeColor)){
            Console.WriteLine($"{indentStr}Invalid Eye Color: {p.Ecl}");
        }
        if (result.HasFlag(ValidationResult.InvalidPassportID)){
            Console.WriteLine($"{indentStr}Invalid Passport ID: {p.Pid}");
        }
    }

    protected ValidationResult PassportDataValidation(Passport p){
        ValidationResult result = ValidationResult.Valid;

        // Nullable types return false for all comparisons, so we check the true conditions then invert
        // (Birth Year) - four digits; at least 1920 and at most 2002.
        if (!(p.Byr >= 1920 && p.Byr <= 2002)) {
            result |= ValidationResult.InvalidBirthYear;
        }
        // (Issue Year) - four digits; at least 2010 and at most 2020.
        if (!(p.Iyr >= 2010 && p.Iyr <= 2020)) {
            result |= ValidationResult.InvalidIssueYear;
        }
        // (Expiration Year) - four digits; at least 2020 and at most 2030.
        if (!(p.Eyr >= 2020 && p.Eyr <= 2030)) {
            result |= ValidationResult.InvalidExpirationYear;
        }
        /* (Height) - a number followed by either cm or in:
            If cm, the number must be at least 150 and at most 193.
            If in, the number must be at least 59 and at most 76. */
        if (p.Hgt != null && p.Hgt.Length >= 4){
            if (int.TryParse(p.Hgt.AsSpan()[..^2], out var heightValue)){
                switch (p.Hgt.AsSpan()[^2..])
                {
                    case "cm":
                        if (heightValue < 150 || heightValue > 193){
                            result |= ValidationResult.InvalidHeight;
                        }
                        break;
                    case "in":
                        if (heightValue < 59 || heightValue > 76){
                            result |= ValidationResult.InvalidHeight;
                        }
                        break;
                    default:
                        result |= ValidationResult.InvalidHeight;
                        break;
                }
            } else {
                result |= ValidationResult.InvalidHeight;
            }
        } else {
            result |= ValidationResult.InvalidHeight;
        }
        // (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
        if (p.Hcl != null && p.Hcl.Length == 7){
            if (p.Hcl[0] != '#') {
                result |= ValidationResult.InvalidHairColor;
            } else if (!p.Hcl.Skip(1).All("0123456789abcdef".Contains)){
                result |= ValidationResult.InvalidHairColor;
            }
        } else {
            result |= ValidationResult.InvalidHairColor;
        }
        // (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
        if (p.Ecl != null && p.Ecl.Length == 3){
            switch (p.Ecl)
            {
                case "amb":
                case "blu":
                case "brn":
                case "gry":
                case "grn":
                case "hzl":
                case "oth":
                    break;
                default:
                    result |= ValidationResult.InvalidEyeColor;
                    break;
            }
        } else {
            result |= ValidationResult.InvalidEyeColor;
        }
        // (Passport ID) - a nine-digit number, including leading zeroes.
        if (p.Pid != null && p.Pid.Length == 9){
            if (!p.Pid.All(char.IsDigit)){
                result |= ValidationResult.InvalidPassportID;
            }
        } else {
            result |= ValidationResult.InvalidPassportID;
        }

        // Passed all checks
        return result;
    }

    public override ulong Run()
    {
        Console.WriteLine("Day 4 Part One");
        var passports = ParsePassports();

        var validity = passports.Select(p => (Passport: p, Valid: PassportDataValidation(p)));
        
        if (IsTestMode){
            foreach (var v in validity) {
                Console.WriteLine(v.Passport);
                PrintValidationResult(v.Passport,v.Valid);
            };
        }

        return (ulong) validity.Where(x => x.Valid == ValidationResult.Valid).Count();
    }
}