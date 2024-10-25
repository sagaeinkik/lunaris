# Lunaris

Programmering i C# .NET på Mittuniversitetet

Projektuppgift av Saga Einarsdotter Kikajon.

## Bakgrund

Detta är min projektuppgift för kursen DT071G på Mittuniversitet, hösten -24. Uppgiften går ut på att visa vad man har lärt sig under kursens gång. Hela projektet är skrivet i Visual Studio Code för Mac.  
Jag har valt att göra en konsollapplikation i form av ett spel där användarens val påverkar slutet. Det finns stor möjlighet för vidareutveckling men jag har valt att lägga det på en lite enklare nivå för att hinna klart innan kursavslut.

### Struktur

Det finns en undermapp för föremål. Alla föremål ligger i en json-fil, det finns en klass för Items, och en klass för hantering av inventory.

Det finns en separat fil för funktioner som inte nödvändigtvis har med spelet att göra men som jag använder flitigt genom projektet, tools.cs.

gameFlow.cs innehåller kontrollfrågor, ofta använda funktioner som relaterar till spelet, samt beskrivningar av scenerna. Detta är för att inte program.cs ska bli lika texttungt. I program.cs ligger de funktioner som påverkar spelet, det vill säga själva beslutsfattandet.

## Lunaris

Lunaris har dragit inspiration av klassiska textbaserade äventyrsspel/fantasyspel. Det finns i dagsläget inga vapen, ingen combat, inga poäng, och ingen möjlighet att använda items – detta är något som absolut kan vidareutvecklas.

I nuvarande utförande går spelet helt enkelt ut på att användaren navigerar runt och samlar på sig 5 av 20 föremål som ligger utspridda lite här och där. Alla föremål hör till någon av fyra kategorier, och baserat på den mest prevalenta kategorin av föremål som användaren har plockat på sig får användaren ett av fem olika avslut.

## Bra att veta

Spelet utgår från svenska, men klarar att hantera utvalda svarsalternativ även på engelska.
Acceptabla inmatningar för ja/nej-frågor:
| Jakande | Nekande |
| ----|---- |
| yes | no |
| y | n |
| ja | nej |
| (tomt) | leave |

Observera att till exempel entertryckningar också räknas som nekande.

Acceptabla inmatningar för andra val kan vara 1, 2, 3 enligt svarsalternativ, att skriva in riktningar såsom "västerut", "väst", "west", "go west" och så vidare.

Det finns en funktion som räknar igenom användarens föremål och kategorier. Om en användare skulle plocka på sig bara ett föremål från vardera kategori och lämna den femte fickan tom så utgår programmet från det föremål som användaren plockade på sig allra först.  
Det är fullt möjligt att "gå i mål" utan att plocka på sig ett enda föremål.

## Walkthrough

Skriv saker här
