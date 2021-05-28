# Ludogame Version 2

## Innehållsförteckning

* [Förord](#preface)
* [Specifikation för uppgift](https://github.com/PGBSNH20/ludo-v2-g3/blob/main/Documentation/description.md)
* [Docker Compose](#docker-compose)
* [Kodstruktur](#kodstruktur)
  * [Backend](#backend)
  * [Frontend](#frontend)
* [API Dokumentation](#api-dokumentation)
  * [Endpoint för ludo resursen](#endpoints)
* [Frontned dokumentation](#frontend-dokumentation)
  * [Nytt spel](#nytt-spel)
  * [Spela](#spel)
* [Backend dokumentation](#backend-dokumentation)
  * [Nytt spel](#nytt-spel-backend)
  * [RollDice](#backend-spela)
  * [Anslut till spel](#anslut-till-spel)
* [SignalR](https://github.com/PGBSNH20/ludo-v2-g3/blob/signalR_implementation/SignalR_Doc.md)

<a name="preface" />

## Förord

Projektarbete **Ludogame v2**, det avslutande projektarbetet för kursen Webbutveckling backend på Teknikhögskolan 2021
Uppgiften vi har fått är att göra om Ludogame v1 som var en consol applikation till en Razor pages applikation. 
All spellogik ska skötas av ett RestAPI och presenteras i en ASP.NET Core Web App.

<a name="docker-compose" />

## Docker compose

Vi har skapat en docker compose fil där vi kör databasen, och genom att ha den i en compose fil så innebär det bland annat att vi båda kan använda oss utav samma connection string. 

<a name="kodstruktur" />

## Kodstruktur

I Vår solution finns det 3st projekt
* Backend - ***RestAPI***
* Frontend - ***ASP.NET Core WebApp (Razor pages)***
* LudoApiTest - ***Xunit***

<a name="backend" />

### Backend

* Controller
  * LudoController
    * ```GET: /RollDice```
    * ```PUT: /RollDice```
    * ```PUT: /MovePawn```
    * ```POST: /NewGame```
* Database
  * Innehåller allting som är relevant för databasen
* Enums
  * Innehåller enums för projektet
* GameLogic
  * Innehåller all spellogik uppdelat i olika cs filer
* Interfaces
  * Innehåller alla interfaces 
* Migrations
  * Innehåller all migration historik
* Models
  * Innehåller de modeller vi använder oss av i vår backend
* Requests
  * Innehåller de requests vi använder oss av 
* Services
  * Innehåller vår dependency injection registry

<a name="frontend" />

### Frontend

* wwwroot
  * css
    * CSS Fil för frontend
  * img
    * Innehåller bilder för tärningen
  * js
    * Innehåller de JavaScript filer vi använder
  * lib
* Models
  * Innehåller de modeller vi använder oss av i vår frontend
* Pages
  * Innehåller de pages vi använder oss av i vår frontend

<a name="api-dokumentation" />

## API Dokumentation

*Vi har valt att inte använda oss av async / await eftersom det inte kommer att förbättra prestandan något i vårat fall. Det som async / await skulle kunna hjälpa till med är att öka skalbarheten, så man kan ta emot fler requests, men det är enbart sant om vi skulle använda oss av fler distanser av databasen, eller exempelvis mongoDB.*

| ⚠️ Notera att dessa endpoints inte ska användas genom postman eller liknande. Det går att använda dom där, men dessa är byggda för att fungera i kombination med vår frontend och syftet med dessa är att låta vår backend agera som spelmotor som håller all spellogik. |
| --- |

**Strukturen för vårat RestAPI**

* BaseURL ```https://localhost:44303/api```
* Resurser 
  * ```/ludo```
* Endpoints för Ludo resursen
  * ```/{gameSessionID}``` -- GET
  * ```/NewGame``` -- POST
  * ```/RollDice``` -- PUT
  * ```/MovePawn``` -- PUT

<a name="endpoints" />

## Endpoint för Ludo resursen

```GET``` ```/{gameSessionID}``` -- Hämtar en spelsession

Example Request 
```https://localhost:44303/api/Ludo/28f6d193-65bf-4e8f-a543-aec9db46fb6e```

<details>
<summary>Body</summary>
 

</details>

<details>
 <summary>Exempelsvar</summary>

```json
[
    {
    "id": "28f6d193-65bf-4e8f-a543-aec9db46fb6e",
    "name": "SignalRTestSession",
    "players": [
        {
            "id": 46,
            "name": "Patric",
            "pawns": [
                {
                    "id": 181,
                    "position": 0,
                    "atFinishLine": false,
                    "isFinished": false,
                    "isInNest": true,
                    "color": 2,
                    "playerId": 46
                },
                {
                    "id": 182,
                    "position": 0,
                    "atFinishLine": false,
                    "isFinished": false,
                    "isInNest": true,
                    "color": 2,
                    "playerId": 46
                },
                {
                    "id": 183,
                    "position": 0,
                    "atFinishLine": false,
                    "isFinished": false,
                    "isInNest": true,
                    "color": 2,
                    "playerId": 46
                },
                {
                    "id": 184,
                    "position": 0,
                    "atFinishLine": false,
                    "isFinished": false,
                    "isInNest": true,
                    "color": 2,
                    "playerId": 46
                }
            ],
            "gameSessionId": "28f6d193-65bf-4e8f-a543-aec9db46fb6e"
        },
        {
            "id": 47,
            "name": "Jonas",
            "pawns": [
                {
                    "id": 185,
                    "position": 0,
                    "atFinishLine": false,
                    "isFinished": false,
                    "isInNest": true,
                    "color": 4,
                    "playerId": 47
                },
                {
                    "id": 186,
                    "position": 0,
                    "atFinishLine": false,
                    "isFinished": false,
                    "isInNest": true,
                    "color": 4,
                    "playerId": 47
                },
                {
                    "id": 187,
                    "position": 0,
                    "atFinishLine": false,
                    "isFinished": false,
                    "isInNest": true,
                    "color": 4,
                    "playerId": 47
                },
                {
                    "id": 188,
                    "position": 0,
                    "atFinishLine": false,
                    "isFinished": false,
                    "isInNest": true,
                    "color": 4,
                    "playerId": 47
                }
            ],
            "gameSessionId": "28f6d193-65bf-4e8f-a543-aec9db46fb6e"
        },
        {
            "id": 48,
            "name": "Sebastian",
            "pawns": [
                {
                    "id": 189,
                    "position": 0,
                    "atFinishLine": false,
                    "isFinished": false,
                    "isInNest": true,
                    "color": 3,
                    "playerId": 48
                },
                {
                    "id": 190,
                    "position": 0,
                    "atFinishLine": false,
                    "isFinished": false,
                    "isInNest": true,
                    "color": 3,
                    "playerId": 48
                },
                {
                    "id": 191,
                    "position": 0,
                    "atFinishLine": false,
                    "isFinished": false,
                    "isInNest": true,
                    "color": 3,
                    "playerId": 48
                },
                {
                    "id": 192,
                    "position": 16,
                    "atFinishLine": false,
                    "isFinished": false,
                    "isInNest": false,
                    "color": 3,
                    "playerId": 48
                }
            ],
            "gameSessionId": "28f6d193-65bf-4e8f-a543-aec9db46fb6e"
        }
    ],
    "activeGame": true,
    "currentPlayer": 0,
    "latestRoll": 2,
    "hasRolled": true
}
]
```
</details>

---

```POST``` ```/Ludo/NewGame``` -- Skapar en ny spelsession

Example Request 
```https://localhost:44531/api/Ludo/NewGame```

<details>
<summary>Body</summary>
 
 ```json
 {
  "sessionName": "NameForGameSession",
  "playerOne": "PlayerOneName",
  "playerTwo": "PlayerTwoName",
  "playerThree": "PlayerThreeName",
  "playerFour": "PlayerFourName"
}
 ```
</details>

<details>
 <summary>Exempelsvar</summary>

```json
[
    "ed14b94b-e7e8-4bfe-b417-8008bcb1b8ba"
]
```
</details>

---

```PUT``` ```/Ludo/RollDice``` -- Skapar en ny spelsession

Example Request 
```https://localhost:44531/api/Ludo/RollDice```
<details>
<summary>Body</summary>
 
 ```json
 {
"ed14b94b-e7e8-4bfe-b417-8008bcb1b8ba"
}
 ```
</details>

<details>
 <summary>Exempelsvar</summary>

```json
[
    4
]
```
</details>

---


```PUT``` ```/Ludo/MovePawn``` -- Skapar en ny spelsession

Example Request 
```https://localhost:44531/api/Ludo/MovePawn```
<details>
<summary>Body</summary>
 
 ```json
{
  "pawnId": 200,
  "sessionId": "ed14b94b-e7e8-4bfe-b417-8008bcb1b8ba"
}
 ```
</details>

<details>
 <summary>Exempelsvar</summary>

```json
[
    4
]
```
</details>

---

<a name="frontend-dokumentation" />

## Frontend ASP.NET Core WebApp

Vår frontend är skapad med ASP.NET Core WebApp (Razor pages).

Spelplanen har vi skapat som en tabell i HTML. Storleken på planen är 13x13 rutor. För att sedan rita ut 4st nästen så valde vi att göra en colspan och en rowspan på 5x5 rutor i varje hörn av brädet. Spelpjäserna går ut från nästet ett ruta framför den lilla rutan i egen färg och ska sedan gå ett varv runt spelplanen i yttervarv innan man får gå in på sin egen mållinje vilken är färgad i spelpjäsens egen färg. 

<a name="nytt-spel" />

### Skapa nytt spel

När man kommer till sidan och trycker på menyvalet för att skapa ett nytt spel så kommer man till ett formulär som är skapat i razor pages (*ludo.chtml*). När formuläret är ifyllt och men trycker på **Create** så kommer metoden onPost() i Ludo.cshtml.cs att köras, och det som sker där är ett anrop mot vårat RestAPI med en post request för att skapa ett nytt spel. Alla fält i formuläret kopplas till klassen AddNewGame där varje fält har en inputvalidering i form av regex som kollar så vissa villkor stämmer. För SessionName så får man inte lämna fältet tomt, det måste innehålla minst 3 bokstäver och endast bokstäver från det engelska alfabetet.

```cs
[Required(ErrorMessage = "Required field")]
[MinLength(3, ErrorMessage = "Minimum 3 characters"), MaxLength(30)]
[RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only english alphabetical characters")]
public string SessionName { get; set; }
```

När det kommer till spelare så har spelare 1 och 2 en required annotation medan spelare 3 och 4 inte har någon sådan. Detta har vi gjort för att man måste vara minst 2 spelare för att skapa ett nytt spel, och därefter så är det frivilligt om man vill lägga till spelare 3 och 4. 
[Hur vi löst detta i backend:en](#newgame2player)

```cs
 [Required(ErrorMessage = "Required field")]
 [MinLength(3, ErrorMessage = "Minimum 3 characters"), MaxLength(30)]
 [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only english alphabetical characters")]
 public string PlayerOne { get; set; }
```

<a name="spel" />

### Spela 

Knappen ROLL DICE har ett onClick event som kallar på rollDice som är en JavaScript function som i sin tur kallar på vårat RestAPI och endpointen RollDice. Funktionen har id som parameter och det är spelsessionens GUID som den skickar med som body i apianropet, och det är för att api:et ska veta vilken spelsession det är som rullar tärningen för att vi ska kunna spara de senaste tärningskastet i databasen. Därefter så tar vi resultatet från anropet och kollar om det svaret vi får tillbaka är ett nummer, om det är det så visar vi en bild på en tärningssida som motsvarar den int vi fått tillbaka, annars så visar vi ett felmeddelande i en alert ruta. 


```js
async function rollDice(id) {
    var requestOptions = {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        body: JSON.stringify(id)
    };

    var response1 = await fetch("https://localhost:44303/api/Ludo/RollDice", requestOptions);
    var data = await response1.json();
    document.querySelector(".roll").innerText = data;

    if (!isNaN(data)) {
        var newNumber = parseInt(data);
        diceElement.src = `/img/dice-${newNumber}.png`;
    }
    else {
        alert(data);
    }
}
```

Varje pawn är en div som vi valt att dynamiskt skapa via DOM manipluation istället för att hårdkoda dom på spelplanen, och detta har vi gjort i filen **onLoad.js**. Vi hade lite problem med att dom inte visades på spelplanen, vilket berodde på att vi hade koden i site.js från början, och den js filen läses in efter allt annat innehåll på sidan, och vi löste det genom att lägga den metoden i en separat js fil och la referensen till den inom head taggen i \_Layout så den filen läses in innan resterande innehåll på sidan, och anledningen till att vi inte la till site.js där var för att vi vill undvika att all JavaScript kod ska läsas in direkt. 

```js
let pawn = document.createElement('div');
    pawn.classList.add('pawn');
    pawn.classList.add(pawnColor);
    pawn.setAttribute("pawn-id", pawnId);
    pawn.addEventListener("click", function () {
    PostChanges(this, sessionId);
```

Efter man har rullat tärningen så ska man trycka på den pawn som man vill flytta, och detta har vi löst genom att lägga till en eventListener på varje pawn när vi ritar ut dom. 
Parametern till PastChanges funktionen kräver ett sessionId för att kunna avgöra vilken pawn det är man har valt att flytta. Parametern this innebär att en pawn skickar in sig själv som parameter vilket gör att man kan hämta ut de attibut som vi sätter tilldelar varje pawn när vi skapar dom i form av ett pawn-id. 

Det funktionen PostChanges gör är att kalla på endpointen MovePawn i vårat RestAPI, och skickar in en requestModel i bodyn. Denna modell består av den information vårat RestAPI behöver för att kunna räkna ut vart den pawn:en kommer att hamna efter draget om det är ett giltligt drag, därefter skriver den ut vilket nummer tärningen visade. 

```js
async function PostChanges(pawn, sessionId) {
    var requestModel = { PawnId: pawn.getAttribute("pawn-id"), SessionId: sessionId };

    var response = await fetch('https://localhost:44303/api/Ludo/MovePawn',
        {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            body: JSON.stringify(requestModel)
        });

    var data = await response.json();
    document.querySelector(".roll").innerText = data;
}
```

<a name="backend-dokumentation" />

## Backend ASP.NET Core WebAPI - RestAPI

<a name="nytt-spel-backend" />

### Nytt spel

När frontend:en skickar en POST request till /NewGame endpointen så tar vi en NewGameRequest som hämtas från request bodyn

```cs
 [HttpPost("[action]")]
        public IActionResult NewGame([FromBody] NewGameRequest request)
        {
            IGameSession session = _createNewGame.Create(request);
            try
            {
                _dbContext.GameSessions.Add((GameSession)session);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, session.Id);
            }
            catch
            {
                return Conflict(_displayMessage.DbError());
            }
        }
```

och med informationen där skapas ett nytt spel upp i metoden Create som finns i klassen CreateNewGame som retunerar en GameSession

```cs
        public IGameSession Create(NewGameRequest request)
```

<a name="newgame2player"/>

För spelare 1 och spelare 2 så gäller följande kod

```cs
 IPlayer playerOne = Factory.CreateNewPlayer();
            playerOne.Name = request.PlayerOne;
            for (int i = 0; i < 4; i++)
            {
                IPawn greenPawn = Factory.CreateNewPawn();
                greenPawn.Color = PawnColor.Green;
                playerOne.Pawns.Add((Pawn)greenPawn);
            }
            _gameSession.Players.Add((Player)playerOne);
```

Men för spelare 3 och 4 kör enbart ovanstående kod om följande villkor stämmer

```cs
if (!string.IsNullOrEmpty(request.PlayerThree))
```

Det är av den anledningen vi har valt att göra så man enbart är tvungen att fylla i namn för spelare 1 och 2 i frontend:en

<a name="backend-spela" />

### RollDice

När frontend:en trycker på knappen Roll Dice så skickas det en PUT request till vårat RestAPI, och i bodyn finns det ett Guid ID som vi använder för att med en linq query leta i databasen efter en spelsession med det Guid:t, om vi inte hittar någon så retuneras en BadRequest, annars går vi vidare och kollar om det har varit ett tärningskast utan att någon har flyttat på en pawn, om det är sant så retuneras ett meddelande om att tärningen redan är rullad och spelaren behöver flytta en pawn. Om det villkoret är falskt så går vi vidare och slumpar fram ett nummer mellan 1 och 6, sen ändrar vi LatestRoll till det tärningen visar och ändrar boolen HasRolled till True, vilket kommer vara sant fram till att en pawn har flyttats. Därefter sparar vi ändringarna till databasen och retunerar OK. 

```cs
 [HttpPut("[action]")]
        public IActionResult RollDice([FromBody] Guid id)
        {
            var foundSession = _dbContext.GameSessions.FirstOrDefault(gs => gs.Id == id);

            if (foundSession == null)
            {
                return BadRequest(_displayMessage.NoGameId());
            }

            if (foundSession.HasRolled)
            {
                return Ok(_displayMessage.HasRolled());
            }

            Random rnd = new Random();
            int roll = rnd.Next(1, 7);

            foundSession.LatestRoll = roll;
            foundSession.HasRolled = true;
            _dbContext.SaveChanges();
            return Ok(roll);
        }
```

<a name="anslut-till-spel" />

### Ansluta till ett spel

När man navigerar till JoinGame i frontend får man fylla in ett Guid, och det skickas som en GET request till vårat RestAPI och endpointen /GameSession
och i bodyn tar den emot ett Guid.

Det första vi gör är att skapa upp en IGameSession där vi hämtar GameSessions tabellen från databasen och includerar tabellerna Players och Pawns. Därefter kommer ett villkor som kollar om det finns någon gamesession med det guid:t och om det finns så retunerar vi Ok tillsammans med den spelsessionen, annars en Bad Request med ett meddelande. 

```cs
    public IActionResult GameSession(Guid id)
        {
            IGameSession session = _dbContext.GameSessions
                .Include(gs => gs.Players)
                .ThenInclude(p => p.Pawns)
                .FirstOrDefault(gs => gs.Id == id);

            if (session != null)
            {
                return Ok(session);
            }
            return BadRequest(_displayMessage.SessionNotFound());
        }
```
