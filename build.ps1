try
{
	dotnet restore .\src
    dotnet pack .\src\BCrypt.Net-Core --configuration Release
}
catch
{
	throw
}