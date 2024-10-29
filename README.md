# Lunaris

Programmering i C# .NET på Mittuniversitetet

Projektuppgift av Saga Einarsdotter Kikajon.

## Bakgrund

Detta är min projektuppgift för kursen DT071G på Mittuniversitet, hösten -24. Uppgiften går ut på att visa vad man har lärt sig under kursens gång. Hela projektet är skrivet i Visual Studio Code för Mac.  
Jag har valt att göra en konsollapplikation i form av ett spel där användarens val påverkar slutet. Det finns stor möjlighet för vidareutveckling men jag har valt att lägga det på en lite enklare nivå för att hinna klart innan kursavslut.

## Lunaris

Lunaris har dragit inspiration av klassiska textbaserade äventyrsspel/fantasyspel. Det finns i dagsläget inga vapen, ingen combat, inga poäng, och ingen möjlighet att använda items – detta är något som absolut kan vidareutvecklas.

I nuvarande utförande går spelet helt enkelt ut på att användaren navigerar runt och samlar på sig 5 av 20 föremål som ligger utspridda lite här och där. Alla föremål hör till någon av fyra kategorier, och baserat på den mest prevalenta kategorin av föremål som användaren har plockat på sig får användaren ett av sex olika avslut.

### Struktur

Det finns en undermapp för föremål. Alla föremål ligger i en json-fil, det finns en klass för Items, och en klass för hantering av inventory.

Undermappen utilities innehåller saker som är bra att ha. command.cs innehåller en klass för att hantera inmatning av riktningar. Tools.cs innehåller funktioner jag återanvänder flitigt men som inte nödvändigtvis har med spelet att göra: funktioner för att styla text till skärmen samt för att inte behöva skriva om switch-satser med hjälp av command.cs.

gameFlow.cs innehåller kontrollfrågor, ofta använda funktioner som relaterar till spelet, samt beskrivningar av scenerna. Detta är för att inte program.cs ska bli lika texttungt. I program.cs ligger de funktioner som påverkar spelet, det vill säga själva beslutsfattandet.

## Bra att veta

Spelet utgår från svenska, men klarar att hantera utvalda svarsalternativ även på engelska.
Acceptabla inmatningar för ja/nej-frågor:
| Jakande | Nekande |
| ----|---- |
| yes | no |
| y | n |
| ja | nej |
| (tomt) | leave |

När man ska välja riktning fungerar följande (exempel med sydväst, men funkar med alla riktningar):
| Kommando |
| ---------|
| (siffra ex 1) |
| sydväst |
| gå sydväst |
| sv |
| gå sv |
| southwest |
| go southwest |
| south west |
| go south west|
| sw |
| go sw |

Det finns en funktion som räknar igenom användarens föremål och kategorier. Om en användare skulle plocka på sig bara ett föremål från vardera kategori och lämna den femte fickan tom så utgår programmet från det föremål som användaren plockade på sig allra först.  
Det är fullt möjligt att "gå i mål" utan att plocka på sig ett enda föremål.

För att samla på sig alla fem föremål inom en och samma kategori måste man ha att göra med sjöodjuret. För att sjöodjuret inte ska döda en automatiskt behöver man plocka på sig ett föremål, en pärla, att byta med djuret. Därefter får man välja vilken kategori på föremål man önskar att få i utbyte.

## Walkthrough

Ni har kanske inte tid att sitta och fumla er igenom hela mitt spel, även om det är enkelt och litet, så här är det viktiga:
Från skogsgläntan:

-   Sydväst leder till ett föremål.
-   Västerut leder så småningom till en trollgrotta. På vägen stöter man på ett troll. Besegra trollet genom att kasta sten på det, ellet undvik trollet genom att göra kullerbytta. Andra alternativ leder till Game over.
-   Vägen nordöst leder till en drake. Kryp i valfri riktning för att inte dö av den. Fortsätt nordöst för att komma till Norra strand. Det finns flera föremål på denna väg.
-   Vägen nordöst leder till en bro (här kan man gå nordväst för att hitta ett föremål i en glänta). Direkt efter bron stöter man på en sfinx; svara rätt på gåtan så är du säker från henne efter det. Svarar du fel tre gånger dör du.
-   Söderut från denna punkt finns det ett tält med ett föremål.
-   Direkt öster om Storslätta finns pärlan.
-   Norr om Storslätta finns en sjö med ett odjur. Har man inte plockat på sig pärlan dör man. Har man plockat på sig pärlan men säger nej till att byta dör man. Har man pärla och vill byta så får man välja den kategori av föremål som man samlar på i utbyte.
-   Några steg väster om sjön ligger Lunaris, trollkarlstornet. Väster om detta ligger ett föremål.
-   Inne i Lunaris får du ett av sex slut av trollkarlen.

!!! SKRIV MER DETALJERAT HÄR SEN

Flytta denna sektion till en fuskmapp med flödesschemat
