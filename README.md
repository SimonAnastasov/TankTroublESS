# За тимот ESS:

Проект на: Симон Анастасов 201003, Давид Ѓорѓиевски 203018, Глигорчо Глигоров 203125
Изработен во рамки на Финки, за предметот Визуелно Програмирање, воден од Дејан Ѓорѓевиќ и Стефан Андонов.
Изработен на: 2.7.2022г.

# Функционалности на играта:

Ние креиравме полу-реплика на играта Тенк Трабл (оригиналната игра, која исто така е многу забавна, е достапна на линкот https://tanktrouble.com).

Се разбира, нашата игра е кодирана од нула, и единствено од нас. (И неколку добри луѓе што одговориле на прашања на Stack Overflow во 2012 година 😜).

## Цел на играта:

Играта се игра со 2 играчи (2 Тенка). Тие се движат низ лавиринт, при што паметно треба да ги искористат ѕидовите за да се скријат од куршумите на непријателскиот тенк, а притоа да успеат да го погодат непријателскиот тенк со своите куршуми. Еве како изгледа тоа (без и со испукани куршуми):

![An example of a level of the game](https://github.com/SimonAnastasov/TankTroublESS/blob/master/ReadmeImages/Level.png?raw=true)

![An example of a level of the game with bullets](https://github.com/SimonAnastasov/TankTroublESS/blob/master/ReadmeImages/Bullets.png?raw=true)

## Контролирање на тенковите:

Зелениот тенк се движи на копчињата 'WASD' и пука на копчето 'Q'. Црвениот тенк се движи на 'стрелките' и пука на 'SPACE'.
На следната слика ова е прикажано визуелно.

![Controlling the tanks movement](https://github.com/SimonAnastasov/TankTroublESS/blob/master/ReadmeImages/Controls.png?raw=true)

## Дополнителни работи што ги има во играта:

### - Форма за помош:

![Controlling the tanks movement](https://github.com/SimonAnastasov/TankTroublESS/blob/master/ReadmeImages/HowToPlay.png?raw=true)

### - Секција „За Нас“:

![Controlling the tanks movement](https://github.com/SimonAnastasov/TankTroublESS/blob/master/ReadmeImages/AboutUs.png?raw=true)

### - Статус стрип (долу лево, види ја првата слика погоре), кој го покажува резултатот.

# Кодирање на играта:

## Структури:

Играта е базирана на класи и интеракција помеѓу објекти од класите. Некои од класите кои ги имаме се: PlayingField, Background, Walls, Tank, Bullet...

Во класите се чуваат информации како: X и Y координатата на објектот кој класата го претставува, Должината и висината на објектот, во некои од класите има и листи од објекти (пример листа од куршуми во класата тенк), а во некои има и параметри како што се време до исчезнување (пример класата за куршум).

## Мултимедиа:

Користиме слики и аудио, со цел да го подобриме квалитетот на играта и да го зголемиме задоволството кај играчите.

## Објаснување на една интересна функција:

![Controlling the tanks movement](https://github.com/SimonAnastasov/TankTroublESS/blob/master/ReadmeImages/Function.png?raw=true)

