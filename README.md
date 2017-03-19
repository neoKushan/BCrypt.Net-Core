# BCrypt.Net-Core
A .net Core port of BCrypt.net, with some feature and security enhancements.

Compatible with both the full .net framework and .net core (Netstandard 1.3).

It should be a drop-in replacement for BCrypt.net as the namespaces are unchanged.

[![kushan MyGet Build Status](https://www.myget.org/BuildSource/Badge/kushan?identifier=ebbdc384-57ab-4131-ac19-599d355302ce)](https://www.myget.org/)

# How to use

Standard use:

```C#
// hash and save a password
hashedPassword = BCrypt.Net.BCrypt.HashPassword(clearTextPassword);

// check a password
bool validPassword = BCrypt.Net.BCrypt.Verify(userSubmittedPassword, hashedPassword);
```

If you need to specify a different salt revision when generating hashes, you can pass in a SaltRevision parameter:

```C#
// hash and save a password
hashedPassword = BCrypt.Net.BCrypt.HashPassword(clearTextPassword, SaltRevision.Revision2A);
```

This is only recommended if you're dealing with legacy systems, otherwise you should use the default of 2b. Note that the salt algorithm does not change, it will generate a correct bcrypt hash in all known cases.

# Disclaimer
I did not write this library, I merely ported it to .net core. All credit goes to Ryan D. Emerle for porting BCrypt to .net in the first place.

# Original Project
The original project can be found at Codeplex: https://bcrypt.codeplex.com/
