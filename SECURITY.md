# Security Policy

BCrypt.Net-Core implements password hashing. If it is wrong, applications that depend on it are
wrong in a way their users cannot see. Reports are taken seriously.

## Reporting a vulnerability

**Please do not open a public issue for security vulnerabilities.**

Use GitHub's private vulnerability reporting:
[**Report a vulnerability**](https://github.com/neoKushan/BCrypt.Net-Core/security/advisories/new)

If that is unavailable to you, contact the maintainer directly via the email on
[github.com/neoKushan](https://github.com/neoKushan).

Please include, as far as you are able:

- The version of BCrypt.Net-Core affected
- A description of the issue and its security impact
- Steps to reproduce, ideally a minimal test case with concrete inputs and outputs
- Any hash strings, salts, or work factors needed to demonstrate it

## What to expect

This is a single-maintainer project worked on outside of a day job, so please calibrate
expectations accordingly:

- **Acknowledgement:** within 7 days
- **Initial assessment:** within 30 days
- **Disclosure:** coordinated with you. If a fix will take time, we agree a timeline rather than
  leaving you waiting in silence.

If you do not hear back within 7 days, please chase — a missed notification is far more likely
than a decision to ignore you.

## Supported versions

| Version | Supported |
| ------- | --------- |
| 1.6.x   | ✅ Security fixes |
| < 1.6   | ❌ Please upgrade |

## Scope

**In scope:**

- Incorrect hash generation or verification against the bcrypt specification
- Non-constant-time comparison, or other timing side channels in verification
- Incorrect handling of salt revisions (`$2`, `$2a`, `$2b`, `$2x`, `$2y`)
- Crashes, hangs, or unbounded resource use triggered by untrusted hash or salt input
- Incorrect handling of long passwords, embedded null bytes, or multi-byte UTF-8 input

**Out of scope:**

- bcrypt's inherent 72-byte input limit — this is a property of the algorithm, not a defect in
  this implementation. Pre-hash if you need to accept longer secrets.
- Choosing a work factor too low for your threat model. That is a deployment decision; this
  library exposes the parameter and does not override it.
- Vulnerabilities in applications that *use* this library, rather than in the library itself.

## Maintenance status

Be aware when assessing risk: this package is stable rather than actively developed. Version
1.6.0 shipped in December 2018 and the last substantive commit was June 2020.

If you want a more actively developed .NET bcrypt implementation, see
[BCrypt.Net-Next](https://github.com/BcryptNet/bcrypt.net).

Security reports against this package are still welcomed and acted on.
