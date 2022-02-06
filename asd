[33mcommit 30176b3508958ee72bff4293ca480610013091cd[m[33m ([m[1;36mHEAD -> [m[1;32mi_volgushev[m[33m, [m[1;31morigin/i_volgushev[m[33m)[m
Author: robocrab <ivo.volgushev@yandex.ru>
Date:   Fri Feb 4 13:26:58 2022 +0300

    Added GUI class and moved appropriate functions there. Added InputManager class (and an additional ConsoleLib class for it which i found on stackoverflow), it now gives input events to the Client class.

[33mcommit a663829cbee4ac5234b9d804915bc1a8392f85ed[m[33m ([m[1;31morigin/dev[m[33m, [m[1;31morigin/HEAD[m[33m, [m[1;32mdev[m[33m)[m
Merge: d8c0e1f a39305f
Author: Alex Tinarsky <alex.tinarsky@mail.ru>
Date:   Thu Feb 3 21:09:35 2022 +0700

    Merge branch 'dev' of github.com:UniMathDev/Roguelike into dev

[33mcommit d8c0e1ff9a959a28d5dc0dacd1f1e6b220924620[m
Author: Alex Tinarsky <alex.tinarsky@mail.ru>
Date:   Thu Feb 3 21:03:51 2022 +0700

    Added dynamic camera

[33mcommit a39305f756ee9e0dab9bd2aac74dad697bc6ba59[m
Merge: b7007bf 27a309f
Author: Alexey Tinarsky <87636986+alex-tinarsky@users.noreply.github.com>
Date:   Wed Dec 8 23:10:59 2021 +0300

    Merge pull request #1 from UniMathDev/alex-tinarsky
    
    Alex Tinarsky

[33mcommit 27a309f5d2f1ddfcd9497928eb43bda5680b6f10[m
Author: Alex Tinarsky <alex.tinarsky@mail.ru>
Date:   Wed Dec 8 23:00:04 2021 +0300

    Added console client with processing some pressed buttons.
    
    Added initial player configs.
    Added first test map.

[33mcommit 02072133df32614c4e7d2ebb790908f088a9200b[m
Author: Alex Tinarsky <alex.tinarsky@mail.ru>
Date:   Wed Dec 8 22:47:13 2021 +0300

    Added the ability to walk on the map for the character.
    
    Added 4 directions for walking.
    Added class Game, that contains available methods in game.
    Added struct MapDiff, that contains changes in a map cell.
    Added Player with ability to walk on the map.
    Renamed and moved some elements.

[33mcommit b7007bf53b64fdea8961a56a55840920b76a47b3[m
Author: Alex Tinarsky <alex.tinarsky@mail.ru>
Date:   Wed Dec 1 13:54:06 2021 +0300

    Removed Roguelike.GameConfig

[33mcommit 0ba732579ba9210f8e3fa4f146d6d97ee1fcf6f7[m
Author: Alex Tinarsky <alex.tinarsky@mail.ru>
Date:   Wed Dec 1 13:27:10 2021 +0300

    Added Factory for objects on map.
    
    Added factory method to create the objects on map.
    Added class matching the chars to faсtories (CharsOfObjects) and returning ObjectOnMap.
    GameConfig moved to main game project (Roguelike.GameConfig removed).

[33mcommit 9041fedf3e6e495a4095532c9ee9096ed8f578a5[m
Author: Alex Tinarsky <alex.tinarsky@mail.ru>
Date:   Wed Dec 1 02:09:59 2021 +0300

    Added FixedObject, TxtToMapConverter, MapSize.
    
    Added an abstract class FixedObject and some his heirs: Door, Floor, Wall, Window.
    Added method to convert text file to Map.
    Added MapSize in GameConfig.
    Minor class name changes.

[33mcommit 56589a20e984ab9718aaa1b89a374fa79dbbeb82[m
Author: Alex Tinarsky <alex.tinarsky@mail.ru>
Date:   Tue Nov 30 21:34:13 2021 +0300

    Added an abstract map class and its implementation in an array.
    Added interface for objects on the map.
    Added an interface for getting a map from a file.

[33mcommit 33d19f29bbf889e1a23592da7fe288802f572a7d[m
Author: Alex Tinarsky <alex.tinarsky@mail.ru>
Date:   Mon Nov 29 00:55:20 2021 +0300

    Init VS Studio project.

[33mcommit e146368142e2ce927cc14fd5ba8ba72ec83525d2[m[33m ([m[1;31morigin/main[m[33m, [m[1;31morigin/alex-tinarsky[m[33m, [m[1;32mmain[m[33m)[m
Author: Alexey Tinarsky <87636986+alex-tinarsky@users.noreply.github.com>
Date:   Sun Nov 28 21:20:21 2021 +0300

    Initial commit
