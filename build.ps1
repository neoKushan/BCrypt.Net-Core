try
{
	dotnet restore .\src\BCrypt.Net-Core
    dotnet pack .\src\BCrypt.Net-Core --configuration Release
}
catch
{
	throw
}