# Лабораторная работа 2 по дисциплине "Технологии программирования"

**Изучение фреймворка MVC**

Выполнил: студент группы САПР-1.1 Доценко Екатерина

## Цель работы ##
1. Познакомиться c моделью MVC, ее сущностью и основными фреймворками на ее основе.
2. Разобраться с сущностями «модель», «контроллер», «представление», их функциональным назначением.
3. Получить навыки разработки веб-приложений с использованием MVC-фреймворков, написания модульных тестов к ним и интеграции приложений в конвейер CI / CD;
4. Получить навыки управления автоматизированным тестированием и разворачиванием программного обеспечения, расположенного в системе Git, с помощью инструмента Travis CI.

## Задачи ##
1. Выберите для Вашего проекта тип лицензии и добавьте файл с лицензией в проект.
2. Доработайте проект магазина, добавив в него новую функциональность и информацию в базу данных в соответствии с типом магазина. 
3. Проанализируйте полученные результаты и сделайте выводы.

## Индивидуальный вариант ##

* Вариант 4
* Тип магазина - Магазин товаров для быта
* Функциональность приложения - Магазин должен вести учет покупателей и делать накопительную скидку. Величина скидки зависит от количества покупок любых товаров.

## Использованые язык и библиотеки ##
* Язык программирования - С#
* Библиотеки - Microsoft.EntityFrameworkCore 6.0.11, Microsoft.AspNetCore.Mvc, Microsoft.EntityFrameworkCore.Design 6.0.11, Microsoft.EntityFrameworkCore.Tools 6.0.11, Npgsql 6.0.7, Npgsql.EntityFrameworkCore.PostgreSQL 6.0.7, Npgsql.EntityFrameworkCore.PostgreSQL.Design 1.1.0
* База данных - PostgrSQL

## Краткое описание проекта ##
Проект представляет собой веб-приложениие интернет магазина товаров для быта. Приложение состоит из 3 представления(форм) - Авторизация, Каталог и Корзина. Страница "Авторизация" представляет из себя форму, которую необходимо корректно заполнить для дальнешей работы. Страница "Каталог" состоит из списка доступных к покупке товаров. Необходимо ввести количество товара и нажать кнопку "Добавить в корзину". Страница "Корзина" состоит из списка товаров добавленных в пользователем в корзину. При нажатии на "Продолжить покупки" пользователь вернется к каталогу, а при нажатии на кнопку "Купить" корзина опустошается, количество купленнного товара записывается в базу для дальнейшего расчёта скидки. Величина скидки зависит от количества купленного товара (до 100 товаров-1%, от 100 до 200 товаров - 2% и т.д. до 1500 - 15%- максимальная скидка).

## Скриншоты приложения


## Выводы ##
В результате выполнения данной работы я познакомилась с модель MVC и на основе данной модели построила веб-приложение магазина товаров для быта. Был реализован функционал учета покупателей и их накопительной скидки.
