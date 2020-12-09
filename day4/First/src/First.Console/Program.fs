open System.IO

type Passport = {
    RawString: string
    BirthYear: string option
    IssueYear: string option
    ExpirationYear: string option
    Height: string option
    HairColor: string option
    EyeColor: string option
    PassportId: string option
    CountryId: string option
}

module Passport =
    let Default = {
            RawString = ""
            BirthYear = None
            IssueYear = None
            ExpirationYear = None
            Height = None
            HairColor = None
            EyeColor = None
            PassportId = None
            CountryId = None
    } 
    let ParseCountryId passport = 
        let countryId =
            passport.RawString.Split(" ")
            |> Array.tryFind(fun x -> x.StartsWith("cid:"))
            |> Option.map(fun x -> x.Remove(0, 4))
        { passport with CountryId = countryId }

    let ParsePassportId passport = 
        let passportId =
            passport.RawString.Split(" ")
            |> Array.tryFind(fun x -> x.StartsWith("pid:"))
            |> Option.map(fun x -> x.Remove(0, 4))
        { passport with PassportId = passportId }

    let ParseEyeColor passport = 
        let eyeColor =
            passport.RawString.Split(" ")
            |> Array.tryFind(fun x -> x.StartsWith("ecl:"))
            |> Option.map(fun x -> x.Remove(0, 4))
        { passport with EyeColor = eyeColor }

    let ParseHairColor passport = 
        let hairColor =
            passport.RawString.Split(" ")
            |> Array.tryFind(fun x -> x.StartsWith("hcl:"))
            |> Option.map(fun x -> x.Remove(0, 4))
        { passport with HairColor = hairColor }

    let ParseHeight passport = 
        let height =
            passport.RawString.Split(" ")
            |> Array.tryFind(fun x -> x.StartsWith("hgt:"))
            |> Option.map(fun x -> x.Remove(0, 4))
        { passport with Height = height }

    let ParseExpirationYear passport = 
        let expirationYear =
            passport.RawString.Split(" ")
            |> Array.tryFind(fun x -> x.StartsWith("eyr:"))
            |> Option.map(fun x -> x.Remove(0, 4))
        { passport with ExpirationYear = expirationYear }

    let ParseIssueYear passport =
        let issueYear = 
            passport.RawString.Split(" ")
            |> Array.tryFind(fun x -> x.StartsWith("iyr:"))
            |> Option.map(fun x -> x.Remove(0, 4))
        { passport with IssueYear = issueYear}

    let parseBirthYear passport =
        let birthYear = 
            passport.RawString.Split(" ")
            |> Array.tryFind(fun x -> x.StartsWith("byr:"))
            |> Option.map(fun x -> x.Remove(0, 4))
        { passport with BirthYear = birthYear}

    let create init = 
        { Default with RawString = init }
        |> parseBirthYear
        |> ParseIssueYear
        |> ParseExpirationYear
        |> ParseHeight
        |> ParseHairColor
        |> ParseEyeColor
        |> ParsePassportId
        |> ParseCountryId

    let isValid passport =
        Option.isSome passport.BirthYear
        && Option.isSome passport.IssueYear
        && Option.isSome passport.ExpirationYear
        && Option.isSome passport.Height
        && Option.isSome passport.HairColor
        && Option.isSome passport.EyeColor
        && Option.isSome passport.PassportId

let content = 
    "assets/input.txt"
    |> File.ReadAllText
    |> fun x -> x.Split("\n\n")
    |> Array.map(fun x -> Passport.create (x.Replace("\n", " ")))
    |> Array.filter Passport.isValid

[<EntryPoint>]
let main argv =
    content
    |> Array.length
    |> printfn "%A" 
    0 // return an integer exit code