CheckDigits
===========

Check-digit calculators for identifier validation

This is my dumping ground for various check-digit schemes.

Call using the `CheckDigits` helper class, like:

```csharp
var ok = CheckDigits.GRid.IsValid("A1-2425G-ABC1234002-M");
```

- GridChecker: For IFPI GRid codes ( https://www.ifpi.org/resource/grid/ )
- ISO 7064: 
