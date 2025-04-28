Как запустить проект:

##################### ВАЖНО #####################

- Должен быть установлен .NET SDK 8.0: 
https://dotnet.microsoft.com/ru-ru/download/dotnet/thank-you/sdk-8.0.408-windows-x64-installer

- Должен быть установлен PostgreSQL:
https://sbp.enterprisedb.com/getfile.jsp?fileid=1259505 - последняя версия на актуальный момент




После установки .NET SDK и форка / клонирования проекта нужно сделать следующие вещи:

1) Перейти в командной строке (CMD / Powershell) в папку проекта (MoqTestingProject);

1.1) Скорее всего, придётся поменять Username и Password. указанные в ConnectionString (Program.cs, 19 строка). 
Указываете свои юзернейм и пароль для базы данных.

2) Прописать в командной строке с проектом следующие команды:
	- dotnet tool install --local dotnet-ef
	- dotnet ef migrations add init
	- dotnet ef database update
	- dotnet restore
	
	Данные команды позволят вам автоматически развернуть базу данных и восстановить NuGet-зависимости.

3) Для запуска прописать команды:
	- dotnet build
	- dotnet run

	Для запуска тестов:
	- dotnet test
	
	
Если вы сталкиваетесь с проблемами при подключении к базе данных, убедитесь, что PostgreSQL настроен корректно, и что ваш юзер имеет необходимые права доступа.

В случае проблем с миграциями, попробуйте использовать команду dotnet ef migrations remove для удаления предыдущих миграций перед добавлением новой.