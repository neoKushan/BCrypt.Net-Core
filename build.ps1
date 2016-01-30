try
{
	dnvm upgrade
	dnu restore .\src
	dnu build .\src\BCrypt.Net-Core
    dnu pack .\src\BCrypt.Net-Core
}
catch
{
	throw
}