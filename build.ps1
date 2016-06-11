try
{
	dotnet restore .\src
    dotnet pack .\src\BCrypt.Net-Core --configuration Release --no-dependencies
}
catch
{
	throw
}