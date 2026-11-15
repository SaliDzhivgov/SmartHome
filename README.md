\# SmartHome (УмноЖилище)



Уеб приложение на ASP.NET Core MVC за управление на умен дом: стаи, устройства, категории и производители, с табло за текущата консумация.



\## Функционалности

\- \*\*Табло\*\*: брой стаи и устройства, включени устройства, текуща мощност (W), последно инсталирани устройства

\- \*\*Устройства\*\*: пълен CRUD, филтриране по име или сериен номер, стая, категория и състояние, включване и изключване с един бутон

\- \*\*Стаи\*\*: пълен CRUD, детайлна страница с устройствата в стаята

\- \*\*Категории\*\* и \*\*Производители\*\*: създаване, редакция, изтриване

\- Защита от изтриване на стая, категория или производител, които имат устройства

\- Валидация на сървъра (Data Annotations + проверки в service слоя: уникално име, уникален сериен номер, съществуващи връзки) и в браузъра (jQuery Validation + собствен атрибут `NotInFuture`)



\## Технологии

\- ASP.NET Core MVC (.NET 10), Razor (Layout, partial views, sections)

\- Entity Framework Core 8 + SQL Server (LocalDB), Code First миграции и начални данни (seed)

\- Dependency Injection, service слой с интерфейси

\- Bootstrap 5, Bootstrap Icons, jQuery Validation Unobtrusive



\## Архитектура

```

SmartHome.Web

├── Controllers      – тънки контролери, само извикват услугите

├── Services         – бизнес логика и достъп до данни (+ Interfaces)

├── Data             – DbContext, Entities, Configurations (Fluent API + seed)

├── ViewModels       – модели за изгледите и формите

├── Common           – константи за валидация и съобщения

├── Infrastructure   – собствен атрибут NotInFuture, разширения

└── Views            – Razor изгледи и partial views

```



\## Стартиране

\*\*Изисквания:\*\* .NET 10 SDK, SQL Server LocalDB (идва с Visual Studio).



1\. Клонирай хранилището:

&#x20;  ```

&#x20;  git clone https://github.com/SaliDzhivgov/SmartHome.git

&#x20;  cd SmartHome

&#x20;  ```

2\. Connection string се намира в `SmartHome.Web/appsettings.json`:

&#x20;  ```

&#x20;  Server=(localdb)\\mssqllocaldb;Database=SmartHomeDb;Trusted\_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True

&#x20;  ```

&#x20;  Ако използваш пълен SQL Server, смени `Server=` с името на своя сървър.

3\. Създай базата:

&#x20;  ```

&#x20;  dotnet tool install --global dotnet-ef

&#x20;  dotnet ef database update --project SmartHome.Web

&#x20;  ```

&#x20;  (Приложението прилага миграциите и автоматично при стартиране.)

4\. Стартирай:

&#x20;  ```

&#x20;  dotnet run --project SmartHome.Web

&#x20;  ```

&#x20;  и отвори `https://localhost:7282`, или натисни F5 във Visual Studio.



Не са нужни други променливи на средата и потребителски акаунти. Базата се зарежда с примерни данни: 6 стаи, 6 категории, 6 производители и 12 устройства.

