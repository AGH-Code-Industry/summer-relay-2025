# DraggableTableObject

Najlepiej jest zerknąć na implementacje TicketControler i prefab Ticket

Do obliczania wielkości przedmiotu jest wykorzystywane Bounds, żeby można było dodawać więcej jak jeden colider i działało prosto

Można ustalać czy obiekt można wyciągać poza obszar czy nie

Colider2d używant do chwytania obiektu to ten na którym jest dodant skrypt

Ważne sygnały nadawane przez kontroler:

OnEnterTable - kiedy przedmiot wraca do kontenera
OnExitTable - keidy przedmiot opuszca kontener
SetSortingIndex(int newBaseIndex) - sygnał do zaktualizowania sortowania do wyświetlania, każdy obiekt ma standardowo 5 wartośći, aby używać ich do sortiwania spriteów

> ig można zrobić pod te funkcji interfejsy, ale nie pomyślałem jak robiłem

Aby można było upóścić na coś obiekt trzeba użyć interfejsu IDropZone, a aby handlować upuszczenie trzeba użyć IObjectDropHandler
Czyli:
- pasażer na IDropZone
- bilet ma IObjectDropHandler
kiedy bilet zostaje upuszczony na pasażera, na bilecie jest wywoływana fukcja HandleObjectDropped,
 w której należy wywołać OnObjectDropped na IDropZone, które dostaniemy