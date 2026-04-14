# Lektion 15

Nu när vi i princip fått logiken, datatabas och våra endpoints att fungera är det dags att börja fundera på är detta **bästa** sätt att bygga en applikation som behöver vara expansiv och underhållsbar.

## Clean Architecture
Vi kommer att refaktorera vår applikation enligt "_Uncle Bobs (Robert C. Martins)_" idé om _Clean Architecture_ och samtidigt lära oss några nya knep i C#.

Clean Architecture är en princip att separera kopplingar mellan olika lager i en applikation. Kommunikation ska endast gå åt ett håll för att slippa(_undvika_) hårda kopplingar mellan moduler/komponenter som applikationen är uppbyggd kring.

![Robert C. Martin Clean Architecture](https://res.cloudinary.com/softtech-dev/image/upload/v1776144904/Course%20images/CleanArchitecture_cte1sp.jpg)