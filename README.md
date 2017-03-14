# BCrypt.Net-Core
A .net Core port of BCrypt.net

If you're working on a .net core application and need to use BCrypt to store passwords, this is a quick and dirty port of BCrypt.net. 

I have tested using this library on both the full .net 4.5 framework, as well as .net core RC1 through to RTM.

It should be a drop-in replacement for BCrypt.net as the namespaces are unchanged.

[![kushan MyGet Build Status](https://www.myget.org/BuildSource/Badge/kushan?identifier=ebbdc384-57ab-4131-ac19-599d355302ce)](https://www.myget.org/)

# How to use
```C#
// hash and save a password
hashedPassword = BCrypt.Net.BCrypt.HashPassword(clearTextPassword);

// check a password
bool validPassword = BCrypt.Net.BCrypt.Verify(userSubmittedPassword, hashedPassword);
```

# Disclaimer
I did not write this library, I merely ported it to .net core. All credit goes to Ryan D. Emerle for porting BCrypt to .net in the first place.

# Original Project
The original project can be found at Codeplex: https://bcrypt.codeplex.com/
