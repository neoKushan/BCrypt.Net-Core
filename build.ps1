try
{
	dnvm upgrade
	dnu restore .\src
	dnu build .\src\BCrypt.Net
    dnu pack .\src\BCrypt.Net
}
catch
{
	throw
}