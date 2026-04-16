# Lektion 16

Idag ska vi addera möjlighet att kunna bryta ner en stor datamängd till mindre delar, så kallad _paginering_
Vi kommer även att refaktorera sättet på hur vi hanterarar inkommande frågesträngar samt se hur vi eventuellt kan skapa ett bättre sätt att returnera data på.

Vi ska idag även implementera _än en gång_ säkerhet via Identity biblioteket. Men vi kommer att göra det något annorlunda. Vi ska använda ett nytt bibliotek ifrån Microsoft.
Som gör det lite lättare för oss framförallt behöver vi inte skriva all logik själva för hantering av _tokens_.