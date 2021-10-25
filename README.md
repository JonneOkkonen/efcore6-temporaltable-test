# EFCore 6 Temporal Table Test Project

Issue: https://github.com/dotnet/efcore/issues/26451

## Output
After creating Main Entity
```json
{
  "Id": 1,
  "Description": "Main entity",
  "OwnedEntity": {
    "Description": "Owned entity"
  }
}
```
After changing the Main/Owned entity
```json
{
  "Id": 1,
  "Description": "Changed main entity",
  "OwnedEntity": {
    "Description": "Changed owned entity"
  }
}
```
History data for Main Entity in given time
```json
[
  {
    "Id": 1,
    "Description": "Main entity",
    "OwnedEntity": {
      "Description": "Changed owned entity" // Incorrect data, current value instead of historical value
    }
  }
]
```
